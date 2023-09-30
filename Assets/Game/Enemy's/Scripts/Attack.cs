using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator m_Animator;
    public float speedAttack;
    public float speedRotation;
    public int damage;
    public float forcePush;
    private Rigidbody2D _rb;
    public GameObject fxPush;
    private AudioSource _audioSource;
    public AudioClip[] audioClips;
    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetBool("IsAttack", true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = Vector2.Lerp(transform.position,
                collision.transform.position,
                speedAttack * Time.deltaTime);
            Rotate(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_Animator.SetBool("IsAttack", false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("attack");
    }
    public void Rotate(Collider2D collision)
    {
        Vector2 lookDir = collision.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, Time.deltaTime * speedRotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Instantiate(fxPush, collision.transform.position, transform.rotation);
            _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
            var direction = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Health>().Takedamage(damage);
            Rigidbody2D _rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            _rbPlayer.AddForce(direction * forcePush, ForceMode2D.Impulse);
        }
    }
}
