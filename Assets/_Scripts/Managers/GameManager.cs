using MoreMountains.Feedbacks;
using System;
using System.Collections;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action ChangedToHell;
    /// <summary>
    /// Perform an action when the timer ticks a second, use only when totally needed.
    /// </summary>
    public event Action<int> OnTimerTick;


    [SerializeField] private int _damageOnTick = 3;
    [SerializeField] private LevelData _hellLevel;

    /// <summary>
    /// The in game time that has passed until now, counted in seconds.
    /// </summary>
    public int Timer { get; private set; } = 0;
    private float _timerSeconds;

    /// <summary>
    /// Is the game currently paused.
    /// </summary>
    public bool IsPaused { get; private set; } = true;
    public bool IsHell { get; private set; } = false;
    public bool IsGameActive { get; private set; } = false;

    [SerializeField] private VisualsController _visualsController;

    private void Update()
    {
        if (!IsPaused && IsGameActive)
        {
            _timerSeconds += Time.deltaTime;
            if(_timerSeconds > 1)
            {
                _timerSeconds -= 1;
                Timer++;
                
                if(!IsHell)
                    PlayerController.Instance.Unit.TakeDamage(_damageOnTick, false);
                
                OnTimerTick?.Invoke(Timer);
            }
        }
    }

    public void ChangeToHellLevel()
    {
        LevelManager.Instance.Init(_hellLevel);
        Timer = 0;
        _timerSeconds = 0;
        IsHell = true;
        OnTimerTick?.Invoke(Timer);
        MusicManager.Instance.StartBadPlace();
        _visualsController.Morph();

        ChangedToHell?.Invoke();
    }

    public void StartGame()
    {
        IsGameActive = true;
        IsPaused = false;
        Timer = 0;
        _timerSeconds = 0;
        OnTimerTick?.Invoke(Timer);

        MusicManager.Instance.StartGoodPlace();

        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        OnGameEnd?.Invoke();
    }

    /// <summary>
    /// Pause all the stuff in the game (projectiles, movement, animation, EVERYTHING)
    /// </summary>
    public void PauseGame()
    {
        IsPaused = true;
        OnGamePaused?.Invoke();
    }

    /// <summary>
    /// Resume executing all the things in the game that were paused.
    /// </summary>
    public void ResumeGame()
    {
        IsPaused = false;
        OnGameResumed?.Invoke();
    }


    
}
