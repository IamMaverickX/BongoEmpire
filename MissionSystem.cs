using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "BongoEmpire/MissionData", order = 2)]
public class MissionData : ScriptableObject
{
    public string missionId;
    public string title;
    public string description;
    public long requirementEngagement;
    public int rewardHype;
    public bool repeatable;
}

public class MissionState
{
    public string missionId;
    public bool completed;
}

public class MissionSystem : MonoBehaviour
{
    public List<MissionData> missionCatalog;
    public List<MissionState> missionStates = new List<MissionState>();

    public event Action OnMissionsChanged;

    public void Initialize()
    {
        Load();
        // ensure default mission exists
        if (missionCatalog.Count == 0)
        {
            Debug.Log("No mission data found in MissionSystem. Add missions to the missionCatalog.");
        }
    }

    public void CheckMissions()
    {
        foreach (var m in missionCatalog)
        {
            var state = missionStates.Find(s => s.missionId == m.missionId);
            if (state == null)
            {
                state = new MissionState { missionId = m.missionId, completed = false };
                missionStates.Add(state);
            }
            if (!state.completed && GameManager.Instance.ResourceSystem.State.engagementPoints >= m.requirementEngagement)
            {
                CompleteMission(m, state);
            }
        }
        OnMissionsChanged?.Invoke();
    }

    void CompleteMission(MissionData data, MissionState state)
    {
        state.completed = true;
        GameManager.Instance.ResourceSystem.AddHype(data.rewardHype);
        // add other rewards
        Debug.Log($"Mission complete: {data.title}");
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(new Serialization<List<MissionState>>(missionStates));
        PlayerPrefs.SetString("mission_state", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("mission_state"))
        {
            string json = PlayerPrefs.GetString("mission_state");
            var container = JsonUtility.FromJson<Serialization<List<MissionState>>>(json);
            missionStates = container.target;
        }
    }
}
