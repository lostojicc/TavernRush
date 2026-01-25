using UnityEngine;

public class ReticleController : MonoBehaviour {
	public static ReticleController Instance;

	[SerializeField] private MeshRenderer reticleRenderer;

	private Material reticleMat;
	private float maxTime;
	private float timer;
	private bool filling;

	void Awake() {
		Instance = this;
		reticleMat = reticleRenderer.material;
		reticleMat.SetFloat("_Fill", 0f);
	}

	void Update() {
		if (!filling) return;

		timer += Time.deltaTime;
		reticleMat.SetFloat("_Fill", timer / maxTime);
	}

	public void StartFill(float duration) {
		maxTime = duration;
		timer = 0f;
		filling = true;
		reticleMat.SetFloat("_Fill", 0f);
	}

	public void StopFill() {
		filling = false;
		reticleMat.SetFloat("_Fill", 0f);
	}
}
