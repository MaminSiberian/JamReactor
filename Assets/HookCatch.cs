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
            Debug.Log("catch");
            hook.CatchSomthing(CatchingVariable.point, collision.transform.position);
        }
    }
}
