using SpatialSys.UnitySDK;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnPerSecond = 5f;
    [Tooltip("How far from the player the enemies will spawn.")]
    public float spawnRadius = 10f;
    public int maxEnemies = 10;
    public GameObject enemyPrefab;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / spawnPerSecond)
        {
            timer -= 1f / spawnPerSecond;
            if (EnemiesManager.enemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 pos = SpatialBridge.actorService.localActor.avatar.position;
        Vector2 randomPos = Random.onUnitSphere * spawnRadius;
        pos.x += randomPos.x;
        pos.z += randomPos.y;
        pos.y = 0f;

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
