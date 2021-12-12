using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchKingAttack : StateMachineBehaviour
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
        Vector2 hitboxPos = entity.transform.position - new Vector3(entity.getAttackHitbox().offset, 0, 0);

        if (Vector2.Distance(_player.position, hitboxPos) <= entity.getAttackHitbox().range)
            animator.SetBool("KeepAttacking", true);
        else
            animator.SetBool("KeepAttacking", false);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /*OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that processes and affects root motion
    }*/

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
