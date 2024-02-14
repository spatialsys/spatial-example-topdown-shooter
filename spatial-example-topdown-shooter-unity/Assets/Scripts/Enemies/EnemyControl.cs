using System;
using SpatialSys.UnitySDK;
using UnityEngine;

public class EnemyControl : MonoBehaviour, IDamageable
{
    public int initialHealth = 10;
    private int health;

    public Action OnTakeDamage;
    public Action OnDie;

    private void OnEnable()
    {
        health = initialHealth;
        EnemiesManager.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        EnemiesManager.DeregisterEnemy(this);
    }

    public void Hit(int damage)
    {
        health -= damage;
        OnTakeDamage?.Invoke();
        VFXManager.HitVFX(transform.position);
        // Damage numbers
        SpatialBridge.vfxService.CreateFloatingText("<size=20><b>" + damage.ToString(), FloatingTextAnimStyle.Bouncy, transform.position + UnityEngine.Random.insideUnitSphere , Vector3.up * 13f, Color.white);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        VFXManager.DieVFX(transform.position);
        // Fake XP earned
        SpatialBridge.vfxService.CreateFloatingText("<color=yellow><b><i><size=14>+15xp", FloatingTextAnimStyle.Bouncy, transform.position + UnityEngine.Random.insideUnitSphere , Vector3.up * 3f, Color.white);
        GameObject.Destroy(gameObject);
    }
}
