using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AsteroidsGame.AsteroidsEditor {
	[CustomEditor(typeof(PowerUpData))]
	public class PowerUpDataCustomEditor : Editor {
		public override void OnInspectorGUI() {
			var powerupData = (PowerUpData)target;

			DrawDefaultInspector();

			switch (powerupData.PowerUpType) {
				case PowerUpType.Shield:
					powerupData.ShieldAmount = EditorGUILayout.IntField("Amount of Damage to Absorb: ", powerupData.ShieldAmount);
					break;
				case PowerUpType.CrescentBullet:
					powerupData.CrescentBulletAmmo = EditorGUILayout.IntField("Crescent Bullet Amount: ", powerupData.CrescentBulletAmmo);
					break;
				case PowerUpType.AdditionalHealth:
					powerupData.AdditionalHealthAmount = EditorGUILayout.IntField("Additional Health Amount: ", powerupData.AdditionalHealthAmount);
					break;
				case PowerUpType.ScoreMultiplier:
					powerupData.ScoreMultiplier = EditorGUILayout.IntField("Score Multiplier: ", powerupData.ScoreMultiplier);
					powerupData.Duration = EditorGUILayout.FloatField("Duration (Seconds): ", powerupData.Duration);
					break;
				case PowerUpType.AdditionalScore:
					powerupData.AdditionalScore = EditorGUILayout.IntField("Additional Score Amount: ", powerupData.AdditionalScore);
					break;
			}

		}
	}
}