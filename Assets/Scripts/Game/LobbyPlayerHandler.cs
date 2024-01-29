using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Character.View;
using System.Diagnostics.Tracing;

public class LobbyPlayerHandler : MonoBehaviour
{
    public static event Action OnAllPlayersReady;
    
    private Dictionary<Transform, bool> _playerInputToPlayerTransform;
    private List<TMPro.TMP_Text> _labels;
    private Color[] playerColors = new Color[]{
        GameConstants.PLAYER1_COLOR,
        GameConstants.PLAYER2_COLOR,
        GameConstants.PLAYER3_COLOR,
        GameConstants.PLAYER4_COLOR,
    };

    private void Awake()
    {
        LobbyManager.OnTransitionToGameMode += TransitionToGame;
        Ready.OnPlayerAwake += RegisterPlayer;
        Ready.OnPlayerPressedReady += OnPlayerPressedReady;
        _playerInputToPlayerTransform = new Dictionary<Transform, bool>();
        _labels = new List<TMPro.TMP_Text>();
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
        player.name = string.Concat("P", _playerInputToPlayerTransform.Count);
        Vector3 positionInLobby = AreaManager.Instance.GetRandomPositionInLobby();
        player.position = new Vector3(positionInLobby.x, 0f, positionInLobby.z);
        var playerCharacterView = player.GetComponent<PlayerCharacterView>();
        //TODO fix 3D text color misfit to pass playerColors as param here 
        playerCharacterView.SetPlayerInfoText(true, player.name);
        playerCharacterView.RegisterPlayerData();
    }

    private void MovePlayersToLevel()
    {
        foreach (var player in _playerInputToPlayerTransform)
        {
            Vector3 positionInPlayArea = AreaManager.Instance.GetRandomPositionInPlayArea();
            player.Key.position = new Vector3(positionInPlayArea.x, 0f, positionInPlayArea.z);
        }

        foreach (var label in _labels)
        {
            Destroy(label.gameObject);
        }
        _playerInputToPlayerTransform.Clear();
        _labels.Clear();

    }
}
