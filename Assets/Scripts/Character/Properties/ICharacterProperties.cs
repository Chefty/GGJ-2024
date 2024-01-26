namespace Character.Properties
{
    public interface ICharacterProperties
    {
        bool IsNpc { get; }
        float Speed { get; }
        float FartAmount { get; }
        bool IsCorked { get; }
        bool HasCork { get; }
        int Corks { get; }
        
        void IncrementFart (float amount);
        void DecrementFart (float amount);
        void RemoveCork();
        void ApplyCork();
        void ResetApplyCork();
    }
}