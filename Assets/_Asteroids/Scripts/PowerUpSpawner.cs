using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	public class PowerUpSpawner : MonoBehaviour {
		private float spawnRate;

		private void Start() {
			spawnRate = GameManager.Instance.GameplaySettings.PowerUpDefaultSpawnRate;
		}

		private void Update() {
			if (spawnRate > 0) {
				spawnRate -= Time.deltaTime;
			} else {
				var powerUp = PoolManager.Instance.GetObjectFromPool(PooledType.PowerUp);
				powerUp.transform.SetPositionAndRotation(new Vector3(Random.Range(GameManager.Instance.MinBounds.x, GameManager.Instance.MaxBounds.x), 0, Random.Range(GameManager.Instance.MinBounds.y, GameManager.Instance.MaxBounds.y)), Quaternion.identity);
				spawnRate = GameManager.Instance.GameplaySettings.PowerUpDefaultSpawnRate;
			}
		}
	} 
}
