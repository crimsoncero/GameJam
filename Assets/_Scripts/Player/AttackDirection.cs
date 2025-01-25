using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackDirection : MonoBehaviour
{
    [SerializeField] private Animator _attackAnimator;
    [SerializeField] private HitScan _hitScan;
    [SerializeField] private MMF_Player _attackSFX;
    private Vector3 _lookDirection = Vector3.zero;


    private void Update()
    {
        Vector3 scale = PlayerController.Instance.VisualsAnimator.transform.localScale;
        if(_lookDirection.x > 0)
        {
            scale.x = 0.2f;
            PlayerController.Instance.VisualsAnimator.SetFloat("RunSpeed", 1);
        }
        else if(_lookDirection.x < 0)
        {
            scale.x = -0.2f;
            PlayerController.Instance.VisualsAnimator.SetFloat("RunSpeed", -1);

        }
        PlayerController.Instance.VisualsAnimator.transform.localScale = scale;
    }

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
        _attackSFX.PlayFeedbacks();
        _attackAnimator.gameObject.SetActive(true);
        _hitScan.Scan();
    }


    public void SetDirection(Vector2 direction)
    {
        _lookDirection = direction.normalized;
    }
}
