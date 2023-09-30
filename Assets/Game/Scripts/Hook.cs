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
    [SerializeField] private Transform hookPivot;
    [Range(2, 10)][SerializeField] private float maxDistanseHook;
    [Range(1, 9)][SerializeField] private float minDistanseHook;
    [Range(0f, 300f)][SerializeField] private float speedHook;
    [Range(0f, 3f)][SerializeField] private float timeReloadHook;
    [SerializeField] bool isCathc = false;
    [SerializeField] private bool tryCatchSomthing = false;
    [Range(0f, 5f)][SerializeField] private float intensityShakeCamera;
    [Range(0f, 5f)][SerializeField] private float timeShakeCamera;
    private Vector2 direction;
    private CatchingVariable whoCatching;
    private GameObject catchingTarget;
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

        if (tryCatchSomthing)
            TryCathcSomthing();


    }

    private void TurnInDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        direction = new Vector2(mousePos.x - transform.position.x,
           mousePos.y - transform.position.y);

        transform.up = direction;
    }



    private void TryCathcSomthing()
    {
        if (!isCathc)
        {
            if (current == target)
                target = 0;
            current = Mathf.MoveTowards(current, target, speedHook * Time.deltaTime);
            hook.position = Vector2.Lerp(direction.normalized * minDistanseHook + (Vector2)transform.position,
            direction.normalized * maxDistanseHook + (Vector2)transform.position, current);
        }
        else
        {
            Vector3 targetPos = catchingTarget.transform.position;
            hook.position = targetPos;            
            current = 0;
            target = 1;
            current = Mathf.MoveTowards(current, target, speedHook * Time.deltaTime);
            transform.position = Vector2.Lerp(transform.position, targetPos, current);
            if (Vector2.Distance(catchingTarget.transform.position, transform.position) < 0.1f)
                transform.position = targetPos;    
        }

    }

    private void ReloadHook()
    {
        tryCatchSomthing = false;
        hook.localPosition = Vector2.up * minDistanseHook;
        isCathc = false;///////////////////////////////
    }

    public void CatchSomthing(CatchingVariable variable, GameObject catchingTarget)
    {
        isCathc = true;
        Debug.Log(1);
        MyEventManager.CameraShake(intensityShakeCamera, timeShakeCamera);
        MyEventManager.CatchSomthing();
        whoCatching = variable;
        this.catchingTarget = catchingTarget;
    }
}
