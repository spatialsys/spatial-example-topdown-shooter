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
        //Damage numbers
        SpatialBridge.vfxService.CreateFloatingText("<size=20><b>" + damage.ToString(), FloatingTextAnimStyle.Bouncy, transform.position + UnityEngine.Random.insideUnitSphere , Vector3.up * 13f, Color.white, lifetime: .3f);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        VFXManager.DieVFX(transform.position);
        SpatialBridge.vfxService.CreateFloatingText("<color=yellow><b><i><size=14>+15xp", FloatingTextAnimStyle.Bouncy, transform.position + UnityEngine.Random.insideUnitSphere , Vector3.up * 5f, Color.white, lifetime: .4f);
        GameObject.Destroy(gameObject);
    }
}
