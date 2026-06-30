using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static event Action<int> OnDiamondCountChanged;
    public static event Action<int> OnEnemyCountChanged;
    private int diamondCount = 0;
    private int enemyCount = 0;

    public void DiamondCollect()
    {
        diamondCount++;
        OnDiamondCountChanged?.Invoke(diamondCount);
    }

    public void EnemyDefeated()
    {
        enemyCount--;
        OnEnemyCountChanged?.Invoke(enemyCount);
    }

    public void EnemySpawned()
    {
        enemyCount++;
        OnEnemyCountChanged?.Invoke(enemyCount);
    }
}
