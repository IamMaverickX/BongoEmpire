using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingState
{
    public string id;
    public string buildingKey;
    public int level;
    public Vector3 position;
}

public class BuildingSystem : MonoBehaviour
{
    public List<BuildingData> buildingCatalog;
    public List<GameObject> placedBuildings = new List<GameObject>();
    public Transform buildingParent;
    public BuildingPrefabController buildingPrefab; // assign a simple prefab

    public List<BuildingState> SaveState = new List<BuildingState>();

    public event Action OnBuildingsChanged;

    public void Initialize()
    {
        Load();
        StartCoroutine(ProductionTick());
    }

    public void PlaceBuilding(BuildingData data, Vector3 position)
    {
        var prefab = Instantiate(buildingPrefab.gameObject, position, Quaternion.identity, buildingParent);
        var ctrl = prefab.GetComponent<BuildingPrefabController>();
        ctrl.Setup(data);
        placedBuildings.Add(prefab);
        // update resources or SaveState
        var state = new BuildingState {
            id = System.Guid.NewGuid().ToString(),
            buildingKey = data.name,
            level = 1,
            position = position
        };
        SaveState.Add(state);
        OnBuildingsChanged?.Invoke();
    }

    System.Collections.IEnumerator ProductionTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // 10 sec tick for MVP
            ProduceForAll(10f); // amount scaled by tick interval
        }
    }

    void ProduceForAll(float dt)
    {
        foreach (var obj in placedBuildings)
        {
            var ctrl = obj.GetComponent<BuildingPrefabController>();
            if (ctrl == null) continue;
            var data = ctrl.Data;
            var produced = data.baseProductionPerMinute * (dt / 60f);
            switch (data.produces)
            {
                case ResourceType.Engagement:
                    GameManager.Instance.ResourceSystem.AddEngagement((long)System.Math.Floor(produced));
                    break;
                case ResourceType.Hype:
                    GameManager.Instance.ResourceSystem.AddHype((long)System.Math.Floor(produced));
                    break;
                case ResourceType.BongoToken:
                    GameManager.Instance.ResourceSystem.AddBongoTokens((long)System.Math.Floor(produced));
                    break;
            }
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(new Serialization<List<BuildingState>>(SaveState));
        PlayerPrefs.SetString("building_state", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("building_state"))
        {
            string json = PlayerPrefs.GetString("building_state");
            var container = JsonUtility.FromJson<Serialization<List<BuildingState>>>(json);
            SaveState = container.target;
            // instantiate from save
            foreach (var s in SaveState)
            {
                var data = buildingCatalog.Find(b => b.name == s.buildingKey);
                if (data != null)
                {
                    Vector3 pos = s.position;
                    var prefab = Instantiate(buildingPrefab.gameObject, pos, Quaternion.identity, buildingParent);
                    var ctrl = prefab.GetComponent<BuildingPrefabController>();
                    ctrl.Setup(data);
                    placedBuildings.Add(prefab);
                }
            }
            OnBuildingsChanged?.Invoke();
        }
    }
}

// helper for serializing lists
[System.Serializable]
public class Serialization<T>
{
    public T target;
    public Serialization(T target) { this.target = target; }
}
