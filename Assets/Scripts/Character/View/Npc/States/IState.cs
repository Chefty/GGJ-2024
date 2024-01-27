namespace Character.View.Npc.States
{
    public interface IState
    {
        NextState[] NextStates { get; }
        
        void Start(ICharacterView characterView);
        bool Execute(ICharacterView characterView);
        void End();
    }
}