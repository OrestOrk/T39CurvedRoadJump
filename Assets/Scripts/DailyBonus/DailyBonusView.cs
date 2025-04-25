using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using DG.Tweening;

public class DailyBonusView : MonoBehaviour
{
    [FormerlySerializedAs("bonusReadyPanel")]
    [Header("Game Objects")]
    [SerializeField] private GameObject _bonusReadyIcon;    
    [SerializeField] private GameObject _lockPanel;
    
    [Header("Text")]
    [SerializeField] private Text _timerText;
    
    [FormerlySerializedAs("_openRewardScreenButton")]
    [Header("Buttons")]
    [SerializeField] private Button _openScreenButton;//open rewar window
    
    [SerializeField] private Button _openChestButton;//take reward

    [SerializeField] private Button _claimRewardButton;//close bonus screen

    [SerializeField] private ChestView _chestView;

    private float _butonAnimationDuration = 0.5f;
    
    private ScreenController _screenController;

    private void Start()
    {
        _screenController = ServiceLocator.GetService<ScreenController>();
    }

    public void ShowBonusReady()
    {
        _bonusReadyIcon.SetActive(true);
        _lockPanel.SetActive(false);

        _openScreenButton.interactable = true; // Активна кнопка

        ShowButton(_openChestButton.gameObject);
        HideButton(_claimRewardButton.gameObject);
    }

    public void ShowTimer(System.TimeSpan timeRemaining)
    {
        _bonusReadyIcon.SetActive(false);
        _lockPanel.SetActive(true);

        _openScreenButton.interactable = false; // Неактивна кнопка
        _timerText.text = string.Format("{0:D2}:{1:D2}", (int)timeRemaining.TotalHours + "H", timeRemaining.Minutes + "M");
    }
    
    public void OpenChest(int reward)
    {
        StartCoroutine(OpenChestCoroutine(reward));
    }

    public void ClaimReward()
    {
        HideButton(_claimRewardButton.gameObject);
        
        DelayManager.DelayAction(_screenController.CloseDailyBonusScreen,2f);
    }
    
    private IEnumerator OpenChestCoroutine(int reward)
    {
        HideButton(_openChestButton.gameObject);
        //OnBonusCollected();
        _chestView.OpentDailyBonus();
        yield return new WaitForSeconds(2f);
        _chestView.DisplayCoins(reward);
        yield return new WaitForSeconds(3);
        
        ShowButton(_claimRewardButton.gameObject);
    }

    private void ShowButton(GameObject button)
    {
        button.transform.localScale = Vector3.zero;
        button.SetActive(true);
        
        button.transform.DOScale(Vector3.one, _butonAnimationDuration);
    }

    private void HideButton(GameObject button)
    {
        button.transform.DOScale(Vector3.zero, _butonAnimationDuration).OnComplete(()=>button.SetActive(false));
    }
}