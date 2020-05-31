using System.Collections;
using UnityEngine;

public class EnemyTurretTrack : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float distanceToPlayer;

    public float aggroRange, trackingSpeed, fireRate;

    [SerializeField]
    private bool isTracking, playerInSights;

    public GameObject bulletSpawn, bulletPrefab;

    public Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(TrackPlayer());
        StartCoroutine(FireAtPlayer());

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.gameObject != null)
        {
            TurretTrack();
        }
    }


    IEnumerator TrackPlayer()
    {
        while (gameObject && _player.gameObject != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer > aggroRange)
            {
                isTracking = false;
            }
            else
            {
                isTracking = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void TurretTrack()
    {
        if (isTracking)
        {
            //non slerped, precise auto tracking
            //transform.up = _player.transform.position;

            Vector3 dir = _player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), trackingSpeed * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.transform.position, transform.up, 100.0f);

            if (hit.collider.CompareTag("Player"))
            {
                playerInSights = true;               
            } 
            else if (!hit.collider.CompareTag("Player"))
            {
                playerInSights = false;
            }


        }
    }

    IEnumerator FireAtPlayer()
    {
        while (gameObject)
        {
            while (playerInSights)
            {
                Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                _anim.SetTrigger("Shoot");
                yield return new WaitForSeconds(fireRate);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
