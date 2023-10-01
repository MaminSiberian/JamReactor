using UnityEngine;

namespace Room
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private GameObject crack;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;
        [SerializeField] private AudioDirector audioDirector;

        private const string text = "Из дыры в окне потянуло сквозняком";

        private void Start()
        {
            bool windowIsBroken = GameDirector.windowIsBroken;

            crack.SetActive(windowIsBroken);

            if (GameDirector.eventToHappen == RoomEvent.Window && !windowIsBroken)
                StartWindowEvent();
        }
        private void StartWindowEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();

            crack.SetActive(true);

            player.isControllable = false;
            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopWindowEvent;
            audioDirector.PlayCrackSound();
        }
        private void StopWindowEvent()
        {
            EventText.OnAnimEndedEvent -= StopWindowEvent;
            player.isControllable = true;
        }
    }
}
