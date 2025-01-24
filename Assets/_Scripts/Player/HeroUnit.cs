using Unity.VisualScripting;
using UnityEngine;
using System;

public class HeroUnit : MonoBehaviour
{
    [field: SerializeField] public HeroData Data { get; private set; }


    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb2d;
    public PathfindingModule PathfindingModule;

    // STATS::
    public int CurrentHealth { get; private set; }

    // Stats computational properties (if complicated, use an intermediary method)
    public int MaxHealth { get { return Data.BaseMaxHealth; } }
    public float MaxSpeed { get { return Data.BaseMaxSpeed; } }
    public int Power { get { return Data.BasePower; } }
    public float AttackSpeed { get { return Data.BaseAttackSpeed; } }



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

    }

    
    public void TakeDamage(int damage)
    {
        if (damage < 0) return;
        Debug.Log("Player Hit");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Debug.Log("Player Died");
        }
    }
    
}
