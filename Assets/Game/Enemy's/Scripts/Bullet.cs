using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(1112);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IHP>().TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Enemy"))
            return;
        Destroy(gameObject);
    }
}
