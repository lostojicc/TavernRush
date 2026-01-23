using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class LookToWalk : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 3.0f;
    [SerializeField] private AudioClip walkingAudioEffect;

    [SerializeField] private float minimumAngleTreshold = 35.0f;
    [SerializeField] private float maximumAngleTreshold = 90.0f;

    private Camera mainCamera;
    private Rigidbody rb;
    private bool isWalking = false;
    private AudioSource walkingAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        walkingAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraPitchAngle = GetCameraPitchAngle();

        isWalking = mainCamera.transform.eulerAngles.x >= minimumAngleTreshold
            && mainCamera.transform.eulerAngles.x <= maximumAngleTreshold 
            && PlayerStateController.Instance.CurrentActionState == ActionState.FreeMove
            && PlayerStateController.Instance.CurrentLocationState == LocationState.OutsideBar;

        HandleAudio();
    }

    private float GetCameraPitchAngle()
    {
        // Local Euler angles = “head tilt relative to the body” - Using eulerAngles in world space could break the angle detection if the parent rotates
        float cameraPitchAngle = mainCamera.transform.localEulerAngles.x;
        //if (cameraPitchAngle > 180f) cameraPitchAngle -= 360f; // convert to -180..180
        return cameraPitchAngle;
    }

    private void HandleAudio()
    {
        if (isWalking)
        {
            if (!walkingAudioSource.isPlaying)
                walkingAudioSource.Play();
        }
        else
        {
            if (walkingAudioSource.isPlaying)
                walkingAudioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3 forward = mainCamera.transform.forward;
        // Keep player (and its head) at the same height
        forward.y = 0f;
        // Normalization ensures predictable consistent speed independent of direction (avoids length's dependency on rotation and floating-point error)
        forward.Normalize();

        Vector3 newPosition = rb.position + Time.fixedDeltaTime * walkingSpeed * forward;
        rb.MovePosition(newPosition);

        //Vector3 movementVector = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        //transform.Translate(Time.deltaTime * walkingSpeed * movementVector.normalized);
    }
}
