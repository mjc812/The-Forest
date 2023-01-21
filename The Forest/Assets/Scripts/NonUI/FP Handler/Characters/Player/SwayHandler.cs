using UnityEngine;

public class SwayHandler : MonoBehaviour
{
    private float swaySpeed = 4f;
    private float intensity = 20f;

    void Update()
    {
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");

        Quaternion xRotation = Quaternion.AngleAxis(yMouse * intensity, Vector3.right);
        Quaternion yRotation = Quaternion.AngleAxis(xMouse * intensity, Vector3.up);
        Quaternion targetRotation = xRotation * yRotation;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, swaySpeed * Time.deltaTime);
    }
}
