using Character.Cork;
using Character.Fart;
using Character.Properties;
using Character.Walk;
using Game.GameWin;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.View
{
    public abstract class BaseCharacterView : MonoBehaviour, ICharacterView
    {
        public ICharacterProperties CharacterProperties => characterProperties ??= GetCharacterProperties();
        private ICharacterProperties characterProperties;

        protected WalkAction walkAction = new();
        protected FartAction fartAction = new(new NoMoreFartWinCondition());
        protected CorkAction corkAction = new();
        
        public void Fart(float amount)
        {
            Debug.Log("Prout: " + amount);
            //Play sound, animation and start delay for fart fog
        }

        public void Walk(Vector3 direction)
        {
            transform.position += direction * CharacterProperties.Speed;
        }

        protected abstract ICharacterProperties GetCharacterProperties();
    }
}
