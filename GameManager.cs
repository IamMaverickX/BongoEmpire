using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Systems")]
    public ResourceSystem ResourceSystem;
    public BuildingSystem BuildingSystem;
    public MissionSystem MissionSystem;
    public UIManager UIManager;
    public WalletManager WalletManager;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ResourceSystem.Initialize();
        BuildingSystem.Initialize();
        MissionSystem.Initialize();
        UIManager.RefreshAll();
    }

    void OnApplicationQuit()
    {
        ResourceSystem.Save();
        BuildingSystem.Save();
    }
}
