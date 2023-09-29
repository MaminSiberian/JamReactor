using UnityEngine;

public class MovementEnemyOnCircle : MonoBehaviour
{
    [Header("Move properties")]
    public float speed;
    [Header("RadiusChangePosition properties")]
    public float radius;

    public float startMoveTime;
    private float waitTime;
    private Vector2 randomPoint;

    private void Start()
    {
        var x = Random.Range(-radius, radius);
        var y = Random.Range(-radius, radius);
        randomPoint = new Vector2(x, y);
        waitTime = startMoveTime;
    }
    private void Update()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, randomPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, randomPoint) < 0.1f)
        {
            if(waitTime <= 0) 
            {
                var x = Random.Range(-radius, radius);
                var y = Random.Range(-radius, radius);
                randomPoint = new Vector2(x, y);
                waitTime = startMoveTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    
}
