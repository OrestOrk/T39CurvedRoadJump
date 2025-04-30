using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public event Action OnGameOverSector;
    public event Action<int> OnWheelResult;
    
    public event Action OnStartSpin;
    public event Action OnEndSpin;
    
    [SerializeField] private WheelView _wheelView;

    [SerializeField] private Transform wheelTransform;  
    [SerializeField] private List<WheelSectorController> sectors;
    [SerializeField] private Transform arrowTransform;

     private float minSpinSpeed = 2f;
     private float maxSpinSpeed = 6f;
     
     private float spinDuration = 5f; 
     private float slowdownDuration = 1f;

    private Coroutine spinCoroutine;
    private float spinSpeed;
    
    private WheelSectorController _winningSectorController;
    private ActorController _actorController;
    private GameController _gameController;

    private void Start()
    {
        _actorController = ServiceLocator.GetService<ActorController>();
        _gameController = ServiceLocator.GetService<GameController>();
        
        _actorController.OnJumpsSeriesComplette += _wheelView.ShowWheel;
        _gameController.OnStartPlaying += _wheelView.ShowWheel;
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
        AudioController.instance.PlaySpinWheelClip();
        
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
        _winningSectorController = DetermineWinningSector();
        _winningSectorController.WinSector();
        
        AudioController.instance.PlaySpinEndCLip();
        
        DelayManager.DelayAction(SendWheelResult, 3f);
    }

    private WheelSectorController DetermineWinningSector()
    {
        float closestDistance = float.MaxValue;
        WheelSectorController winningSectorController = null;

        foreach (var sector in sectors)
        {
            Transform sectorTransform = sector.transform;
            float distance = Vector3.Distance(arrowTransform.position, sectorTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                winningSectorController = sector;
            }
        }

        return winningSectorController;
    }

    private void SendWheelResult()
    {
        _wheelView.HideWheel();

        if (_winningSectorController.isGameOver == true)
        {
            OnGameOverSector?.Invoke();
            return;
        }
        
        int winningValue = _winningSectorController.sectorValue;
        
        OnWheelResult?.Invoke(winningValue);
    }

    private void OnDestroy()
    {
        _actorController.OnJumpsSeriesComplette -= _wheelView.ShowWheel;
        _gameController.OnStartPlaying -= _wheelView.ShowWheel;
    }
}