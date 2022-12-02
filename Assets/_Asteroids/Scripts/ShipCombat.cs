using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids {
	public class ShipCombat : MonoBehaviour {
		[SerializeField] private Transform muzzle;
		[SerializeField] private float defaultFireRate = 0.25f;
		private float fireRate;

		private bool isFired;

		private void Update() {
			if (isFired) {
				if(fireRate <= 0) {
					fireRate = defaultFireRate;
					NormalFire();
				} else {
					fireRate -= Time.deltaTime;
				}
			}
		}

		public void NormalFire() {
			var bullet1 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();
			var bullet2 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();
			var bullet3 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();

			bullet1.Transform.SetPositionAndRotation(muzzle.position, Quaternion.identity);
			bullet2.Transform.SetPositionAndRotation(muzzle.position, Quaternion.Euler(0, muzzle.localEulerAngles.y + 45, 0));
			bullet3.Transform.SetPositionAndRotation(muzzle.position, Quaternion.Euler(0, muzzle.localEulerAngles.y - 45, 0));

			var bulletRigidbody = bullet1.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet1.Transform.TransformDirection(muzzle.forward) * bullet1.Speed, ForceMode.Impulse);

			bulletRigidbody = bullet2.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet2.Transform.TransformDirection(muzzle.forward) * bullet2.Speed, ForceMode.Impulse);

			bulletRigidbody = bullet3.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet3.Transform.TransformDirection(muzzle.forward) * bullet3.Speed, ForceMode.Impulse);
		}

		public void FireInput(InputAction.CallbackContext callbackContext) {
			switch (callbackContext.phase) {
				case InputActionPhase.Performed:
					isFired = true;
					fireRate = 0;
					break;
				default:
					isFired = false;
					fireRate = defaultFireRate;
					break;
			}
		}
	}
}