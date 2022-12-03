using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class Asteroid : MonoBehaviour {
		private int life;
		private GameObject myGameObject;
		private IEnumerator CO_WaitToDisable;

		public int Life => life;
		public Rigidbody Rigidbody { get; private set; }
		public Transform Transform { get; private set; }


		private void Awake() {
			Rigidbody = GetComponent<Rigidbody>();
			myGameObject = gameObject;
			Transform = transform;
		}

		private void OnDisable() {
			Rigidbody.Sleep();
			if (CO_WaitToDisable != null) StopCoroutine(CO_WaitToDisable);
		}

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out Bullet bullet)) {
				TakeDamage();
			} 
		}

		private void OnCollisionEnter(Collision collision) {
			if (collision.collider.TryGetComponent(out PlayerManager playerManager)) {
				playerManager.TakeDamage();
				var direction = collision.contacts[0].point - Transform.position;
				Rigidbody.AddForce((-direction.normalized - Rigidbody.velocity) * 1.5f, ForceMode.VelocityChange);
			}
		}

		private void OnBecameVisible() {
			if(!myGameObject.activeInHierarchy) return;
		}

		private void OnBecameInvisible() {
			if(!myGameObject.activeInHierarchy) return;
			if (CO_WaitToDisable == null) CO_WaitToDisable = WaitToDisable();
			StartCoroutine(CO_WaitToDisable);
		}

		public void SetDefaultLife() => SetLife(Random.Range(1, GameManager.Instance.GameplaySettings.AsteroidDefaultLife+1));

		public void SetLife(int life) {
			this.life = life;
			Transform.localScale = Vector3.one * life;
			Rigidbody.mass = life;
		}

		public void TakeDamage() {
			if (--life > 0) {
				myGameObject.SetActive(false);

				var asteroid1 = PoolManager.Instance.GetObjectFromPool(1).GetComponent<Asteroid>();
				var asteroid2 = PoolManager.Instance.GetObjectFromPool(1).GetComponent<Asteroid>();

				asteroid1.Transform.SetPositionAndRotation(Transform.position, Quaternion.identity);
				asteroid2.Transform.SetPositionAndRotation(Transform.position, Quaternion.identity);

				asteroid1.SetLife(life);
				asteroid2.SetLife(life);

				asteroid1.Transform.SetPositionAndRotation(Transform.position + (new Vector3(Random.onUnitSphere.x, 0, Random.onUnitSphere.z) * 2), Quaternion.identity);
				asteroid2.Transform.SetPositionAndRotation(Transform.position + (new Vector3(Random.onUnitSphere.x, 0, Random.onUnitSphere.z) * 2), Quaternion.identity);

				asteroid1.Rigidbody.AddRelativeForce(new Vector3(Random.onUnitSphere.x, 0, Random.onUnitSphere.z) * GameManager.Instance.GameplaySettings.AsteroidDestroyForce, ForceMode.VelocityChange);
				asteroid2.Rigidbody.AddRelativeForce(new Vector3(Random.onUnitSphere.x, 0, Random.onUnitSphere.z) * GameManager.Instance.GameplaySettings.AsteroidDestroyForce, ForceMode.VelocityChange);
			} else {
				GameManager.Instance.AddScore(1);
				myGameObject.SetActive(false);
			}
		}

		public IEnumerator WaitToDisable(float delay = 5f) {
			yield return new WaitForSecondsRealtime(delay);
			myGameObject.SetActive(false);
		}
	}
}