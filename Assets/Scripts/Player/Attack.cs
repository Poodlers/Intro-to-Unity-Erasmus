using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{

    PlayerInput playerAttack;

    private static readonly string ATTACK_1 = "Attack1";

    private static readonly string ATTACK_2 = "Attack2";

    private static readonly string ATTACK_3 = "Attack3";

    Animator animator;

    BoxCollider2D playerSwordCollider;

    private int CountAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = new PlayerInput();
        playerSwordCollider = GameObject.Find("PlayerSword").GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        playerAttack.Enable();
        playerAttack.Player.Attack.performed += ctx => AttackAction();
    }

    // Update is called once per frame
    void Update()
    {

        CheckAttackPhase();
    }
    private void ResetAttackPhase()
    {
        playerSwordCollider.enabled = false;
        animator.SetInteger("attackCounter", 0);
        CountAttack = 0;

    }

    public void EnableSwordCollider()
    {
        playerSwordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        playerSwordCollider.enabled = false;
    }

    public void CheckAttackPhase()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_1))
        {
            if (CountAttack > 1)
            {
                animator.SetInteger("attackCounter", 2);
            }
            else
            {
                ResetAttackPhase();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_2))
        {
            if (CountAttack > 2)
            {
                animator.SetInteger("attackCounter", 3);

            }
            else
            {
                ResetAttackPhase();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_3))
        {
            if (CountAttack >= 3)
            {
                ResetAttackPhase();
            }
        }
    }
    void AttackAction()
    {
        CountAttack++;
        if (CountAttack == 1)
        {

            animator.SetInteger("attackCounter", 1);
        }
    }
}
