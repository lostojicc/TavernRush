using UnityEditor;
using UnityEngine;

public class DoorInteractable : Interactable {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override bool ShouldIgnoreDistance => true;
    protected override void OnInteract() {
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
