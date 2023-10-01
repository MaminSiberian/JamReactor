using UnityEngine;
using DG.Tweening;

namespace Room
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Transform oculusSeat;
        [SerializeField] private AudioDirector audioDirector;

        private Rigidbody2D rb;
        private AudioSource stepSound;
        private float horizontalSpeed;
        private Animator anim;
        private string currentState;
        private Tween tween;
        public bool isControllable;

        private const string idleAnim = "Character2Idle";
        private const string moveAnim = "Character2Run";

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = sprite.GetComponent<Animator>();
            stepSound = GetComponent<AudioSource>();
        }

        private void Start()
        {
            isControllable = true;
        }
        private void Update()
        {
            if (isControllable)
            {
                horizontalSpeed = Input.GetAxis("Horizontal");
                Flip();
            }
            else
                horizontalSpeed = 0;

            if (Mathf.Abs(horizontalSpeed) >= 0.3)
            {
                PlayAnim(moveAnim);
                PlayStepSound();
            }
            else
                PlayAnim(idleAnim);
        }
        private void FixedUpdate()
        {
            MovePlayer();
        }
        private void OnDisable()
        {
            tween.Kill();
        }

        private void MovePlayer()
        {
            rb.velocity = Vector2.right * playerSpeed * horizontalSpeed * Time.deltaTime;
        }
        private void Flip()
        {
            if (horizontalSpeed < 0 && !sprite.flipX)
            {
                sprite.flipX = true;
            }
            if (horizontalSpeed > 0 && sprite.flipX)
            {
                sprite.flipX = false;
            }
        }
        private void PlayAnim(string name)
        {
            if (name == currentState) return;

            anim.Play(name);
            currentState = name;
        }
        public void GetOculus(OculusController oculus, float time)
        {
            //audioDirector.PlayOculusSound();
            isControllable = false;
            GameDirector.oculusOnThePlayer = true;
            tween = oculus.transform.DOMove(oculusSeat.position, time).OnKill(PlayGame);
        }
        public void DropOculus(OculusController oculus, float force)
        {
            GameDirector.oculusOnThePlayer = false;

            oculus.transform.position = oculusSeat.position;
            Rigidbody2D rigidbody2D = oculus.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 1;
            rigidbody2D.AddForce(Vector2.up * force);
            rigidbody2D.AddForce(Vector2.right * force);
            isControllable = true;
        }
        private void PlayGame()
        {
            GameDirector.PlayGame();
        }
        private void PlayStepSound()
        {
            if (!stepSound.isPlaying && Time.timeScale != 0)
            {
                stepSound.pitch = Random.Range(0.9f, 1.1f);
                stepSound.Play();
            }
        }
    }

}
