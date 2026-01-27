using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public abstract class Interactable : MonoBehaviour
{
	[Header("Gaze Settings")]
	[SerializeField] private float maxGazeTime = 2f;
	[SerializeField] private AudioSource feedbackAudioSource;
	[SerializeField] private AudioClip outOfRangeSoundEffect;

    [Header("Gaze label")]
    [SerializeField] private Transform label;

	[Header("Player")]
	[SerializeField] protected Transform player;

    private float elapsedGazeTime = 0f;
	private bool isGazing = false;
	protected virtual bool ShouldIgnoreDistance => false;

	protected MeshRenderer meshRenderer;

	protected virtual void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
	}

	protected virtual void Update() {
		if (isGazing) {
			elapsedGazeTime += Time.deltaTime;

			if (elapsedGazeTime >= maxGazeTime) {
				elapsedGazeTime = 0f; 
				isGazing = false;
                if (Vector3.Distance(player.position, transform.position) > 2
					&& player.position.y <= 5
					&& !ShouldIgnoreDistance
					&& PlayerStateController.Instance.CurrentLocationState != LocationState.InsideBar) {
					if (feedbackAudioSource.isPlaying)
						feedbackAudioSource.Stop();
                    feedbackAudioSource.clip = outOfRangeSoundEffect;
                    feedbackAudioSource.Play();
					return;
				}
                TutorialManager.Instance.NotifyStepComplete("Interact");
                OnInteract();
            }
		}
	}

	#region Gaze Methods
	public void OnPointerEnter() {
		StartGaze();
	}

	public void OnPointerExit() {
		StopGaze();
	}

	private void StartGaze() {
		isGazing = true;
        ReticleController.Instance.StartFill(maxGazeTime);
        PlayerStateController.Instance.SetActionState(ActionState.Interacting);
        label.transform.position = transform.position + Vector3.up * 0.5f - label.forward * 0.5f;
		label.gameObject.GetComponentInChildren<TMP_Text>().text = transform.root.name;
		label.gameObject.SetActive(true);
    }

	private void StopGaze() {
		isGazing = false;
        ReticleController.Instance.StopFill();
        PlayerStateController.Instance.SetActionState(ActionState.FreeMove);
		elapsedGazeTime = 0f;
        label.gameObject.SetActive(false);
    }
	#endregion

	protected abstract void OnInteract();
}
