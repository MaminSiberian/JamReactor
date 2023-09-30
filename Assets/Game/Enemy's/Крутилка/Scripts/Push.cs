using UnityEngine;

public class Push : MonoBehaviour
{
    public float forcePush;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bot");
        if(collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(direction * forcePush * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
