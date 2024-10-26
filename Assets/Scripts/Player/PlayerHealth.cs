using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdateHealth))]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider slider;
    private Vector3 spawnPoint;
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
        spawnPoint = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { RestartScene(); }
    }

    public void TakeDamage(int amount)
    {
        health = Mathf.Max(health - amount, 0);

        if (health == 0)
            RestartScene();
    }

    private void UpdateHealth(int oldHealth, int newHealth)
    {
        slider.value = newHealth;
        health = newHealth;
    }


    public void RestartScene()
    {
        RpcRestartScene();
    }

    private void RpcRestartScene()
    {
        health = maxHealth;
        GetComponent<PlayerShoot>().RestartGame();
        transform.position = spawnPoint;
    }
}
