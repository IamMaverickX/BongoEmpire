using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveAll()
    {
        GameManager.Instance.ResourceSystem.Save();
        GameManager.Instance.BuildingSystem.Save();
        GameManager.Instance.MissionSystem.Save();
    }
}
