using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public Collider hitbox;
    [SerializeField] private int damage = 10;
    private HashSet<Enemy> hittedEnemies = new();

    private void Awake()
    {
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hittedEnemies.Clear();
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && hittedEnemies.Add(enemy))
        {
            enemy.TakeDamage(damage);
            Debug.Log($"Hit {enemy.name} for {damage} damage.");
        }
    }
}
