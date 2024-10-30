using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class StairTrigger : NetworkBehaviour
{
    public GameObject uiCanvas;
    public GameObject XROrigin;
    public Vector3 targetPosition;
    private NetworkObject netObj;
    public Transform stairPosition;
    public Transform target;
    public float activationDistance = 2.0f; // UI가 활성화되는 거리

    private IEnumerator WaitForNetworkObject()
    {
        while (NetworkObjectManager.Instance.GetNetworkObject() == null)
        {
            Debug.Log("Waiting for network object...");
            yield return new WaitForSeconds(0.1f); // 네트워크 오브젝트가 등록될 때까지 대기
        }

        netObj = NetworkObjectManager.Instance.GetNetworkObject();
        Debug.Log("Network object found, initializing...");

        if (netObj != null)
        {
            target = netObj.transform.Find("Head");
        }
        else
        {
            Debug.LogError("NetworkObject is null.");
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForNetworkObject());
    }

    private void Update()
    {
        if (netObj != null && Vector3.Distance(target.position, stairPosition.position) < activationDistance)
        {
            uiCanvas.SetActive(true); // 특정 거리 이내에 들어오면 UI 활성화
        }
        else
        {
            uiCanvas.SetActive(false); // 거리 벗어나면 UI 비활성화
        }
    }

    public void YesButtonPress()
    {
        if (netObj == null)
        {
            Debug.LogError("Network object is not assigned yet.");
            return;
        }

        MovePlayer(targetPosition);
        uiCanvas.SetActive(false); // UI 비활성화
    }

    private void MovePlayer(Vector3 targetPosition)
    {
        if (IsServer)
        {
            MovePlayerServerRpc(targetPosition);
        }
        else
        {
            MovePlayerServerRpc(targetPosition);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void MovePlayerServerRpc(Vector3 targetPosition)
    {
        XROrigin.transform.position = targetPosition;
        netObj.transform.position = targetPosition;
        UpdateClientPositionClientRpc(targetPosition);
    }

    [ClientRpc]
    private void UpdateClientPositionClientRpc(Vector3 targetPosition)
    {
        if (!IsOwner)
        {
            XROrigin.transform.position = targetPosition;
            netObj.transform.position = targetPosition;
        }
    }
}