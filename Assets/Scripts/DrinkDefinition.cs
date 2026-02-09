using UnityEngine;

[CreateAssetMenu(fileName = "DrinkDefinition", menuName = "Scriptable Objects/DrinkDefinition")]
public class DrinkDefinition : ScriptableObject
{
	public DrinkType type;
	public Color color;
	public string displayName;
}
