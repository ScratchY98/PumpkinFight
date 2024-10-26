using UnityEngine;

public class MobileAttackInput : MonoBehaviour
{
    private PlayerShoot playerShoot;

    public void Shoot()
    {
        if (playerShoot == null)
            playerShoot = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerShoot>();

        playerShoot.Shoot();
    }
}
