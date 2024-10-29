using Unity.Netcode;
using UnityEngine;

public class ContinuousHorizontalRotation : NetworkBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도를 조절하는 변수

    void Update()
    {
        // 오브젝트를 X축을 기준으로 지속적으로 회전시킴
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
