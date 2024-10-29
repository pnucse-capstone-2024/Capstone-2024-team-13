using UnityEngine;
using UnityEngine.UI;

public class TransformObject : MonoBehaviour
{
    public Transform XROrigin;
    public Transform Fire;
    public Dropdown selectPlayerLocation;
    public Dropdown selectFireLocation;
    public Transform[] settingPersonLocation;
    public Transform[] settingFireLocation;

    void Start()
    {
        selectPlayerLocation.onValueChanged.AddListener(delegate { OnDropdownValueChanged(selectPlayerLocation); });
        selectFireLocation.onValueChanged.AddListener(delegate { OnDropdownValueChanged2(selectFireLocation); });
    }

    public void OnDropdownValueChanged(Dropdown dropdown)
    {
        int selectedIndex = dropdown.value;

        if (selectedIndex < settingFireLocation.Length)
        {
            XROrigin.position = settingPersonLocation[selectedIndex].position;
            XROrigin.rotation = settingPersonLocation[selectedIndex].rotation;
        }
    }
    public void OnDropdownValueChanged2(Dropdown dropdown)
    {
        int selectedIndex = dropdown.value;

        if (selectedIndex < settingFireLocation.Length)
        {
            Fire.position = settingFireLocation[selectedIndex].position;
        }
    }
}
