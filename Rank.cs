using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandSorter
{
    static class Rank
    {
        public const int RoyalFlush = 10;
        public const int StraightFlush = 9;
        public const int FourOfAKind = 8;
        public const int FullHouse = 7;
        public const int Flush = 6;
        public const int Straight = 5;
        public const int ThreeOfAKind = 4;
        public const int TwoPairs = 3;
        public const int Pair = 2;
        public const int HighCard = 1;
        public const int Negative = -10;
    }
}