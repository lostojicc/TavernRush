using System.Linq;
using UnityEngine;

public class MugShelfInteractable : Interactable {
	[Header("Shelf")]
	[SerializeField] private Transform shelf;
	
	[Header("Mugs on this shelf")]
	[SerializeField] private MugInteractable[] mugs;

	private Vector3 mugPosition = new Vector3(0.2f, 0.11f, -0.25f);

    private void Start() {
		foreach (var mug in mugs)
			mug.Initialize(this);
    }

    protected override void OnInteract() {
		var heldItem = PlayerInventory.Instance.GetHeldItem();

		if (PlayerInventory.Instance.HasItem) {
			if (heldItem.GetComponent<MugInteractable>().HomeShelf != this) return;
			PlayerInventory.Instance.DropCurrentItem();
			ReturnMugToShelf(heldItem);
		}
		else {
			for (int i = 0; i < mugs.Length; i++) {
				var mug = mugs[i];
				if (!mug.IsOnShelf) continue;
                PlayerInventory.Instance.PickUp(mug);
				mug.IsOnShelf = false;
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
				mug.GetComponent<MugInteractable>().IsOnShelf = true;
                break;
            }		
		}
	}
}
