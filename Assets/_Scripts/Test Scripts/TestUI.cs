using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public class TestUI : Singleton<TestUI>
{

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _pauseText;
    [SerializeField] public CircularProgressBar _bubbleProgressBar;


    void Start()
    {
        GameManager.Instance.OnTimerTick += UpdateTimerText;
        GameManager.Instance.OnGamePaused += OnPauseChange;
        GameManager.Instance.OnGameResumed += OnPauseChange;
    }

    
    private void UpdateTimerText(int timer)
    {
       _timerText.text = Helpers.SecondsToMMSS(timer);

    }

    public void UpdateBubbleBar(int value, int min, int max)
    {
        _bubbleProgressBar.UpdateBar(value, min, max);
    
    }
    private void OnPauseChange()
    {
        if(GameManager.Instance.IsPaused)
            _pauseText.gameObject.SetActive(true);
        else
            _pauseText.gameObject.SetActive(false);
    }
}
