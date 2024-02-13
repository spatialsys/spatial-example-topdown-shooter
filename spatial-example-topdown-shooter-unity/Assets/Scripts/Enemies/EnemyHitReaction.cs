using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make the enemy flash when hit
public class EnemyHitReaction : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hitMaterial;
    public MeshRenderer meshRenderer;

    public EnemyControl enemyControl;

    public float hitDuration = 0.1f;

    private Coroutine hitCoroutine;
    private float hitTimer;

    private void OnEnable()
    {
        enemyControl.OnTakeDamage += OnDamage;
    }

    private void OnDisable()
    {
        enemyControl.OnTakeDamage -= OnDamage;
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
        }
    }

    private void OnDamage()
    {
        hitTimer = hitDuration;
        if (hitCoroutine == null)
        {
            hitCoroutine = StartCoroutine(HitCoroutine());
        }
    }

    private IEnumerator HitCoroutine()
    {
        meshRenderer.material = hitMaterial;
        while (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
            yield return null;
        }
        meshRenderer.material = defaultMaterial;
    }
}
