using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : Singleton<PlayerController>
{
    [field: SerializeField] public HeroData Data { get; private set; }
    [field: SerializeField] public HeroUnit Unit { get; private set; }
    [field: SerializeField] public VisualsAnimator VisualsAnimator { get; private set; }

    public bool IsGodMode = false;
    
    [SerializeField] private HeroMover _mover;
    [SerializeField] private AttackDirection _attackDirection;
    private bool _canAttack = true;
    public Transform Center { get { return transform; } }

    private void Start()
    {
        UpdateSpeed();
        Unit.Init(Data);
    }

    public void UpdateSpeed()
    {
        _mover.Speed = Data.BaseMaxSpeed;
    }

    #region Player Input Methods

    public void OnAttack(CallbackContext context)
    {
        if(context.started && _canAttack)
        {
            _canAttack = false;
            _attackDirection.Attack();
            StartCoroutine(WaitForAttack());
        }
    }
    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(Data.BaseAttackSpeed);
        _canAttack = true;
    }

    public void OnLook(CallbackContext context)
    {
        Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(context.action.ReadValue<Vector2>());
        Vector2 dir;
        dir.x = pointerPosition.x - transform.position.x;
        dir.y = pointerPosition.y - transform.position.y;
        _attackDirection.SetDirection(dir.normalized);
    }
    public void OnMove(CallbackContext context)
    {
    //    if (GameManager.Instance.IsPaused) return; // Cant move while paused;

        Vector2 moveVec = context.ReadValue<Vector2>();
        _mover.Move(moveVec);
    }

    public void OnInteract(CallbackContext context)
    {
        if (GameManager.Instance.IsPaused) return; // Can't interact while paused.


        // Interact with stuff.
    }

    public void OnPause(CallbackContext context)
    {
        if (context.started)
        {
            if(!GameManager.Instance.IsPaused)
                GameManager.Instance.PauseGame();
            else // TODO - Remove this later, only for debugging atm, exiting pause will be from the pause menu or after selecing an upgrade.
                GameManager.Instance.ResumeGame();
        }
    }

    public void OnDeviceLost()
    {
        GameManager.Instance.PauseGame();
    }

    #endregion


}
