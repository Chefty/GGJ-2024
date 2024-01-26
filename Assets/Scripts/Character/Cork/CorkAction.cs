using System.Threading.Tasks;
using Character.View;

namespace Character.Cork
{
    public class CorkAction
    {
        private const int CORK_MILLISECONDS = 5000;
        
        public void Execute(ICharacterView actorCharacter, ICharacterView[] affectedCharacters)
        {
            actorCharacter.CharacterProperties.RemoveCork();
            foreach (var affectedCharacter in affectedCharacters)
            {
                affectedCharacter.CharacterProperties.ApplyCork();
            }

            WaitAndResetAppliedCork(affectedCharacters);
        }

        private async Task WaitAndResetAppliedCork(ICharacterView[] affectedCharacters)
        {
            await Task.Delay(CORK_MILLISECONDS);
            
            foreach (var affectedCharacter in affectedCharacters)
            {
                affectedCharacter.CharacterProperties.ResetApplyCork();
            }
        }
    }
}