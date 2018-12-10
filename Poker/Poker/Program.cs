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

            List<int> cardsToReplace = new List<int>();

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
                while (!(int.TryParse(input, out int cardsToHold) && ReplaceCards(cardsToHold,out cardsToReplace)));

                //replace cards
                for(int i = 0; i< cardsToReplace.Count; ++i)
                {
                    myDealer.ReplaceCard(cardsToReplace[i]);
                }

                Console.WriteLine(myDealer.ShowCards());
                Console.WriteLine("Best Hand: " + myDealer.ShowBestHand());

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

        private static bool ReplaceCards(int input, out List<int> cardsToReplace)
        {
            bool ok = false;

            cardsToReplace = new List<int>();
            cardsToReplace.AddRange(Enumerable.Range(0, 5));


            //check if input is not negative
            if (input < 0)
            {
                ok = false;
            }

            //check if input is zero (i.e. replace all 5 cards)
            else if (input == 0)
            {
                ok = true;
            }

            //get each digit of input integer
            else
            {
                List<int> cardsToHold = SplitIntegerIntoDigits(input);

                //remove duplicate digits
                cardsToHold = cardsToHold.Distinct().ToList();

                //sort list
                cardsToHold.Sort();

                //0 is not allowed in the input integer
                if (cardsToHold[0] == 0)
                {
                    ok = false;
                }
                //each digit should not greater than 5
                else if (cardsToHold[cardsToHold.Count-1]>5)
                {
                    ok = false;
                }
                else
                {
                    //get cards to replace
                    for (int i = 0; i < cardsToHold.Count; ++i)
                    {
                        //convert cards to 0 based
                        cardsToReplace.Remove(cardsToHold[i]-1);
                    }

                    ok = true;
                }
            }

            return ok;
        }


        private static List<int> SplitIntegerIntoDigits (int input)
        {
            List<int> digits = new List<int>();

            while (input > 0)
            {
                digits.Add(input % 10);
                input /= 10;
            }

            return digits;
        }


    }
}
