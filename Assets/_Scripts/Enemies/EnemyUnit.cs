using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyUnit : MonoBehaviour
{
    [field: SerializeField] public EnemyData Data { get; private set; }


    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    public CircleCollider2D Collider;
    public PathfindingModule PathfindingModule;
    [SerializeField] private LayerMask _attackableLayers;
    
    private ObjectPool<EnemyUnit> _pool;

    // STATS::
    public int CurrentHealth { get; private set; }

    // Stats computational properties (if complicated, use an intermediary method)
    public int MaxHealth { get { return Data.BaseMaxHealth; } }
    public float MaxSpeed { get { return Data.BaseMaxSpeed; } }
    public int Power { get { return Data.BasePower; } }
    
    [Header("Gameplay Stats")]
    [SerializeField] private float _attackSpeed;
    private bool _canAttack;


   

    public void Initialize(EnemyData data, Vector3 position, ObjectPool<EnemyUnit> pool)
    {
        if (data.IsUnityNull())
            throw new ArgumentNullException("data", "Can't initilize a unit with null data");

        _pool = pool;

        Data = data;
        gameObject.name = $"{Data.name}";

        gameObject.transform.position = position;

        PathfindingModule.SetMaxSpeed(MaxSpeed);
        PathfindingModule.SetMaxAcceleration(1000);

        // Set Target
        PathfindingModule.SetTarget(PlayerController.Instance.Center);

        // Init Stats
        CurrentHealth = MaxHealth;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGamePaused += PauseEnemy;
        GameManager.Instance.OnGameResumed += ResumeEnemy;
    }
    private void OnDisable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePaused -= PauseEnemy;
            GameManager.Instance.OnGameResumed -= ResumeEnemy;
        }

        DestroyUnit();
    }
    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        CurrentHealth -= damage;
        
        if(CurrentHealth < 0)
        {
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
        _pool?.Release(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((_attackableLayers & collision.gameObject.layer) != 0)
        {
            if (_canAttack)
            {
                _canAttack = false;
                HeroUnit hero = collision.gameObject.GetComponent<HeroUnit>();
                hero.TakeDamage(Power);

                StartCoroutine(EnableAttack());
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
