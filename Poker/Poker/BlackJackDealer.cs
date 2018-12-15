using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class BlackJackDealer
    {
        private Deck _Shoe;

        private PokerHand _DealerHand;
        private PokerHand _PlayerHand;
       
        private int _NumberOfDecks;

        private const int _MinimumNumberOfCards = 15;

        private int _BlackJackWin;
        private int _NonBlackJackWin;
        private int _Lose;
        private int _Push;

        public int BlackJackWin
        {
            get
            {
                return _BlackJackWin;
            }
        }
       
        public int NonBlackJackWin
        {
            get
            {
                return _NonBlackJackWin;
            }
        }
        public int Lose
        {
            get
            {
                return _Lose;
            }
        }
        public int Push
        {
            get
            {
                return _Push;
            }
        }

        public int TotalWin
        {
            get
            {
                return BlackJackWin + NonBlackJackWin;
            }
        }

       
        /// <summary>
        /// Create a "shoe" of cards with different number of decks, valid range is 1-4
        /// </summary>
        /// <param name="numberOfDecks">number of decks in a "shoe"</param>
        private void CreateShoe(int numberOfDecks)
        {
            try
            {
                _Shoe = new Deck(numberOfDecks);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch(Exception)
            {
                Console.WriteLine("Error initializing decks");
            }
        }

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numberOfDecks">number of decks in a "shoe"</param>
        public BlackJackDealer(int numberOfDecks)
        {
            CreateShoe(numberOfDecks);
            _BlackJackWin = 0;
            _NonBlackJackWin = 0;
            _Lose = 0;
            _Push = 0;
            _NumberOfDecks = numberOfDecks;
        }


        /// <summary>
        /// Shuffle cards when remaing cards is fewer than 15
        /// </summary>
        private void CheckRemainingCard ()
        {
            if(_Shoe.Count < _MinimumNumberOfCards)
            {
                CreateShoe(_NumberOfDecks);
                
            }
        }

        /// <summary>
        /// Start a new player hand and dealer hand
        /// </summary>
        /// <returns>true if player's hand is a black jack</returns>
        public bool PlayNewHand ()
        {
            bool isBlackJack;

            CheckRemainingCard();
            _PlayerHand = new PokerHand(_Shoe);

            CheckRemainingCard();
            _DealerHand = new PokerHand(_Shoe);

            CheckRemainingCard();
            _DealerHand.DealCardToSoft17(_Shoe);

           if(_PlayerHand.IsBlackJack)
            {
                isBlackJack = true;
            }
           else
            {
                isBlackJack = false;
            }

            return isBlackJack;

        }

        /// <summary>
        /// Deal a new card to player's hand if it is less than 21
        /// </summary>
        public void DealerHandDeal()
        {
            if (_PlayerHand.Total < 21)
            {
                CheckRemainingCard();
                _DealerHand.DealCard(_Shoe);
            }
            
        }


        public enum Result
        {
            BlackJackWin,
            NonBlackJackWin,
            Lose,
            push
        }

        /// <summary>
        /// Compare dealer's hand and player's hand
        /// </summary>
        /// <returns>Result enum showing weather player win, push or lose</returns>
        public Result CompareDealerHandWithPlayerHand()
        {
            Result result;

            if (_PlayerHand.IsBlackJack)
            {
                if (_DealerHand.Total == 21)
                {
                    ++_Push;
                    result = Result.push;
                }
                else
                {
                    ++_BlackJackWin;
                    result = Result.BlackJackWin;
                }
            }
            else
            {
                if (_PlayerHand.Total > _DealerHand.Total)
                {
                    ++_NonBlackJackWin;
                    result = Result.NonBlackJackWin;
                }
                else if (_PlayerHand.Total < _DealerHand.Total)
                {
                    ++_Lose;
                    result = Result.Lose;
                }
                else
                {
                    ++_Push;
                    result = Result.push;
                }
            }

            return result;

        }
    }    
}
