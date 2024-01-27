using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Character.View.Npc;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int amountOfNpcs = 50;
        [SerializeField] private NpcCharacterView npcPrefab;

        private List<NpcCharacterView> allNpcs = new List<NpcCharacterView>();
        
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

            var countDown = 3;
            for (int i = 0; i < 3; i++)
            {
                Debug.LogWarning($"GAME IS STARTING IN {countDown--}");
                await UniTask.Delay(1000);
            }
            
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