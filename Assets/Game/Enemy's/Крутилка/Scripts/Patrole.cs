using UnityEngine;

public class Patrole : MonoBehaviour
{
    public float speed = 1f;
    public float cooldown = 1f;
    public float distance = 3f;
    private Vector2 randomPosition;
    private bool _isCathing;
    private float waitTime;
    private EnemyController _enemyController;
    private void Start()
    {
        var x = transform.position.x + Random.Range(-distance, distance);
        var y = transform.position.y + Random.Range(-distance, distance);
        randomPosition = new Vector2(x, y);
        waitTime = cooldown;
        _enemyController = GetComponent<EnemyController>();
    }
    private void Update()
    {
        if (!_enemyController._iscatch)
        {
            if (!_enemyController.GetIsAttack())
            {
                transform.position = Vector2.MoveTowards(transform.position, randomPosition, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, randomPosition) < 0.1f)
                {
                    if (waitTime <= 0)
                    {

                        var x = transform.position.x + Random.Range(-distance, distance);
                        var y = transform.position.y + Random.Range(-distance, distance);
                        randomPosition = new Vector2(x, y);
                        waitTime = cooldown;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;

                    }
                }
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_enemyController._iscatch)
        {
            if(!collision.gameObject.CompareTag("Player"))
            {
                var x = Random.Range(-distance, distance);
                var y = Random.Range(-distance, distance);
                randomPosition = new Vector2(x, y);
                waitTime = cooldown;
            }
        }
    }
}
