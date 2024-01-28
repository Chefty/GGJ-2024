namespace Character.Properties
{
    public static class CharacterPropertiesFactory
    {
        private const float InitialFartAmount = 100;
        private const float CharacterSpeed = 0.06f;
        private const int InitialNumberOfCorks = 1;
        
        public static ICharacterProperties Get(bool isNpc)
        {
            return new CharacterProperties(isNpc, InitialFartAmount, CharacterSpeed, InitialNumberOfCorks);
        }
    }
}