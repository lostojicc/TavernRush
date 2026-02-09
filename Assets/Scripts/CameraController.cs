using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float sensitivity = 3.0f;

	private float pitch;
	private float yaw;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
#if UNITY_EDITOR
		float mouseX = Input.GetAxis("Mouse X") * sensitivity;
		float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

		yaw += mouseX;
		pitch -= mouseY;

		pitch = Mathf.Clamp(pitch, -90f, 90f);
		transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
#endif
	}

	public float GetPitch() {
		return pitch;
	}
}
