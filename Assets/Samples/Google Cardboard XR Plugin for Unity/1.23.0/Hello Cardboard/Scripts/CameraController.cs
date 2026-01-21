using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3.0f;

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

        // Negative mouseY is used because moving the mouse up gives a positive Y value,
        // but rotating the camera up requires a negative rotation around the X axis.
        transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0f);
#endif
    }
}
