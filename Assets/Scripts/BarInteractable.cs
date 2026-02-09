using UnityEngine;

public class BarInteractable : Interactable {
    protected override void OnInteract() {
        LocationState currentState = PlayerStateController.Instance.CurrentLocationState;
        Vector3 newPos = currentState == LocationState.OutsideBar ? 
            new Vector3(6.0f, player.position.y, -1.0f) :
            new Vector3(5.0f, player.position.y, -5.0f);
        LocationState newState = currentState == LocationState.OutsideBar ? LocationState.InsideBar : LocationState.OutsideBar;
        PlayerStateController.Instance.SetLocationState(newState);
        player.position = newPos;
    }
}
