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

    [Header("Jump Settings")] private float _jumpTime = 0.3f;
    private float _jumpDelay = 0.1f;

    private float _jumpHeight = 1.2f;
    private float _groundY;
    private int _pointsPerJump = 1;

    private int _currentIndex;
    private const int START_INDEX = 3;

    private bool _isMoving = false;
    private bool _isPaused = false;
    private bool _bonusJumpRequested = false;


    private List<double> percents = new();
    private IEnumerator activeCoroutine;


    // ------------------- Initialization -------------------
    private void Start()
    {
        _currentIndex = START_INDEX;
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

    public void StartJumps(int jumpsAmount)
    {
        if (_isMoving) return;

        DefineJumpSettings(jumpsAmount);

        activeCoroutine = JumpMultiplePoints(jumpsAmount);
        StartCoroutine(activeCoroutine);
    }

    public void StopJumps()
    {
        if (_isMoving && activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            _isMoving = false;
            _isPaused = false; // Скидаємо стан паузи
        }
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

            // Логіка бонусного стрибка
            if (_bonusJumpRequested)
            {
                jumpLength = 10; // Бонусний стрибок на 10 точок вперед
                _bonusJumpRequested = false; // Скидаємо запит на бонусний стрибок
            }

            int nextIndex = _currentIndex + jumpLength;

            if (nextIndex >= roadPoints.Count)
            {
                nextIndex = roadPoints.Count - 1;
                pointsCrossed = totalPoints;
            }
            else
            {
                pointsCrossed += jumpLength;
            }

            yield return StartCoroutine(JumpToSplinePoint(percents[nextIndex])); // Стрибаємо до наступної точки
            roadPoints[nextIndex].ActorTrigger();

            _currentIndex = nextIndex;
            OnTouchdown?.Invoke();

            // Затримка перед наступним стрибком
            yield return new WaitForSeconds(_jumpDelay);
        }

        _isMoving = false;

        OnJumpsSeriesEnd?.Invoke(); // Викликаємо подію завершення серії стрибків
    }


    private IEnumerator JumpToSplinePoint(double targetPercent)
    {
        Vector3 startPos = transform.position;

        // Вирівнюємо актора уздовж напрямку сплайна на старті
        AlignToSplineDirection((float)targetPercent);

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

            Vector3 splinePosition = follower.spline.EvaluatePosition(currentPercent);
            float y = _groundY + Mathf.Sin(Mathf.PI * t) * _jumpHeight;
            transform.position = new Vector3(splinePosition.x, y, splinePosition.z);


            AlignToSplineDirection(currentPercent); // Вирівнювання відповідно до напряму сплайна

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
        finalPos.y = _groundY;

        transform.position = finalPos;
        follower.SetPercent(targetPercent);
        AlignToSplineDirection(targetPercent); // Вирівнювання після приземлення
    }

    private void AlignToSplineDirection(float targetPercent)
    {
        // Невелике зміщення вперед по сплайну для визначення напрямку
        float delta = 0.01f;

        // Отримуємо позиції для обчислення напрямку
        Vector3 forwardPoint = follower.spline.EvaluatePosition(Mathf.Clamp01(targetPercent + delta));
        Vector3 currentPoint = follower.spline.EvaluatePosition(Mathf.Clamp01(targetPercent));

        // Розрахунок вектора напрямку
        Vector3 direction = (forwardPoint - currentPoint).normalized;

        if (direction != Vector3.zero)
        {
            direction.y = 0f; // Ігноруємо нахил по осі Y
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Обчислюємо поворот уздовж напрямку
            transform.rotation = lookRotation; // Застосовуємо поворот
        }
    }

    // ------------------- Debug/Extra -------------------
    public void BonusJump()
    {
        _bonusJumpRequested = true;
    }

    public void ResetMoveData()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        _isMoving = false;
        _isPaused = false;
        _currentIndex = START_INDEX;

        // Скидаємо позицію на стартову точку
        SetInitialPosition();
    }
}