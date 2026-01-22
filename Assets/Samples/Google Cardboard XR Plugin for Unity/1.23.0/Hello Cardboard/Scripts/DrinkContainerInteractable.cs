using UnityEngine;

public class DrinkContainerInteractable : Interactable
{
    [SerializeField] private DrinkType drinkType;
    protected override void OnInteract() {
        var held = PlayerInventory.Instance.GetHeldItem();
        if (!held) return;

        var mug = held.GetComponent<MugInteractable>();
        if (!mug) return;

        mug.Fill(drinkType);
    }

}
