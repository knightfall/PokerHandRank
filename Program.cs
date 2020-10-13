using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokerHandSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<string> Pipe = new List<string>();
            while ((input = Console.ReadLine()) != null & input !="")
            {
                Pipe.Add(input);
            }

            Worker(Pipe.ToArray());
        }

        static void Worker(string[] hands)
        {
            var score = new int[hands.Length];

            for (var index = 0; index < hands.Length; index++)
            {
                var currentHand = hands[index].Replace(" ", "");

                var p1 = new Evaluator().GetHighestRank(currentHand.Substring(0, 10));
                var p2 = new Evaluator().GetHighestRank(currentHand.Substring(10, 10));
                if (p1[0] == p2[0])
                {
                    score[index] = ScoreManager.TieBreaker(p1[0], currentHand.Substring(0, 10),
                        currentHand.Substring(10, 10), p1[1], p2[1]);
                }
                else
                {
                    score[index] = ScoreManager.CalculateScore(p1[0], p2[0]);
                }
            }

            Console.WriteLine("Player 1: " + score.Count(x => x == 0));
            Console.WriteLine("Player 2: " + score.Count(x => x == 1));
        }
    }
}