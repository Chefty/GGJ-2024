using UnityEngine;

namespace Character.Properties
{
    public class CharacterProperties : ICharacterProperties
    {
        public bool IsNpc { get; }
        public float Speed { get; }
        
        public bool IsCorked { get; private set; }

        public bool HasCork => Corks > 0;
        public int Corks => corks;
        private int corks;
        
        public float FartAmount => fartAmount;
        private float fartAmount;

        private float maximiumFartAmount;

        public CharacterProperties(bool isNpc, float fartAmount, float speed, int corks)
        {
            IsNpc = isNpc;
            Speed = speed;
            this.fartAmount = maximiumFartAmount = fartAmount;
            this.corks = corks;
        }

        public void IncrementFart(float amount)
        {
            fartAmount = Mathf.Min(fartAmount + amount, maximiumFartAmount);
        }

        public void DecrementFart(float amount)
        {
            fartAmount -= amount;
        }

        public void RemoveCork()
        {
            corks -= 1;
        }

        public void ApplyCork()
        {
            IsCorked = true;
        }

        public void ResetApplyCork()
        {
            IsCorked = false;
        }
    }
}