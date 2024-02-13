using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float moveSpeed = 3f;

    private IAvatar localAvatar => SpatialBridge.actorService.localActor.avatar;

    void Update()
    {
        if (SpatialBridge.actorService.localActor == null || localAvatar == null) return;
        
        Vector3 move = (localAvatar.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        move.y = 0;
        transform.position += move;
    }
}
