using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidsGame {
	public class PlayerMovement : MonoBehaviour {
		[SerializeField] private PlayerManager playerManager;
		private Vector3 movement;

		private void FixedUpdate() {
			playerManager.ShipRigidbody.AddRelativeForce(new Vector3(0, 0, movement.z) * GameManager.Instance.GameplaySettings.PlayerSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
			playerManager.ShipRigidbody.MoveRotation(playerManager.ShipRigidbody.rotation * Quaternion.Euler(new Vector3(0, movement.x, 0) * GameManager.Instance.GameplaySettings.PlayerRotationSpeed * Time.fixedDeltaTime));
		}

		private void LateUpdate() {
			Vector3 shipPos = playerManager.ShipRigidbody.position;
			if(shipPos.x > GameManager.Instance.MaxBounds.x) shipPos.x = GameManager.Instance.MinBounds.x;
			if(shipPos.x < GameManager.Instance.MinBounds.x) shipPos.x = GameManager.Instance.MaxBounds.x;

			if (shipPos.z < GameManager.Instance.MinBounds.y) shipPos.z = GameManager.Instance.MaxBounds.y;
			if (shipPos.z > GameManager.Instance.MaxBounds.y) shipPos.z = GameManager.Instance.MinBounds.y;
			playerManager.ShipRigidbody.MovePosition(shipPos);
		}

		public void Move(InputAction.CallbackContext callbackContext) => movement = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);
	}
}