using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using System;

public class ActorMove : MonoBehaviour
{
    public event Action OnJumpStart;
    
    public SplineFollower follower;
    public List<RoadPointController> roadPoints;

    private int pointsPerJump = 1;
    private float jumpTime = 2f;
    private float jumpHeight = 2f;
    private float jumpDelay = 0.5f;
    private float followerSpeed = 0.03f;

    private int currentIndex = 3;
    private bool isMoving = false;

    private List<double> percents = new();

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
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            int totalPointsToCross = 30;

            CalculatePointsPerJump(totalPointsToCross);
            StartCoroutine(JumpMultiplePoints(totalPointsToCross));
        }
    }

    public void StartJumps(int jumpsAmount)
    {
        CalculatePointsPerJump(jumpsAmount);
        StartCoroutine(JumpMultiplePoints(jumpsAmount));
    }

    private void CalculatePointsPerJump(int jumpsAmount)
    {
        if (jumpsAmount < 10)
            pointsPerJump = 0;
        else if (jumpsAmount < 15)
            pointsPerJump = 1;
        else if (jumpsAmount < 25)
            pointsPerJump = 2;
        else
            pointsPerJump = 3;
    }

    IEnumerator JumpMultiplePoints(int totalPoints)
    {
        isMoving = true;
        int pointsCrossed = 0;

        while (pointsCrossed < totalPoints)
        {
            int nextIndex = currentIndex + pointsPerJump + 1;
            if (nextIndex >= roadPoints.Count)
            {
                nextIndex = roadPoints.Count - 1;
                pointsCrossed = totalPoints;
            }
            else
            {
                pointsCrossed += pointsPerJump + 1;
            }

            yield return StartCoroutine(JumpToSplinePoint(percents[nextIndex]));
            roadPoints[nextIndex].ActorTrigger();
            currentIndex = nextIndex;
            yield return new WaitForSeconds(jumpDelay);
        }

        isMoving = false;
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
            // Ігноруємо X та Z координати для дотримання повороту тільки по Y
            direction.y = 0f; // Забезпечуємо, що напрямок не враховує нахил вгору чи вниз

            // Створюємо обертання навколо осі Y
            Quaternion lookRot = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 10f * Time.deltaTime);
        }
    }

    private float CalculateMoveDuration(float startPercent, float targetPercentFloat)
    {
        return Mathf.Abs(targetPercentFloat - startPercent) / followerSpeed;
    }

    private IEnumerator MoveAlongSpline(Vector3 startPos, float targetPercentFloat, float moveDuration, float elapsedTime)
    {
        float startPercent = (float)follower.GetPercent();

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // Пересування по сплайновому шляху
            follower.SetPercent(Mathf.Lerp(startPercent, targetPercentFloat, elapsedTime / moveDuration));
            transform.position = follower.transform.position;

            // Розрахунок позиції Y
            float yPosition = Mathf.Lerp(startPos.y, startPos.y + jumpHeight,
                Mathf.Sin(Mathf.PI * (elapsedTime / moveDuration)));
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

            // Поворот тільки по осі Y
            Vector3 targetPos = follower.spline.EvaluatePosition(targetPercentFloat);
            AlignToDirection(targetPos);

            yield return null;
        }
    }

    private void FinalizePosition(float targetPercent)
    {
        // Встановлення фінальної позиції на сплайні
        transform.position = new Vector3(follower.transform.position.x, transform.position.y, follower.transform.position.z);
        follower.SetPercent(targetPercent);
    }

    private void SetFollowerStartPosition()
    {
        if (currentIndex >= 0 && currentIndex < percents.Count)
        {
            double initialPercent = percents[currentIndex];
            follower.SetPercent(initialPercent);
        }
    }
}