using System.Collections;
using UnityEngine;

namespace UI
{
    public class PlayerUIBehaviour : MonoBehaviour
    {
        [SerializeField] private RectTransform _gaugeNeedle;
        [SerializeField] private float _rotationSpeed;
        private readonly Vector2 _gaugeEmptyToFullRotation = new Vector2(-90f, 0f);

        private float currentPercentage = 0f;

        [ContextMenu("Add10Percent")]
        private void Add10Percent()
        {
            UpdateNeedleRotation(currentPercentage + 0.1f);
        }

        public void UpdateNeedleRotation(float gaugePercentage)
        {
            StartCoroutine(UpdateNeedle(gaugePercentage));
            currentPercentage = gaugePercentage;
        }

        private IEnumerator UpdateNeedle(float gaugePercentage) // percentage between 0 and 1f
        {
            float targetRotation = Mathf.Lerp(_gaugeEmptyToFullRotation.x, _gaugeEmptyToFullRotation.y, gaugePercentage);
            float ellapsedTime = 0f;

            while (ellapsedTime < _rotationSpeed)
            {
                _gaugeNeedle.rotation = Quaternion.Euler(_gaugeNeedle.eulerAngles.x, _gaugeNeedle.eulerAngles.y, Mathf.Lerp(_gaugeNeedle.eulerAngles.z, targetRotation, ellapsedTime / _rotationSpeed));
                yield return new WaitForEndOfFrame();
                ellapsedTime += Time.deltaTime;
            }
        }
    }
}