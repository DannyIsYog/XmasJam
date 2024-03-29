using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchKingRun : StateMachineBehaviour
{

    Transform _player;
    Entity entity;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        entity = animator.GetComponent<Entity>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float offset = _player.position.x - animator.transform.position.x;
        float side = (offset) / Mathf.Abs(offset);
        if (Vector2.Distance(_player.position, entity.transform.position) <= 0.5)
            entity.SetMovement(1);
        else
            entity.SetMovement(side);

        if (Vector2.Distance(_player.position, entity.GetHitboxPos()) <= entity.GetHitboxRange())
        {
            entity.SetMovement(0);
            animator.SetTrigger("Attack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
