using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public static PlayerStateController Instance;

	private ActionState currentActionState = ActionState.FreeMove;
	private LocationState currentLocationState = LocationState.OutsideBar;

	private void Awake() {
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public ActionState CurrentActionState => currentActionState;
	public LocationState CurrentLocationState => currentLocationState;

	public void SetActionState(ActionState state) {
		if (currentActionState == state) return;
		currentActionState = state;
	}
	
	public void SetLocationState(LocationState state) {
		if (currentLocationState == state) return;
		currentLocationState = state;
	}
}

