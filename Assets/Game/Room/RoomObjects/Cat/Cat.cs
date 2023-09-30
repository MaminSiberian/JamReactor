using DG.Tweening;
using UnityEngine;

namespace Room
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private Transform outPoint;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;
        [SerializeField] private AudioDirector audioDirector;

        [SerializeField] private float walkTime = 1f;

        private Animator anim;
        private SpriteRenderer sprite;

        private const string idleAnim = "Idle";
        private const string moveAnim = "Moving";
        private const string text = "Вы врезались в кота!";
        private Tween tween;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            if (GameDirector.catIsGone) this.gameObject.SetActive(false);

            anim.Play(idleAnim);

            if (GameDirector.eventToHappen == RoomEvent.Cat && !GameDirector.catIsGone)
                StartCatEvent();
        }
        private void OnDisable()
        {
            tween.Kill();
        }
        private void StartCatEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();
            player.isControllable = false;

            sprite.flipX = true;
            anim.Play(moveAnim);
            tween = this.transform.DOMove(outPoint.position, walkTime);

            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopCatEvent;
            audioDirector.PlayCatSound();
        }
        private void StopCatEvent()
        {
            EventText.OnAnimEndedEvent -= StopCatEvent;
            player.isControllable = true;
        }
    }
}
