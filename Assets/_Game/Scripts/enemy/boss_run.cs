using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    Transform player;
    public float speed = 2.5f;
    public int attackRange;
    Rigidbody2D rb;
    Boss boss;
    float attackDelay = 4f; // Thời gian delay giữa các lần tấn công
    float attackTimer = 0f; // Biến đếm thời gian

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //Debug.Log(Vector2.Distance(player.position, rb.position));
        if (Vector2.Distance(player.position, rb.position) <= 16)
        {
            attackTimer += Time.deltaTime; // Tăng biến đếm thời gian

            if (attackTimer >= attackDelay)
            {
                Debug.Log("attack");
                animator.SetTrigger("Attack");

                attackTimer = 0f; // Đặt lại biến đếm thời gian về 0
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
