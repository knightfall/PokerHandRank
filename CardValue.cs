using System.Collections.Generic;
using System.Linq;

namespace PokerHandSorter
{
    internal static class CardValue
    {
        public static int[] CardFlushCompositeValuesRank = new int[]
        {
            31367009,14535931,6678671,2800733,1062347,323323,85085,15015,2310
        };
        //public static readonly int[] CardFourKindCompositeValuesRank = new int[]
        //{
        //    (int) Math.Pow(41, 4),(int) Math.Pow(37, 4),(int) Math.Pow(31, 4),(int) Math.Pow(29, 4),(int) Math.Pow(23, 4),(int) Math.Pow(19, 4),
        //    (int) Math.Pow(17, 4),(int) Math.Pow(13, 4),(int) Math.Pow(11, 4),(int) Math.Pow(7, 4),(int) Math.Pow(5, 4),(int) Math.Pow(3, 4),(int) Math.Pow(2, 4)
        //};
        public static Dictionary<string, int> CardPrimeValue = new Dictionary<string, int>
        {
            {"A", 41}, {"K", 37}, {"Q", 31}, {"J", 29},{"T", 23}, {"9", 19}, {"8", 17}, {"7", 13},{"6", 11}, {"5", 7}, {"4", 5}, {"3", 3}, {"2", 2}
        };
        public static Dictionary<string, int> CardSuite = new Dictionary<string, int>
        {
            {"D", 0}, {"H", 1}, {"S", 2}, {"C", 3}
        };

        public static (int[], int[]) ConvertHand(string cards)
        {
            int[] values = new int[5];
            int[] suites = new int[5];
            int cardCount = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i % 2 == 0)
                {
                    values[cardCount] = CardPrimeValue[cards[i].ToString()];
                }
                else
                {
                    suites[cardCount] = CardSuite[cards[i].ToString()];
                    cardCount++;
                }
            }
            return (values, suites);
        }

        public static int[] ConvertHandValue(string cards)
        {
            int[] values = new int[5];
            int[] suites = new int[5];
            int cardCount = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (i % 2 == 0)
                {
                    values[cardCount] = CardPrimeValue[cards[i].ToString()];
                }
                else
                {
                    suites[cardCount] = CardSuite[cards[i].ToString()];
                    cardCount++;
                }
            }
            return values;
        }

        public static int[] RemoveValues(int[] cardvalues, int valueToRemove )
        {
            return cardvalues.Where(x => x != valueToRemove).ToArray();
        }
    }
}
