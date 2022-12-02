using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids {
	public class Bullet : MonoBehaviour {
		[SerializeField] private float speed;

		private Rigidbody myRigidbody;
		private GameObject myGameObject;
		private Transform myTransform;

		public float Speed => speed;

		public Rigidbody Rigidbody => myRigidbody;
		public Transform Transform => myTransform;

		private void Awake() {
			myRigidbody = GetComponent<Rigidbody>();
			myGameObject = gameObject;
			myTransform = transform;
		}

		private void OnBecameInvisible() {
			myGameObject.SetActive(false);
		}

		private void OnDisable() {
			myRigidbody.Sleep();
		}

	}
}