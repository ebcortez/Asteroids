using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidsGame {
	public class PlayerMovement : MonoBehaviour {
		[SerializeField] private PlayerManager playerManager;
		private Vector3 movement;
		private Vector2 minBounds, maxBounds;

		private void Start() {
			var screenBounds = GameManager.Instance.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));
			minBounds = new Vector2(-screenBounds.x, screenBounds.z);
			maxBounds = new Vector2(screenBounds.x, -screenBounds.z);
		}

		private void FixedUpdate() {
			playerManager.ShipRigidbody.AddRelativeForce(new Vector3(0, 0, movement.z) * playerManager.Speed * Time.fixedDeltaTime, ForceMode.Acceleration);
			playerManager.ShipRigidbody.MoveRotation(playerManager.ShipRigidbody.rotation * Quaternion.Euler(new Vector3(0, movement.x, 0) * playerManager.RotationSpeed * Time.fixedDeltaTime));
		}

		private void LateUpdate() {
			Vector3 shipPos = playerManager.ShipRigidbody.position;
			if(shipPos.x > maxBounds.x) shipPos.x = minBounds.x;
			if(shipPos.x < minBounds.x) shipPos.x = maxBounds.x;

			if (shipPos.z < minBounds.y) shipPos.z = maxBounds.y;
			if (shipPos.z > maxBounds.y) shipPos.z = minBounds.y;
			playerManager.ShipRigidbody.MovePosition(shipPos);
		}

		public void Move(InputAction.CallbackContext callbackContext) => movement = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);
	}
}