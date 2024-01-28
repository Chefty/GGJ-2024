using System.Collections;
using UnityEngine;

public class LobbyElevatorScript : MonoBehaviour
{
    public float moveDistance;
    public float maxDistance;
    public float beginDistance = 0f;
    [SerializeField] private float _timeBetweenScrolls;
    [SerializeField] private TMPro.TMP_Text _text;
    private readonly string _defaultDisplayText = "Press start to get ready";
    private RectTransform _rect;

    void Start()
    {
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
        StartCoroutine(DisplayCountdown());
    }

    private IEnumerator LedDisplayBehaviour()
    {
        _text.fontSize = 1.6f;
        _text.text = _defaultDisplayText + _defaultDisplayText + _defaultDisplayText;

        while (true)
        {
            MoveText();
            yield return new WaitForSeconds(_timeBetweenScrolls);
        }
    }

    private void MoveText()
    {
        Debug.Log(_rect.anchoredPosition.x);
        _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x - moveDistance, _rect.anchoredPosition.y);

        if (_rect.anchoredPosition.x > maxDistance)
        {
            _rect.anchoredPosition = new Vector2(beginDistance, _rect.anchoredPosition.y);
        }
    }

    private IEnumerator DisplayCountdown()
    {
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
