using Character.Properties;
using Game.Level;
using UnityEngine;

namespace Character.View.Npc
{
    public class NpcCharacterView : BaseCharacterView
    {
        private Vector3 moveDirection;
        public StateMachine StateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            StateMachine = new StateMachine(this);
        }

        private void OnEnable() 
        {
            SetNPCModel();
        }

        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(true);
        }

        private void Update()
        {
            StateMachine.Update();
            characterAnimator.SetFloat("Input Magnitude", moveDirection.magnitude);
        }

        public override void Walk(Vector3 direction)
        {
            base.Walk(direction);
            moveDirection = direction;
        }

        public void SetNPCModel()
        {
            var registeredPlayersModels = LevelManager.Instance.RegisteredPlayersData.PlayersModels;
            var randomModelName = registeredPlayersModels[Random.Range(0, registeredPlayersModels.Count)];
            foreach (var model in models)
            {
                if (model.name == randomModelName)
                {
                    model.SetActive(true);
                }
            }
        }
    }
}