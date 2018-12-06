using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class PokerHand : List<Card>
    {
        private Deck _Deck;

        public Deck Deck
        {
            get
            {
                if (_Deck == null)
                {
                    _Deck = new Deck();
                }

                return _Deck;
            }
        }


        public List<Card> SortedHand
        {
            get
            {
                List<Card> cards = new List<Card>(this);
                cards.Sort();
                return cards;

            }
        }

        public void DealHand()
        {
            try
            {
                Clear();
                AddRange(Deck.Deal(5));
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: Insufficient cards in the deck! {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unknown error: {ex.Message}");
            }
        }

        public PokerHand()
        {
            DealHand();
        }

        public PokerHand(List<Card> cards)
        {
            AddRange(cards);
        }

        public void DrawHand(int index)
        {
            if (index < 0 || index > 4)
            {
                throw new ArgumentOutOfRangeException($"You have entered an invalid card index: {index}", "index");
            }

            Card card = Deck.Deal();
            this[index] = card;
        }

        public static PokerHand Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Invalid poker hand format", "input");
            }
            try
            {
                var cardsInput = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (cardsInput.Length != 5)
                {
                    throw new ArgumentException($"Invalid poker hand format: {input}", "input");
                }
                List<Card> cards = new List<Card>();

                foreach (string card in cardsInput)
                {
                    cards.Add(Card.Parse(card));
                }

                return new PokerHand(cards);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public enum PokerHandTypes
        {
            None = 0,
            JackOrBetter,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public PokerHandTypes GetBestPokerHand()
        {


            if (IsRoyalFlush())
            {
                return PokerHandTypes.RoyalFlush;
            }

            if (IsStraightFlush())
            {
                return PokerHandTypes.StraightFlush;
            }

            if (IsFourOfAKind())
            {
                return PokerHandTypes.FourOfAKind;
            }

            if (IsFullHouse())
            {
                return PokerHandTypes.FullHouse;
            }

            if (IsFlush())
            {
                return PokerHandTypes.Flush;
            }

            if (IsStraight())
            {
                return PokerHandTypes.Straight;
            }

            if (IsThreeOfAKind())
            {
                return PokerHandTypes.ThreeOfAKind;
            }

            if (IsTwoPairs())
            {
                return PokerHandTypes.TwoPairs;
            }

            if (IsJackOrBetter())
            {
                return PokerHandTypes.JackOrBetter;
            }

            return PokerHandTypes.None;
        }

        private bool IsFlush()
        {
            //loop through poker hand to determine if all cards have same suit
            for (int i = 1; i < Count; ++i)
            {
                if (!this[i].Suit.Equals(this[0].Suit))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsStraight()
        {
            //Sort Poker Hand
            List<Card> cards = SortedHand;

            //Loop through Poker Hand, and compare card numeric value with the next card
            for (int i = 0; i < Count - 1; ++i)
            {
                if (cards[i + 1].NumericValue - cards[i].NumericValue != 1)
                {
                    //Check if last card is Ace, if so, verify if the poker hand is a straight when Ace equals 1
                    if (i == (Count - 2) && cards[i + 1].NumericValue == 14 && (cards[i + 1].NumericValue - cards[i].NumericValue) == 9)
                    {
                        return true;
                    }

                    return false;
                }
            }
            return true;
        }

        private bool IsStraightFlush()
        {
            //The hand is both a straight and a flush
            if (IsFlush() && IsStraight())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsRoyalFlush()
        {
            //Sort poker hand
            List<Card> cards = SortedHand;

            //hand is both a straight flush and the lowest card is a 10
            if (IsStraightFlush() && this[0].NumericValue == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFourOfAKind()
        {
            //sort poker hand
            List<Card> cards = SortedHand;

            //check if there are 4 cards with same value          
            if (cards[0].NumericValue == cards[3].NumericValue || cards[1].NumericValue == cards[4].NumericValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsThreeOfAKind()
        {
            //sort poker hand
            List<Card> cards = SortedHand;

            //check if there are three cards with same value
            if (cards[0].NumericValue == cards[2].NumericValue || cards[1].NumericValue == cards[3].NumericValue || cards[2].NumericValue == cards[4].NumericValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsTwoPairs()
        {
            //sort poker hand
            List<Card> cards = SortedHand;

            //check if there are two pairs
            if (cards[0].NumericValue == cards[1].NumericValue && cards[2].NumericValue == cards[3].NumericValue)
            {
                return true;
            }
            else if (cards[1].NumericValue == cards[2].NumericValue && cards[3].NumericValue == cards[4].NumericValue)
            {
                return true;
            }
            else if (cards[0].NumericValue == cards[1].NumericValue && cards[3].NumericValue == cards[4].NumericValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsFullHouse()
        {

            //full house must be both three of a kind and two pairs
            if (IsThreeOfAKind() && IsTwoPairs())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsJackOrBetter()
        {
            //sort poker hand
            List<Card> cards = SortedHand;

            //check if poker hand has a pair of Jack or better
            for (int i = 0; i < Count - 1; ++i)
            {
                if (cards[i].NumericValue > 10 && cards[i].NumericValue == cards[i + 1].NumericValue)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
