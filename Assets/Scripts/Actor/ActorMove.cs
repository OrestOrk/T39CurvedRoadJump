using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;

public class ActorMove : MonoBehaviour
{
    public SplineFollower follower;
    public List<RoadPoint> roadPoints;

    private int pointsPerJump = 1;
    private float jumpTime = 0.6f;
    private float jumpHeight = 1f;
    private float jumpDelay = 0.2f;
    private float followerSpeed = 0.1f;

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

        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 0.1f);
        }

        float startPercent = (float)follower.GetPercent();
        float targetPercentFloat = (float)targetPercent;
        float moveDuration = Mathf.Abs(targetPercentFloat - startPercent) / followerSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            follower.SetPercent(Mathf.Lerp(startPercent, targetPercentFloat, elapsedTime / moveDuration));
            transform.position = follower.transform.position;

            float yPosition = Mathf.Lerp(startPos.y, startPos.y + jumpHeight,
                Mathf.Sin(Mathf.PI * (elapsedTime / moveDuration)));
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

            yield return null;
        }

        transform.position =
            new Vector3(follower.transform.position.x, startPos.y, follower.transform.position.z);
        follower.SetPercent((float)targetPercent);
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