using UnityEngine;

public class EnemyAttackState : StateMachineBehaviour
{
    private EnemyFollow enemy;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<EnemyFollow>();

        if (enemy != null)
            enemy.StartAttack();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy != null)
            enemy.StopAttack();
        animator.SetBool("isAttacking", false);

    }
}
