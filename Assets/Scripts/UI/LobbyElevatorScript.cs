using System.Collections;
using System.Linq;
using UnityEngine;

public class LobbyElevatorScript : MonoBehaviour
{
    public float moveDistance;
    public float maxDistance;
    public float beginDistance = 0f;
    [SerializeField] private float _timeBetweenScrolls;
    [SerializeField] private TMPro.TMP_Text _text;
    private readonly string _defaultDisplayText = "Press start to get ready.  ";
    private RectTransform _rect;
    private bool _updateDisplay;

    void Start()
    {
        _updateDisplay = true;
        LobbyManager.OnStartCountDown += UpdateElevatorCountdown;
        _rect = _text.GetComponent<RectTransform>();
        beginDistance = _rect.anchoredPosition.x;
        StartCoroutine(LedDisplayBehaviour());
    }

    void OnDestroy()
    {
        LobbyManager.OnStartCountDown -= UpdateElevatorCountdown;
    }

    private void UpdateElevatorCountdown()
    {
        _updateDisplay = false;
        StartCoroutine(DisplayCountdown());
    }

    private IEnumerator LedDisplayBehaviour()
    {
        _text.fontSize = 1.6f;
        _text.text = _defaultDisplayText;

        while (_updateDisplay)
        {
            MoveText();
            yield return new WaitForSeconds(_timeBetweenScrolls);
        }
    }

    private void MoveText()
    {
        _text.text = string.Join("", _text.text.Skip(1)) + _text.text[0];
    }

    private IEnumerator DisplayCountdown()
    {
        _text.alignment = TMPro.TextAlignmentOptions.CenterGeoAligned;
        _text.fontSize = 1.6f;
        _text.text = "READY ?";
        yield return new WaitForSeconds(1f);
        _text.fontSize = 2.8f;
        _text.text = "3";
        yield return new WaitForSeconds(1f);
        _text.text = "2";
        yield return new WaitForSeconds(1f);
        _text.text = "1";
        yield return new WaitForSeconds(1f);
        _text.fontSize = 1.6f;
        _text.text = "Play !";
        Destroy(gameObject, LobbyManager.lobbyAnimationDuration / 2f);
    }
}
