using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {

        static void Main(string[] args)
        {
            Dealer myDealer = new Dealer();

            bool playAgain = false;

            do
            {
                //divider
                Console.WriteLine(new String('=', 20));

                //show credits before playing
                Console.WriteLine($"Credits: {myDealer.Credits}");
                Console.WriteLine();

                //deal cards
                myDealer.Deal();

                //show cards in poker hand
                Console.WriteLine(myDealer.ShowCards());

                //show best hand and update credits
                Console.WriteLine("Best Hand: " + myDealer.ShowBestHand());
                Console.WriteLine();
                Console.WriteLine($"Credits: {myDealer.Credits}");
                Console.WriteLine();

                //prompt user if wants to play again
                do
                {
                    Console.Write("Play again (y/n)?");
                }
                while (!CheckPlayAgain(Console.ReadLine(), out playAgain));

            }
            while (playAgain);
        }

        private static bool CheckPlayAgain(string s, out bool playAgain)
        {
            //ignore case
            s = s.ToUpper();

            // check if string is empty 
            if (string.IsNullOrEmpty(s))
            {
                playAgain = false;
                return false;
            }
            else if (s[0] == 'Y')
            {
                playAgain = true;
                return true;
            }
            else if (s[0] == 'N')
            {
                playAgain = false;
                return true;
            }
            else
            {
                playAgain = false;
                return false;
            }

        }
    }
}
