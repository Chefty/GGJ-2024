using Character.Properties;

namespace Character.View.Npc
{
    public class NpcCharacterView : BaseCharacterView
    {
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
        }
    }
}