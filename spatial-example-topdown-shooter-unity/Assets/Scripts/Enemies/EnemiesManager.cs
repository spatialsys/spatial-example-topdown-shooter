using System.Collections.Generic;
using UnityEngine;

// Using a monoBehavior so we can clear static data when the game is destroyed
public class EnemiesManager : MonoBehaviour
{
    public static List<EnemyControl> enemies { get; private set; }

    private void Awake()
    {
        enemies = new List<EnemyControl>();
    }

    private void OnDestroy()
    {
        enemies = null;
    }

    public static void RegisterEnemy(EnemyControl enemy)
    {
        enemies.Add(enemy);
    }

    public static void DeregisterEnemy(EnemyControl enemy)
    {
        if (enemies != null)
        {
            enemies.Remove(enemy);
        }
    }
}
