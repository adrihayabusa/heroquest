HeroQuest

Version 0.1

Console Version - Unity project in future

Fill this document with all information related to the app, considering that
the players knowns a bit about the original game.

Steps:
- Create all the classes to handle data correctly.

This is a long process but once it's done, almost half of the job will be so.
At the moment, I mostly need to associate the players to the board, and the
ennemies (monsters), the traps, doors... well everything. I can construct a
board but I didn't tied the data such like Player, Ennemy and all into the
Tiles.

- Need to handle the game start, loop and game over

If every Player dies during the quest, if they complete the objectives, or if
they want to quit: Game Over.

Until then, I need to add a loop to keep playing and change players turn by 
turn and then the Ennemy (the master who is handling the creatures).

- Be able to move

Hard one, I need to create an algorithm based on the Tiles (especially the walls and doors)
and to make a player or a monster moving through the Tiles. For this, I've
got the coordinates X and Y, specified in a class Position for each Entity.

One thing I need to consider is managing the walls:
* For a tile, you can have up to two walls. They are listed in an Enum class,
like Left, Right, Top, Bottom, LeftTop, TopRight...
I need to, somehow, manage that during the move so a player or a monster can't move
through it if he wants to.

- Implement the algorithms for the interactions

The monsters and players (heroes) needs to be able to fight each other.

A hero need to be able to open a Chest too, to interact with an object, or
with a door.

- Define an objective for a quest

A quest must have an objective, and this objective will determine the
confition to finish the game when cmpleted. For example, if every monster
is dead, or if you manage to take a treasure, etc.

Then once the Objective is completed, you have to go back to the stairs.

- Complete the spells effects

Some spells are quite specific, like pushing ennemies around you for 1 or more tiles.
For this, I've created an interface and classes but It needs to be completed.

- Chaos Spells?

I need to see about that

- Draw the board

I can do it but it's ugly af. The best would be to draw it nicely, with a
library like GD for PHP. I need to think about that, or just display
the room where the player is and the Tile.

Maybe, for the beginning, I should consider 1 or more walls = 1 Tile. Same for
doors, and so if someone goes on a Tile with a door, he will cross it and be on the other side.

Like : x = 2, y = 2 The player is at 3,2, and he want to go out of the Room. So he goes on the
tile at 2,2 and he will be at 1,2. Make sense. Easy, and then later with Unity I'll see what
to do. If I go this way, I may add a property like Direction to the Door, and say "Left : right" and 
if the Door is on a horizontal wall (from a view perspective like above the board) then It'll be
Top : Bottom, and the player would go out or in.

I'll see.

And I think that's it.

It will require lot of debug, and refactoring.

Once everything is completed for a console version, I'll switch to Unity. Slowly as I need to
learn how to handle the tool.

GO!