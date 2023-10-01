using UnityEngine;
using NaughtyAttributes;

namespace Room
{
    public class OculusController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private float requiredDistanceToPlayer;
        [SerializeField] private GameObject helpWindow;
        [SerializeField] private float setupTime = 1;
        [SerializeField] private KeyCode getKey = KeyCode.Space;
        [SerializeField] private float dropForce;

        private Rigidbody2D rb;
        private Collider2D coll;
        private AudioSource audioSource;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            if (GameDirector.eventToHappen == RoomEvent.Final)
            {
                player.transform.position = this.transform.position;
                DropOculus();
            }

            Physics2D.IgnoreCollision(coll, player.GetComponent<Collider2D>());
        }
        private void Update()
        {
            if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= requiredDistanceToPlayer && GameDirector.eventToHappen != RoomEvent.Final)
            {
                helpWindow.SetActive(true);
                if (Input.GetKeyDown(getKey))
                {
                    GetOculus();
                }
            }
            else
            {
                helpWindow.SetActive(false);
            }
        }
        [Button]
        public void GetOculus()
        {
            audioSource.Play();
            rb.gravityScale = 0;
            player.GetOculus(this, setupTime);
        }
        [Button]
        public void DropOculus()
        {
            player.DropOculus(this, dropForce);
        }
    }
}
