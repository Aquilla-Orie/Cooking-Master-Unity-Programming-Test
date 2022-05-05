# Cooking-Master-Unity-Programming-Test
Technical Assessment for Tentworks Interactive


## Update 1
Created a blank 2D project in Unity and set up two sprites (one red, one blue) as the two players

## Update 2
The game camera pans in and out by changing its orthographic size based off the distance between the players.
This was done by simply calculating the distance between the two players and adjusting the orthographic size
between two clamped values based off the distance.

## Update 3
Created an abstract Player class that holds the core player logic, and two player classes (PlayerOne.cs, and PlayerTwo.cs).
For now, the children player classes only implement movement.
Updated camera script to allow smoother change in orthographic size.
Camera now lerps between current orthographic size and target orthographic size.

## Update 4
Added vegetables, with different enum types. Player can interact with vegetable and add to collected vegetable queue.
Player can no longer add veggie to queue if queue contains two veggies already or if player tries to pick the same veggie twice.
