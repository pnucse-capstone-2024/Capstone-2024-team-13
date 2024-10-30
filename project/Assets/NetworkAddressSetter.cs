using System.Net;
using UnityEngine;
using Unity.Netcode;

public class NetworkAddressSetter : MonoBehaviour
{
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.Singleton;

        if (networkManager == null)
        {
            Debug.LogError("NetworkManager is not found!");
            return;
        }

        // �ڽ��� �����̺� IP �ּ� ��������
        string localIPAddress = GetLocalIPAddress();

        if (!string.IsNullOrEmpty(localIPAddress))
        {
            networkManager.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = localIPAddress;
            Debug.Log("Network address set to: " + localIPAddress);
        }
        else
        {
            Debug.LogError("Failed to get the local IP address.");
        }
    }

    private string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString(); // IPv4 �ּ� ��ȯ
                }
            }
            Debug.LogError("No IPv4 address found.");
            return null;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error getting local IP address: " + ex.Message);
            return null;
        }
    }
}