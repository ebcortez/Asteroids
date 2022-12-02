using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids {
	public class PlayerMovement : MonoBehaviour {
		[SerializeField] private float speed;
		[SerializeField] private float rotationSpeed;

		private Rigidbody shipRigidbody;
		private Vector3 movement;

		private void Awake() => shipRigidbody = GetComponent<Rigidbody>();

		private void FixedUpdate() {
			shipRigidbody.AddRelativeForce(new Vector3(0, 0, movement.z) * speed * Time.fixedDeltaTime);
			//shipRigidbody.AddTorque(new Vector3(0, movement.x, 0) * rotationSpeed * Time.fixedDeltaTime);
			shipRigidbody.MoveRotation(shipRigidbody.rotation * Quaternion.Euler(new Vector3(0, movement.x, 0) * rotationSpeed * Time.fixedDeltaTime));
		}

		public void Move(InputAction.CallbackContext callbackContext) => movement = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);
	}
}