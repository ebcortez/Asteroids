using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AsteroidsGame {
	public class GameplayUI : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI scoreText;
		[SerializeField] private TextMeshProUGUI lifeText;
		[SerializeField] private PlayerManager playerManager;

		private void OnEnable() {
			GameManager.Instance.scoreChanged.AddListener(UpdateScore);
			playerManager.healthChanged.AddListener(health => UpdateLife(health));
		}

		private void OnDisable() {
			GameManager.Instance.scoreChanged.RemoveListener(UpdateScore);
			playerManager.healthChanged.RemoveListener(UpdateLife);
		}

		public void UpdateScore() {
			scoreText.text = $"Score: {GameManager.Instance.Score}";
		}

		public void UpdateLife(int currentLife) {
			lifeText.text = $"Life: {currentLife}";
		}
	} 
}
