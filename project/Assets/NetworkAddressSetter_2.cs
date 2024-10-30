using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkAddressSetter_2 : MonoBehaviour
{
    public InputField ipInputField; // IP 주소를 입력받을 InputField
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.Singleton;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager is not found!");
            return;
        }

        // InputField의 값이 변경될 때마다 호출되는 리스너 등록
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