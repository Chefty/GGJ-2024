using Character.Properties;

namespace Character.View
{
    public class PlayerCharacterView : BaseCharacterView
    {
        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(false);
        }
    }
}