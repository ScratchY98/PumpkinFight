using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/SpawnProjectile", order = 1)]
public class ProjectileScriptableObject : ScriptableObject
{
    public int projectilePrefabId;
    public int animatorId;
    public Sprite projectileSprite;

    public float maxCooldown = 100f;
    public float cooldownAdd = 85;

    public RuntimeAnimatorController animator;
    public Vector3 graphicsOffset;
}