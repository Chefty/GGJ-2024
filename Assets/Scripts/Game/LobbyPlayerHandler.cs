using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerHandler : MonoBehaviour
{
    private Dictionary<Transform, bool> _playerInputToPlayerTransform;

    public static event Action OnAllPlayersReady;

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
        Ready.OnPlayerAwake += RegisterPlayer;
        Ready.OnPlayerPressedReady += RegisterPlayer;
        _playerInputToPlayerTransform = new Dictionary<Transform, bool>();
    }

    private void OnDestroy()
    {
        LobbyManager.OnTransitionToGameMode -= TransitionToGame;
        Ready.OnPlayerAwake -= RegisterPlayer;
    }

    public void OnPlayerPressedReady(Transform player)
    {
        if (!_playerInputToPlayerTransform.ContainsKey(player)) RegisterPlayer(player);
        _playerInputToPlayerTransform[player] = !_playerInputToPlayerTransform[player];
        CheckIfAllPlayersReady();
    }

    public void RegisterPlayer(Transform player)
    {
        Debug.Log($"<color=green>RegisterPlayer {player.gameObject.name}</color>");
        _playerInputToPlayerTransform.Add(player, false);
    }

    public void UnRegisterPlayer(Transform player)
    {
        Debug.Log($"<color=red>UnRegisterPlayer {player.gameObject.name}</color>");

        _playerInputToPlayerTransform.Remove(player);
    }

    private void CheckIfAllPlayersReady()
    {
        bool allPlayersReady = true;
        foreach (var isReady in _playerInputToPlayerTransform.Values)
        {
            if (!isReady) allPlayersReady = false;
        }

        if (allPlayersReady) OnAllPlayersReady?.Invoke();
    }

    public void TransitionToGame()
    {
        StartCoroutine(DoTransitionToGameMode());
    }

    private IEnumerator DoTransitionToGameMode()
    {
        yield return new WaitForSeconds(LobbyManager.fadeDuration / 2f);
        MovePlayersToLevel();
    }

    private void MovePlayersToLevel()
    {
        foreach (var player in _playerInputToPlayerTransform)
        {
            // TODO move all the players here if needed otherwise remove this function + subscription
        }
    }
}
