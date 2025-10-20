using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI EngagementText;
    public TextMeshProUGUI HypeText;
    public TextMeshProUGUI TokensText;

    void OnEnable()
    {
        if (GameManager.Instance != null && GameManager.Instance.ResourceSystem != null)
            GameManager.Instance.ResourceSystem.OnResourcesChanged += RefreshAll;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null && GameManager.Instance.ResourceSystem != null)
            GameManager.Instance.ResourceSystem.OnResourcesChanged -= RefreshAll;
    }

    public void RefreshAll()
    {
        var r = GameManager.Instance.ResourceSystem.State;
        if (EngagementText) EngagementText.text = $"Engagement: {r.engagementPoints}";
        if (HypeText) HypeText.text = $"Hype: {r.hype}";
        if (TokensText) TokensText.text = $"Bongo: {r.bongoTokens}";
    }

    // button hook
    public void OnPlaceBuildingButton(int buildingIndex)
    {
        var building = GameManager.Instance.BuildingSystem.buildingCatalog[buildingIndex];
        // place at random near origin for MVP
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-3f,3f), UnityEngine.Random.Range(-2f,2f), 0f);
        GameManager.Instance.BuildingSystem.PlaceBuilding(building, pos);
        RefreshAll();
    }
}
