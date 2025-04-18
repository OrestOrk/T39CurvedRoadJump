using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using System;

public class ActorMove : MonoBehaviour
{
    public event Action OnJumpStart;
    public event Action OnTouchdown;//one jump complette

    public SplineFollower follower;
    public List<RoadPointController> roadPoints;

    private int _pointsPerJump = 1;
    private float _jumpTime = 0.8f;
    private float _jumpHeight = 2.5f;
    private float _jumpDelay = 0.5f;
    private float _followerSpeed = 0.03f;

    private int _currentIndex = 3;
    private bool _isMoving = false;
    private bool _isPaused = false;

    private List<double> percents = new();

    private IEnumerator activeCoroutine;

    private void Start()
    {
        follower.follow = false;

        percents.Clear();
        foreach (var point in roadPoints)
        {
            SplineSample sample = follower.spline.Project(point.transform.position);
            double percent = sample.percent;
            percents.Add(percent);
        }

        SetFollowerStartPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            BonusJump();
        }
    }

    public void StartJumps(int jumpsAmount)
    {
        if (_isMoving) return;

        CalculatePointsPerJump(jumpsAmount);
        CalculateJumpHeight();
        activeCoroutine = JumpMultiplePoints(jumpsAmount);
        StartCoroutine(activeCoroutine);
    }

    public void PauseJumps()
    {
        if (!_isMoving || _isPaused) return;

        _isPaused = true;
    }

    public void ResumeJumps()
    {
        if (!_isPaused) return;

        _isPaused = false;
        StartCoroutine(activeCoroutine);
    }

    private void CalculatePointsPerJump(int jumpsAmount)
    {
        if (jumpsAmount < 10)
            _pointsPerJump = 0;
        else if (jumpsAmount < 15)
            _pointsPerJump = 1;
        else if (jumpsAmount < 25)
            _pointsPerJump = 2;
        else
            _pointsPerJump = 3;
    }

    private void CalculateJumpHeight()
    {
        _jumpHeight = 1 + (_pointsPerJump * 1);
    }

    IEnumerator JumpMultiplePoints(int totalPoints)
    {
        _isMoving = true;
        int pointsCrossed = 0;

        while (pointsCrossed < totalPoints)
        {
            while (_isPaused)
            {
                yield return null;
            }

            int nextIndex = _currentIndex + _pointsPerJump + 1;
            if (nextIndex >= roadPoints.Count)
            {
                nextIndex = roadPoints.Count - 1;
                pointsCrossed = totalPoints;
            }
            else
            {
                pointsCrossed += _pointsPerJump + 1;
            }

            yield return StartCoroutine(JumpToSplinePoint(percents[nextIndex]));
            roadPoints[nextIndex].ActorTrigger();
            _currentIndex = nextIndex;
            
            OnTouchdown.Invoke();
            yield return new WaitForSeconds(_jumpDelay);
        }

        _isMoving = false;
    }

    IEnumerator JumpToSplinePoint(double targetPercent)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = follower.spline.EvaluatePosition((float)targetPercent);

        AlignToDirection(targetPos);

        float startPercent = (float)follower.GetPercent();
        float targetPercentFloat = (float)targetPercent;
        float moveDuration = CalculateMoveDuration(startPercent, targetPercentFloat);
        float elapsedTime = 0f;

        OnJumpStart?.Invoke();

        yield return MoveAlongSpline(startPos, targetPercentFloat, moveDuration, elapsedTime);

        FinalizePosition((float)targetPercent);
    }

    private void AlignToDirection(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            direction.y = 0f;

            Quaternion lookRot = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 10f * Time.deltaTime);
        }
    }

    /*private float CalculateMoveDuration(float startPercent, float targetPercentFloat)
    {
        return Mathf.Abs(targetPercentFloat - startPercent) / _followerSpeed;
    }*/

    private float CalculateMoveDuration(float startPercent, float targetPercentFloat)
    {
        // Встановити тривалість руху однаковою для всіх стрибків
        return _jumpTime; // Використовуємо фіксовану тривалість `_jumpTime`
    }

    private IEnumerator MoveAlongSpline(Vector3 startPos, float targetPercentFloat, float moveDuration,
        float elapsedTime)
    {
        float startPercent = (float)follower.GetPercent();

        while (elapsedTime < moveDuration)
        {
            while (_isPaused)
            {
                yield return null;
            }

            elapsedTime += Time.deltaTime;

            follower.SetPercent(Mathf.Lerp(startPercent, targetPercentFloat, elapsedTime / moveDuration));
            transform.position = follower.transform.position;

            float yPosition = Mathf.Lerp(startPos.y, startPos.y + _jumpHeight,
                Mathf.Sin(Mathf.PI * (elapsedTime / moveDuration)));
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

            Vector3 targetPos = follower.spline.EvaluatePosition(targetPercentFloat);
            AlignToDirection(targetPos);

            yield return null;
        }
    }

    private void FinalizePosition(float targetPercent)
    {
        transform.position = new Vector3(follower.transform.position.x, transform.position.y,
            follower.transform.position.z);
        follower.SetPercent(targetPercent);
    }

    private void SetFollowerStartPosition()
    {
        if (_currentIndex >= 0 && _currentIndex < percents.Count)
        {
            double initialPercent = percents[_currentIndex];
            follower.SetPercent(initialPercent);
        }
    }

    //bonus jump 
    public void BonusJump()
    {
        //need make return previous jumps after end bonus jump
        if (_isMoving)
        {
            PauseJumps();
        }

        int pointsToJump = 10;
        int originalJumpStep = _pointsPerJump;
        _pointsPerJump = 10;
        float originalJumpTime = _jumpTime;
        _jumpTime = 1f;

        StartCoroutine(JumpTenPoints(pointsToJump, originalJumpTime, originalJumpStep));
    }

    private IEnumerator JumpTenPoints(int pointsToJump, float originalJumpTime, int originalJumpStep)
    {
        bool wasPaused = _isPaused;
        _isPaused = false;

        activeCoroutine = JumpMultiplePoints(pointsToJump);
        yield return StartCoroutine(activeCoroutine);

        _pointsPerJump = originalJumpStep;
        _jumpTime = originalJumpTime;

        if (wasPaused)
        {
            PauseJumps();
        }
    }
}