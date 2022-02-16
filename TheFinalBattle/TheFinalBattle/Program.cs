using System;
using TheFinalBattle.Utilities;


Console.Title = "The Final Battle - a Game by ian6ft5";

WorldBuilder.Populate();
WorldBuilder.RunGame();

/*TrueProgrammer player = new TrueProgrammer();
player.TeamUp(new Skeleton());
player.TeamUp(new Skeleton());
Battle TestBattle = new Battle(player);
TestBattle.Enemies.AddToParty(new Skeleton());
TestBattle.Enemies.AddToParty(new Skeleton());

int turnCount = 0;
while (true)
{
    turnCount++;
    TestBattle.PlayTurn();
    TestBattle.PlayEnemyTurn();
    if (turnCount % 3 == 0)
        Console.Clear();
}*/





