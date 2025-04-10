using UnityEngine;
using DG.Tweening;

public class ActorMove : MonoBehaviour
{
    public Transform[] points;        // Масив трансформів цільових точок
    public float jumpTime = 0.6f;
    public float jumpHeight = 1f;

    private int currentIndex = 0;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex >= points.Length) return;

            JumpToPoint(points[nextIndex]);
            currentIndex = nextIndex;
        }
    }

    void JumpToPoint(Transform target)
    {
        isMoving = true;

        // Поворот у напрямку точки
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.DORotateQuaternion(lookRotation, 0.3f);
        }

        float YOffset = 0.7f;
        
        Vector3 jumpPosition = target.position;
        jumpPosition.y += YOffset;
        
        transform.DOJump(jumpPosition, jumpHeight, 1, jumpTime)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => isMoving = false);
    }
}