using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class PowerUp : MonoBehaviour {
		[SerializeField] private PowerUpData powerUpData;

		public PowerUpData PowerUpData => powerUpData;

		private void OnEnable() {
			Invoke("AutoDisable", 20f);
		}

		private void OnTriggerEnter(Collider other) {
			if(other.TryGetComponent(out PlayerManager playerManager)) {
				playerManager.ActivatePowerUp(this);
			}
		}

		private void AutoDisable() {
			gameObject.SetActive(false);
		}
	} 
}
