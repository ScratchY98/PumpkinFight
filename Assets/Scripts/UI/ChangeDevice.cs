using UnityEngine;

public class ChangeDevice : MonoBehaviour
{
    public void ChangeDeviceOfPlayer(bool isMobile)
    {
        DeviceBasedUI.isMobile = isMobile;
    }
}
