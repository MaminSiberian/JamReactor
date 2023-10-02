using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

namespace Room
{
    public class Exit : MonoBehaviour
    {
        [SerializeField] private Image endScreen;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Player player;
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource fireSound;
        [SerializeField] private GameObject TV;
        [SerializeField] private float time;

        private AudioSource fireworkSound;

        private void Awake()
        {
            fireworkSound = GetComponent<AudioSource>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Player>() && GameDirector.eventToHappen == RoomEvent.Final)
            {
                Color screenColor = endScreen.color;
                Color textColor = text.color;
                screenColor.a = 0;
                textColor.a = 0;
                endScreen.color = screenColor;
                text.color = textColor;

                endScreen.enabled = true;
                text.enabled = true;
                player.isControllable = false;
                music.Stop();
                fireSound.Stop();
                TV.SetActive(false);
                fireworkSound.Play();

                endScreen.DOFade(1, time);
                text.DOFade(1, time);
                StartCoroutine(ExitGame());
            }
        }
        private IEnumerator ExitGame()
        {
            yield return new WaitForSeconds(time + 0.7f);
            Application.Quit();

        }
    }
    
}
