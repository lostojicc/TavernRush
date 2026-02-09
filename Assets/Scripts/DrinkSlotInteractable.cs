using UnityEngine;

public class DrinkSlotInteractable : Interactable
{
    private MugInteractable mugInteractable;
    protected override void OnInteract() {
        var heldItem = PlayerInventory.Instance.GetHeldItem();
        Debug.Log("Cao");
        if (PlayerInventory.Instance.HasItem && !mugInteractable) {
            PlayerInventory.Instance.DropCurrentItem();
            heldItem.transform.position = transform.position;
            heldItem.transform.rotation = Quaternion.identity;
            mugInteractable = heldItem.GetComponent<MugInteractable>();
        } else if (!PlayerInventory.Instance.HasItem && mugInteractable) {
            PlayerInventory.Instance.PickUp(mugInteractable);
            mugInteractable = null;
        }
    }
}
