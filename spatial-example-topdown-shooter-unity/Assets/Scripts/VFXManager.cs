using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public ParticleSystem enemyHitVFX;
    public ParticleSystem enemyDieVFX;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static void HitVFX(Vector3 position)
    {
        instance.enemyHitVFX.transform.position = position;
        instance.enemyHitVFX.transform.rotation = Quaternion.LookRotation(position - SpatialBridge.actorService.localActor.avatar.position);
        instance.enemyHitVFX.Emit(5);
    }

    public static void DieVFX(Vector3 position)
    {
        instance.enemyDieVFX.transform.position = position;
        instance.enemyDieVFX.Emit(1);
    }
}
