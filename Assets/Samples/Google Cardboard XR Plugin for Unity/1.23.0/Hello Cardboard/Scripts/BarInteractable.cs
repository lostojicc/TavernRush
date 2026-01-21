using UnityEngine;

public class BarInteractable : Interactable {
    [SerializeField] private GameObject player;

    protected override void OnInteract() {
        Vector3 newPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.transform.position = newPos;
    }
}
