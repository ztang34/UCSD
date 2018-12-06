using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Deck : List<Card>
    {
        private Random _Random = new Random();
        private const int shuffleHands = 100000;
        private void CreateDeck()
        {
            Clear();

            foreach (string faceValue in Card.ValidFaceValues)
            {
                Add(new Card(faceValue, Card.SuitEnum.Clubs));
                Add(new Card(faceValue, Card.SuitEnum.Diamonds));
                Add(new Card(faceValue, Card.SuitEnum.Hearts));
                Add(new Card(faceValue, Card.SuitEnum.Spades));
            }
        }

        private void ShuffleDeck()
        {
            for (int i = 0; i < shuffleHands; ++i)
            {
                int cardToMove = _Random.Next(0, Count);
                Card card = this[cardToMove];
                RemoveAt(cardToMove);
                Add(card);
            }
        }

        public Deck()
        {
            CreateDeck();
            ShuffleDeck();
        }

        public Card Deal()
        {
            if (Count < 1)
            {
                throw new ArgumentOutOfRangeException("Empty Deck!", "Insufficient cards in deck!");
            }
            else
            {
                Card card = this[0];
                RemoveAt(0);
                return card;
            }
        }

        public List<Card> Deal(int numberOfCards)
        {
            if (Count < numberOfCards)
            {
                throw new ArgumentOutOfRangeException("Empty Deck!", "Insufficient cards in deck!");
            }
            else
            {
                List<Card> cards = new List<Card>();
                for (int i = 0; i < numberOfCards; ++i)
                {
                    cards.Add(Deal());
                }

                return cards;
            }
        }
    }
}
