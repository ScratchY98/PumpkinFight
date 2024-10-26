using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;
    private PauseMenu pauseMenu;
    [SerializeField] private string remoteLayerAndTagName = "RemotePlayer";
    [SerializeField] private string pauseMenuTag = "PauseMenu";

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
            gameObject.name = "RemotePlayer";
        }
        else
        {
            gameObject.name = "LocalPlayer";

            pauseMenu = GameObject.FindGameObjectWithTag(pauseMenuTag).GetComponent<PauseMenu>();
            pauseMenu.scriptToDisable = componentsToDisable;
        }
       
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerAndTagName);
        gameObject.tag = remoteLayerAndTagName;
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
}