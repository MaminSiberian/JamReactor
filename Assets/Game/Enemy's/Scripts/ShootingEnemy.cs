using System;
using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float startTimeFire;
    public bool isAttack;
    public float speedRotation;
    public AudioClip attackShot;
    public LayerMask whoIsPlayer;

    public float bulletForce;
    private Rigidbody2D _rb;
    private float timeFire;
    private AudioSource _as;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        timeFire = startTimeFire;
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
            GameObject prefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D _rb2d = prefab.GetComponent<Rigidbody2D>();
            _rb2d.AddForce(firePoint.up * bulletForce * Time.deltaTime, ForceMode2D.Impulse);
            _as.PlayOneShot(attackShot);
         
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
                timeFire = startTimeFire;
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
