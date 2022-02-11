using System;
using System.Collections.Generic;
using System.Threading;

Skeleton PunchingBag = new Skeleton();
Skeleton PokeDoll = new Skeleton();
Console.Title = "The Final Battle - a Game by ian6ft5";
TrueProgrammer player = new TrueProgrammer();
player.TeamUp(new Skeleton());
player.TeamUp(new Skeleton());
Battle TestBattle = new Battle(player);
TestBattle.Enemies.AddToParty(new Skeleton());
TestBattle.Enemies.AddToParty(new Skeleton());

player.SetTarget(PunchingBag);
foreach (Character character in player.Allies.Members)
{
    character.SetTarget(PunchingBag);
}

foreach (Character character in TestBattle.Enemies.Members)
{
    character.SetTarget(PokeDoll);
}

int turnCount = 0;
while (true)
{
    turnCount++;
    TestBattle.PlayTurn();
    TestBattle.PlayEnemyTurn();
    if (turnCount % 3 == 0)
        Console.Clear();
    Console.WriteLine($"Your team has dealt {PunchingBag.DamageTaken} damage.\nThe enemy team has dealt {PokeDoll.DamageTaken} damage.");
}

public class Battle
{
    public TrueProgrammer PlayerCharacter { get; init; }
    public Party Enemies { get; set; } = new Party();

    public Battle(TrueProgrammer player)
    { PlayerCharacter = player; }


    public void PlayTurn()
    {
        Console.WriteLine($"It is {PlayerCharacter.Name}'s turn.");
        PlayerCharacter.Act();
        foreach (Character character in PlayerCharacter.Allies.Members)
        {
            Console.WriteLine($"It is {character.Name}'s turn.");
            character.Act();
        }
    }
    public void PlayEnemyTurn()
    {
        foreach (Character character in Enemies.Members)
        {
            Console.WriteLine($"It is the enemy {character.Name}'s turn;");
            character.Act();
        }
    }
}

public class Character
{
    public string Name { get; init; }
    public int HP { get; init; }
    public int DamageTaken { get; set; }
    public Character CurrentTarget { get; protected set; }
    public virtual void Act() => DoNothing();
    public void DoNothing()
    {
        Narrator.NarrateAction(this, false, $"{Name} just stood there.");
    }

    public void SetTarget(Character newTarget)
    {
        CurrentTarget = newTarget;
    }

    public void TakeDamage(int i ) => DamageTaken += i;
}


public class Skeleton : Character
{
    public Skeleton()
    {
        Name = "Skeleton";
        HP = 5;
        DamageTaken = 0;
    }

    public override void Act() => BoneCrunch();
    public void BoneCrunch()
    {
        Random r = new Random();
        int i = r.Next(2);
        CurrentTarget.TakeDamage(i);
        Narrator.NarrateAction(this, false, $"{Name} attacked with Bone Crunch. It dealt {i} damage to {CurrentTarget.Name}.");
    }
}

public class TrueProgrammer : Character
{
    public Party Allies = new Party();

    public TrueProgrammer()
    {
        Console.WriteLine("You are the True Programmer. What is your name?");
        Name = Console.ReadLine();
        Console.Clear();
        HP = 25;
    }

    public override void Act()
    {
        Console.WriteLine("Choose your action.\n    1 - Attack\n    2 - Do nothing");
        ConsoleKey input = Console.ReadKey(true).Key;
        switch (input)
        {
            case ConsoleKey.NumPad1:
            case ConsoleKey.D1:
                Punch();
                break;
            default:
                DoNothing();
                break;
        }

    }

    public void Punch()
    {
        CurrentTarget.TakeDamage(1);
        Narrator.NarrateAction(this, true, $"{Name} threw a strong punch. {Name} dealt 1 damage to {CurrentTarget.Name}.");
    }

    public void TeamUp(Character newAlly)
    {
        Allies.AddToParty(newAlly);
    }
}

public record Party
{
    public List<Character> Members = new List<Character>();

    public void AddToParty(Character newMember)
    {
        Members.Add(newMember);
    }
}

public static class Narrator
{
    public static void NarrateAction(Character actor, bool player, string action)
    {
        if (!player)
        {
            Thread.Sleep(1750);
            TypeOut(action);
            Thread.Sleep(750);
        }
        else
        {
            TypeOut(action);
            Thread.Sleep(750);
        }
    }

    public static void TypeOut(string input)
    {
        foreach (char c in input)
        {
            Console.Write(c);
            switch (c)
            {
                case '.':
                    Thread.Sleep(150);
                    break;
                case ',':
                    Thread.Sleep(115);
                    break;
                case ' ':
                    Thread.Sleep(35);
                    break;
                default:
                    Thread.Sleep(50);
                    break;            
            }
        }
        Console.Write("\n");
    }
}