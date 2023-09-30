using UnityEngine;

namespace Room
{
    public class Vase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer flower;
        [SerializeField] private SpriteRenderer brokenFlower;
        [SerializeField] private SpriteRenderer vase;
        [SerializeField] private SpriteRenderer brokenVase;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;
        [SerializeField] private AudioDirector audioDirector;

        private const string text = "Oops! You have applied critical damage on vase";

        private void Start()
        {
            bool vaseIsBroken = GameDirector.vaseIsBroken;

            flower.enabled = !vaseIsBroken;
            vase.enabled = !vaseIsBroken;

            brokenFlower.enabled = vaseIsBroken;
            brokenVase.enabled = vaseIsBroken;

            if (GameDirector.eventToHappen == RoomEvent.Vase && !vaseIsBroken)
                StartVaseEvent();
        }
        private void StartVaseEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();

            flower.enabled = false;
            vase.enabled = false;

            brokenFlower.enabled = true;
            brokenVase.enabled = true;

            player.isControllable = false;
            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopVaseEvent;
            audioDirector.PlayVaseSound();
        }
        private void StopVaseEvent()
        {
            EventText.OnAnimEndedEvent -= StopVaseEvent;
            GameDirector.vaseIsBroken = true;
            player.isControllable = true;
        }

    }
}
