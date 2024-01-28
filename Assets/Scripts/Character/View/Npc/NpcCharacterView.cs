using Character.Properties;
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
    }
}