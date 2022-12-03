using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsGame {
	[CreateAssetMenu(fileName = "New Power Up Data", menuName = "Asteroids/Create Power Up")]
	public class PowerUpData : ScriptableObject {
		[SerializeField] private string powerUpName;
		[SerializeField] private PowerUpType powerUpType;
		[SerializeField] private Color playerColorEffect;

		public int ShieldAmount { get; set; }

		public int CrescentBulletAmmo { get; set; }

		public int AdditionalHealthAmount { get; set; }

		public int ScoreMultiplier { get; set; }

		public int AdditionalScore { get; set; }

		public float Duration { get; set; }

		public string PowerUpName => powerUpName;
		public PowerUpType PowerUpType => powerUpType;
		public Color PlayerColorEffect => playerColorEffect;
	}
}