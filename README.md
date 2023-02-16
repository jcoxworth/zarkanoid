# zarkanoid
Arkanoid Clone
zarkanoid
Arkanoid Clone by John Coxworth 2022/4/3

How to Play -Touch the screen or click the mouse to launch the ball. -Hit the bricks with the ball to destroy them. -After all the bricks are destroyed, you can advance to the next level. Click the large green button in the center to go to the next level. -The Player has 3 lives, plus the life he starts with. They are represented by red balls in the upper right corner.

Powerups -There are 3 different powerups: -Molten Ball: The ball is blazing hot and cuts through any block like a hot knife through butter. Currently works for 7 seconds. -Back Wall: A temporary wall appears below the player to stop the ball from going out. Also, the ball moves twice as fast. Be careful when the powerup time runs out! -Gun: The player can shoot little bullets at the bricks to destroy them

Levels -There are more than 3 levels. I wanted to draw backgrounds and make a storyline, but no time...

Score -When the player can get the ball to hit bricks multiple times before touching the paddle again, it builds up a chain score. This multiplies the score by the number of extra hits the player could get on the bricks. This encourages sending the ball behind a group of bricks where it can bounce around in an exciting way before returning to the player. -Besides chain score, the player gets points by simply destroying and hitting the bricks. -There is no time limit or time effect on the score.

Problems -Occasionally, the ball will bounce from side to side, while not moving much vertically. This means the player can't touch it for a while. I tried fixing it by creating a counter. The counter is on the ball and it counts how many times it hits anything besides bricks and the player. When that counter builds up, it starts moving the velocity vector to the downward direction. However, it doesn't work fast enough. I do think it's acceptable though. The ball doesn't get stuck for more than 10 seconds or so. -The player's paddle can send the ball upward by hitting it from behind. It's unrealistic, but as a player, I feel like I get a second chance and kind of enjoy the bug. I felt like I could save the ball at the last minute.
