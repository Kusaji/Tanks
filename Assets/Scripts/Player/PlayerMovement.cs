using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed, turnSpeed, MainFireRate;
    public GameObject turretBase, bulletSpawn, bulletPrefab;
    private Transform shoulder;
    public bool mainTurretReady;
    public bool _playerIsAlive;
    private Animator _anim;

    // Start is called before the first frame update
    void Awake()
    {
        _playerIsAlive = true;
        shoulder = this.transform;
        mainTurretReady = true;
        PlayerHealth.playerDied += PlayerIsDead;
        _anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        PlayerHealth.playerDied -= PlayerIsDead;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerIsAlive)
        {
            Movement();
            TurretRotation();
        }
    }

    void Movement()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (vertical > 0)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
            //Debug.Log("W is pressed");
        }

        if (vertical < 0)
        {
            transform.position -= transform.up * moveSpeed * Time.deltaTime;
            //Debug.Log("S is pressed");
        }

        if (horizontal < 0)
        {
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, turnSpeed * Time.deltaTime);
            //Debug.Log("D is pressed");
        }

        if (horizontal > 0)
        {
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, -turnSpeed * Time.deltaTime);
            //Debug.Log("A is pressed");
        }

        if (Input.GetMouseButtonDown(0) && mainTurretReady)
        {
            StartCoroutine(MainTurretShoot());
        }

    }

    void TurretRotation()
    {

        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mouseScreenPosition - transform.position).normalized;
        direction.z = 0.0f;

        turretBase.transform.up = direction;
    }

    IEnumerator MainTurretShoot()
    {
        mainTurretReady = false;
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        _anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(MainFireRate);
        mainTurretReady = true;
    }

    void PlayerIsDead()
    {
        _playerIsAlive = false;
    }


}
