using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private Rigidbody2D _rb;
    public float force;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("123");
        }
        if (collision.gameObject.CompareTag("Enemy"))
            return;
        Destroy(gameObject);
    }


}
