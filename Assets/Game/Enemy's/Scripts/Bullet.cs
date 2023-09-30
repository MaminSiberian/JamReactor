using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Takedamage(damage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
            return;
        Destroy(gameObject);
    }
}
