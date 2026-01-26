using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public abstract class Interactable : MonoBehaviour
{
	[Header("Gaze Settings")]
	[SerializeField] private float maxGazeTime = 2f;
	private float elapsedGazeTime = 0f;
	private bool isGazing = false;

	protected MeshRenderer meshRenderer;

	protected virtual void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
	}

	protected virtual void Update() {
		if (isGazing) {
			elapsedGazeTime += Time.deltaTime;

			if (elapsedGazeTime >= maxGazeTime) {
				elapsedGazeTime = 0f;
				OnInteract(); 
				isGazing = false;
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
	}

	private void StopGaze() {
		isGazing = false;
        ReticleController.Instance.StopFill();
        PlayerStateController.Instance.SetActionState(ActionState.FreeMove);
		elapsedGazeTime = 0f;
	}
	#endregion

	protected abstract void OnInteract();
}
