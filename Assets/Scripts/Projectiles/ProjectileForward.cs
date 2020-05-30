using UnityEngine;

public class ProjectileForward : MonoBehaviour
{
    public float projectileSpeed, projectileDamage;

    public delegate void DealDamageToPlayer(float damageDealt);
    public static event DealDamageToPlayer DealDamage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");

            if (DealDamage != null)
            {
                DealDamage(projectileDamage);
            }

            Destroy(this.gameObject);
        }
    }
}
