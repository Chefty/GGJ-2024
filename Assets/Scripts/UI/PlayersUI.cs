using UnityEngine;

namespace UI
{
    public class PlayersUI : MonoBehaviour
    {
        public static PlayersUI Instance { get; private set; }

        [SerializeField] private PlayerUIBehaviour[] behaviours;

        private void Awake()
        {
            Instance = this;

            foreach (var behaviour in behaviours)
            {
                behaviour.gameObject.SetActive(false);
            }
        }

        public PlayerUIBehaviour GetUIBehaviourFor(int userId)
        {
            return behaviours[userId];
        }
    }
}
