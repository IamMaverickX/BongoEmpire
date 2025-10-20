using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public bool Connected = false;
    public string MockAddress = "0xBONGO_MOCK";

    public void ConnectMock()
    {
        Connected = true;
        Debug.Log("Mock wallet connected: " + MockAddress);
    }

    public void Disconnect()
    {
        Connected = false;
        Debug.Log("Mock wallet disconnected.");
    }
}
