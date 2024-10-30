using Unity.Netcode;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{   public GameObject StartCanvas;
    public GameObject SettingCanvas;
    public GameObject WaitCanvas;
    public GameObject PCManager;
    public GameObject XROrigin;
    public Camera playerCamera;

    [ServerRpc(RequireOwnership = false)]
    private void RequestSpawnXROnServerServerRpc(ulong clientId, ServerRpcParams rpcParams = default)
    {   
        Debug.Log("VRPlayer spawned on server.");
        GameObject xrOriginPrefab = Instantiate(XROrigin);
        NetworkObject netObj = xrOriginPrefab.GetComponent<NetworkObject>();
        xrOriginPrefab.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId);
        //xrOriginPrefab.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        StartCanvas.SetActive(false);
        playerCamera = xrOriginPrefab.GetComponentInChildren<Camera>();
        playerCamera.targetDisplay = 0;
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
        {
            RequestSpawnXROnServerServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    public void LoadVRScene()
    {
        //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.StartClient();
        //RequestSpawnXROnServerServerRpc();

        StartCanvas.SetActive(false);
        WaitCanvas.SetActive(true);
    }

    public void LoadPCScene()
    {
        NetworkManager.Singleton.StartHost();
        StartCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        var pcCamera = Instantiate(PCManager);
        pcCamera.GetComponent<NetworkObject>().Spawn();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

}

/*using UnityEngine;
using Unity.Netcode;

public class StartSceneManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject SettingCanvas;
    public GameObject PCManager;
    //public Camera playerCamera;

    public NetworkSpawnManager networkSpawnManager;


    public void LoadVRScene()
    {
        NetworkManager.Singleton.StartClient();
        //networkSpawnManager.RequestSpawnXROnServer(NetworkManager.Singleton.LocalClientId);
        StartCanvas.SetActive(false);
    }

    public void LoadPCScene()
    {
        NetworkManager.Singleton.StartHost();
        StartCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        var pcCamera = Instantiate(PCManager);
        pcCamera.GetComponent<NetworkObject>().Spawn();
    }

    private void OnClientConnected(ulong clientId)
    {
        if (networkSpawnManager != null)
        {
            networkSpawnManager.RequestSpawnXROnServer(clientId);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}*/