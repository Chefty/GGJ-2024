using Character.View;
using Game.GameWin;
using UnityEngine;

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
            if (characterView.CharacterProperties.IsCorked || !CanFart(characterView))
            {
                return;
            }

            var amount = GameConstants.DEFAULT_FART_VALUE * secondsHolding;
            characterView.Fart(amount);
            characterView.CharacterProperties.SetLastFartTime(Time.realtimeSinceStartup);
            
            if (!characterView.CharacterProperties.IsNpc)
            {
                characterView.CharacterProperties.DecrementFart(amount);
            }

            if (gameWinCondition.Validate(characterView))
            {
                gameWinCondition.Execute(characterView);
            }
        }

        private bool CanFart(ICharacterView characterView)
        {
            return Time.realtimeSinceStartup - characterView.CharacterProperties.LastTimeFarted >=
                   GameConstants.FART_COOLDOWN;
        }

        public static FartAction Get()
        {
            return new FartAction(new NoMoreFartWinCondition());
        }
    }
}