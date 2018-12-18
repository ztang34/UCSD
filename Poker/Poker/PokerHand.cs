using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class PokerHand : List<Card>
    {
        /// <summary>
        /// Show total of a poker hand
        /// </summary>
        public int Total
        {
            get
            {
                int total = 0;
                int numberofAces = 0;

                foreach (Card c in this)
                {
                    if (c.FaceValue == "A")
                    {
                        ++numberofAces;
                    }

                    total += c.NumericValue;
                }

                if (total > 21 && numberofAces > 0)
                {
                    while (numberofAces > 0)
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

        /// <summary>
        /// Return hand cards in string
        /// </summary>
        public string HandCards
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Card c in this)
                {
                    sb.Append(c.ToString() + " ");
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Return only up card in string
        /// </summary>
        public string UpCard
        {
            get
            {
                return this[1].ToString();
            }
        }



        /// <summary>
        /// Show if the poker hand is a black jack or not
        /// </summary>
        public bool IsBlackJack
        {
            get
            {
                if(this.Total == 21 && this.Count == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsBusted
        {
            get
            {
                if(this.Total > 21)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Deal one card from deck
        /// </summary>
        /// <param name="deck"></param>
        public void DealCard (Deck deck)
        {
            try
            {
                this.Add(deck[0]);
                deck.RemoveAt(0);
            }
            catch
            {
                throw new ArgumentOutOfRangeException("No card in deck to add to poker hand!", "empty deck");
            }
            
        }

        /// <summary>
        /// Deal multiple cards from deck
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="numberOfCards"></param>
        public void DealCard(Deck deck, int numberOfCards)
        {
            for(int i = 0; i < numberOfCards; ++i)
            {
                DealCard(deck);
            }
        }

        /// <summary>
        /// Poker hand is initialized with two cards in hand
        /// </summary>
        /// <param name="deck"></param>
        public PokerHand(Deck deck)
        {
            DealCard(deck, 2);
        }

        /// <summary>
        /// Deal card until total hits soft 17
        /// </summary>
        /// <param name="deck"></param>
        public void DealCardToSoft17(Deck deck)
        {
            while(this.Total < 17)
            {
                DealCard(deck);
            }
        }


    }
}
