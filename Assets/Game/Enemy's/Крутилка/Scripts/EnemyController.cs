using UnityEngine;

public class EnemyController : MonoBehaviour, ICanCatching
{
    public GameObject player;
    public float speed;
    public float speedAngle;
    public float visibleDistance;
    public Transform pivot;

    private float _distance;
    private float angle;
    private bool isAttack;
    public bool _iscatch;


    private void Start()
    {
        
    }
    private void Update()
    {
        if (!_iscatch)
        {
            _distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            angle += speedAngle * Time.deltaTime;

            pivot.rotation = Quaternion.Euler(0, 0, angle);
            if (_distance < visibleDistance)
            {
                isAttack = true;
                transform.position = Vector2.MoveTowards(transform.position,
                    player.transform.position,
                    speed * Time.deltaTime);
            }
            else
                isAttack = false;
        }
    }
    public bool GetIsAttack()
    {
        return isAttack;
    }
    public void CatchOn() 
    {
        _iscatch = true;
    }
    public void CatchOff() 
    {
        _iscatch = false;
    }

}
