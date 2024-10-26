using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [System.Serializable]
    public class ProjectileEntry
    {
        public int id;
        public GameObject prefab;
    }

    public List<ProjectileEntry> projectiles = new List<ProjectileEntry>();

    private Dictionary<int, GameObject> projectilePrefabs;

    private void Awake()
    {
        projectilePrefabs = new Dictionary<int, GameObject>();

        foreach (var entry in projectiles)
        {
            if (entry.prefab != null && !projectilePrefabs.ContainsKey(entry.id))
                projectilePrefabs.Add(entry.id, entry.prefab);
            else
                Debug.LogWarning($"Projectile ID {entry.id} est dupliqué ou le prefab est manquant.");
        }
    }

    public GameObject GetProjectilePrefab(int id)
    {
        projectilePrefabs.TryGetValue(id, out GameObject prefab);
        return prefab;
    }
}
