using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public abstract class Interactable : MonoBehaviour
{
	[Header("Gaze Colors")]
	[SerializeField] protected Color inactiveColor = Color.white;
	[SerializeField] protected Color gazeColor = Color.yellow;
	
	[Header("Gaze Settings")]
	[SerializeField] private float maxGazeTime = 2f;
	private float elapsedGazeTime = 0f;
	private bool isGazing = false;

	protected MeshRenderer meshRenderer;

	protected virtual void Awake() {
	    meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.color = inactiveColor;
	}

    protected virtual void Update() {
        if (isGazing) {
            elapsedGazeTime += Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(inactiveColor, gazeColor, elapsedGazeTime / maxGazeTime);

            if (elapsedGazeTime >= maxGazeTime) {
                elapsedGazeTime = 0f;
                OnInteract(); 
                meshRenderer.material.color = inactiveColor;
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
    }

    private void StopGaze() {
        isGazing = false;
        elapsedGazeTime = 0f;
        meshRenderer.material.color = inactiveColor;
    }
    #endregion

    protected abstract void OnInteract();
}
