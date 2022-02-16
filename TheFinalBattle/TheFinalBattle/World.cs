using System;
using System.Collections.Generic;
using TheFinalBattle.CharacterClasses;
using TheFinalBattle.Utilities;

namespace TheFinalBattle.World
{
    public class Battle
    {
        public TrueProgrammer PlayerCharacter { get; init; }
        public Party Enemies { get; set; } = new Party();

        public Battle(TrueProgrammer player)
        { PlayerCharacter = player; }

        public void BattleCycle()
        {
            while (true)
            {
                PlayTurn();
                if (CheckForBattleOver())
                    break;
                PlayEnemyTurn();
                if (CheckForBattleOver())
                    break;
            }
        }

        public void PlayTurn()
        {
            Console.WriteLine($"It is {PlayerCharacter.Name}'s turn.");
            PlayerCharacter.Act(this);
            foreach (Character character in PlayerCharacter.Allies.Members)
            {
                Narrator.TypeOut($"It is {character.Name}'s turn.");
                character.Act(this, true);
            }
            CheckForDeaths(false);
        }
        public void PlayEnemyTurn()
        {
            foreach (Character character in Enemies.Members)
            {
                Narrator.TypeOut($"It is the enemy {character.Name}'s turn.");
                character.Act(this, false);
            }
            CheckForDeaths(true);
        }

        public void Target(Character target)
        {
            PlayerCharacter.SetTarget(target);
        }

        public void Target(Character actor, Character target)
        {
            actor.SetTarget(target);
        }

        public void CheckForDeaths(bool player)
        {
            if (player)
            {
                List<int> toRemove = new List<int>();
                for (int i = 0; i < PlayerCharacter.Allies.Members.Count; i++)
                {
                    Character check = PlayerCharacter.Allies.Members[i];
                    if (check.DamageTaken >= check.HP)
                    {
                        check.DeathKnell();
                        toRemove.Insert(0, i);
                    }
                }
                foreach (int i in toRemove)
                {
                    PlayerCharacter.Allies.Members.Remove(PlayerCharacter.Allies.Members[i]);
                }
            }
            else
            {
                List<int> toRemove = new List<int>();
                for (int i = 0; i < Enemies.Members.Count; i++)
                {
                    Character check = Enemies.Members[i];
                    if (check.DamageTaken >= check.HP)
                    {
                        check.DeathKnell();
                        toRemove.Insert(0, i);
                    }
                }
                foreach (int i in toRemove)
                {
                    Enemies.Members.Remove(Enemies.Members[i]);
                }
            }
        }

        public bool CheckForBattleOver()
        {
            //player lose
            if (WorldBuilder.CheckForGameOver(PlayerCharacter))
            {
                return true;
            }
            //player win
            else if (Enemies.Members.Count == 0)
                return true;
            else return false;
        }
    }


}
