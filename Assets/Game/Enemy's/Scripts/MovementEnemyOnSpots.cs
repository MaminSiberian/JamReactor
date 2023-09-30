using UnityEngine;

public class MovementEnemyOnSpots : MonoBehaviour
{
    [Header("Move properties")]
    public float speed;
    [Header("Spots properties")]
    public Transform[] moveSpots;
    private int countSpot;
    [Header("Force")]
    public float forcePush;
    [Header("Timer")]    
    
    public float startMoveTime;
    public bool isRotate;
    private float waitTime;
    private bool temp = true;
    private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;

    private void Start()
    {
        waitTime = startMoveTime;
        countSpot = 0;
        _rb = GetComponent<Rigidbody2D>();
        transform.position = moveSpots[0].position;
    }
    private void Update()
    {

        if (isRotate)
        {
            Vector2 lookDir = moveSpots[countSpot].position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = angle;
        }
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[countSpot].position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, moveSpots[countSpot].position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if(Direction())
                {
                    countSpot++;
                }
                else
                    countSpot--;
                waitTime = startMoveTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private bool Direction()
    {
        
        if (countSpot == 0)
        {
            temp = true;
        }
        if(countSpot == moveSpots.Length - 1) 
        {
            temp = false;
        }
        return temp;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.CompareTag("Player"))
        {
            _anim.SetTrigger("Attack");
            var direction = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<MoveTest>().fall = true;
            collision.rigidbody.AddForce(direction * forcePush,
                ForceMode2D.Impulse); ;
            
        }
    }
    
    
}
