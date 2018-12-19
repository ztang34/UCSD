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
            Console.WriteLine("Welcome to BlackJack!");

            int numberofDecks = 0;

            while (true)
            {
                Console.Write("Please select how many decks of cards you wish to play with (1-4): ");

                if (!int.TryParse(Console.ReadLine(), out numberofDecks))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }
                else if (numberofDecks < 1 || numberofDecks > 4)
                {
                    Console.WriteLine("Valid number of decks is between 1 and 4");
                    continue;
                }
                else
                {
                    break;
                }
            }

            BlackJackDealer myDealer = new BlackJackDealer(numberofDecks);

            var options = Enum.GetValues(typeof(MenuChocies));
            MenuChocies choice;

            while (true)
            {
                Console.WriteLine(new string('=',50));
                Console.WriteLine("Please choose from following menu options (1-3):");

                foreach (MenuChocies option in options)
                {
                    Console.WriteLine($" [{option:D}]. {option:G}");
                }

                if(!Enum.TryParse(Console.ReadLine(),true, out choice) || (int)choice < 0 || (int) choice>2)
                {
                    Console.WriteLine("Please select a valid option from menu!");
                    continue;
                }

                switch (choice)
                {
                    case MenuChocies.PlayNewHand:
                        PlayNewHand(myDealer);
                        break;
                    case MenuChocies.ShowStatistics:
                        ShowStatistics(myDealer);
                        break;
                    case MenuChocies.Quit:
                        ShowStatistics(myDealer);
                        Console.WriteLine("Press any key to quit the game...");
                        Console.ReadLine();
                        return;
                    default:
                        break;
                }

                
            }


        }

        static void PlayNewHand(BlackJackDealer dealer)
        {
            dealer.PlayNewHand();

         
            Console.WriteLine($"Player's hand: {dealer.PlayerHandCard}");
            Console.WriteLine($"Dealer's hand: [hidden hole card] {dealer.DealerUpCard}");

            
            while (true)
            {
                if (dealer.IsBlackJack)
                {
                    Console.WriteLine("You have got blackJack!!");
                    break;
                }

                else if (dealer.PlayerTotal == 21)
                {
                    Console.WriteLine("You have hit 21 points!");
                    break;
                }

                else if (dealer.PlayerTotal > 21)
                {
                    Console.WriteLine("You have busted!");
                    break;
                }

                else
                {
                    bool dealCard = false;

                    Console.Write("Do you want to hit? (Y/N): ");
                    while (!BoolTryParse(Console.ReadLine(), out dealCard))
                    {
                        Console.WriteLine("Invalid input!");
                        Console.Write("Do you want to hit? (Y/N): ");
                    }

                    if(dealCard)
                    {
                        dealer.PlayerHandDeal();
                        Console.WriteLine("You decide to hit!");
                        Console.WriteLine($"Player's hand: {dealer.PlayerHandCard}");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("You decide to stand!");
                        break;
                    }
                }                
            }   

            if (dealer.PlayerTotal <=21)
            {
                Console.WriteLine("Press any key to see dealer's hand cards...");
                Console.ReadLine();
                Console.WriteLine($"Dealer's hand: {dealer.DealerHandCard}");
                Console.WriteLine($"Your total is: {dealer.PlayerTotal}; Dealer's total is: {dealer.DealerTotal} ");
            }
            
            Console.WriteLine(CompareHands(dealer));
            Console.WriteLine("Press any key to go back to main menu...");
            Console.ReadLine();
                                  
        }

        static void ShowStatistics (BlackJackDealer dealer)
        {
            
            Console.WriteLine();
            Console.WriteLine("Game Statistics");
            Console.WriteLine(new string('-',25));
            Console.WriteLine($"{"Total Win:",-20} {dealer.TotalWin}");
            Console.WriteLine($"{"BlackJack Win:",-20} {dealer.BlackJackWin}");
            Console.WriteLine($"{"Non-blackJack Win:",-20} {dealer.NonBlackJackWin}");
            Console.WriteLine($"{"Total Lose:",-20} {dealer.Lose}");
            Console.WriteLine($"{"Total Push:",-20} {dealer.Push}");
            Console.WriteLine();
            return;
        }

        static string CompareHands(BlackJackDealer dealer)
        {
            
            switch (dealer.CompareDealerHandWithPlayerHand())
            {
                case BlackJackDealer.Result.BlackJackWin:
                    return "You have won this hand with a blackjack!";
                case BlackJackDealer.Result.NonBlackJackWin:
                    return "You have won this hand!";
                case BlackJackDealer.Result.Lose:
                    return "You have lost this hand!";
                case BlackJackDealer.Result.push:
                    return "You have got same total as dealer!";
                default:
                    return string.Empty;
                   
            }
        }

        static bool BoolTryParse (string input, out bool result)
        {
            bool ok = false;

            input = input.ToUpper();

            if (input == "YES" || input =="Y")
            {
                result = true;
                ok = true;
            }
            else if (input == "NO" || input == "N")
            {
                result = false;
                ok = true;
            }
            else
            {
                result = false;
                ok = false;
            }

            return ok;
        }

        enum MenuChocies
        {
            PlayNewHand,
            ShowStatistics,
            Quit
        }



    }
}


