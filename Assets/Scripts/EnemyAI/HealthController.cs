using UnityEngine;

public class HealthController : MonoBehaviour
{

    public float currentHealth, maxHealth;

    public delegate void EnemyDeath(GameObject thisEnemy);
    public static event EnemyDeath EnemyDied;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        PlayerProjectileForward.DealDamage += DealDamage;
    }

    private void OnDestroy()
    {
        PlayerProjectileForward.DealDamage -= DealDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {

            if (EnemyDied != null)
            {
                EnemyDied(this.gameObject);
            }

            Destroy(this.gameObject);
        }
    }


    void DealDamage(GameObject targetHit, float damage)
    {
        if (targetHit == this.gameObject)
        {
            currentHealth -= damage;
        }
    }

}
