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
        
        public void Execute(ICharacterView characterView, float secondsHolding = 1f)
        {
            if (characterView.CharacterProperties.IsCorked)
            {
                return;
            }

            var amount = GameConstants.DEFAULT_FART_VALUE * secondsHolding;
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

        public static FartAction Get()
        {
            return new FartAction(new NoMoreFartWinCondition());
        }
    }
}