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

        public int TotalWin
        {
            get
            {
                return _BlackJackWin + _NonBlackJackWin;
            }
        }

        private void CreateShoe(int numberOfDeck)
        {
            try
            {
                _Deck = new Deck(numberOfDeck);
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

        

        public BlackJackDealer(int numberOfDeck)
        {
            CreateShoe(numberOfDeck);
            _DealerHand = new List<Card>();
            _PlayerHand = new List<Card>();
            _BlackJackWin = 0;
            _NonBlackJackWin = 0;
            _Lose = 0;
            _Push = 0;
        }

        
    }
}
