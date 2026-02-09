using UnityEngine;

public class BucketInteractable : Interactable
{
    protected override void OnInteract() {
        PlayerInventory.Instance.GetHeldItem()?.GetComponent<MugInteractable>().Empty();
    }
}
