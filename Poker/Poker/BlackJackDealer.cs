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

        public int BlackJackWin { get; private set; }
        public int NonBlackJackWin { get; private set; }
        public int Lose { get; private set; }
        public int Push { get; private set; }


        public int TotalWin
        {
            get
            {
                return BlackJackWin + NonBlackJackWin;
            }
        }

        public string DealerHandCard
        {
            get
            {
                if(_DealerHand == null)
                {
                    PlayNewHand();
                }

                return _DealerHand.HandCards;
            }
        }

        public string PlayerHandCard
        {
            get
            {
                if(_PlayerHand == null)
                {
                    PlayNewHand();
                }
                return _PlayerHand.HandCards;
            }
        }

        public string DealerUpCard
        {
            get
            {
                if(_DealerHand == null)
                {
                    PlayNewHand();
                }
                return _DealerHand.UpCard;
            }
        }

        public int PlayerTotal
        {
            get
            {
                return _PlayerHand.Total;
            }
        }

        public int DealerTotal
        {
            get
            {
                return _DealerHand.Total;
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
            BlackJackWin = 0;
            NonBlackJackWin = 0;
            Lose = 0;
            Push = 0;
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
       
        public void PlayNewHand ()
        {
                   
            try
            {
                CheckRemainingCard();
                _PlayerHand = new PokerHand(_Shoe);

                CheckRemainingCard();
                _DealerHand = new PokerHand(_Shoe);

                CheckRemainingCard();
                _DealerHand.DealCardToSoft17(_Shoe);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Insufficent cards in deck: {ex.Message}");
            }

        }

        /// <summary>
        /// Deal a new card to player's hand if it is less than 21
        /// </summary>
        public void PlayerHandDeal()
        {
            if (!_PlayerHand.IsBusted)
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
                    ++Push;
                    result = Result.push;
                }
                else
                {
                    ++BlackJackWin;
                    result = Result.BlackJackWin;
                }
            }
            else if (_PlayerHand.IsBusted)
            {
                ++Lose;
                result = Result.Lose;
            }

            else
            {
                if (_PlayerHand.Total > _DealerHand.Total)
                {
                    ++NonBlackJackWin;
                    result = Result.NonBlackJackWin;
                }
                else if (_PlayerHand.Total < _DealerHand.Total)
                {
                    ++Lose;
                    result = Result.Lose;
                }
                else
                {
                    ++Push;
                    result = Result.push;
                }
            }

            return result;

        }
    }    
}
