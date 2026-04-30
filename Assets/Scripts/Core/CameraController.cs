using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        Vector3 move = new Vector3(
            Input.GetAxisRaw("Horizontal"), // A / D
            Input.GetAxisRaw("Vertical"),   // W / S
            0f
        ).normalized;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}