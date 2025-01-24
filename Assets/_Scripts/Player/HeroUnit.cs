using Unity.VisualScripting;
using UnityEngine;
using System;
using MoreMountains.Tools;
using UnityEngine.UI;

public class HeroUnit : MonoBehaviour
{
    [field: SerializeField] public HeroData Data { get; private set; }


    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    public PathfindingModule PathfindingModule;
    [SerializeField] private MMProgressBar _bloodlustBar;

    // STATS::
    public int CurrentHealth { get; private set; }

    // Stats computational properties (if complicated, use an intermediary method)
    public int MaxHealth { get { return Data.BaseMaxHealth; } }
    public float MaxSpeed { get { return Data.BaseMaxSpeed; } }
    public int Power { get { return Data.BasePower * _damageMultiplier; } }
    public float AttackSpeed { get { return Data.BaseAttackSpeed; } }

    private int _damageMultiplier = 1;
    [SerializeField] private int _maxMultiplier = 3;
    [SerializeField] private float _bloodlustDuration = 3;
    private float _currentBloodlustDuration = 0;
    [SerializeField] private int _healAmount = 20;
    [SerializeField] private Image _foregroundBloodlust;
    [SerializeField] private Color _doubleBloodlustColor;
    [SerializeField] private Color _tripleBloodlustColor;
    private void Start()
    {
        // Init the unit automatically if starting with data. (for testing mainly)
        if (!Data.IsUnityNull())
            Init(Data);
    }

    public void Init(HeroData data)
    {
        if (data.IsUnityNull())
            throw new ArgumentNullException("data", "Can't initilize a unit with null data");

        Data = data;
        gameObject.name = $"Hero - {Data.name}";

        CurrentHealth = MaxHealth;
        _currentBloodlustDuration = 0;
    }
    private void Update()
    {
        if(_currentBloodlustDuration <= 0)
        {
            _currentBloodlustDuration = 0;
            _damageMultiplier = 1;
        }
        else
        {
            _currentBloodlustDuration -= Time.deltaTime;
        }
        _bloodlustBar.UpdateBar(_currentBloodlustDuration, 0, _bloodlustDuration);
        if(_damageMultiplier == 2)
        {
            _foregroundBloodlust.color = _doubleBloodlustColor;
        }
        if(_damageMultiplier == 3)
        {
            _foregroundBloodlust.color = _tripleBloodlustColor;
        }
    }
    public void GainBloodlust()
    {
        if(_damageMultiplier < _maxMultiplier)
        {
            _damageMultiplier++;
        }
        _currentBloodlustDuration = _bloodlustDuration;

    }
    public void TakeDamage(int damage)
    {
        if (damage < 0) return;
        CurrentHealth -= damage;
        TestUI.Instance.UpdateBubbleBar(CurrentHealth, 0, MaxHealth);
        if (CurrentHealth <= 0 || GameManager.Instance.IsHell)
        {
            //TODO change to Hellmode
            Debug.Log("Into Hell");
        }
        else
        {
            Debug.Log("Game Over");
        }

    }

    public void Heal(int amount = -1)
    {
        if (amount < 0)
            amount = _healAmount;

        CurrentHealth += amount;
        if(CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        TestUI.Instance.UpdateBubbleBar(CurrentHealth, 0, MaxHealth);
        

    }

}
