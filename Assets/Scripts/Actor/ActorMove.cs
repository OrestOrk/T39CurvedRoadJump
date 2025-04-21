using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using System;

public class ActorMove : MonoBehaviour
{
    public event Action OnJumpStart;
    public event Action OnTouchdown;
    public event Action OnJumpsSeriesEnd;

    public SplineFollower follower;
    public List<RoadPointController> roadPoints;

    [Header("Jump Settings")]
     private float _jumpTime = 0.5f;
     private float _jumpDelay = 0.2f;

    private float _jumpHeight = 2f;
    private float _groundY;
    private int _pointsPerJump = 1;

    private int _currentIndex = 3;
    
    private bool _isMoving = false;
    private bool _isPaused = false;
    private bool _bonusJumpRequested = false;


    private List<double> percents = new();
    private IEnumerator activeCoroutine;
    

    // ------------------- Initialization -------------------
    private void Start()
    {
        follower.follow = false;
        CalculateSplinePercents();
        SetInitialPosition();
        _groundY = transform.position.y;
    }

    private void CalculateSplinePercents()
    {
        percents.Clear();
        foreach (var point in roadPoints)
        {
            double percent = follower.spline.Project(point.transform.position).percent;
            percents.Add(percent);
        }
    }

    private void SetInitialPosition()
    {
        if (_currentIndex >= 0 && _currentIndex < percents.Count)
        {
            follower.SetPercent(percents[_currentIndex]);
        }
    }

    // ------------------- Public Controls -------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            BonusJump(); // debug placeholder
        }
    }

    public void StartJumps(int jumpsAmount)
    {
        if (_isMoving) return;

        DefineJumpSettings(jumpsAmount);

        activeCoroutine = JumpMultiplePoints(jumpsAmount);
        StartCoroutine(activeCoroutine);
    }

    public void PauseJumps()
    {
        if (_isMoving && !_isPaused)
            _isPaused = true;
    }

    public void ResumeJumps()
    {
        if (_isPaused)
            _isPaused = false;
    }

    // ------------------- Jump Settings Logic -------------------
    private void DefineJumpSettings(int jumpAmount)
    {
        _pointsPerJump = jumpAmount switch
        {
            < 10 => 0,
            < 15 => 1,
            < 25 => 2,
            _ => 3
        };

        _jumpHeight = 1f + (_pointsPerJump * 1f);
    }

    // ------------------- Jump Execution -------------------
    private IEnumerator JumpMultiplePoints(int totalPoints)
    {
        _isMoving = true;
        int pointsCrossed = 0;

        while (pointsCrossed < totalPoints)
        {
            yield return WaitWhilePaused();

            int jumpLength = _pointsPerJump + 1;

            if (_bonusJumpRequested)
            {
                jumpLength = 10;
                _bonusJumpRequested = false;
            }

            int nextIndex = _currentIndex + jumpLength;

            if (nextIndex >= roadPoints.Count)
            {
                nextIndex = roadPoints.Count - 1;
                pointsCrossed = totalPoints; // завершити цикл
            }
            else
            {
                pointsCrossed++; // рахувати як один стрибок
            }

            yield return StartCoroutine(JumpToSplinePoint(percents[nextIndex]));
            roadPoints[nextIndex].ActorTrigger();

            _currentIndex = nextIndex;
            OnTouchdown?.Invoke();

            yield return new WaitForSeconds(_jumpDelay);
        }

        _isMoving = false;

        OnJumpsSeriesEnd?.Invoke();
    }


    private IEnumerator JumpToSplinePoint(double targetPercent)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = follower.spline.EvaluatePosition((float)targetPercent);

        AlignToDirection(targetPos);

        float moveDuration = _jumpTime;
        float elapsedTime = 0f;

        OnJumpStart?.Invoke();

        yield return MoveAlongSpline(startPos, (float)targetPercent, moveDuration, elapsedTime);

        FinalizePosition((float)targetPercent);
    }

    private IEnumerator MoveAlongSpline(Vector3 startPos, float targetPercent, float duration, float elapsed)
    {
        float startPercent = (float)follower.GetPercent();

        while (elapsed < duration)
        {
            yield return WaitWhilePaused();

            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            float currentPercent = Mathf.Lerp(startPercent, targetPercent, t);
            follower.SetPercent(currentPercent);

            Vector3 currentPosition = follower.transform.position;
            float y = _groundY + Mathf.Sin(Mathf.PI * t) * _jumpHeight;
            transform.position = new Vector3(currentPosition.x, y, currentPosition.z);

            AlignToDirection(follower.spline.EvaluatePosition(targetPercent));

            yield return null;
        }
    }

    private IEnumerator WaitWhilePaused()
    {
        while (_isPaused)
        {
            yield return null;
        }
    }

    private void FinalizePosition(float targetPercent)
    {
        Vector3 finalPos = follower.transform.position;
        transform.position = new Vector3(finalPos.x, transform.position.y, finalPos.z);
        follower.SetPercent(targetPercent);
    }

    private void AlignToDirection(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            direction.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }
    }

    // ------------------- Debug/Extra -------------------
    public void BonusJump()
    {
        _bonusJumpRequested = true;
    }


}
