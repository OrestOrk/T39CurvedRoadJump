using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BootView : MonoBehaviour
{
    public void ActivationEffect()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}
