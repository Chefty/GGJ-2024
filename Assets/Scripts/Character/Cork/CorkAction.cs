using System.Threading.Tasks;
using Character.View;

namespace Character.Cork
{
    public class CorkAction
    {
        public void Execute(ICharacterView actorCharacter, ICharacterView[] affectedCharacters)
        {
            if (affectedCharacters.Length == 0)
            {
                return;
            }
            
            actorCharacter.CharacterProperties.RemoveCork();
            foreach (var affectedCharacter in affectedCharacters)
            {
                affectedCharacter.OnCorked();
                affectedCharacter.CharacterProperties.ApplyCork();
            }

            WaitAndResetAppliedCork(affectedCharacters);
        }

        private async Task WaitAndResetAppliedCork(ICharacterView[] affectedCharacters)
        {
            await Task.Delay((int)(GameConstants.CORK_TIME * GameConstants.MILLISECOND));
            
            foreach (var affectedCharacter in affectedCharacters)
            {
                affectedCharacter.OnUncorked();
                affectedCharacter.CharacterProperties.ResetApplyCork();
            }
        }
    }
}