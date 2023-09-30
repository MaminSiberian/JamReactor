using System.Collections;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;
    public bool fall;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        fall = false;
    }

    private void FixedUpdate()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            if (!fall)
            {
                _rb.velocity = new Vector2(h, v) * speed * Time.fixedDeltaTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            fall = true;
            StopAllCoroutines();
            StartCoroutine(Block());
        }
    }
    private IEnumerator Block()
    {
        yield return new WaitForSeconds(2f);
        fall = false;
    }
}
