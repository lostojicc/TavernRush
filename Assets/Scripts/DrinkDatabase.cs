using UnityEngine;
using System.Collections.Generic;

public class DrinkDatabase : MonoBehaviour {
    public static DrinkDatabase Instance;

    [SerializeField] private DrinkDefinition[] drinks;

    private Dictionary<DrinkType, DrinkDefinition> lookup;

    private void Awake() {
        Instance = this;

        lookup = new Dictionary<DrinkType, DrinkDefinition>();
        foreach (var drink in drinks)
            lookup[drink.type] = drink;
    }

    public DrinkDefinition Get(DrinkType type) {
        lookup.TryGetValue(type, out var drink);
        return drink;
    }
}
