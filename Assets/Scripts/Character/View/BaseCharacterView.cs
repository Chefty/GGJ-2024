using Character.Cork;
using Character.Fart;
using Character.Properties;
using Character.Walk;
using DG.Tweening;
using Game.GameWin;
using UnityEngine;
using UnityEngine.UI;

namespace Character.View
{
    public abstract class BaseCharacterView : MonoBehaviour, ICharacterView
    {
        public ICharacterProperties CharacterProperties => characterProperties ??= GetCharacterProperties();
        public Transform Transform => transform;
        private ICharacterProperties characterProperties;

        [SerializeField] protected Animator characterAnimator;
        [SerializeField] private RectTransform corkImage;

        protected WalkAction walkAction { get; } = new();
        protected FartAction fartAction { get; } = new(new NoMoreFartWinCondition());
        protected CorkAction corkAction { get; } = new();

        protected virtual void Awake()
        {
            corkImage.localScale = Vector3.zero;
        }
        
        public void StartFarting()
        {
            characterAnimator.SetBool("Hold Fart", true);
        }

        public void Fart(float amount)
        {
            GameObject instance = (GameObject)Instantiate(Resources.Load("Fart Volume Fog 2"));
            instance.GetComponent<FartBehaviour>().IsFartBig = amount > GameConstants.DEFAULT_FART_VALUE;
            instance.transform.position = transform.position;
            instance.SetActive(true);
            characterAnimator.SetBool("Hold Fart", false);
        }

        public virtual void Walk(Vector3 direction)
        {
            transform.position += direction * CharacterProperties.Speed;
            transform.forward = direction;
        }

        public void OnCorked()
        {
            corkImage.DOScale(1, 0.5f).SetEase(Ease.OutBack);
            characterAnimator.SetTrigger("Corked");
        }

        public void OnUncorked()
        {
            corkImage.DOScale(0, 0.5f).SetEase(Ease.InBack);
        }

        protected abstract ICharacterProperties GetCharacterProperties();
    }
}
