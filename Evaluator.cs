using System.Collections.Generic;
using System.Linq;


namespace PokerHandSorter
{
    /*Rank
     * ACE = 12, King = 11, Queen = 10, Jack = 9, 10 = 8,
     * 9 = 7, 8 = 6, 7 = 5, 6 = 4, 5 = 3, 4 = 2, 3 = 1, 2 = 0
     *
     *Value of cards
     * Rank	    2	3	4	5	6	7	8	9	10	J	Q	K	A
     * Prime	2	3	5	7	11	13	17	19	23	29	31	37	41
     *
     * Diamonds = 0  * Hearts = 1  * Spades = 2   * Clubs = 3
     *
     * Royal F = 41x37x31x29x23 = 31,367,009 * King High F = 37x31x29x23x19 = 14,535,931  * Queen High F= 31x29x23x19x17 = 6,678,671
     * Jack High F= 29x23x19x17x13 = 2,800,733  * 10 High F = 23x19x17x13x11 = 1,062,347  * 9 High F = 19x17x13x11x7 = 323,323
     * 8 High F = 17x13x11x7x5 = 85,085   * 7 High F = 13x11x7x5x3 = 15,015   * 6 High F = 11x7x5x3x2 = 2,310
     */
    
    class Evaluator
    {
        private int _handRank;
        private int _highCard;

        public int[] GetHighestRank(string cards)
        {
            var (values, suites) = CardValue.ConvertHand(cards);
            var distinctSuiteCount = suites.Distinct().Count();
            var (distinctValueOccurence, occurenceMax, occurenceMin) = DistinctValueOccurence(values);

            StraightFlushRankCheck(values, distinctSuiteCount);
            //Rank 8: Four of a kind
            if (_handRank < Rank.StraightFlush & occurenceMax == 4)
            {
                SetRankHighCard(distinctValueOccurence, 4, false, Rank.FourOfAKind);
            }

            // Rank 7: Three of a kind and a pair / Full house
            else if (_handRank < Rank.FourOfAKind & occurenceMax == 3 & occurenceMin == 2)
            {
                SetRankHighCard(distinctValueOccurence, 3, false, Rank.FullHouse);
            }

            // Rank 4: Three of a kind
            else if (_handRank < Rank.Straight & occurenceMax == 3 & occurenceMin == 1)
            {
                SetRankHighCard(distinctValueOccurence, 3, false, Rank.ThreeOfAKind);
            }

            // Rank 3: Two different Pairs
            else if (_handRank < Rank.ThreeOfAKind & occurenceMax == 2 &
                     distinctValueOccurence.Where(x => x.Value == 2).Select(y => y.Key).Count() == 2)
            {
                SetRankHighCard(distinctValueOccurence, 2, true, Rank.TwoPairs);
            }

            // Rank 2: Pairs
            else if (_handRank < Rank.TwoPairs & occurenceMax == 2 &
                     distinctValueOccurence.Where(x => x.Value == 2).Select(y => y.Key).Count() == 1)
            {
                SetRankHighCard(distinctValueOccurence, 2, false, Rank.Pair);
            }

            // Rank 1: High Card
            else if (_handRank < Rank.Pair)
            {
                SetRankHighCard(distinctValueOccurence, 1, true, Rank.HighCard);
            }

            return new[] {_handRank, _highCard};
        }




        private void SetRankHighCard(Dictionary<int, int> distinctValueOccurence, int count, bool max, int rank)
        {
            _highCard = !max ? distinctValueOccurence.FirstOrDefault(x => x.Value == count).Key : distinctValueOccurence.Where(x => x.Value == count).Select(y => y.Key).Max();

            _handRank = rank;
        }

        private (Dictionary<int, int>, int, int) DistinctValueOccurence(int[] values)
        {
            var distinctValueOccurence = values.GroupBy(n => n)
                .Select(n => new
                    {n.Key, Value = n.Count()})
                .ToDictionary(o => o.Key, o => o.Value);
            return (distinctValueOccurence, distinctValueOccurence.Values.Max(), distinctValueOccurence.Values.Min());
        }


        /// <summary>
        /// Checks if rank is 10, 9 or 5 or 6
        /// </summary>
        /// <param name="values"></param>
        /// <param name="distinctCount"></param>
        /// <returns></returns>
        private void StraightFlushRankCheck(int[] values, int distinctCount)
        {
            var check = distinctCount == 1;

            var compositeValue = values.Aggregate(1, (x, y) => x * y);

            if (CardValue.CardFlushCompositeValuesRank.Contains(compositeValue))
            {
                if (check)
                {
                    _handRank = Rank.StraightFlush;
                    _highCard = values.Max();
                }
                else
                {
                    _handRank = Rank.Straight;
                    _highCard = values.Max();
                }
            }
            else
            {
                if (!check)
                {
                    _handRank = Rank.Negative;
                }
                else
                {
                    _handRank = Rank.Flush;
                    _highCard = values.Max();
                }
            }
        }
    }
}