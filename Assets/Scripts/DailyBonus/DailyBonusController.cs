using System;
using UnityEngine;
using System.Collections;

public class DailyBonusController : MonoBehaviour
{
    public event Action OnRewardClaimed;
    
    private DateTime nextAvailableTime;
    private bool bonusAvailable;

    private const string BonusKey = "NextBonusTime";
    
    private DailyBonusView _bonusView;
    private CoinsBalanceController _coinsBalanceController;

    private const int COINS_REWARD = 10000;
    
    private void Start()
    {
        _bonusView = GetComponent<DailyBonusView>();
        _coinsBalanceController = ServiceLocator.GetService<CoinsBalanceController>();
        
        
        LoadNextAvailableTime();
        CheckBonusAvailability();
        StartCoroutine(UpdateTimerRoutine());
    }

    private void LoadNextAvailableTime()
    {
        if (PlayerPrefs.HasKey(BonusKey))
        {
            long binaryTime = Convert.ToInt64(PlayerPrefs.GetString(BonusKey));
            nextAvailableTime = DateTime.FromBinary(binaryTime);
        }
        else
        {
            nextAvailableTime = DateTime.MinValue;
        }
    }

    private void SaveNextAvailableTime()
    {
        PlayerPrefs.SetString(BonusKey, nextAvailableTime.ToBinary().ToString());
    }

    private void CheckBonusAvailability()
    {
        if (DateTime.Now >= nextAvailableTime)
        {
            bonusAvailable = true;
            _bonusView.ShowBonusReady();
        }
        else
        {
            bonusAvailable = false;
            TimeSpan timeRemaining = nextAvailableTime - DateTime.Now;
            _bonusView.ShowTimer(timeRemaining);
        }
    }

    public void OpenRewardChest()
    {
        if (!bonusAvailable) return;

        bonusAvailable = false;
        nextAvailableTime = DateTime.Now.AddHours(24);
        SaveNextAvailableTime();
       // _bonusView.OnBonusCollected(); // оновлення вікна

        // Тут можна додати нагороду гравцеві
        _bonusView.OpenChest(COINS_REWARD);
        
        AudioController.instance.PlayChestOpen();
    }

    public void ClaimCoins()
    {
        _coinsBalanceController.AddCoins(5000);
        _bonusView.ClaimReward();
        
        OnRewardClaimed?.Invoke();
    }

    private IEnumerator UpdateTimerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // оновлюємо щохвилини
            if (!bonusAvailable)
            {
                TimeSpan timeRemaining = nextAvailableTime - DateTime.Now;
                if (timeRemaining.TotalSeconds <= 0)
                {
                    bonusAvailable = true;
                    _bonusView.ShowBonusReady();
                }
                else
                {
                    _bonusView.ShowTimer(timeRemaining);
                }
            }
        }
    }
}
