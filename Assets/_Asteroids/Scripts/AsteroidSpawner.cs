using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class AsteroidSpawner : MonoBehaviour {
		[SerializeField] private float defaultSpawnRate = 2.5f, minimumSpawnRate = 1f;
		[SerializeField, Range(1, 100)] private float spawnRateDecayRate = 50;
		[SerializeField, Range(5, 15)] private int minimumAsteroidForce;
		[SerializeField, Range(5, 15)] private int maximumAsteroidForce;
		private float spawnRate;
		private Transform[] spawnPointTransforms;
		private Vector2 minBounds, maxBounds;
		private Transform playerTransform;

		private void Awake() {
			if(minimumAsteroidForce > maximumAsteroidForce) minimumAsteroidForce = maximumAsteroidForce;
		}

		private void Start() {
			playerTransform = FindObjectOfType<PlayerManager>().transform;

			var screenBounds = GameManager.Instance.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));
			minBounds = new Vector2(-screenBounds.x, screenBounds.z);
			maxBounds = new Vector2(screenBounds.x, -screenBounds.z);

			var spawnPoints = new Vector2[8];
			spawnPoints[0] = new Vector2(minBounds.x, minBounds.y) * 1.25f;
			spawnPoints[1] = new Vector2(maxBounds.x, minBounds.y) * 1.25f;
			spawnPoints[2] = new Vector2(minBounds.x, maxBounds.y) * 1.25f;
			spawnPoints[3] = new Vector2(maxBounds.x, maxBounds.y) * 1.25f;
			spawnPoints[4] = new Vector2(0, minBounds.y) * 1.25f;
			spawnPoints[5] = new Vector2(minBounds.x, 0) * 1.25f;
			spawnPoints[6] = new Vector2(0, maxBounds.y) * 1.25f;
			spawnPoints[7] = new Vector2(maxBounds.x, 0) * 1.25f;

			spawnPointTransforms = new Transform[8];
			for (int i = 0; i < spawnPoints.Length; i++) {
				Vector2 spawnPoint = spawnPoints[i];
				var spawnerTransform = new GameObject().transform;
				spawnerTransform.SetPositionAndRotation(new Vector3(spawnPoint.x, 0, spawnPoint.y), Quaternion.identity);
				spawnerTransform.LookAt(Vector3.zero);
				spawnPointTransforms[i] = spawnerTransform;
			}
		}

		private void Update() {
			if(spawnRate <= 0) {
				var asteroid = PoolManager.Instance.GetObjectFromPool(1).GetComponent<Asteroid>();
				var spawnPointTransform = spawnPointTransforms[Random.Range(0, spawnPointTransforms.Length)];
				asteroid.SetDefaultLife();
				asteroid.Transform.SetPositionAndRotation(spawnPointTransform.position, spawnPointTransform.rotation);
				asteroid.Transform.LookAt(new Vector3(playerTransform.position.x, 0, playerTransform.position.z) + Random.insideUnitSphere * 2);
				asteroid.Rigidbody.AddRelativeForce(asteroid.Transform.forward * Random.Range(minimumAsteroidForce, maximumAsteroidForce), ForceMode.VelocityChange);
				spawnRate = defaultSpawnRate;
			} else {
				spawnRate -= Time.deltaTime;
			}

			if(defaultSpawnRate > minimumSpawnRate) {
				defaultSpawnRate -= Time.deltaTime * (spawnRateDecayRate / 100);
			} else {
				defaultSpawnRate = minimumSpawnRate;
			}
		}
	}
}