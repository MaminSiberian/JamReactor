using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _rb.velocity = Vector3.zero;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(h,v) * speed * Time.deltaTime);
            
        
    }
}
