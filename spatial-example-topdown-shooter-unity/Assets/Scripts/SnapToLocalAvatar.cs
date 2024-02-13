using UnityEngine;
using SpatialSys.UnitySDK;

public class SnapToLocalAvatar : MonoBehaviour
{
    private IAvatar localAvatar => SpatialBridge.actorService.localActor.avatar;
    void LateUpdate()
    {
        if (SpatialBridge.actorService.localActor == null || localAvatar == null) return;
        transform.position = localAvatar.position;
    }
}
