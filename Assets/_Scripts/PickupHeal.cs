using UnityEngine;

public class PickupHeal : MonoBehaviour
{
    [SerializeField] private int _healAmount = 20;

    private void Start()
    {
        GameManager.Instance.ChangedToHell += DestroyOnHell;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HeroUnit hero = collision.gameObject.GetComponent<HeroUnit>();
        hero.Heal(_healAmount);
        Destroy(gameObject);
    }

    public void DestroyOnHell()
    {
        Destroy(gameObject);
    }
}
