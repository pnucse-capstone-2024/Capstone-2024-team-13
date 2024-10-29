using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class debugtest : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnServerStarted()
    {
        Debug.Log("Sever Start.");
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Connecting Client, Client ID: " + clientId);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.LogError("Disconnecting Client. Client ID: " + clientId);
    }

}
