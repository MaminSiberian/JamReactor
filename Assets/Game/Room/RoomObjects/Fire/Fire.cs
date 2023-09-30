using UnityEngine;

namespace Room
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private GameObject fires;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;

        private const string text = "Становится жарковато! Нужно пройти игру как можно скорее";

        private void Start()
        {
            bool roomIsBurning = GameDirector.roomIsBurning;

            fires.SetActive(roomIsBurning);

            if (GameDirector.eventToHappen == RoomEvent.Fire && !roomIsBurning)
                StartFireEvent();
        }

        private void StartFireEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();

            fires.SetActive(true);

            player.isControllable = false;
            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopFireEvent;
        }
        private void StopFireEvent()
        {
            EventText.OnAnimEndedEvent -= StopFireEvent;
            player.isControllable = true;
        }

    }
}
