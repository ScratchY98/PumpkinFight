using Mirror;
using Mirror.SimpleWeb;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    private string ipAdress;
    private SimpleWebTransport transport;

    private void Start()
    {
        transport = NetworkManager.singleton.gameObject.GetComponent<SimpleWebTransport>();
    }

    public void StartClient()
    {
        if (!string.IsNullOrEmpty(ipAdress))
            NetworkManager.singleton.networkAddress = ipAdress;

        NetworkManager.singleton.StartClient();
    }


    public void ChangePort(string newPort)
    {
       transport.port = ushort.Parse(newPort);
    }

    public void ChangeiP(string newIp)
    {
        ipAdress = newIp;
    }
}
