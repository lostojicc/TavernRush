using UnityEngine;

public class MugShelfInteractable : Interactable {
	[Header("Shelf")]
	[SerializeField] private Transform shelf;
	
	[Header("Mugs on this shelf")]
	[SerializeField] private Interactable[] mugs;

	private Vector3 mugPosition = new Vector3(0.2f, 0.11f, -0.25f);

	protected override void OnInteract() {
		var heldItem = PlayerInventory.Instance.GetHeldItem();

		if (PlayerInventory.Instance.HasItem) {
			PlayerInventory.Instance.DropCurrentItem();
			ReturnMugToShelf(heldItem);
		}
		else {
			for (int i = 0; i < mugs.Length; i++) {
                PlayerInventory.Instance.PickUp(mugs[i]);
				break;
            }
		}
	}

	private void ReturnMugToShelf(Interactable mug) {
		for (int i = 0; i < mugs.Length; i++) {
			if (mugs[i].gameObject == mug.gameObject) {
                mug.transform.SetParent(shelf);
                Vector3 pos = mugPosition;
                pos.x -= i * 0.25f;
                mug.transform.localPosition = pos;
                mug.transform.localRotation = Quaternion.identity;
                break;
            }		
		}
	}
}
