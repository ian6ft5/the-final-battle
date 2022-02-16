using System;
using System.Collections.Generic;
using TheFinalBattle.World;
using TheFinalBattle.Utilities;

namespace TheFinalBattle.CharacterClasses
{
    public class Character
    {
        public string Name { get; init; }
        public int HP { get; init; }
        public int DamageTaken { get; set; }
        public Character CurrentTarget { get; protected set; }
        public override string ToString()
        {
            return Name;
        }
        public virtual void Act(Battle battle, bool ally) => DoNothing();
        public virtual void DoNothing()
        {
            Narrator.NarrateAction(this, false, $"{Name} just stood there.");
        }

        public void SetTarget(Character newTarget)
        {
            CurrentTarget = newTarget;
        }
        public void RandomTarget(Battle battle, bool ally)
        {
            Random r = new Random();
            if (ally)
            {
                SetTarget(battle.Enemies.Members[r.Next(battle.Enemies.Members.Count)]);
            }
            else
            {
                int targetIndex = r.Next(battle.PlayerCharacter.Allies.Members.Count + 1);
                if (targetIndex == 0)
                {
                    SetTarget(battle.PlayerCharacter);
                }
                else
                {
                    SetTarget(battle.PlayerCharacter.Allies.Members[targetIndex - 1]);
                }
            }
        }
        public void TakeDamage(int i) => DamageTaken += i;

        public void DamageReport()
        {
            if (CurrentTarget.DamageTaken >= CurrentTarget.HP)
            {
                return;
            } else
            Narrator.TypeOut($"{CurrentTarget.Name} is now at {CurrentTarget.HP - CurrentTarget.DamageTaken}/{CurrentTarget.HP} HP.");
        }

        public virtual void DeathKnell()
        {
            Console.WriteLine($"{Name} has fallen in battle.");
        }
    }


    public class Skeleton : Character
    {
        public Skeleton()
        {
            Name = "Skeleton";
            HP = 5;
            DamageTaken = 0;
        }

        
        public override void DeathKnell()
        {
            Narrator.TypeOut($"The magic energies holding together the {Name}'s bones falter and fail, and it falls down into a pile of calcified dust.");
        }

        public override void Act(Battle battle, bool ally)
        {
            RandomTarget(battle, ally);
            BoneCrunch();
            DamageReport();
        }
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
            Narrator.TypeOut("You are the True Programmer. What is your name?");
            Name = Console.ReadLine();
            Console.Clear();
            HP = 25;
        }

        public void Act(Battle battle)
        {
            Narrator.TypeOut("Choose your action.");
            Console.WriteLine("    1 - Attack\n    2 - Do nothing");
            ConsoleKey input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ChooseTarget(battle);
                    Punch();
                    break;
                default:
                    SetTarget(battle.Enemies.Members[0]);
                    DoNothing();
                    break;
            }
            DamageReport();
        }

        public override void DoNothing()
        {
            Narrator.NarrateAction(this, true, $"{Name} just stood there.");
        }

        private void ChooseTarget(Battle battle)
        {
            Narrator.TypeOut("Choose your target:");
            for (int i = 0; i < battle.Enemies.Members.Count; i++)
            {
                Console.WriteLine($"    {i + 1} - {battle.Enemies.Members[i].Name}");
            }
            ConsoleKey input = Console.ReadKey(true).Key;
            if (!Int32.TryParse(input.ToString(), out _))
            {
                SetTarget(battle.Enemies.Members[0]);
            }
            else
            {
                for (int i = 0; i < battle.Enemies.Members.Count; i++)
                {
                    if (Convert.ToInt32(input.ToString()) == i + 1)
                    {
                        SetTarget(battle.Enemies.Members[i]);
                        break;
                    }
                }
            }

        }

        private void Punch()
        {
            CurrentTarget.TakeDamage(3);
            Narrator.NarrateAction(this, true, $"{Name} threw a strong punch. {Name} dealt 3 damage to {CurrentTarget.Name}.");
        }

        public void TeamUp(Character newAlly)
        {
            Allies.AddToParty(newAlly);
        }
    }

    public class UncodedOne : Character
    {
        public UncodedOne()
        {
            Name = "The Uncoded One";
            HP = 15;
        }

        public override void Act(Battle battle, bool player)
        {
            RandomTarget(battle, player);
            Unravel();
            DamageReport();
        }

        public void Unravel()
        {
            Random r = new Random();
            int dam = r.Next(3);
            CurrentTarget.TakeDamage(dam);
            Narrator.NarrateAction(this, true, $"{Name} cast Unravel. {Name} dealt {dam} damage to {CurrentTarget.Name}.");
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
}