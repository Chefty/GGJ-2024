using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LobbyPlayerHandler : MonoBehaviour
{
    private Dictionary<Transform, bool> _playerInputToPlayerTransform;

    public static event Action OnAllPlayersReady;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
        Ready.OnPlayerAwake += RegisterPlayer;
        Ready.OnPlayerPressedReady += OnPlayerPressedReady;
        _playerInputToPlayerTransform = new Dictionary<Transform, bool>();
    }

    private void OnDestroy() => UnsubscribeToEvents();

    private void UnsubscribeToEvents()
    {
        LobbyManager.OnTransitionToGameMode -= TransitionToGame;
        Ready.OnPlayerAwake -= RegisterPlayer;
        Ready.OnPlayerPressedReady -= OnPlayerPressedReady;
    }

    public void OnPlayerPressedReady(Transform player)
    {
        if (!_playerInputToPlayerTransform.ContainsKey(player)) RegisterPlayer(player);
        _playerInputToPlayerTransform[player] = !_playerInputToPlayerTransform[player];
        CheckIfAllPlayersReady();
    }

    public void RegisterPlayer(Transform player)
    {
        _playerInputToPlayerTransform.Add(player, false);
        SetupPlayerForLobby(player);
    }

    public void UnRegisterPlayer(Transform player)
    {
        _playerInputToPlayerTransform.Remove(player);
    }

    private void CheckIfAllPlayersReady()
    {
        bool allPlayersReady = _playerInputToPlayerTransform.Where(x => !x.Value).Count() == 0;
        if (allPlayersReady) OnAllPlayersReady?.Invoke();
    }

    public void TransitionToGame()
    {
        UnsubscribeToEvents();
        StartCoroutine(DoTransitionToGameMode());
    }

    private IEnumerator DoTransitionToGameMode()
    {
        yield return new WaitForSeconds(LobbyManager.fadeDuration / 2f);
        MovePlayersToLevel();
    }

    private void SetupPlayerForLobby(Transform player)
    {
        Vector3 positionInLobby = AreaManager.Instance.GetRandomPositionInLobby();
        player.position = new Vector3(positionInLobby.x, 2f, positionInLobby.z);
    }

    private void MovePlayersToLevel()
    {
        foreach (var player in _playerInputToPlayerTransform)
        {
            Vector3 positionInPlayArea = AreaManager.Instance.GetRandomPositionInPlayArea();
            player.Key.position = new Vector3(positionInPlayArea.x, 2f, positionInPlayArea.z);
        }
    }
}
