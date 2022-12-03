using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class Bullet : MonoBehaviour {
		[SerializeField] private float speed;

		private GameObject myGameObject;

		public float Speed => speed;

		public Rigidbody Rigidbody { get; private set; }
		public Transform Transform { get; private set; }

		private void Awake() {
			Rigidbody = GetComponent<Rigidbody>();
			myGameObject = gameObject;
			Transform = transform;
		}

		private void OnDisable() {
			Rigidbody.Sleep();
			Transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		}

		private void OnTriggerEnter(Collider other) {
			myGameObject.SetActive(false);
		}

		private void OnBecameInvisible() {
			myGameObject.SetActive(false);
		}
	}
}