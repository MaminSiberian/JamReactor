using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Bullet Properties")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    [Header("Reload Properties")]
    public float cooldownFireInSeconds;
    public float visibleDistance;
    public bool isAttack { get; private set; }
    [Header("Rotate on Enemy Properties")]
    public float speedRotation;
    public float angleOffSet;
    [Header("Audio Properties")]
    public AudioClip[] attackShots;

    private Rigidbody2D _rb;
    private float timeFire;
    private AudioSource _as;
    private EnemyController _enemyController;
    private float _distance;
    private GameObject player;
    private Animator _anim;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        timeFire = cooldownFireInSeconds;
        _as = GetComponent<AudioSource>();
        _enemyController = GetComponent<EnemyController>();
        player = GameObject.FindWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    public void Rotate()
    {
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + angleOffSet;
        _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, Time.deltaTime * speedRotation);
    }
    public void Shoot()
    {
        var countAttack = UnityEngine.Random.Range(0, attackShots.Length - 1);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        _as.PlayOneShot(attackShots[countAttack]);
        _anim.SetTrigger("Attack");
         
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if(!_enemyController._iscatch)
                isAttack = true;
        }
    }
    private void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        if (_distance < visibleDistance)
        {
            
            if (!_enemyController._iscatch)
            {
                isAttack = true;
                Rotate();
                if (timeFire <= 0)
                {
                    Shoot();
                    timeFire = cooldownFireInSeconds;
                }
                else
                    timeFire -= Time.deltaTime;
            }
        }
        else
        {
            isAttack = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Timer());
            
        }
    }
    
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeFire);
        isAttack = false;
    }
}
