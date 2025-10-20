using UnityEngine;
using UnityEngine.UI;

public class BuildingPrefabController : MonoBehaviour
{
    public BuildingData Data;
    public Image Icon;
    public TMPro.TextMeshProUGUI LevelText;

    public void Setup(BuildingData data)
    {
        Data = data;
        if (Icon != null && data.icon != null) Icon.sprite = data.icon;
        if (LevelText != null) LevelText.text = "Lv 1";
    }
}
