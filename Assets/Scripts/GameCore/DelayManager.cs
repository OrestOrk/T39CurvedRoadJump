using System;
using System.Collections;
using UnityEngine;

public static class DelayManager
{
    private static MonoBehaviour coroutineRunner;

    public static void Initialize(MonoBehaviour runner)
    {
        if (runner == null)
        {
            Debug.LogError("Coroutine runner cannot be null.");
            return;
        }

        coroutineRunner = runner;
    }

    public static void DelayAction(Action methodToCall, float delayInSeconds)
    {
        if (coroutineRunner == null)
        {
            Debug.LogError("DelayController is not initialized. Call Initialize() before using.");
            return;
        }

        if (methodToCall == null)
        {
            Debug.LogError("Method is null.");
            return;
        }

        coroutineRunner.StartCoroutine(ExecuteWithDelayCoroutine(methodToCall, delayInSeconds));
    }

    private static IEnumerator ExecuteWithDelayCoroutine(Action methodToCall, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        methodToCall?.Invoke();
    }
}