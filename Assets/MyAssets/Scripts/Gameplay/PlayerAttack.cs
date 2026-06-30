using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public WeaponHitbox weaponHitbox;
    [SerializeField] private TrailRenderer swordTrail;

    public void EnableHitbox()
    {
        weaponHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        weaponHitbox.DisableHitbox();
        if (swordTrail != null)
            swordTrail.emitting = false;

    }

    public void EnableTrail()
    {
        if (swordTrail != null)
        {
            swordTrail.Clear();
            swordTrail.emitting = true;
        }
    }
}