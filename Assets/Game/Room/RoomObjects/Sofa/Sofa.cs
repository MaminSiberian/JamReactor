using UnityEngine;
using DG.Tweening;

namespace Room
{
    public class Sofa : MonoBehaviour
    {
        [SerializeField] private GameObject cig;
        [SerializeField] private Transform cigStartPos;
        [SerializeField] private Transform cigEndPos;
        [SerializeField] private float cigFlightTime = 2f;

        [SerializeField] private Player player;
        [SerializeField] private OculusController oculus;

        private const string text = "Запахло горелым...";
        private Tween tween;

        private void Start()
        {
            bool sofaIsBurning = GameDirector.sofaIsBurning;

            if (sofaIsBurning)
            {
                cig.SetActive(true);
                cig.transform.position = cigEndPos.position;
            }
            else
            {
                cig.SetActive(false);
                cig.transform.position = cigStartPos.position;
            }

            if (GameDirector.eventToHappen == RoomEvent.Sofa && !sofaIsBurning)
                StartSofaEvent();
        }
        private void OnDisable()
        {
            tween.Kill();
        }
        private void StartSofaEvent()
        {
            player.transform.position = new Vector2(this.transform.position.x, player.transform.position.y);
            if (GameDirector.oculusOnThePlayer) oculus.DropOculus();

            cig.SetActive(true);
            tween = cig.transform.DOMove(cigEndPos.position, cigFlightTime).SetEase(Ease.OutCubic);

            player.isControllable = false;
            EventText.PlayEventAnim(text);
            EventText.OnAnimEndedEvent += StopSofaEvent;
        }
        private void StopSofaEvent()
        {
            EventText.OnAnimEndedEvent -= StopSofaEvent;
            player.isControllable = true;
        }
    }
}
