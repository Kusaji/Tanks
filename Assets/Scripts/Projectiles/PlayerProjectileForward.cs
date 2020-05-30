using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileForward : MonoBehaviour
{
    public float projectileSpeed, projectileDamage;

    public delegate void DealDamageToTarget(GameObject targetHit, float damageDealt);
    public static event DealDamageToTarget DealDamage;

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
        if (!collision.gameObject.CompareTag("EnemyTank"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyTank"))
        {

            if (DealDamage != null)
            {
                DealDamage(collision.gameObject, projectileDamage);
            }

            Destroy(this.gameObject);
        }
    }
}
