using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // 활성화/비활성화할 패널을 에디터에서 할당

    // 패널의 활성화/비활성화를 토글하는 함수
    public void TogglePanelVisibility()
    {
        if (panel != null)
        {
            // 패널의 현재 활성화 상태를 반대로 변경
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            Debug.LogWarning("No panel assigned!");
        }
    }
}
