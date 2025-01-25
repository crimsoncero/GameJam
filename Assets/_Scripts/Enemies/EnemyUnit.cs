using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyUnit : MonoBehaviour
{
    [field: SerializeField] public EnemyData GoodBunnyData { get; private set; }
    [field: SerializeField] public EnemyData BadBunnyData { get; private set; }
    [field: SerializeField] public EnemyData GoodUnicornData { get; private set; }
    [field: SerializeField] public EnemyData BadUnicornData { get; private set; }
    

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    public PathfindingModule PathfindingModule;
    [SerializeField] private LayerMask _attackableLayers;
    [SerializeField] private VisualsAnimator _visualsAnimator;
    [SerializeField] private GameObject _bunnyVisuals;
    [SerializeField] private GameObject _unicornVisuals;
    private ObjectPool<EnemyUnit> _pool;
    private Coroutine _attackReset;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;

    public EnemyData CurrentData { get; private set; }
    // STATS::
    public int CurrentHealth { get; private set; }

    // Stats computational properties (if complicated, use an intermediary method)
    public int MaxHealth { get { return CurrentData.BaseMaxHealth; } }
    public float MaxSpeed { get { return CurrentData.BaseMaxSpeed; } }
    public int Power { get { return CurrentData.BasePower; } }
    
    [Header("Gameplay Stats")]
    [SerializeField] private float _attackSpeed;
    private bool _canAttack = true;


    [Header("Debugging")]
    [SerializeField] private bool _isDummy = false;

    private void Start()
    {
        if (_isDummy)
            CurrentHealth = MaxHealth;
    }
    public void Initialize(EnemyData data, Vector3 position, ObjectPool<EnemyUnit> pool)
    {
        if (data.IsUnityNull())
            throw new ArgumentNullException("data", "Can't initilize a unit with null data");

        _pool = pool;

        CurrentData = data;
        if(CurrentData.name == GoodBunnyData.name || CurrentData.name == BadBunnyData.name)
        {
            _bunnyVisuals.gameObject.SetActive(true);
            _unicornVisuals.gameObject.SetActive(false);
            _circleCollider = _bunnyVisuals.GetComponent<CircleCollider2D>();
            _capsuleCollider = _bunnyVisuals.GetComponent<CapsuleCollider2D>();

        }
        else if(CurrentData.name == GoodUnicornData.name || CurrentData.name == BadUnicornData.name)
        {
            _bunnyVisuals.gameObject.SetActive(false);
            _unicornVisuals.gameObject.SetActive(true);
            _circleCollider = _unicornVisuals.GetComponent<CircleCollider2D>();
            _capsuleCollider = _unicornVisuals.GetComponent<CapsuleCollider2D>();
        }


        gameObject.name = $"{CurrentData.name}";

        gameObject.transform.position = position;

        PathfindingModule.SetMaxSpeed(MaxSpeed);
        PathfindingModule.SetMaxAcceleration(1000);

        // Set Target
        PathfindingModule.SetTarget(PlayerController.Instance.Center);

        // Init Stats
        CurrentHealth = MaxHealth;
        _canAttack = true;
        
    }

    public void OnChangeToHell()
    {
        if (CurrentData.name == GoodBunnyData.name)
            CurrentData = BadBunnyData;
        else
            CurrentData = BadUnicornData;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGamePaused += PauseEnemy;
        GameManager.Instance.OnGameResumed += ResumeEnemy;
        GameManager.Instance.ChangedToHell += OnChangeToHell;
    }
    private void OnDisable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePaused -= PauseEnemy;
            GameManager.Instance.OnGameResumed -= ResumeEnemy;
            GameManager.Instance.ChangedToHell -= OnChangeToHell;

        }

        DestroyUnit();
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        CurrentHealth -= damage;
        
        if(CurrentHealth <= 0)
        {
            PlayerController.Instance.Unit.GainBloodlust();
            DestroyUnit();
        }
    }

    private IEnumerator EnableAttack()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _canAttack = true;
    }
    private void DestroyUnit()
    {
        //if(_pool == null)
        //{
        //    Destroy(gameObject);
        //}
        if(_attackReset != null)
        StopCoroutine(_attackReset);
        _pool?.Release(this);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((_attackableLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (_canAttack)
            {
                _canAttack = false;
                HeroUnit hero = collision.gameObject.GetComponent<HeroUnit>();
                hero.TakeDamage(Power);
                
                if(CurrentHealth > 0)
                    _attackReset = StartCoroutine(EnableAttack());
            }
        }
    }
    #region Pause & Resume

    private void PauseEnemy()
    {
        // Set speed to zero
        _rb2d.linearVelocity = Vector2.zero;
        PathfindingModule.PausePathfinding();
    }

    private void ResumeEnemy()
    {
        PathfindingModule.ResumePathfinding();
    }

   


    #endregion
}
