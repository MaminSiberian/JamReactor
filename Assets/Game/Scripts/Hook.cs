using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
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
    [SerializeField] private float radiusHook;
    //[SerializeField] private LayerMask targetMask;
    //[SerializeField] private LayerMask targetMask;
    [Range(0f, 3f)][SerializeField] private float timeThrowHook;
    [Range(0f, 3f)][SerializeField] private float timePullUpHook;
    [SerializeField] bool isCathc = false;
    [SerializeField] private bool tryCatchSomthing = false;
    [Range(0f, 5f)][SerializeField] private float intensityShakeCamera;
    [Range(0f, 5f)][SerializeField] private float timeShakeCamera;
    private Vector2 direction;
    private CatchingVariable whoCatching;
    private GameObject catchingTarget;
    [SerializeField] private bool isCatchEnemy = false;
    [SerializeField] private float forcePush;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if ((!tryCatchSomthing) && (!isCatchEnemy))
            {
                tryCatchSomthing = true;
                StartCoroutine(ThrowHook());
            }
            else
            {
                StartCoroutine(ThrowEnemy());
            }
        }
        else
            if (!tryCatchSomthing)
            TurnInDirection();

        IEnumerator ThrowEnemy()
        {
            Debug.Log("Throw Enemy");
            isCatchEnemy = false;
            isCathc = false;
            catchingTarget.GetComponent<Rigidbody2D>().AddForce(direction * forcePush, ForceMode2D.Impulse); 
            yield return null;
            yield break;
        }   

    }

    private void FixedUpdate()
    {

    }

    private void TurnInDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        direction = new Vector2(mousePos.x - transform.position.x,
           mousePos.y - transform.position.y);

        transform.up = direction;
    }


    private void ReloadHook()
    {
        tryCatchSomthing = false;
        hook.localPosition = Vector2.up * minDistanseHook;
        isCathc = false;///////////////////////////////
    }

    IEnumerator ThrowHook()
    {

        var startPos = direction.normalized * minDistanseHook + (Vector2)transform.position;
        var endPos = direction.normalized * maxDistanseHook + (Vector2)transform.position;
        float current = 0;
        //StartCoroutine(MoveToTarget(startPos, endPos, timeThrowHook));
        while (current < 1) 
        {
            CheckColllision();
            if (isCathc) break;
                hook.position = Vector2.Lerp(startPos, endPos, current);
            current += Time.deltaTime / timeThrowHook;
            yield return null;
        }

        Debug.Log("hook on max");
        if (isCathc == false)
        {
            StartCoroutine(PullUpHook());
        }
        else
        {
            if (whoCatching == CatchingVariable.enemy)
            {
                Debug.Log("Catch Enemy To Self");
                StartCoroutine(PullUpEnemyToSelf());
            }
            else if (whoCatching == CatchingVariable.point)
            {
                Debug.Log("Catch to point");
                StartCoroutine(PullUpSelfToPoint());
            }
        }
        yield break;
    }

    IEnumerator PullUpHook()
    {
        var startPos = direction.normalized * maxDistanseHook + (Vector2)transform.position;
        var endPos = direction.normalized * minDistanseHook + (Vector2)transform.position;
        //StartCoroutine(MoveToTarget(startPos, endPos, timePullUpHook));
        float current = 0;
        while (current < 1)
        {
            hook.position = Vector2.Lerp(startPos, endPos, current);
            current += Time.deltaTime / timePullUpHook;
            yield return null;
        }
        tryCatchSomthing = false;
        isCathc = false;
        Debug.Log("hook on min");
        yield break;
    }


    IEnumerator PullUpSelfToPoint()
    {
        var startPos = transform.position;
        var endPos = catchingTarget.transform.position;
        float current = 0;
        Debug.Log(catchingTarget.transform.position);
        while (current < 1)
        {
            transform.position = Vector2.Lerp(startPos, endPos, current);
            hook.position = endPos;
            current += Time.deltaTime / timePullUpHook;
            if (Vector2.Distance(catchingTarget.transform.position, transform.position) < 0.1f)
            {
                transform.position = endPos;
                hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
            }
            yield return null;
        }
        tryCatchSomthing = false;
        isCathc = false;
        Debug.Log("PullUpToPoint");
        yield break;
    }

    IEnumerator PullUpEnemyToSelf()
    {
        var startPos = direction.normalized * maxDistanseHook + (Vector2)transform.position;
        var endPos = direction.normalized * minDistanseHook + (Vector2)transform.position;
        //StartCoroutine(MoveToTarget(startPos, endPos, timePullUpHook));
        float current = 0;
        while (current < 1)
        {

            hook.position = Vector2.Lerp(startPos, endPos, current);
            catchingTarget.transform.position = hook.transform.position;
            current += Time.deltaTime / timePullUpHook;
            yield return null;
        }
        tryCatchSomthing = false;
        isCatchEnemy = true;
        StartCoroutine(CathcTargetInHook());
        Debug.Log("hook on min");
        yield break;
    }


    IEnumerator CathcTargetInHook()
    {
        while (isCatchEnemy)
        {
            catchingTarget.transform.position = hook.transform.position;
            yield return null;
        }
        Debug.Log("Throw Enemy");
        yield break;
    }

    private void CheckColllision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hook.transform.position, radiusHook);
        List<GameObject> targets = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<ICanCatching>() != null)
                targets.Add(collider.gameObject);
        }
        //bool isEnemyInList = false;

        foreach (GameObject target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                Debug.Log("catchEnemy");
                CatchSomthing(CatchingVariable.enemy, target);
                break;
            }
            else if (target.CompareTag("PointToCatch"))
            {
                Debug.Log("catchPoint");
                CatchSomthing(CatchingVariable.point, target);
            }
            
        }
    }


    private void CatchSomthing(CatchingVariable variable, GameObject catchingTarget)
    {
        isCathc = true;
        MyEventManager.CameraShake(intensityShakeCamera, timeShakeCamera);
        MyEventManager.CatchSomthing();
        whoCatching = variable;
        Debug.Log(whoCatching);
        this.catchingTarget = catchingTarget;
        if (variable == CatchingVariable.enemy)
        {
            var enemy = catchingTarget.GetComponent<ICanCatching>();
            enemy.CatchOn();
            Debug.Log(enemy);
            isCatchEnemy = true;
        }
    }
}


