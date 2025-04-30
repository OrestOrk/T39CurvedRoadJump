using System.Collections;
using UnityEngine;

public class SpikeTrapController : BaseRoadItem
{
    private float minTime = 1f;
    private float maxTime = 4f;
    
    private SpikeState _spikeState;
    
    private SpikeTrapView _spikeTrapView;
    private void Start()
    {
        base.Start();
        
        _spikeTrapView = GetComponent<SpikeTrapView>();
        StartCoroutine(SendMessageWithDelay());
        _spikeState = SpikeState.Deactivated;
    }

    // Корутин для відправки повідомлення
    private IEnumerator SendMessageWithDelay()
    {
        while (true)
        {
            // Вибираємо випадковий час
            float randomTime = Random.Range(minTime, maxTime);

            // Чекаємо цей час
            yield return new WaitForSeconds(randomTime);

            ActivateSpike();
            
        }
    }

    public override void Activate()//callFromPlayer
    {
        if (_spikeState == SpikeState.Active)
        {
            _actorController.SpikeTrapTrigger();
        }
    }
    
    private void ActivateSpike()
    {
        _spikeState = SpikeState.Active;
        
        _spikeTrapView.PlayActivateSpikeAnimation();
        _spikeTrapView.PlaySkullVFX();
        
        DelayManager.DelayAction(DeactivateSpike,0.5f);
    }

    private void DeactivateSpike()
    {
        _spikeState = SpikeState.Deactivated;
    }
}