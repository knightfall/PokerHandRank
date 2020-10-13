using System.Collections.Generic;
using System.Linq;


namespace PokerHandSorter
{

    class Evaluator
    {
        private int _handRank;
        private int _highCard;
        /// <summary>
        /// Checks with occurrences and sets rank accordingly
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Setting Rank and High Card
        /// </summary>
        /// <param name="distinctValueOccurence"> To find the high card value</param>
        /// <param name="count"> Depends on the rank.</param>
        /// <param name="max"> For High Card & Two Pair, it is true</param>
        /// <param name="rank">Rank found in RankSorter</param>
        private void SetRankHighCard(Dictionary<int, int> distinctValueOccurence, int count, bool max, int rank)
        {
            _highCard = !max ? distinctValueOccurence.FirstOrDefault(x => x.Value == count).Key : distinctValueOccurence.Where(x => x.Value == count).Select(y => y.Key).Max();

            _handRank = rank;
        }
        /// <summary>
        /// Find duplicate values of cards.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static (Dictionary<int, int>, int, int) DistinctValueOccurence(int[] values)
        {
            var distinctValueOccurence = values.GroupBy(n => n)
                .Select(n => new
                    {n.Key, Value = n.Count()})
                .ToDictionary(o => o.Key, o => o.Value);
            return (distinctValueOccurence, distinctValueOccurence.Values.Max(), distinctValueOccurence.Values.Min());
        }


        /// <summary>
        /// Checks if rank is 9 or 5 or 6
        /// As Royal Flush is rare, highest value is 9. If both are 9, highest card is checked.
        /// Checked against the product of the card values,
        /// which are prime numbers. If it is in compositeValueRank Array, it is either 9 or 6
        /// Otherwise, it can be either flush or returns a negative number.
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