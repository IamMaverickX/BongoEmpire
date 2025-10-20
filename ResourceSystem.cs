using System;
using UnityEngine;

[Serializable]
public class ResourceState {
    public long engagementPoints;
    public long hype;
    public long bongoTokens;
}

public class ResourceSystem : MonoBehaviour
{
    public ResourceState State = new ResourceState();
    public event Action OnResourcesChanged;

    public void Initialize()
    {
        Load();
        OnResourcesChanged?.Invoke();
    }

    public void AddEngagement(long amount)
    {
        State.engagementPoints += amount;
        OnResourcesChanged?.Invoke();
    }

    public void AddHype(long amount)
    {
        State.hype += amount;
        OnResourcesChanged?.Invoke();
    }

    public bool SpendHype(long amount)
    {
        if (State.hype < amount) return false;
        State.hype -= amount;
        OnResourcesChanged?.Invoke();
        return true;
    }

    public void AddBongoTokens(long amount)
    {
        State.bongoTokens += amount;
        OnResourcesChanged?.Invoke();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(State);
        PlayerPrefs.SetString("resource_state", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("resource_state"))
        {
            string json = PlayerPrefs.GetString("resource_state");
            State = JsonUtility.FromJson<ResourceState>(json);
        }
        else
        {
            State = new ResourceState { engagementPoints = 0, hype = 10, bongoTokens = 0 };
        }
    }
}
