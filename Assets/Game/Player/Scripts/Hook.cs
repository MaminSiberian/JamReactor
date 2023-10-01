using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
//using static UnityEngine.GraphicsBuffer;

public enum CatchingVariable
{
    point,
    enemy
}

public class Hook : MonoBehaviour
{
    private int lavelID;
    [SerializeField] private float timeToRestartlave;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sountThrowHook;
    [SerializeField] private AudioClip soundCatchHook;
    [SerializeField] private AudioClip soundPullUpHook;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform hook;

    [Range(2, 10)][SerializeField] private float maxDistanseHook;
    [Range(1, 9)][SerializeField] private float minDistanseHook;
    [SerializeField] private float radiusHook;
    [Range(0f, 3f)][SerializeField] private float timeThrowHook;
    [Range(0f, 3f)][SerializeField] private float timePullUpHook;
    [SerializeField] bool isCathc = false;
    [SerializeField] private bool tryCatchSomthing = false;
    [Range(0f, 5f)][SerializeField] private float intensityShakeCamera;
    [Range(0f, 5f)][SerializeField] private float timeShakeCamera;
    [SerializeField] private GameObject palyerGFX;
    [SerializeField] private GameObject particleDeath;
    private bool isHookReload = true;
    private Vector2 direction;
    private CatchingVariable whoCatching;
    private GameObject catchingTarget;
    [SerializeField] private bool isCatchEnemy = false;
    [SerializeField] private float forcePush;
    [SerializeField] private float forcePushMe;
    private Rigidbody2D _rb;
    private float pitch;
    private float timeReloadHook;
    private bool isDeath = false;

    private void Start()
    {
        lavelID = SceneManager.GetActiveScene().buildIndex;
        timeReloadHook = timeThrowHook + timePullUpHook + 0.05f;
        audioSource = GetComponent<AudioSource>();
        pitch = audioSource.pitch;
        _rb = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!isDeath)
        {

            if ((Input.GetMouseButtonDown(0)) && (isHookReload))
            {
                isHookReload = false;
                Invoke("ReloadHook", timeReloadHook);
                Invoke("ReloadHook", timeReloadHook * 2);
                if ((!tryCatchSomthing) && (!isCatchEnemy))
                {
                    tryCatchSomthing = true;
                    StartCoroutine(ThrowHook());
                }
                else if (!tryCatchSomthing && isCatchEnemy)
                {
                    if ((catchingTarget != null) && (catchingTarget.gameObject.CompareTag("Enemy")))
                        StartCoroutine(ThrowEnemy());
                }
                _rb.velocity = Vector3.zero;
            }
            else
                if (!tryCatchSomthing)
                TurnInDirection();
        }
    }

    private void ReloadHook()
    {
        //    _rb.velocity = Vector2.zero;
        //    tryCatchSomthing = false;
        //    catchingTarget = null;
        //    isCathc = false;
        isHookReload = true;
        hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
        //isCatchEnemy = false;
        //hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
    }

    private void PlaySound(AudioClip clip)
    {
        var rand = Random.Range(-0.2f, 0.2f);
        audioSource.pitch = pitch + rand;
        Debug.Log("Sound");
        audioSource.PlayOneShot(clip);

    }

    IEnumerator ThrowEnemy()
    {
        if (catchingTarget != null)
        {
            Debug.Log("Throw Enemy");
            //PlaySound(sountThrowHook);
            isCatchEnemy = false;
            isCathc = false;
            var enemy = catchingTarget.GetComponent<EnemyController>();
            enemy.ChangeFall(true);
            _rb.AddForce(-direction * forcePushMe * Time.deltaTime, ForceMode2D.Impulse);
            var rbEnemy = catchingTarget.GetComponent<Rigidbody2D>();
            rbEnemy.AddForce(direction * forcePush, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.4f);
            _rb.velocity = Vector2.zero;
            if ((catchingTarget != null) && (rbEnemy != null))
            {
                enemy.ChangeFall(false);
                enemy._iscatch = false;
                rbEnemy.velocity = Vector2.zero;
            }
        }
        //isHookReload = true;
        yield break;
    }




    private void TurnInDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        direction = new Vector2(mousePos.x - transform.position.x,
           mousePos.y - transform.position.y);

        transform.up = direction;
    }

    IEnumerator ThrowHook()
    {
        PlaySound(sountThrowHook);
        var startPos = direction.normalized * minDistanseHook + (Vector2)transform.position;
        var endPos = direction.normalized * maxDistanseHook + (Vector2)transform.position;
        float current = 0;
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
        PlaySound(soundPullUpHook);
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
        //isHookReload = true;
        hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
        Debug.Log("hook on min");
        yield break;
    }


    IEnumerator PullUpSelfToPoint()
    {
        PlaySound(soundPullUpHook);
        var startPos = transform.position;
        var endPos = catchingTarget.transform.position;
        float current = 0;
        while (current < 1)
        {
            if (catchingTarget != null)
            {
                transform.position = Vector2.Lerp(startPos, endPos, current);
                hook.position = endPos;
                current += Time.deltaTime / timePullUpHook;
                if (Vector2.Distance(catchingTarget.transform.position, transform.position) < 0.1f)
                {
                    transform.position = endPos;
                }
            }
            yield return null;
        }
        hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
        tryCatchSomthing = false;
        isCathc = false;
        //isHookReload = true;
        Debug.Log("PullUpToPoint");
        yield break;
    }

    IEnumerator PullUpEnemyToSelf()
    {
        PlaySound(soundPullUpHook);
        var startPos = direction.normalized * maxDistanseHook + (Vector2)transform.position;
        var endPos = direction.normalized * minDistanseHook + (Vector2)transform.position;
        //StartCoroutine(MoveToTarget(startPos, endPos, timePullUpHook));
        float current = 0;
        while (current < 1)
        {
            if (catchingTarget)
            {
                hook.position = Vector2.Lerp(startPos, endPos, current);
                catchingTarget.transform.position = hook.transform.position;
                current += Time.deltaTime / timePullUpHook;
                yield return null;
            }
            else yield return null;
        }
        tryCatchSomthing = false;
        isCatchEnemy = true;
        StartCoroutine(CathcTargetInHook());
        //isHookReload = true;
        Debug.Log("hook on min");
        yield break;
    }


    IEnumerator CathcTargetInHook()
    {
        if (!isDeath)
        while (isCatchEnemy)
        {
            if ((catchingTarget != null) && (catchingTarget.gameObject.CompareTag("Enemy")))
            {
                catchingTarget.transform.position = hook.transform.position;
            }
            yield return null;
        }
        Debug.Log("Throw Enemy");
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
        if (collision.gameObject.CompareTag("Thorn"))
        {
            Death();
        }

    }
    private IEnumerator Timer()
    {

        yield return new WaitForSeconds(0.5f);
        _rb.velocity = Vector2.zero;
        tryCatchSomthing = false;
        catchingTarget = null;
        isCathc = false;
        //isHookReload = true;
        isCatchEnemy = false;
        hook.position = direction.normalized * minDistanseHook + (Vector2)transform.position;
        yield break;
    }


    private void Death()
    {
        isDeath = true;
        StopAllCoroutines();
        isCathc = false;
        isCatchEnemy = false;
        tryCatchSomthing = false;
        var particels = Instantiate(particleDeath, transform.position, transform.rotation);
        particels.SetActive(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        palyerGFX.SetActive(false);
        hook.gameObject.SetActive(false);
        transform.position = particels.transform.position;
        Invoke("LoadScene", timeToRestartlave);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(lavelID);
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

    //public void PushMe()
    //{
    //    isCatchEnemy = false;
    //    isCathc = false;
    //    isHookReload = true;
    //    tryCatchSomthing = false;
    //}

}


