using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Dealer
    {
        private PokerHand _PokerHand;

        private int _Credits = 100;

        public int Credits
        {
            get
            {
                return _Credits;
            }
        }

        public void Deal()
        {
            if (_PokerHand == null || _PokerHand.Deck.Count < 5)
            {
                _PokerHand = new PokerHand();
            }
            else
            {
                _PokerHand.DealHand();
            }

        }

        public string ShowCards()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Card c in _PokerHand)
            {
                sb.Append(c.ToString() + " ");
            }

            return sb.ToString();
        }

        public void ReplaceCard(int index)
        {

            //check if there are sufficient cards in deck to allow replace
            if (_PokerHand.Deck.Count <1)
            {
                _PokerHand = new PokerHand();
            }

            _PokerHand.DrawHand(index);
        }

        public string ShowBestHand()
        {
            UpdateCredits();
            return _PokerHand.GetBestPokerHand().ToString();
        }

        public void UpdateCredits()
        {
            int rewards = 0;

            PokerHand.PokerHandTypes h = _PokerHand.GetBestPokerHand();

            switch (h)
            {
                case PokerHand.PokerHandTypes.RoyalFlush:
                    rewards = 250;
                    break;
                case PokerHand.PokerHandTypes.StraightFlush:
                    rewards = 50;
                    break;
                case PokerHand.PokerHandTypes.FourOfAKind:
                    rewards = 25;
                    break;
                case PokerHand.PokerHandTypes.FullHouse:
                    rewards = 9;
                    break;
                case PokerHand.PokerHandTypes.Flush:
                    rewards = 6;
                    break;
                case PokerHand.PokerHandTypes.Straight:
                    rewards = 4;
                    break;
                case PokerHand.PokerHandTypes.ThreeOfAKind:
                    rewards = 3;
                    break;
                case PokerHand.PokerHandTypes.TwoPairs:
                    rewards = 2;
                    break;
                case PokerHand.PokerHandTypes.JackOrBetter:
                    rewards = 1;
                    break;
                default:
                    rewards = 0;
                    break;
            }

            _Credits = _Credits + rewards - 1;
        }


    }
}
