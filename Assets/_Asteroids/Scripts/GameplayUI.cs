using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace AsteroidsGame {
	public class GameplayUI : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI scoreText;
		[SerializeField] private TextMeshProUGUI lifeText;
		[SerializeField] private TextMeshProUGUI shieldText;
		[SerializeField] private TextMeshProUGUI crescentBulletAmmoText;
		[SerializeField] private PlayerManager playerManager;
		[SerializeField] private GameObject gameOver;
		[SerializeField] private TextMeshProUGUI gameOverScoreText;



		private void OnEnable() {
			GameManager.Instance.scoreChanged.AddListener(UpdateScore);
			playerManager.healthChanged.AddListener(health => UpdateLife(health));
			playerManager.shieldHealthChanged.AddListener(shield => UpdateShield(shield));
			playerManager.crescentBulletAmmoChanged.AddListener(crescentBulletAmmo => UpdateCrescentBulletAmmo(crescentBulletAmmo));
		}

		private void OnDisable() {
			GameManager.Instance.scoreChanged.RemoveListener(UpdateScore);
			playerManager.healthChanged.RemoveListener(UpdateLife);
		}

		public void UpdateScore() {
			scoreText.text = gameOverScoreText.text = $"Score: {GameManager.Instance.Score}";
		}

		public void UpdateLife(int currentLife) {
			lifeText.text = $"Life: {currentLife}";
			if(currentLife <= 0) gameOver.SetActive(true);
		}

		public void UpdateShield(int currentShield) {
			shieldText.enabled = currentShield > 0;
			shieldText.text = $"Shield: {currentShield}";
		}

		public void UpdateCrescentBulletAmmo(int currentAmmo) {
			crescentBulletAmmoText.enabled = currentAmmo > 0;
			crescentBulletAmmoText.text = $"Crescent Bullet Ammo: {currentAmmo}";
		}
		
		public void ResetGame() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	} 
}
