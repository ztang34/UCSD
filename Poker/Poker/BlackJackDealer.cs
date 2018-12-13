using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class BlackJackDealer
    {
        private Deck _Deck;

        private List<Card> _DealerHand;
        private List<Card> _PlayerHand;
        private int _BlackJackWin;
        private int _NonBlackJackWin;
        private int _Lose;
        private int _Push;
        private int _NumberOfDecks;

        public int TotalWin
        {
            get
            {
                return _BlackJackWin + _NonBlackJackWin;
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
                _Deck = new Deck(numberOfDecks);
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
            _DealerHand = new List<Card>();
            _PlayerHand = new List<Card>();
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
            if(_Deck.Count < 16)
            {
                CreateShoe(_NumberOfDecks);
            }
        }

        /// <summary>
        /// Deal first card from "shoe" and add it to hand
        /// </summary>
        /// <param name="hand">dealer's hand or player's hand cards</param>
        private void DealCard(List<Card> hand)
        {
            CheckRemainingCard();
            Card card = _Deck[0];
            _Deck.RemoveAt(0);
            hand.Add(card);
        }

        /// <summary>
        /// Deal multiple cards from "shoe" and add it to hand
        /// </summary>
        /// <param name="hand">dealer's or player's hand card</param>
        /// <param name="numberOfCards">number of cards adds to hand card</param>
        private void DealCard(List<Card> hand, int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; ++i)
            {
                DealCard(hand);
            }
        }

        /// <summary>
        /// Calculate numeric total of a hand
        /// </summary>
        /// <param name="hand">dealer or players' hand</param>
        /// <returns>numeric total of a hand</returns>
        private int GetHandTotal (List<Card> hand)
        {
            int total = 0;
            int numberofAces = 0;

            foreach (Card c in hand)
            {
                if(c.FaceValue == "A")
                {
                    ++numberofAces;
                }

                total += c.NumericValue;
            }

            if(total > 21 && numberofAces >0)
            {
                while(numberofAces > 0)
                {
                    total -= 10;
                    --numberofAces;

                    if (total <= 21)
                    {
                        break;
                    }
                }
            }

            return total;
        }

        
    }
}
