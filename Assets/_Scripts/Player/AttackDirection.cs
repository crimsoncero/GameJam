using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackDirection : MonoBehaviour
{
    [SerializeField] private Animator _attackAnimator;
    [SerializeField] private HitScan _hitScan;
    private Vector3 _lookDirection = Vector3.zero;
    

  

    public void Attack()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, _lookDirection);
        transform.rotation = lookRotation;

        if(transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 180)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        _attackAnimator.gameObject.SetActive(true);
        _hitScan.Scan();
    }


    public void SetDirection(Vector2 direction)
    {
        _lookDirection = direction.normalized;
    }
}
