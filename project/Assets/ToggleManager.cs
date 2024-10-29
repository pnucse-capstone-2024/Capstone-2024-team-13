using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class SmartphoneTogglemanager : NetworkBehaviour
{
    public Toggle toggle;
    public NetworkObject[] networkObjects;
    private NetworkVariable<bool> isActive = new NetworkVariable<bool>(true);

    void Start()
    {
        if (IsServer)
        {
            // Initialize toggle based on the current state
            toggle.isOn = isActive.Value;
        }

        // Add listener for toggle changes
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Subscribe to changes in the NetworkVariable
        isActive.OnValueChanged += OnIsActiveChanged;
    }

    void OnToggleValueChanged(bool isOn)
    {
        if (IsServer)
        {
            // Update the NetworkVariable on the server
            isActive.Value = isOn;
        }
    }

    private void OnIsActiveChanged(bool previousValue, bool newValue)
    {
        // Update all network objects based on the new value
        foreach (var obj in networkObjects)
        {
            obj.gameObject.SetActive(newValue);
            Debug.Log($"Setting {obj.name} active state to {newValue}");
        }
    }
}