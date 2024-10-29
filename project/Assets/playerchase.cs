using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class playerchase : MonoBehaviour
{
    public Transform target;
    public Vector3 positionOffset = new Vector3(-0.5f, 0.5f, -1f);
    private IEnumerator WaitForNetworkObject()
    {
        while (NetworkObjectManager.Instance.GetNetworkObject() == null)
        {
            Debug.Log("Waiting for network object...");
            yield return new WaitForSeconds(0.1f); // ��Ʈ��ũ ������Ʈ�� ��ϵ� ������ ���
        }

        NetworkObject netObj = NetworkObjectManager.Instance.GetNetworkObject();
        //playerCamera = netObj.GetComponentInChildren<Camera>();
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

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}