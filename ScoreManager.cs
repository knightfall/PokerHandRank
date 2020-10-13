using System.Linq;


namespace PokerHandSorter
{
    class ScoreManager
    {
        private const int Tie = 3;
        private const int PlayerOneWin = 0;
        private const int PlayerTwoWin = 1;
        /// <summary>
        /// Calculates score
        /// </summary>
        /// <param name="p1">Player 1 Rank/Card</param>
        /// <param name="p2">Player 2 Rank/Card</param>
        /// <returns></returns>
        public static int CalculateScore(int p1, int p2)
        {
            
            return p1 > p2 ? PlayerOneWin : PlayerTwoWin;
        }

        /// <summary>
        /// Is called if rank  & high card values are same
        /// </summary>
        /// <param name="rank">common rank of player 1 and player 2</param>
        /// <param name="playerOneHand">Player 1 cards</param>
        /// <param name="playerTwoHand">Player 2 cards</param>
        /// <param name="playerOneMax">High card of player 1</param>
        /// <param name="playerTwoMax">High card of player 2</param>
        /// <returns></returns>
        public static int TieBreaker(int rank, string playerOneHand, string playerTwoHand, int playerOneMax, int playerTwoMax )
        {
            if (rank == Rank.StraightFlush && playerOneMax == 41)
            {
                return Tie;
            }
            else if (rank == Rank.TwoPairs)
            {
                return RankThreeTie(playerOneMax, playerTwoMax, playerOneHand, playerTwoHand);
            }
            else 
                return BreakTie(playerOneMax, playerTwoMax, playerOneHand, playerTwoHand);


        }
        /// <summary>
        /// Called from TieBreaker
        /// </summary>
        /// <param name="playerOneMax"></param>
        /// <param name="playerTwoMax"></param>
        /// <param name="playerOneHand"></param>
        /// <param name="playerTwoHand"></param>
        /// <returns></returns>
        private static int BreakTie(int playerOneMax, int playerTwoMax, string playerOneHand, string playerTwoHand)
        {
            if (playerOneMax == playerTwoMax)
            {
                var playerOneValue = CardValue.ConvertHandValue(playerOneHand);
                var playerTwoValue = CardValue.ConvertHandValue(playerTwoHand);
                try
                {
                    var playerOneRemainingMax = playerOneValue.Except(playerTwoValue).Max();
                    var playerTwoRemainingMax = playerTwoValue.Except(playerOneValue).Max();
                    return playerOneRemainingMax == playerTwoRemainingMax ? Tie : CalculateScore(playerOneRemainingMax, playerTwoRemainingMax);
                }
                catch
                {
                    return Tie;
                }
            }

            return CalculateScore(playerOneMax, playerTwoMax);
        }


        private static int RankThreeTie(int playerOneMax, int playerTwoMax, string playerOneHand, string playerTwoHand)
        {
            if (playerOneMax == playerTwoMax)
            {
                var playerOneValue = CardValue.ConvertHandValue(playerOneHand);
                var playerTwoValue = CardValue.ConvertHandValue(playerTwoHand);
                playerOneValue = CardValue.RemoveValues(playerOneValue, playerOneMax);
                playerTwoValue = CardValue.RemoveValues(playerTwoValue, playerTwoMax);
                return playerOneValue[0] == playerTwoValue[0] ? Tie : CalculateScore(playerOneValue[0], playerTwoValue[0]);
            }

            return CalculateScore(playerOneMax, playerTwoMax);
        }
    }

}
