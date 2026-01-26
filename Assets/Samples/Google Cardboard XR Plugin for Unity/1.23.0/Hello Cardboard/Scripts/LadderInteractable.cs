using Unity.XR.CoreUtils;
using UnityEngine;

public class LadderInteractable : Interactable {
	[Header("Ladder settings")]
	[SerializeField] private float attachOffset = 0.5f;

	[Header("Player")]
	[SerializeField] private Transform player;

	private BoxCollider collider;

	private Vector3 bottomPoint, topPoint, centerPoint;

	protected override void Awake() {
		base.Awake();
		collider = GetComponent<BoxCollider>();

		centerPoint = collider.bounds.center;
		float height = collider.size.x * 0.4f * transform.lossyScale.x;

		bottomPoint = centerPoint + transform.right * height;
		topPoint = centerPoint - transform.right * height;
	}

    private void FixedUpdate() {
		Debug.Log(player.position.y);
		if (PlayerStateController.Instance.CurrentLocationState != LocationState.OnLadder) return;
		
		HandleDetach();
	}

	protected override void OnInteract() {
		Attach();
	}

	private void Attach() {
        PlayerStateController.Instance.SetLocationState(LocationState.OnLadder);
        PlayerStateController.Instance.SetActionState(ActionState.FreeMove);
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

		Vector3 attachPosition = centerPoint - transform.forward * attachOffset;
		player.position = attachPosition;
	}

    private void HandleDetach() {
		if (player.position.y >= bottomPoint.y && player.position.y <= topPoint.y) return;
		
		if (player.position.y >= topPoint.y) 
			player.position += 3 * attachOffset * transform.up;

		PlayerStateController.Instance.SetLocationState(LocationState.OutsideBar);
		gameObject.layer = LayerMask.NameToLayer("Interactive");
	}
}
