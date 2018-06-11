Feature: SnakeAndLadderFeature
	In order to ensure that the Snake and the ladder
	in the game do correct job of biting and escalating 
	the player.

@mytag
Scenario: The Snake bits
	Given I have the player who landed at the snake head	
	When The snake bites
	Then the player positions ends up to be the snake tail
