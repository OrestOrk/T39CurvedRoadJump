using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;

public class ActorMove : MonoBehaviour
{
    public SplineFollower follower;         // Компонент для слідування по сплайну
    public GameObject actor;                // Основний актор, який буде рухатись по сплайну
    public List<RoadPoint> roadPoints;      // Всі точки на шляху
    public int pointsPerJump = 2;           // Скільки точок пропускається
    public float jumpTime = 0.6f;           // Час для стрибка
    public float jumpHeight = 1f;           // Висота стрибка
    public float jumpDelay = 0.2f;          // Затримка між стрибками
    public float followerSpeed = 0.1f;      // Швидкість руху сплайна

    private int currentIndex = 0;
    private bool isMoving = false;

    private List<double> percents = new();

    private void Start()
    {
        follower.follow = false;

        percents.Clear();
        foreach (var point in roadPoints)
        {
            // Визначення відсотка для кожної точки
            SplineSample sample = follower.spline.Project(point.transform.position);
            double percent = sample.percent;
            percents.Add(percent);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            int totalPointsToCross = 15;
            StartCoroutine(JumpMultiplePoints(totalPointsToCross));
        }
    }

    IEnumerator JumpMultiplePoints(int totalPoints)
    {
        isMoving = true;
        int pointsCrossed = 0;

        while (pointsCrossed < totalPoints)
        {
            // Отримуємо індекс наступної точки
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

            // Перехід до сплайну для стрибка на певну точку
            yield return StartCoroutine(JumpToSplinePoint(percents[nextIndex]));

            // Виклик для фінальної точки
            roadPoints[nextIndex].ActorTrigger();

            currentIndex = nextIndex;

            yield return new WaitForSeconds(jumpDelay);
        }

        isMoving = false;
    }

    IEnumerator JumpToSplinePoint(double targetPercent)
    {
        // Отримуємо початкову позицію
        Vector3 startPos = actor.transform.position;
        // Отримуємо позицію по сплайну для заданого відсотка
        Vector3 targetPos = follower.spline.EvaluatePosition((float)targetPercent);

        // Поворот актора до напрямку сплайна
        Vector3 direction = (targetPos - actor.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);
            actor.transform.rotation = Quaternion.Lerp(actor.transform.rotation, lookRot, 0.1f);
        }

        // Створюємо плавне переміщення по X і Z
        float startPercent = (float)follower.GetPercent();
        float targetPercentFloat = (float)targetPercent;

        // Визначаємо час для руху на сплайні
        float moveDuration = Mathf.Abs(targetPercentFloat - startPercent) / followerSpeed;

        // Плавно рухаємося по X і Z одночасно з підйомом по Y
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // Рух по сплайну по X та Z
            follower.SetPercent(Mathf.Lerp(startPercent, targetPercentFloat, elapsedTime / moveDuration));
            actor.transform.position = follower.transform.position;

            // Стрибок по осі Y (плавний підйом та спуск)
            float yPosition = Mathf.Lerp(startPos.y, startPos.y + jumpHeight, Mathf.Sin(Mathf.PI * (elapsedTime / moveDuration)));
            actor.transform.position = new Vector3(actor.transform.position.x, yPosition, actor.transform.position.z);

            yield return null;
        }

        // Завершуємо рух по сплайну та стрибок
        actor.transform.position = new Vector3(follower.transform.position.x, startPos.y, follower.transform.position.z);

        // Після завершення руху по сплайну, оновлюємо відсоток сплайна
        follower.SetPercent((float)targetPercent);
    }
}
