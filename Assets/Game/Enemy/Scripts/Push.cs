using UnityEngine;

public class Push : MonoBehaviour
{
    public float forcePush;
    public AudioClip pushClip;
    public AudioSource pushSource;
    public GameObject pushFX;
    [SerializeField] private EnemyController ec;
    [SerializeField] private Animator _anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ec._iscatch)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _anim.SetTrigger("Attack");
                Instantiate(pushFX, collision.transform.position, transform.rotation);
                pushSource.PlayOneShot(pushClip);
                var direction = collision.transform.position - transform.position;
                collision.rigidbody.AddForce(direction * forcePush, ForceMode2D.Impulse);
                var player = collision.gameObject.GetComponent<Hook>();
                ec._iscatch = false;
                player.StartPullUpHook();

            }
        }
    }
}
