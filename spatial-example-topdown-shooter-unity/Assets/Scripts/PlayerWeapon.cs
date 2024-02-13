using UnityEngine;
using SpatialSys.UnitySDK;

[RequireComponent(typeof(ParticleSystem))]
public class PlayerWeapon : MonoBehaviour
{
    public Vector2Int damageRange;
    [Tooltip("Rounds per second")]
    public float rps;
    public int bullets = 1;

    private ParticleSystem ps;
    private float shotTimer;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        transform.position = SpatialBridge.actorService.localActor.avatar.position + Vector3.up;

        if (EnemiesManager.enemies.Count == 0)
            return;

        //find closest enemy (slow)
        float minDistance = float.MaxValue;
        EnemyControl closestEnemy = null;
        foreach (var enemy in EnemiesManager.enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        shotTimer += Time.deltaTime;
        if (shotTimer >= 1f / rps)
        {
            shotTimer -= 1f / rps;
            if (closestEnemy != null)
            {
                Vector3 dir = closestEnemy.transform.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);
                ps.Emit(bullets);
            }
        }
    }

    // read collision events from attached particle system
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Hit(Random.Range(damageRange.x, damageRange.y));
        }
    }
}
