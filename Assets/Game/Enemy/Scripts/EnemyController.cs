using UnityEngine;

public class EnemyController : MonoBehaviour, ICanCatching
{
    private GameObject player;
    public float speed;
    public float speedAngle;
    public float visibleDistance;
    public Transform pivot;

    private float _distance;
    private float angle;
    private bool isAttack;
    public bool _iscatch;
    public bool isFall;
    public bool isOn;
    public GameObject boomDeadParticles;
    private Rigidbody2D _rb;


    private void Start()
    {
        isFall = false;
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (!_iscatch && !isFall && isOn)
        {
            _distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            angle += speedAngle * Time.deltaTime;

            pivot.rotation = Quaternion.Euler(0, 0, angle);
            if (_distance < visibleDistance)
            {
                isAttack = true;

                transform.position = Vector2.MoveTowards(transform.position,
                    player.transform.position,
                    speed * Time.deltaTime);
            }
            else
                isAttack = false;
        }

    }
    public bool GetIsAttack()
    {
        return isAttack;
    }
    public void CatchOn()
    {
        _iscatch = true;
    }
    public void CatchOff()
    {
        _iscatch = false;
    }
    public void ChangeFall(bool value)
    {

        isFall = value;
        if (_rb != null)
            if (isFall)
            {
                _rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
                _rb.bodyType = RigidbodyType2D.Kinematic;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFall && collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(boomDeadParticles, collision.transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Instantiate(boomDeadParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (isFall && collision.gameObject.CompareTag("Thorn"))
        {
            Instantiate(boomDeadParticles, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
    public void Death()
    {
        Instantiate(boomDeadParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
