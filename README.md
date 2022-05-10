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

## Update 6
### Customer Interaction With Player
Created CustomerManager that handles spawning customers to tables and removes them when they are done.
<br>Customer spawning and destruction are done by button presses at the moment, but work perfectly.
<br>Player can submit food to table, which is then received and inspected by the customer. Matching orders are rewarded, and the serving player is penalized for mismatched orders.
<br>Customer timer starts counting as soon as the customer is settled on the table. Depending on the type of order, the time varies (smaller orders require shorter periods of time, longer orders, more time).
If the time runs out before the customer is served, both players are penalized.
<br>Had some design issues implementing the score system: Player references the ScoreManager to compute the score and recieves the value calculated. I had to simplify and move some of the logic to the PlayerBase class because the dependencies were becoming troubling. The downside is that there is some 'tight' coupling in the logic.
<br>I might have to implement the same with the UIManager when I implement it; only the most general functionalities would be handled by the UIManager (like displaying player scores, time, ui prompts), other more specific UI elements like veggies displaying their type, and customer orders printed on the table would be handled by the individual classes. This is to prevent passing a lot of objects around that can otherwise be handled by the class holding such data.

### Helper Script
Two new helper methods have been added to the Helper script:
<ol>
  <li>public static T RandomEnumValue<T>() which returns a single random value from an enumeration of type T
  <li>public static T[] GetDistinctEnumValues<T>(int size) which returns an array of distinct values from an enumeration
</ol>

## Update 7
### Bonus and Pickups
Added bonus and pickups which the player can walk over to collect. If the player serves the customer within 70% of the custome's waiting time, a bonus is dropped. This could either be speed, score or time bonus.

### UI
<ol>
<li>UI has been updated. Plates now carry the letter of the vegetable that is on them. The chopping board also shows the combination of vegetables that are added on them.
<li>The player has UI indications of what vegetable he is carrying, and another that shows whether or not he has picked up a plate from the chopping board to serve the customer.
<li>Slider on the player's head to indicate that he is using the chopping board
<li>Help Menu displays when any player presses the 'H' Key
<li>Gameover menu added after both players run out of time.
</ol>
