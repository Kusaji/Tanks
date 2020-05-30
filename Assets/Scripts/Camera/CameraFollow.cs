using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float followSpeed;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (_player.gameObject != null)
        {

            var targetPos = _player.transform.position;

            targetPos.z = -10.0f;

            transform.position = Vector3.Slerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        }
    }
}
