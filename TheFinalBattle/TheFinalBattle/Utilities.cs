using System;
using System.Collections.Generic;
using System.Threading;
using TheFinalBattle.CharacterClasses;
using TheFinalBattle.World;

namespace TheFinalBattle.Utilities
{
    public static class WorldBuilder
    {
        public static List<Party> Encounters = new List<Party>();

        public static void RunGame()
        {
            TrueProgrammer player = new TrueProgrammer();
            
            while (true)
            {
                while (true)
                {
                    Battle battle = new Battle(player);
                    battle.Enemies = Encounters[0];
                    Encounters.Remove(Encounters[0]);
                    while (true)
                    {
                        battle.BattleCycle();
                        if (battle.CheckForBattleOver())
                            break;
                    }
                    break;
                }

                if (CheckForGameOver(player))
                {
                    Narrator.GameLoss(player);
                    return;
                }
                else if (Encounters.Count == 0)
                {
                    Narrator.GameWin();
                    return;
                }
            }
            
        }

        public static void Populate()
        {
            Encounters.Add(new Party());
            Encounters.Add(new Party());
            Encounters.Add(new Party());
            Encounters[0].AddToParty(new Skeleton());
            Encounters[1].AddToParty(new Skeleton());
            Encounters[1].AddToParty(new Skeleton());
            Encounters[2].AddToParty(new UncodedOne());
        }

        public static bool CheckForGameOver(TrueProgrammer player)
        {
            return (player.DamageTaken >= player.HP);
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
            while (Console.KeyAvailable)
                Console.ReadKey(true);
            foreach (char c in input)
            {
                Console.Write(c);
                switch (c)
                {
                    case '.':
                        Thread.Sleep(85);
                        break;
                    case ',':
                        Thread.Sleep(65);
                        break;
                    case ' ':
                        Thread.Sleep(20);
                        break;
                    default:
                        Thread.Sleep(35);
                        break;
                }
            }
            Console.Write("\n");
        }
        public static void GameLoss(TrueProgrammer player) => TypeOut($"Despite the battle raging on every side, an oppresive silence fills the ears of {player}.\nYou slump to your knees as the color drains from your vision,\n    figures\n        turning to shapes\n            turning to blurs\n                turning to darkness.\nYou are dead.");

        public static void GameWin() => TypeOut($"An eerie still falls over the battlefield as the last foe falls to the dirt. As fast as the silence fell, a roar arises as your forces chear in victory. The foe is vanquished and peace is restored to the land.");
    }
}