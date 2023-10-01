using DG.Tweening;
using UnityEngine;

namespace Room
{
    public class TV : MonoBehaviour
    {
        [SerializeField] private GameObject okTV;
        [SerializeField] private GameObject brokenTV;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;
        [SerializeField] private AudioDirector audioDirector;

        private const string text = "Ты задамажил телевизор!";

        private void Start()
        {
            bool TVisBroken = GameDirector.TVIsBroken;

            okTV.SetActive(!TVisBroken);
            brokenTV.SetActive(TVisBroken);

            if (GameDirector.eventToHappen == RoomEvent.TV && !TVisBroken)
                StartTVEvent();
        }

        private void StartTVEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();

            okTV.SetActive(false);
            brokenTV.SetActive(true);

            player.isControllable = false;
            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopTVEvent;
            audioDirector.PlayTVEventSound();
        }
        private void StopTVEvent()
        {
            EventText.OnAnimEndedEvent -= StopTVEvent;
            player.isControllable = true;
        }
    }
}
