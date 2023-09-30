using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Bullet Properties")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;
    [Header("Reload Properties")]
    public float cooldownFireInSeconds;
    public bool isAttack { get; private set; }
    [Header("Rotate on Enemy Properties")]
    public float speedRotation;
    [Header("Audio Properties")]
    public AudioClip[] attackShots;

    private Rigidbody2D _rb;
    private float timeFire;
    private AudioSource _as;
    private bool _isCatch;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        timeFire = cooldownFireInSeconds;
        _as = GetComponent<AudioSource>();
    }

    public void Rotate(Collider2D collision)
    {
        Vector2 lookDir = collision.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, Time.deltaTime * speedRotation);
    }
    public void Shoot()
    {
        var countAttack = UnityEngine.Random.Range(0, attackShots.Length - 1);
            GameObject prefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D _rb2d = prefab.GetComponent<Rigidbody2D>();
            _rb2d.AddForce(firePoint.up * bulletForce * Time.deltaTime, ForceMode2D.Impulse);
        _as.PlayOneShot(attackShots[countAttack]);
         
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isAttack = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Rotate(collision);
            if(timeFire <= 0) 
            {
                Shoot();
                timeFire = cooldownFireInSeconds;
            }
            else
                timeFire -= Time.deltaTime;
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
