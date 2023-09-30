using UnityEditor;
using UnityEngine;

public class MovementEnemyOnCircle : MonoBehaviour, ICanCatching
{
    [Header("Move properties")]
    [SerializeField] private float speed;
    [Header("RadiusChangePosition properties")]
    [SerializeField] private float radius;

    [SerializeField] private float startMoveTime;
    [SerializeField] private ShootingEnemy _se;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _isCatching;
    private float waitTime;
    private Vector2 randomPoint;

    private void Start()
    {
        var x = Random.Range(-radius, radius);
        var y = Random.Range(-radius, radius);
        randomPoint = new Vector2(x, y);
        waitTime = startMoveTime;
        _isCatching = false;
    }
    private void Update()
    {
        if (!_isCatching)
        {
            if (!_se.isAttack)
            {
                Vector2 lookDir = randomPoint - _rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                _rb.rotation = angle;
                transform.position = Vector2.MoveTowards(transform.position, randomPoint, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, randomPoint) < 0.1f)
                {
                    if (waitTime <= 0)
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
    }
    public void CatchOn()
    {
        _isCatching = true;
        _se.CatchOn();
    }
    public void CatchOff() { 
        _isCatching = false;
        _se.CatchOff();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
