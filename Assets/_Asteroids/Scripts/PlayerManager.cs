using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class PlayerManager : MonoBehaviour {
		[SerializeField] private int health = 3;
		[SerializeField] private float speed = 500;
		[SerializeField] private float rotationSpeed = 250;
		[SerializeField, Range(0.25f, 1f)] private float defaultFireRate = 0.25f;
		[SerializeField, Range(30, 60)] private float burstAngle = 30f;

		public int Health => health;
		public float Speed => speed;
		public float RotationSpeed => rotationSpeed;
		public float DefaultFireRate => defaultFireRate;
		public float BurstAngle => burstAngle;

		public UnityEngine.Events.UnityEvent<int> healthChanged;

		public Rigidbody ShipRigidbody { get; private set; }

		private void Awake() {
			ShipRigidbody = GetComponent<Rigidbody>();
			UpdateHealth();
		}

		public void UpdateHealth() {
			healthChanged?.Invoke(health);
		}

		public void TakeDamage() {
			if(--health > 0) {
				ShipRigidbody.Sleep();
				ShipRigidbody.MoveRotation(Quaternion.identity);
				ShipRigidbody.MovePosition(Vector3.zero);
			} else {
				gameObject.SetActive(false);
			}

			UpdateHealth();
		}
	}
}