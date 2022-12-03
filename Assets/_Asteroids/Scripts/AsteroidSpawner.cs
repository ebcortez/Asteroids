using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class AsteroidSpawner : MonoBehaviour {
		private float spawnRate, defaultSpawnRate;
		private Transform[] spawnPointTransforms;
		private Transform playerTransform;

		private void Start() {
			playerTransform = FindObjectOfType<PlayerManager>().transform;
			defaultSpawnRate = GameManager.Instance.GameplaySettings.AsteroidDefaultSpawnRate;

			var spawnPoints = new Vector2[8];
			spawnPoints[0] = new Vector2(GameManager.Instance.MinBounds.x, GameManager.Instance.MinBounds.y) * 1.25f;
			spawnPoints[1] = new Vector2(GameManager.Instance.MaxBounds.x, GameManager.Instance.MinBounds.y) * 1.25f;
			spawnPoints[2] = new Vector2(GameManager.Instance.MinBounds.x, GameManager.Instance.MaxBounds.y) * 1.25f;
			spawnPoints[3] = new Vector2(GameManager.Instance.MaxBounds.x, GameManager.Instance.MaxBounds.y) * 1.25f;
			spawnPoints[4] = new Vector2(0, GameManager.Instance.MinBounds.y) * 1.25f;
			spawnPoints[5] = new Vector2(GameManager.Instance.MinBounds.x, 0) * 1.25f;
			spawnPoints[6] = new Vector2(0, GameManager.Instance.MaxBounds.y) * 1.25f;
			spawnPoints[7] = new Vector2(GameManager.Instance.MaxBounds.x, 0) * 1.25f;

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
				asteroid.Rigidbody.AddRelativeForce(asteroid.Transform.forward * Random.Range(GameManager.Instance.GameplaySettings.AsteroidMinimumSpawnForce, GameManager.Instance.GameplaySettings.AsteroidMaximumSpawnForce), ForceMode.VelocityChange);
				spawnRate = defaultSpawnRate;
			} else {
				spawnRate -= Time.deltaTime;
			}

			if(defaultSpawnRate > GameManager.Instance.GameplaySettings.AsteroidMinimumSpawnRate) {
				defaultSpawnRate -= Time.deltaTime * (GameManager.Instance.GameplaySettings.AsteroidSpawnRateDecayRate / 100);
			} else {
				defaultSpawnRate = GameManager.Instance.GameplaySettings.AsteroidMinimumSpawnRate;
			}
		}
	}
}