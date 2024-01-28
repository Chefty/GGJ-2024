using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private BoxCollider _gameArea;
    [SerializeField] private BoxCollider _lobbyArea;

    private Bounds _playAreaBounds;
    private Bounds _lobbyBounds;

    public static AreaManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Debug.LogError("There are several instances of AreaManager in the scene.", gameObject);

        _playAreaBounds = _gameArea.bounds;
        _lobbyBounds = _lobbyArea.bounds;
        _gameArea.enabled = false;
        _lobbyArea.enabled = false;
    }

    public Vector3 GetRandomPositionInLobby() => GetRandomPositionInBounds(_lobbyBounds);
    public Vector3 GetRandomPositionInPlayArea() => GetRandomPositionInBounds(_playAreaBounds);

    public static Vector3 GetRandomPositionInBounds(Bounds bound)
    {
        return new Vector3(Random.Range(bound.min.x, bound.max.x), Random.Range(bound.min.y, bound.max.y), Random.Range(bound.min.z, bound.max.z));
    }

    public bool IsPositionInBounds(Vector3 position)
    {
        return _gameArea.bounds.Contains(position);
    }
}
