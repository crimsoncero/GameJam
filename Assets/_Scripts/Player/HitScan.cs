using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    
    public void Scan()
    {
        _collider.enabled = true;
        StartCoroutine(WaitForScan());
    }
    
    private IEnumerator WaitForScan()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        _collider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        EnemyUnit enemy = collision.GetComponent<EnemyUnit>();
        enemy.TakeDamage(5);
    }



}
