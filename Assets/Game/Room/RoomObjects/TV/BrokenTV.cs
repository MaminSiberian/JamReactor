using UnityEngine;

public class BrokenTV : MonoBehaviour
{
    [SerializeField] private float animRate;

    private AudioSource audioSource;
    private Animator anim;

    private const string idleAnim = "Idle";
    private const string circuitAnim = "Circuit";

    private float timer = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        anim.Play(circuitAnim);
    }
    private void Update()
    {
        SetTimer();
    }
    private void SetTimer()
    {
        if (timer >= animRate)
        {
            timer -= animRate;
            anim.Play(circuitAnim);
        }
        else
            timer += Time.deltaTime;
    }
    private void PlayIdleAnim()
    {
        anim.Play(idleAnim);
    }
    private void PlaySound()
    {
        audioSource.Play();
    }
}
