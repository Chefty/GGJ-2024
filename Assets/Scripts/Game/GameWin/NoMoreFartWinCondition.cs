using Character.View;

namespace Game.GameWin
{
    public class NoMoreFartWinCondition : IGameWinCondition
    {
        public bool Validate(ICharacterView characterView)
        {
            return !characterView.CharacterProperties.IsNpc && characterView.CharacterProperties.FartAmount <= 0;
        }

        public void Execute(ICharacterView characterView)
        {
            //TODO: characterView won the game
        }
    }
}