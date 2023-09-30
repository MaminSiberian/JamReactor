using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCatch : MonoBehaviour
{
    [SerializeField] private Hook hook;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ICanCatching>() != null)
        {
            if (collision.gameObject.CompareTag("PointToCatch"))
            {
                Debug.Log("catchPoint");
                hook.CatchSomthing(CatchingVariable.point, collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("catchEnemy");
                hook.CatchSomthing(CatchingVariable.enemy, collision.gameObject);
            }
        }
    }
}
