using System.Linq;


namespace PokerHandSorter
{
    class ScoreManager
    {
        private const int Tie = 3;
        private const int PlayerOneWin = 0;
        private const int PlayerTwoWin = 1;
        public static int CalculateScore(int p1, int p2)
        {
            
            return p1 > p2 ? PlayerOneWin : PlayerTwoWin;
        }

        public static int TieBreaker(int rank, string playerOneHand, string playerTwoHand, int playerOneMax, int playerTwoMax )
        {
            if (rank == Rank.TwoPairs)
            {
                return RankThreeTie(playerOneMax, playerTwoMax, playerOneHand, playerTwoHand);
            }
            else 
                return BreakTie(playerOneMax, playerTwoMax, playerOneHand, playerTwoHand);


        }

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
