using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsteroidsGame {
	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance => instance;

		public Camera MainCamera { get; private set; }
		public int Score { get; private set; }
		public Vector2 MinBounds { get; private set; }
		public Vector2 MaxBounds { get; private set; }

		[SerializeField] private GameplaySettings gameplaySettings;
		public GameplaySettings GameplaySettings => gameplaySettings;

		public UnityEvent scoreChanged;

		private void Awake() {
			instance = this;

			MainCamera = Camera.main;
		}

		private void Start() {
			AddScore(0);

			var screenBounds = GameManager.Instance.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, Screen.height));
			MinBounds = new Vector2(-screenBounds.x, screenBounds.z);
			MaxBounds = new Vector2(screenBounds.x, -screenBounds.z);
		}

		public void AddScore(int scoreToAdd) {
			Score += scoreToAdd;
			scoreChanged?.Invoke();
		}
	}
}