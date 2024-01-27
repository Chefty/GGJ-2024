using Character.Properties;
using UnityEngine;

namespace Character.View
{
    public interface ICharacterView
    {
        ICharacterProperties CharacterProperties { get; }
        void Fart(float amount);
        void Walk(Vector3 direction);
    }
}