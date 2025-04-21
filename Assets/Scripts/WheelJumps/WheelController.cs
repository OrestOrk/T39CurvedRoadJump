using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public event Action<int> OnWheelResult;
    
    public event Action OnStartSpin;
    public event Action OnEndSpin;
    
    [SerializeField] private WheelView _wheelView;

    [SerializeField] private Transform wheelTransform;  
    [SerializeField] private List<WheelSector> sectors;
    [SerializeField] private Transform arrowTransform;

     private float minSpinSpeed = 2f;
     private float maxSpinSpeed = 6f;
     
     private float spinDuration = 5f; 
     private float slowdownDuration = 1f;

    private Coroutine spinCoroutine;
    private float spinSpeed;
    
    private WheelSector _winningSector;
    private ActorController _actorController;

    private void Start()
    {
        _actorController = ServiceLocator.GetService<ActorController>();
        _actorController.OnJumpsSeriesComplette += _wheelView.ShowWheel;
    }

    public void StartSpin()
    {
        if (spinCoroutine != null) return;

        spinSpeed = UnityEngine.Random.Range(minSpinSpeed, maxSpinSpeed); // Рандомізуємо швидкість
        spinCoroutine = StartCoroutine(SpinWheel());
    }

    private IEnumerator SpinWheel()
    {
        //OnStartSpin?.Invoke();
        
        _wheelView.DeactivateSpinButton();
        
        float currentTime = 0f;
        float initialSpeed = 360f * spinSpeed;
        float rotationSpeed = 0f;

        while (currentTime < spinDuration)
        {
            currentTime += Time.deltaTime;
            rotationSpeed = Mathf.Lerp(0, initialSpeed, currentTime / spinDuration);
            wheelTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        currentTime = 0f;
        float finalSpeed = rotationSpeed;

        while (currentTime < slowdownDuration)
        {
            currentTime += Time.deltaTime;
            rotationSpeed = Mathf.Lerp(finalSpeed, 0, currentTime / slowdownDuration);
            wheelTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        spinCoroutine = null;
        //OnEndSpin?.Invoke();
        _winningSector = DetermineWinningSector();
        
        DelayManager.DelayAction(SendWheelResult, 3f);
    }

    private WheelSector DetermineWinningSector()
    {
        float closestDistance = float.MaxValue;
        WheelSector winningSector = null;

        foreach (var sector in sectors)
        {
            Transform sectorTransform = sector.transform;
            float distance = Vector3.Distance(arrowTransform.position, sectorTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                winningSector = sector;
            }
        }

        return winningSector;
    }

    private void SendWheelResult()
    {
        _wheelView.HideWheel();
        
        int winningValue = _winningSector.sectorValue;
        
        OnWheelResult?.Invoke(winningValue);
    }

    private void OnDestroy()
    {
        _actorController.OnJumpsSeriesComplette -= _wheelView.ShowWheel;
    }
}