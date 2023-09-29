using UnityEngine;

public class MovementEnemyOnSpots : MonoBehaviour
{
    [Header("Move properties")]
    public float speed;
    [Header("Spots properties")]
    public Transform[] moveSpots;
    private int countSpot;
    [Header("Force")]
    public float force;
    

    public float startMoveTime;
    private float waitTime;
    private bool temp = true;

    private void Start()
    {
        waitTime = startMoveTime;
        countSpot = 0;
    }
    private void Update()
    {
        
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
        Debug.Log("Col");
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player");
            var direction = transform.position - collision.transform.position;
            collision.rigidbody.AddForce(-direction * force * Time.deltaTime,
                ForceMode2D.Impulse);
            Debug.Log("off");
        }
    }
    
    
}
