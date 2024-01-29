using System.Collections.Generic;
using System.Linq;
using Character.View.Npc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [SerializeField] private int amountOfNpcs = 50;
        [SerializeField] private NpcCharacterView npcPrefab;

        private List<NpcCharacterView> allNpcs = new List<NpcCharacterView>();

        public PlayersData RegisteredPlayersData;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            LobbyManager.OnStartCountDown += DoStartLevel;
            RegisteredPlayersData = new();
        }

        private void DoStartLevel()
        {
            StartLevel();
        }

        public async UniTask StartLevel()
        {
            DeleteAllNpcs();

            for (int i = 0; i < amountOfNpcs; i++)
            {
                var newNpc = Instantiate(npcPrefab, transform);
                newNpc.transform.localPosition = Vector3.zero;
                allNpcs.Add(newNpc);
            }

            await UniTask.WaitUntil(() => allNpcs.All(x => x.StateMachine.IsReadyToStart));

            allNpcs.ForEach(x => x.StateMachine.Start());
        }

        private void DeleteAllNpcs()
        {
            foreach (var npc in allNpcs)
            {
                Destroy(npc.gameObject);
            }
            
            allNpcs.Clear();
        }
    }
}