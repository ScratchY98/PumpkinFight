using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceBasedUI : MonoBehaviour
{
    [SerializeField] private GameObject[] mobileControlsUI;
    [SerializeField] private Joystick sceneJoystick;
    public static Joystick joystick;
    public static bool isMobile = false;

    private void Awake()
    {
        joystick = sceneJoystick;
        UpdateUIBasedOnDevice();
    }

    private void UpdateUIBasedOnDevice()
    {
        if (!IsWebGL.isWebGL)
        {
            isMobile = false;

            foreach (InputDevice device in InputSystem.devices)
            {
                if (device is Touchscreen)
                {
                    isMobile = true;
                    break;
                }
            }
        }

        for (int i = 0; i < mobileControlsUI.Length; i++) {
            mobileControlsUI[i].SetActive(isMobile);
        }
    }
}
