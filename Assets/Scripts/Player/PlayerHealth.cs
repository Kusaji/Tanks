using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float playerCurrentHealth, playerMaxHealth;
    public bool playerIsAlive;

    public delegate void playerIsDead();
    public static event playerIsDead playerDied;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        ProjectileForward.DealDamage += DealDamage;
        playerDied += DestroyPlayer;
    }

    private void OnDestroy()
    {
        ProjectileForward.DealDamage -= DealDamage;
        playerDied -= DestroyPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth > 0)
        {
            playerIsAlive = true;
        } 
        else if (playerCurrentHealth <= 0 && playerIsAlive)
        {
            playerIsAlive = false;

            if (playerDied != null)
            {
                playerDied();
            }

        }
    }


    void DealDamage(float damage)
    {
        playerCurrentHealth -= damage;
    }

    void DestroyPlayer()
    {
        Destroy(this.gameObject, 1.0f);
    }

}
