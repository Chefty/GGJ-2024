using Character.Properties;
using UnityEngine;

namespace Character.View
{
    public abstract class BaseCharacterView : MonoBehaviour, ICharacterView
    {
        public ICharacterProperties CharacterProperties => characterProperties ??= GetCharacterProperties();
        private ICharacterProperties characterProperties;
        
        public void Fart(float amount)
        {
            throw new System.NotImplementedException();
        }

        public void Walk(Vector2 direction)
        {
            
        }

        protected abstract ICharacterProperties GetCharacterProperties();
    }
}
