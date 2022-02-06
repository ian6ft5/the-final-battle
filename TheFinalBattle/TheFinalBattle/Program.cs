using System;
using System.Collections.Generic;
using System.Threading;

Console.Title = "The Final Battle - a Game by ian6ft5";
TrueProgrammer player = new TrueProgrammer();
Battle TestBattle = new Battle();
TestBattle.Parties[0].AddToParty(player);
TestBattle.Parties[0].AddToParty(new Skeleton());
TestBattle.Parties[1].AddToParty(new Skeleton());

int turnCount = 0;
while (true)
{
    for (int i = 0; i < TestBattle.Parties.Count; i++)
    {
        turnCount++;
        TestBattle.PlayTurn(i);
        Console.WriteLine("\n");
    }
    if (turnCount % 3 == 0)
        Console.Clear();
}

public class Battle
{
    public List<Party> Parties = new List<Party>();

    public Battle()
    {
        Parties.Add(new Party(true));
        Parties.Add(new Party(false));
    }

    public void PlayTurn(int party)
    {
        foreach (ICharacter character in Parties[party].Characters)
        {
            if (!Parties[party].IsPlayer)
            {
                Console.WriteLine($"It's the enemy {character.Name}'s turn.");
                character.Act(false);
            }
            else
            {
                Console.WriteLine($"It is {character.Name}'s turn.");
                Console.ReadKey(true);
                character.Act(true);
            }
        }
    }
}
public interface ICharacter
{
    public string Name { get; init; }
    public void Act(bool isPlayer);
}

public class Skeleton : ICharacter
{
    public string Name { get; init; }
    public Skeleton()
    {
        Name = "Skeleton";
    }

    public void Act(bool isPlayer)
    {
        DoNothing(isPlayer);
    }
    public void DoNothing(bool isPlayer)
    {
        if (!isPlayer)
        {
            Thread.Sleep(1750);
            Console.WriteLine($"{Name} just stood there.");
            Thread.Sleep(750);
        }
        else
        {
            Console.WriteLine($"{Name} just stood there.");
            Thread.Sleep(750);
        }
    }
}

public class TrueProgrammer : ICharacter
{
    public string Name { get; init; }

    public TrueProgrammer()
    {
        Console.WriteLine("You are the True Programmer. What is your name?");
        Name = Console.ReadLine();
        Console.Clear();
    }

    public void Act(bool isPlayer)
    {
        DoNothing();
    }

    public void DoNothing()
    {
        Console.WriteLine($"{Name} just stood there.");
        Thread.Sleep(750);
    }
}

public class Party
{
    public List<ICharacter> Characters = new List<ICharacter>();
    public bool IsPlayer { get; set; }

    public Party(bool x)
    {
        IsPlayer = x;
    }

    public void AddToParty(ICharacter newMember)
    {
        Characters.Add(newMember);
    }
}