using UnityEngine;

public class MovementEnemyOnCircle : MonoBehaviour, ICanCatching
{
    [Header("Move properties")]
    public float speed;
    [Header("RadiusChangePosition properties")]
    public float radius;

    public float startMoveTime;
    private float waitTime;
    private Vector2 randomPoint;
    private Rigidbody2D _rb;
    private bool _isCatching;
    private Animator _anim;

    private void Start()
    {
        var x = Random.Range(-radius, radius);
        var y = Random.Range(-radius, radius);
        randomPoint = new Vector2(x, y);
        waitTime = startMoveTime;
        _rb = GetComponent<Rigidbody2D>();
        _isCatching = false;
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!_isCatching)
        {
            if (!_anim.GetBool("IsAttack"))
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
    public void CatchOn() { _isCatching = true; }
    public void CatchOff() { _isCatching = false; }

}
