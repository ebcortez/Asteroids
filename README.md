# Asteroids


## MainGameplaySettings.asset
Access the following through the MainGameplaySettings.asset ScriptableObject located at Assets/_Asteroids/</br>
### Player Data:
• Player Health: Default player health at start.</br>
• Player Speed: Player forward movement speed.</br>
• Player Rotation Speed: Player rotation speed on the y-axis.</br>
• Player Default Fire Rate</br>
• Player Burst Angle: Angle of the normal bullets from the center of the player</br>

### Asteroid Data:
• Asteroid Default Life: This sets the maximum possible health of asteroids when spawned. This also handles the initial scale.</br>
• Asteroid Destroy Force: This is the force of the asteroid's remnants when the asteroid's health is reduced.</br>

### Asteroid Spawn Data:
• Asteroid Default Spawn Rate: Initial spawn rate of asteroids.</br>
• Asteroid Minimum Spawn Rate: Ensures that the default spawn rate stays above zero when decaying.</br>
• Asteroid Spawn Rate Decay: Decay rate of the default spawn rate.</br>
• Asteroid Minimum and Maximum Spawn Force: Random spawn force to move towards the screen when spawned.</br>

### Power Up Spawn Data:
• Power Up Default Spawn Rate: Initial spawn rate of power ups.</br>

</br>

## Power Ups Data
Power Ups Data are ScriptableObjects located at Assets/_Asteroids/Data/Power Ups</br>
