using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "BongoEmpire/BuildingData", order = 1)]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public Sprite icon;
    public int baseCost = 10;
    public float baseProductionPerMinute = 1f;
    public ResourceType produces = ResourceType.Engagement;
}

public enum ResourceType
{
    Engagement,
    Hype,
    BongoToken
}
