using DG.Tweening;
using UnityEngine;

namespace Room
{
    public class TV : MonoBehaviour
    {
        [SerializeField] private GameObject okTV;
        [SerializeField] private GameObject brokenTV;

        [SerializeField] private Player player;
        [SerializeField] private AudioDirector audioDirector;

        private const string text = "TV have been defeated!";

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
            GameDirector.TVIsBroken = true;
            player.isControllable = true;
        }


    }
}
