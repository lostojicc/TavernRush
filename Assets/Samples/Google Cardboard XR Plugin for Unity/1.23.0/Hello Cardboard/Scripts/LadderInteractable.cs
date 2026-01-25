using Unity.XR.CoreUtils;
using UnityEngine;

public class LadderInteractable : Interactable {
    [SerializeField] private LadderInteractable ladder;

    protected override void OnInteract() {
        PlayerStateController.Instance.SetLocationState(LocationState.OnLadder);
        PlayerStateController.Instance.SetActionState(ActionState.FreeMove);
        ladder.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
}
