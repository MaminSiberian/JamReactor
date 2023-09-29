using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public enum CatchingVariable
{
    point,
    enemy
}

public class Hook : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform hook;
    [Range(2, 10)][SerializeField] private float maxDistanseHook;
    [Range(1, 9)][SerializeField] private float minDistanseHook;
    [Range(0f, 300f)][SerializeField] private float speedHook;
    [Range(0f, 3f)][SerializeField] private float timeReloadHook;
    [SerializeField] bool isCathc = false;
    [SerializeField] private bool tryCatchSomthing = false;
    private Vector2 direction;
    private CatchingVariable whoCatching;
    private Vector2 catchingTarget;
    private float current, target;


    private void Update()
    {
        if ((!tryCatchSomthing) && (Input.GetMouseButtonDown(0)))
        {
            tryCatchSomthing = true;
            current = 0;
            target = 1;
            Invoke("ReloadHook", timeReloadHook);
        }
        else
            TurnInDirection();


    }

    private void TurnInDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        direction = new Vector2(mousePos.x - transform.position.x,
           mousePos.y - transform.position.y);

        transform.up = direction;

    }

    private void FixedUpdate()
    {
        if (tryCatchSomthing)
            TryCathcSomthing();
    }

    private void TryCathcSomthing()
    {
        if (!isCathc)
        {
            if (current == target)
                target = 0;
            current = Mathf.MoveTowards(current, target, speedHook * Time.fixedDeltaTime);
            hook.position = Vector2.Lerp(direction.normalized * minDistanseHook + (Vector2)transform.position,
            direction.normalized * maxDistanseHook + (Vector2)transform.position, current);
        }
        else
        {
            current = 0;
            target = 1;

            current = Mathf.MoveTowards(current, target, speedHook * Time.fixedDeltaTime);
            hook.position = catchingTarget;
            transform.position = Vector2.Lerp(transform.position, catchingTarget, current);
            if (Vector2.Distance(catchingTarget, transform.position) < 0.1f)
                transform.position = catchingTarget;    
        }

    }



    private void ReloadHook()
    {
        tryCatchSomthing = false;
        Debug.Log("reload");
        hook.localPosition = Vector2.up * minDistanseHook;
        isCathc = false;///////////////////////////////
    }

    public void CatchSomthing(CatchingVariable variable, Vector2 catchingTarget)
    {
        isCathc = true;
        whoCatching = variable;
        this.catchingTarget = catchingTarget;
    }
}
