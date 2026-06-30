using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;     // ѕосиланн€ на геро€
    [SerializeField] private float moveSpeed = 3f; // Ўвидк≥сть руху
    [SerializeField] private float stopDistance = 1.5f; // ¬≥дстань, на €к≥й зупин€Їтьс€ ворог

    public Animator animator;
    
    private bool isAttacking = false;

    private void Start()
    {
        // якщо не призначено вручну Ч шукаЇмо геро€ по тегу
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        if (isAttacking) return;

        if (player == null) return;

        // ¬ектор напр€мку до гравц€ (по горизонтал≥)
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // ўоб ворог не нахил€вс€ вгору/вниз

        float distance = direction.magnitude;

        // якщо ще далеко Ч рухаЇмось
        if (distance > stopDistance)
        {
            if (animator.GetBool("isAttacking"))
                animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ѕовертаЇмось у напр€мку гравц€
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isMoving", false);
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

}
