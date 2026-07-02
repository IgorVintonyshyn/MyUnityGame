using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider weaponHitBox;
    public TrailRenderer swordTrail;


    public void EnableHitBox()
    {
        weaponHitBox.enabled = true;
    }

    public void DisableHitBox()
    {
        weaponHitBox.enabled = false;
    }

    public void EnableTrail()
    {
        if (swordTrail != null)
        {
            swordTrail.Clear();
            swordTrail.emitting = true;
        }
    }
    public void DisableTrail()
    {
        if (swordTrail != null)
        {
            swordTrail.emitting = false;
        }
    }
}
