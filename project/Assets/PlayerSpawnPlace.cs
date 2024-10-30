using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerSpawnPlace : NetworkBehaviour
{
    public Dropdown floorDropdown;
    public Transform firstFloorPosition;
    public Transform secondFloorPosition;
    public Transform thirdFloorPosition;
    public Vector3 firstFloor = new Vector3(0, 2, 0);
    public Vector3 secondFloor = new Vector3(0, 6, 0);
    public Vector3 thirdFloor = new Vector3(0, 10, 0) ;
    public GameObject XROrigin;
    private NetworkObject netObj;

    private IEnumerator WaitForNetworkObject()
    {
        while (NetworkObjectManager.Instance.GetNetworkObject() == null)
        {
            Debug.Log("Waiting for network object...");
            yield return new WaitForSeconds(0.1f); // ��Ʈ��ũ ������Ʈ�� ��ϵ� ������ ���
        }

        netObj = NetworkObjectManager.Instance.GetNetworkObject();
        Debug.Log("Network object found, initializing...");

        // netObj�� �Ҵ�� �Ŀ��� Dropdown �����ʸ� ���
        floorDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void Start()
    {
        // �ڷ�ƾ�� �����Ͽ� netObj�� �Ҵ�� ������ ��ٸ�
        StartCoroutine(WaitForNetworkObject());
    }

    // Dropdown���� ���� �������� �� ȣ��� �Լ�
    private void OnDropdownValueChanged(int index)
    {
        if (netObj == null)
        {
            Debug.LogError("Network object is not assigned yet.");
            return;
        }

        switch (index)
        {
            case 0:
                TeleportToPosition(firstFloor);
                Debug.Log("Player Location Change(1st)");
                break;
            case 1:
                TeleportToPosition(secondFloor);
                Debug.Log("Player Location Change(2nd)");
                break;
            case 2:
                TeleportToPosition(thirdFloor);
                Debug.Log("Player Location Change(3rd)");
                break;
        }
    }

    private void TeleportToPosition(Vector3 newPosition)
    {
        if (IsServer)
        {
            netObj.transform.position = newPosition;
            RequestPositionChangeClientRpc(newPosition);

            Debug.Log("Request Teleport");
        }
        else
        {
            // Ŭ���̾�Ʈ�� ������ ��ġ ���� ��û
            TeleportServerRpc(newPosition);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TeleportServerRpc(Vector3 newPosition)
    {
        XROrigin.transform.position = newPosition;
        netObj.transform.position = newPosition;        

        RequestPositionChangeClientRpc(newPosition);
    }

    [ClientRpc]
    private void RequestPositionChangeClientRpc(Vector3 newPosition)
    {
        if (!IsOwner)
        {
            XROrigin.transform.position = newPosition;
            netObj.transform.position = newPosition;

            Debug.Log("Successfully teleported on client");
        }
    }
}
