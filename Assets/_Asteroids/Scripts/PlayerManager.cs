using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class PlayerManager : MonoBehaviour {
		[SerializeField] private Renderer playerRenderer;

		private Color defaultColor;
		private int shieldHealth;

		public int CurrentHealth { get; private set; }
		public int CrescentBulletAmmo { get; set; }

		public UnityEngine.Events.UnityEvent<int> healthChanged;
		public UnityEngine.Events.UnityEvent<int> crescentBulletAmmoChanged;
		public UnityEngine.Events.UnityEvent<int> shieldHealthChanged;

		public Rigidbody ShipRigidbody { get; private set; }

		private void Awake() {
			ShipRigidbody = GetComponent<Rigidbody>();
			defaultColor = playerRenderer.material.color;
			
			CurrentHealth = GameManager.Instance.GameplaySettings.PlayerHealth;

			crescentBulletAmmoChanged.AddListener(ammo => {
				if(ammo <= 0) {
					ChangeColor(defaultColor);
				}
			});

			shieldHealthChanged.AddListener(shield => {
				if (shield <= 0) {
					ChangeColor(defaultColor);
				}
			});

			healthChanged?.Invoke(CurrentHealth);
			shieldHealthChanged?.Invoke(shieldHealth);
			crescentBulletAmmoChanged?.Invoke(CrescentBulletAmmo);

		}

		public void ActivatePowerUp(PowerUp powerUp) {
			switch (powerUp.PowerUpData.PowerUpType) {
				case PowerUpType.CrescentBullet:
					CrescentBulletAmmo = powerUp.PowerUpData.CrescentBulletAmmo;
					shieldHealth = 0;
					shieldHealthChanged?.Invoke(shieldHealth);
					crescentBulletAmmoChanged?.Invoke(CrescentBulletAmmo);
					ChangeColor(powerUp.PowerUpData.PlayerColorEffect);
					break;
				case PowerUpType.Shield:
					shieldHealth = powerUp.PowerUpData.ShieldAmount;
					CrescentBulletAmmo = 0;
					shieldHealthChanged?.Invoke(shieldHealth);
					crescentBulletAmmoChanged?.Invoke(CrescentBulletAmmo);
					ChangeColor(powerUp.PowerUpData.PlayerColorEffect);
					break;
			}
			powerUp.gameObject.SetActive(false);
		}

		public void TakeDamage() {
			if (shieldHealth > 0) {
				shieldHealth--;
				shieldHealthChanged?.Invoke(shieldHealth);
				ChangeColor(defaultColor);
				return;
			}
			if (--CurrentHealth > 0) {
				ShipRigidbody.Sleep();
				ShipRigidbody.MoveRotation(Quaternion.identity);
				ShipRigidbody.MovePosition(Vector3.zero);
			} else {
				gameObject.SetActive(false);
			}

			healthChanged?.Invoke(CurrentHealth);
		}

		public void ChangeColor(Color color) {
			playerRenderer.material.color = color;
		}
	}
}