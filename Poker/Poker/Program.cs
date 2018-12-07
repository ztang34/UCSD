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

                //prompt user to select cards to hold
                string input = string.Empty;
                do
                {
                    Console.Write("Select cards to hold (Example: 135): ");
                    input = Console.ReadLine();
                }
                while (int.TryParse(input, out int cardsToHold) && ReplaceCards(cardsToHold,input.Length, out List<int>cardsToReplace));


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

        private static bool ReplaceCards (int input, int digits, out List<int>cardsToReplace)
        {

            cardsToReplace = null;

            

           //check if input is within range
            if (input < 0 || input > 12345)
            {
                return false;
            }

            //get each digit of input integer
            for (int i = (digits-1); i >=0; --i)
            {
                int index = input / (int)Math.Pow(10, i);

                //check no index is 0 if there are more than 1 digits (i.e. only 0 is allowed)
                if (index == 0 && digits != 1)
                {
                    return false;
                }

                cardsToHold.Add(index);
                input -= index * (int)Math.Pow(10, i);
            }

            //sort list
            cardsToHold.Sort();

            //verify no index is greater than 5
            if(cardsToHold[digits-1] > 5)
            {
                return false;
            }

            //TODO
            if (true)
            return false;

        }


    }
}
