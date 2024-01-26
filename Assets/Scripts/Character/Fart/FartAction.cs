using Character.View;
using Game.GameWin;

namespace Character.Fart
{
    public class FartAction
    {
        private readonly IGameWinCondition gameWinCondition;

        public FartAction(IGameWinCondition gameWinCondition)
        {
            this.gameWinCondition = gameWinCondition;
        }
        
        public void Execute(ICharacterView characterView, float amount)
        {
            if (characterView.CharacterProperties.IsCorked)
            {
                return;
            }
            
            characterView.Fart(amount);
            
            if (!characterView.CharacterProperties.IsNpc)
            {
                characterView.CharacterProperties.DecrementFart(amount);
            }

            if (gameWinCondition.Validate(characterView))
            {
                gameWinCondition.Execute(characterView);
            }
        }
    }
}