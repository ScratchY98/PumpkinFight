using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerShoot : NetworkBehaviour
{
    // Component's Reference
    private PlayerController playerController;
    private ProjectileScriptableObject projectileSO;

    [Header ("Projectile's settings")]
    [SerializeField] private float projectileLiveTime = 6f;
    [SerializeField] private GameObject enemy;

    [Header ("Cooldown:")]
    [SyncVar(hook = nameof(UpdateCooldown))]
    [SerializeField] private float cooldown;

    [Header("UI:")]
    [SerializeField] private Slider playerCooldownUI;

    [SerializeField] private string preshotUIMobileTag = "PreshotUIMobile";
    private Image preshotUIMobile;

    [Header("Inputs:")]
    [SerializeField] private InputActionProperty shootInputSource;

    [Header("Graphics:")]
    [SerializeField] private Transform graphics;


    [Header("Animator Settings")]
    [SyncVar(hook = nameof(OnSkinChanged))]
    private int skinID;
    [SerializeField] private Animator animator;
    [SerializeField] private List<RuntimeAnimatorController> animatorSkins;

    private ProjectileManager projectileManager;

    private void Awake()
    {
        projectileManager = FindObjectOfType<ProjectileManager>();
        playerController = GetComponent<PlayerController>();
        projectileSO = SaveProjectileData.projectileScriptableObject;

        graphics.localPosition += projectileSO.graphicsOffset;

        if (DeviceBasedUI.isMobile)
        {
            // Set the image of the preshot button in the mobile UI.
            preshotUIMobile = GameObject.FindGameObjectWithTag(preshotUIMobileTag).GetComponent<Image>();
            preshotUIMobile.sprite = projectileSO.projectileSprite;
        }
    }

    #region Animations

    [Command]
    private void CmdSetSkinID(int id)
    {
        skinID = id;
    }

    private void OnSkinChanged(int oldID, int newID)
    {
        Debug.Log(newID);
        if (newID >= 0 && newID < animatorSkins.Count)
            animator.runtimeAnimatorController = animatorSkins[newID];
    }

    #endregion

    void Start()
    {
        cooldown = projectileSO.maxCooldown;
        CmdSetSkinID(projectileSO.animatorId);
    }

    void Update()
    {
        if (shootInputSource.action.WasReleasedThisFrame())
            Shoot();

        if (cooldown < projectileSO.maxCooldown)
            cooldown = Mathf.Min(cooldown + projectileSO.cooldownAdd * Time.deltaTime, projectileSO.maxCooldown);
    }


    private void UpdateCooldown(float oldCooldown, float newCoolldown)
    {
        cooldown = newCoolldown;
        playerCooldownUI.value = newCoolldown / projectileSO.maxCooldown;
    }


    public void Shoot()
    {
        if (cooldown < projectileSO.maxCooldown)
            return;

        Vector2 projectileDirection = (playerController.movement == Vector2.zero) ? playerController.lastDirection.normalized : playerController.movement.normalized;

        if (enemy == null)
            enemy = GameObject.FindGameObjectWithTag("RemotePlayer");

        cooldown = 0;

        CmdShoot(projectileDirection, enemy, projectileSO.projectilePrefabId);
    }



    [Command]
    private void CmdShoot(Vector2 direction, GameObject fireballEnemy, int projectileID)
    {
        GameObject prefab = projectileManager.GetProjectilePrefab(projectileID);
        if (prefab != null)
        {
            GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            Projectile projectileBehaviour = projectile.GetComponent<Projectile>();
            projectileBehaviour.LaunchProjectile(direction, fireballEnemy);

            NetworkServer.Spawn(projectile);
            Destroy(projectile, projectileLiveTime);
        }
        else
            Debug.LogError("Projectile prefab introuvable pour l'ID : " + projectileID);
    }

    public void RestartGame()
    {
        cooldown = projectileSO.maxCooldown;
    }
}