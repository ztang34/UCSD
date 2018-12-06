using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Poker
{
    public static class PokerTester
    {
        public static void Test()
        {
            PokerHand h = null;
            PokerHand.PokerHandTypes t = PokerHand.PokerHandTypes.None;


            h = PokerHand.Parse("10♠,8♣,3♥,5♣,7♦");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.None, "Expected PokerHandTypes.None");

            h = PokerHand.Parse("5♣,6♥,9♣,7♦,9♥");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.None, "Expected PokerHandTypes.None");

            //Test Jacks or Better 
            h = PokerHand.Parse("2♣,7♠,J♣,9♦,J♥");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.JackOrBetter, "Expected PokerHandTypes.JacksOrBetter");

            h = PokerHand.Parse("A♥,Q♣,A♦,10♣,7♣");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.JackOrBetter, "Expected PokerHandTypes.JacksOrBetter");

            //Test Two Pairs 
            h = PokerHand.Parse("6♥,8♥,3♥,3♣,6♣");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.TwoPairs, "Expected PokerHandTypes.TwoPairs");

            //Test Flush
            h = PokerHand.Parse("6♥,8♥,3♥,3♥,6♥");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.Flush, "Expected PokerHandTypes.Flush");

            //Test Straight
            h = PokerHand.Parse("2♥,5♥,4♥,3♣,A♣");
            t = h.GetBestPokerHand();
            Debug.Assert(t == PokerHand.PokerHandTypes.Straight, "Expected PokerHandTypes.Straight");
        }
    }
}
