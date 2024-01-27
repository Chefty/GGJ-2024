using Character.Properties;

namespace Character.View.Npc
{
    public class NpcCharacterView : BaseCharacterView
    {
        public StateMachine StateMachine { get; private set; }

        private void Awake()
        {
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