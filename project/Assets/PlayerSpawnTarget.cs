using Unity.Netcode;

public class PlayerSpawnTarget : NetworkBehaviour
{
    public VrPlayerViewCameraController cameraFollowScript;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            cameraFollowScript.target = transform;
        }
    }
}

