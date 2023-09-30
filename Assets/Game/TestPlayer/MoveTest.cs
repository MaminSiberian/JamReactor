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
        
        
            
        
    }
    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        _rb.AddForce(new Vector2(h, v) * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
