# Homebound
![screenshot](screenshot.png)
[Read More About Homebound64](https://portfolium.com/entry/homebound-64)

**Homebound 64** is an homage to the never-released Earthbound 64 game by Nintendo. Homebound64 revolves around vilagers of a mysterious town that find themselves under attack from an unknown assailant. Help the villagers whenever you can, trading items and interacting with villagers to figure out why villagers have been disappearing and how you found yourself in this village in the first place.

Additionally, I developed the core gameplay mechanics including player movement, collision-based interactions (NPC's, level changing, fishing), and cinematic sequences in the form of a start screen and NPC cutscenes.The scipts above are core gameplay scripts used to move and animate the character, as well as change scenes via trigger colliders to paint the world as a series of interconnected environments.

**Human.cs**: Reads input values from user (via controller or keyboard) and relays that information to designated Character.cs class attached to our main player. Other values sent to Character are camera rotational values to rotate player to face the same direction as the camera. Values are also read-in from the Character to disable certain actions when Character is in particular states such as fishing or interacting with NPCs.

**Character.cs**: The main script that moves the player, values sent in from Human are used to move the player via Unity's CharacterController component as well as rotate the character to face the same direction as camera during a fishing cast and fishing game.

**Door.cs**: Used to change scenes via attached trigger. This script takes in the name of the scene you want to transition to when the player touches the trigger and stores the last known rotation to make the player face the proper direction upon entering the new scene.

**Interaction.cs**: Similar to Door, this uses a trigger collider injuction with player input to make the Character enter an interaction state with the GameObject (NPC) this script is attached to. When the Character is in the trigger and the player presses the Interaction Button, the Cinemachine camera is enabled and the proper targets are set so the camera focuses on to the Character and the NPC. Dialogue UI Menus are also enabled, allowing the player to interact with the NPC or to exit the interaction.

