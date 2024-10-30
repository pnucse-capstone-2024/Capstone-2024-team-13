using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkAddressSetter_2 : MonoBehaviour
{
    public InputField ipInputField; // IP �ּҸ� �Է¹��� InputField
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.Singleton;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager is not found!");
            return;
        }

        // InputField�� ���� ����� ������ ȣ��Ǵ� ������ ���
        ipInputField.onEndEdit.AddListener(SetNetworkAddress);
    }

    private void SetNetworkAddress(string newAddress)
    {
        if (networkManager != null)
        {
            networkManager.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = newAddress;
            Debug.Log("Network address set to: " + newAddress);
        }
    }
}