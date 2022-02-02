using System;
using System.Collections.Generic;
using System.Threading;

public class Character
{
    public string Name { get; set; }
}

public class Skeleton : Character
{
    public Skeleton()
    {
        Name = "Skeleton";
    }
    public void DoNothing()
    {
        Thread.Sleep(3000);
        Console.WriteLine("");
    }
}

public class Party
{
    List<Character> Characters = new List<Character>();
    public bool IsPlayer { get; set; }

    public Party(bool x)
    {
        IsPlayer = x;
    }

    public void AddToParty(Character newMember)
    {
        Characters.Add(newMember);
    }
}