using Character.Cork;
using Character.Fart;
using Character.Properties;
using Character.Walk;
using Game.GameWin;
using UnityEngine;

namespace Character.View
{
    public abstract class BaseCharacterView : MonoBehaviour, ICharacterView
    {
        public ICharacterProperties CharacterProperties => characterProperties ??= GetCharacterProperties();
        public Transform Transform => transform;
        private ICharacterProperties characterProperties;

        protected WalkAction walkAction { get; } = new();
        protected FartAction fartAction { get; } = new(new NoMoreFartWinCondition());
        protected CorkAction corkAction { get; } = new();

        public void StartFarting()
        {
            //Start the fart animation
        }

        public void Fart(float amount)
        {
            GameObject instance = (GameObject)Instantiate(Resources.Load("Fart Volume Fog"));
            instance.GetComponent<FartBehaviour>().IsFartBig = amount > GameConstants.DEFAULT_FART_VALUE;
            Debug.Log(amount);
            instance.transform.position = transform.position;
            instance.SetActive(true);
        }

        public void Walk(Vector3 direction)
        {
            transform.position += direction * CharacterProperties.Speed;
            transform.forward = direction;
        }

        protected abstract ICharacterProperties GetCharacterProperties();
    }
}
