using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum MovementState {
	None,
	Walking,
	GoingUp,
	GoingDown
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class LookToWalk : MonoBehaviour
{
	[Header("Walking Settings")]
	[SerializeField] private float walkingSpeed = 3.0f;
	[SerializeField] private AudioClip walkingAudioEffect;
	[SerializeField] private float minimumWalkingAngleTreshold = 35.0f;
	[SerializeField] private float maximumWalkingAngleTreshold = 80.0f;

	[Header("Climbing Settings")]
	[SerializeField] private float climbingSpeed = 2.0f;
	[SerializeField] private float minimumClimbingAngleTreshold = 60f;
	[SerializeField] private AudioClip climbingAudioEffect;

	private CameraController cameraController;
	private Rigidbody rb;
	private MovementState movementState = MovementState.None;
	private AudioSource movementAudioSource;

	// Start is called before the first frame update
	void Start() {
		cameraController = Camera.main.GetComponent<CameraController>();
		rb = GetComponent<Rigidbody>();
		movementAudioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
		float cameraPitchAngle = cameraController.GetPitch();
		HandleAudio();

		if (PlayerStateController.Instance.CurrentActionState == ActionState.Interacting) {
			movementState = MovementState.None;
			return;
		}

		if (PlayerStateController.Instance.CurrentLocationState == LocationState.OutsideBar
			&& cameraPitchAngle >= minimumWalkingAngleTreshold
			&& cameraPitchAngle <= maximumWalkingAngleTreshold) {
			movementState = MovementState.Walking;
			return;
		}
		else if (PlayerStateController.Instance.CurrentLocationState == LocationState.OnLadder) {
			if (cameraPitchAngle <= -minimumClimbingAngleTreshold) {
				movementState = MovementState.GoingUp;
				return;
			}

			if (cameraPitchAngle >= minimumClimbingAngleTreshold) {
				movementState = MovementState.GoingDown;
				return;
			}

			movementState = MovementState.None;
			return;
		}

		movementState = MovementState.None;
	}

	private void HandleAudio() {
		if (movementState == MovementState.None) {
			if (movementAudioSource.isPlaying) 
				movementAudioSource.Stop();
			return;
		}

		movementAudioSource.clip = movementState == MovementState.Walking ? walkingAudioEffect : climbingAudioEffect;
		if (!movementAudioSource.isPlaying) 
			movementAudioSource.Play();
	}

	private void FixedUpdate() {
		rb.useGravity = PlayerStateController.Instance.CurrentLocationState != LocationState.OnLadder;
		if (movementState != MovementState.None)
			MovePlayer();
	}

	private void MovePlayer() {
		Vector3 direction;
		switch (movementState) {
			case MovementState.GoingUp:
				direction = Vector3.up;
				rb.MovePosition(rb.position + climbingSpeed * Time.fixedDeltaTime * direction);
				TutorialManager.Instance.NotifyStepComplete("Ladder");
				break;

			case MovementState.GoingDown:
				direction = Vector3.down;
				rb.MovePosition(rb.position + climbingSpeed * Time.fixedDeltaTime * direction);
                TutorialManager.Instance.NotifyStepComplete("Ladder");
                break;

			default:
				Vector3 forward = cameraController.transform.forward;
				// Keep player (and its head) at the same height
				forward.y = 0f;
				forward.Normalize();
				rb.MovePosition(rb.position + Time.fixedDeltaTime * walkingSpeed * forward);
                TutorialManager.Instance.NotifyStepComplete("Walk");
                break;
		}

		// Normalization ensures predictable consistent speed independent of direction (avoids length's dependency on rotation and floating-point error)


		//Vector3 movementVector = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
		//transform.Translate(Time.deltaTime * walkingSpeed * movementVector.normalized);
	}
}
