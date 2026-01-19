using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        pitch -= mouseY * sensitivity;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        float yaw = transform.eulerAngles.y + mouseX * sensitivity;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
#endif
    }
}
