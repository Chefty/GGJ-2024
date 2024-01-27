using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PlayerUIBehaviour : MonoBehaviour
    {
        [SerializeField] private RectTransform _gaugeNeedle;
        [SerializeField] private GameObject _cork;
        [SerializeField] private float secondsToRotate = 0.5f;
        private readonly Vector2 _gaugeEmptyToFullRotation = new Vector2(0f, -90f);

        private float currentPercentage = 0f;

        // an example
        [ContextMenu("Add10Percent")]
        private void Add10Percent()
        {
            UpdateNeedleRotation(currentPercentage + 0.1f);
        }

        public void OnUseCork()
        {
            _cork.SetActive(false);
        }

        public void UpdateNeedleRotation(float gaugePercentage)
        {
            var targetRotation = Mathf.Lerp(_gaugeEmptyToFullRotation.x, _gaugeEmptyToFullRotation.y, gaugePercentage);
            _gaugeNeedle.DOLocalRotate(new Vector3(0, 0, targetRotation), secondsToRotate).SetEase(Ease.InOutBounce);
            currentPercentage = gaugePercentage;
        }
    }
}