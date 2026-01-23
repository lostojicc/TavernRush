using System.IO;
using UnityEngine;

public class MugInteractable : Interactable
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	[Header("Mug State")]
	[SerializeField] private DrinkType currentDrink = DrinkType.None;
	[SerializeField] private MeshRenderer liquidRenderer;
    
    public MugShelfInteractable HomeShelf { get; private set; }
	public bool IsEmpty => currentDrink == DrinkType.None;
	public DrinkType CurrentDrink => currentDrink;

    protected override void Awake() {
        liquidRenderer.enabled = false;
    }

    public void Initialize(MugShelfInteractable shelf) {
        HomeShelf = shelf;
    }

    public bool Fill(DrinkType drink) {
        if (!IsEmpty)
            return false;

        currentDrink = drink;

        var data = DrinkDatabase.Instance.Get(drink);
        liquidRenderer.material.color = data.color;
        liquidRenderer.enabled = true;

        return true;
    }

	public void Empty() {
		currentDrink = DrinkType.None;
		Debug.Log("Mug emptied");
	}

    protected override void OnInteract() {
        throw new System.NotImplementedException();
    }
}
