using UnityEngine;

public class BarInteractable : Interactable {
    [SerializeField] private GameObject player;

    protected override void OnInteract() {
        LocationState currentState = PlayerStateController.Instance.CurrentLocationState;
        Vector3 newPos = currentState == LocationState.OutsideBar ? 
            new Vector3(6.0f, player.transform.position.y, -1.0f) :
            new Vector3(5.0f, player.transform.position.y, -5.0f);
        LocationState newState = currentState == LocationState.OutsideBar ? LocationState.InsideBar : LocationState.OutsideBar;
        PlayerStateController.Instance.SetLocationState(newState);
        player.transform.position = newPos;
    }
}
