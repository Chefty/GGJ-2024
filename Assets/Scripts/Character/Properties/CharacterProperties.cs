using UI;
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

        private float FartPercentage => FartAmount / 100f;
        public float FartAmount => fartAmount;
        private float fartAmount;

        private float maximiumFartAmount;
        private PlayerUIBehaviour playerUIBehaviour;

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
            playerUIBehaviour.UpdateNeedleRotation(fartAmount);
        }

        public void DecrementFart(float amount)
        {
            fartAmount -= amount;
            playerUIBehaviour.UpdateNeedleRotation(FartPercentage);
        }

        public void RemoveCork()
        {
            corks -= 1;
            playerUIBehaviour.OnUseCork();
        }

        public void ApplyCork()
        {
            IsCorked = true;
        }

        public void ResetApplyCork()
        {
            IsCorked = false;
        }

        public void InjectUI(PlayerUIBehaviour playerUIBehaviour)
        {
            this.playerUIBehaviour = playerUIBehaviour;
            playerUIBehaviour.gameObject.SetActive(true);
            
            LobbyManager.OnTransitionToGameMode += ResetPlayerUI;
        }

        private void ResetPlayerUI()
        {
            playerUIBehaviour.UpdateNeedleRotation(FartPercentage);
        }
    }
}