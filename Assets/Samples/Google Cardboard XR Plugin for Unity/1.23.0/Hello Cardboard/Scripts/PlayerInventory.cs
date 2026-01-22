using UnityEngine;

public class PlayerInventory : MonoBehaviour {
	public static PlayerInventory Instance;

	[Header("Hand Transform")]
	[SerializeField] private Transform handSlot;

	private Interactable heldItem;

	private void Awake() {
		// Singleton for easy access
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public bool HasItem => heldItem != null;

	public void PickUp(Interactable item) {
		heldItem = item;

        item.transform.SetParent(handSlot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

	public void DropCurrentItem() {
		if (HasItem) {
            heldItem.transform.SetParent(null);
			heldItem = null;
        }
	}

	public Interactable GetHeldItem() { return heldItem; }
}
