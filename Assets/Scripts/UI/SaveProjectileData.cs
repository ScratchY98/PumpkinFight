using UnityEngine;
using UnityEngine.UI;

public class SaveProjectileData : MonoBehaviour
{
    public ProjectileScriptableObject[] projectileScriptableObjects;
    public static ProjectileScriptableObject projectileScriptableObject;
    [SerializeField] private Dropdown projectileDropdown;

    private void Start()
    {
        int i =  PlayerPrefs.GetInt("projectileScriptableObject", 10);

        if (i == 10)
        {
            projectileScriptableObject = projectileScriptableObjects[0];
            return;
        }

        projectileScriptableObject = projectileScriptableObjects[i];
        projectileDropdown.value = i;
    }

    // Use a dropdown
    public void ChangeProjectile(int i)
    {
        projectileScriptableObject = projectileScriptableObjects[i];
        PlayerPrefs.SetInt("projectileScriptableObject", i);
    }
}
