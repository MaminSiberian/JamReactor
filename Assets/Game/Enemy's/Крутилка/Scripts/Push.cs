using UnityEngine;

public class Push : MonoBehaviour
{
    public float forcePush;
    public AudioClip pushClip;
    public AudioSource pushSource;
    public GameObject pushFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bot");
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(pushFX, collision.transform.position, transform.rotation);
            pushSource.PlayOneShot(pushClip);
            var direction = collision.transform.position - transform.position;
            collision.rigidbody.AddForce(direction * forcePush * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
