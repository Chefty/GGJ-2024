using System;
using System.Collections.Generic;
using System.Linq;
using Character.View;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameWin
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        public void DisplayWinners(List<PlayerCharacterView> players)
        {
            var ordered = players.OrderByDescending(x => x.CharacterProperties.FartAmount).ToList();

            var text = $"Player {ordered[0].UserId} is the winner!\n\n";
            for (int i = 1; i < ordered.Count; i++)
            {
                var p = ordered[i];
                text += $"Player {p.UserId} still had {p.CharacterProperties.FartAmount}%\n";
            }

            winnerText.text = text;
        }
        
        private void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}