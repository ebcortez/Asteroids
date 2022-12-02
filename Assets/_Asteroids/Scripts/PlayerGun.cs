using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids {
	public class PlayerGun : MonoBehaviour {
		[SerializeField] private Transform muzzle;
		[SerializeField] private float defaultFireRate = 0.25f;
		private float fireRate;

		private bool isFired;

		private void Update() {
			if (isFired) {
				if(fireRate <= 0) {
					fireRate = defaultFireRate;
					NormalFire();
				}
			}
			if(fireRate > 0) {
				fireRate -= Time.deltaTime;
			}
		}

		public void NormalFire() {
			var bullet1 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();
			var bullet2 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();
			var bullet3 = PoolManager.Instance.GetObjectFromPool(0).GetComponent<Bullet>();

			var burstAngle = 30;

			bullet1.Transform.SetPositionAndRotation(muzzle.position, Quaternion.LookRotation(muzzle.forward));
			bullet2.Transform.SetPositionAndRotation(muzzle.position, Quaternion.LookRotation(muzzle.forward) * Quaternion.Euler(0, muzzle.localEulerAngles.y + burstAngle, 0));
			bullet3.Transform.SetPositionAndRotation(muzzle.position, Quaternion.LookRotation(muzzle.forward) * Quaternion.Euler(0, muzzle.localEulerAngles.y - burstAngle, 0));

			var bulletRigidbody = bullet1.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet1.Transform.forward * bullet1.Speed, ForceMode.Impulse);

			bulletRigidbody = bullet2.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet2.Transform.forward * bullet2.Speed, ForceMode.Impulse);

			bulletRigidbody = bullet3.Rigidbody;
			bulletRigidbody.AddRelativeForce(bullet3.Transform.forward * bullet3.Speed, ForceMode.Impulse);
		}

		public void FireInput(InputAction.CallbackContext callbackContext) {
			isFired = callbackContext.phase == InputActionPhase.Performed;
		}
	}
}