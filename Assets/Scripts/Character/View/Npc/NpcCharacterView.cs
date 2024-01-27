using Character.Properties;

namespace Character.View
{
    public class NpcCharacterView : BaseCharacterView
    {
        protected override ICharacterProperties GetCharacterProperties()
        {
            return CharacterPropertiesFactory.Get(true);
        }
    }
}