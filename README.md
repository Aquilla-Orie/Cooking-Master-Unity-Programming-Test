# Cooking-Master-Unity-Programming-Test
Technical Assessment for Tentworks Interactive


## Update 1
Created a blank 2D project in Unity and set up two sprites (one red, one blue) as the two players

## Update 2
The game camera pans in and out by changing its orthographic size based off the distance between the players.
This was done by simply calculating the distance between the two players and adjusting the orthographic size
between two clamped values based off the distance.

## Update 3
<ul>
  <li>Created an abstract Player class that holds the core player logic, and two player classes (PlayerOne.cs, and PlayerTwo.cs):
For now, the children player classes only implement movement.
  <li>Updated camera script to allow smoother change in orthographic size.
  <li>Camera now lerps between current orthographic size and target orthographic size.
</ul>

## Update 4
Added vegetables, with different enum types. Player can interact with vegetable and add to collected vegetable queue.
<br>Player can no longer add veggie to queue if queue contains two veggies already or if player tries to pick the same veggie twice.

## Update 5
### Chopping Board
<ul>
  <li>Added two chopping boards.
  <li>Players can drop vegetables on the chopping board: Veggie type is added to stack on chopping board script in order to keep track of veggies.
  <li>While using the chopping board, player's movement is restricted: player move speed is set to 0.
  <li>Player can pickup vegetables from the chopping board: veggie stack is retrieved from the chopping board script to the player script
  <li>Seperate button for picking up prepared vegetables from the board. This helps reduce complexity in figuring out when the board is ready for pickup. By this, the player can pickup what ever is placed on the board at any time.
  <li>Board is limited to 3 vegetables per time. This variable can be adjusted if necessary.
</ul>

### Helper script
Implemented a helper script with the namespace Helpers. This contains helper methods that provide custom or in some cases, slightly optimized functionality.
<br>The script currently only implements a CloneStack() that clones a stack in order and returns the cloned stack.
