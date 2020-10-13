using System.Collections.Generic;
using System.Linq;

namespace PokerHandSorter
{
    internal static class CardValue
    {
        /// <summary>
        /// Product of hands where the Rank is straight
        /// </summary>
        public static int[] CardFlushCompositeValuesRank = new int[]
        {
            31367009,14535931,6678671,2800733,1062347,323323,85085,15015,2310
        };

        /// <summary>
        /// Cards and their prime values
        /// </summary>
        public static Dictionary<string, int> CardPrimeValue = new Dictionary<string, int>
        {
            {"A", 41}, {"K", 37}, {"Q", 31}, {"J", 29},{"T", 23}, {"9", 19}, {"8", 17}, {"7", 13},{"6", 11}, {"5", 7}, {"4", 5}, {"3", 3}, {"2", 2}
        };

        public static Dictionary<string, int> CardSuite = new Dictionary<string, int>
        {
            {"D", 0}, {"H", 1}, {"S", 2}, {"C", 3}
        };

        /// <summary>
        /// convert a single hand into two different arrays.
        /// </summary>
        /// <param name="cards">input is card values without space. e.g. ADQDTHQH9S</param>
        /// <returns></returns>
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
