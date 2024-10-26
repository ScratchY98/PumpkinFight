using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class FindLocalIP : MonoBehaviour
{
    [SerializeField] private string originalText;
    [SerializeField] private Text ipText;

    private void Start()
    {
        UpdateIPText();
    }

    private void UpdateIPText()
    {
        string localIp = GetLocalIPAddress();
        ipText.text = originalText + localIp;
    }

    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string localIp = null;

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIp = ip.ToString();
                break;
            }
        }

        if (localIp == null)
            return ("Erreur : aucune adresse IP locale trouvée.");

        TcpListener listener = new TcpListener(IPAddress.Any, 0);
        listener.Start();

        return (localIp);
    }
}

