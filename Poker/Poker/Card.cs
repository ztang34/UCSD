using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Poker
{
    class Card : IComparable<Card>
    {
        public enum SuitEnum
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3
        }

        private SuitEnum _Suit = SuitEnum.Clubs; //default to Clubs
        public SuitEnum Suit
        {
            get
            {
                return _Suit;
            }
            set
            {
                _Suit = value;
            }
        }

        private static List<string> _ValidFaceValues = new List<string>("2,3,4,5,6,7,8,9,10,J,Q,K,A".Split(','));
        public static List<string> ValidFaceValues
        {
            get
            {
                return _ValidFaceValues;
            }
        }

        private string _FaceValue = string.Empty;
        public string FaceValue
        {
            get
            {
                return _FaceValue;
            }
            set
            {
                if (ValidFaceValues.Contains(value))
                {
                    _FaceValue = value;
                }
                else throw new ArgumentOutOfRangeException("FaceValue", string.Format($"The give face value {value} is invalid"));
            }
        }

        public string SuitSymbol
        {
            get
            {
                switch (Suit)
                {
                    case SuitEnum.Clubs:
                        return "\u2663";
                    case SuitEnum.Diamonds:
                        return "\u2666";
                    case SuitEnum.Hearts:
                        return "\u2665";
                    case SuitEnum.Spades:
                        return "\u2660";
                    default:
                        return string.Empty;

                }
            }
        }

        public string FaceValueName
        {
            get
            {
                switch (FaceValue)
                {
                    case "J":
                        return "Jack";
                    case "Q":
                        return "Queen";
                    case "K":
                        return "King";
                    case "A":
                        return "Ace";
                    default:
                        return FaceValue;
                }
            }
        }

        public int NumericValue
        {
            get
            {
                return (ValidFaceValues.IndexOf(FaceValue) + 2);
            }
        }

        public string LongName
        {
            get
            {
                return string.Format($"{FaceValueName} of {Suit}");
            }
        }

        public Card(string faceValue, SuitEnum suit)
        {
            FaceValue = faceValue;
            Suit = suit;
        }

        public override string ToString()
        {
            return string.Format($"{FaceValue}{SuitSymbol}");
        }

        public int CompareTo(Card other)
        {
            if (other == null) throw new ArgumentException("Other");

            return this.NumericValue.CompareTo(other.NumericValue);

        }

        public static Card Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Invalid card format", "input cannot be empty!");
            }
            try
            {
                if (Regex.IsMatch(input, "(10|[JQKA2-9]{1})[♣♦♥♠]{1}"))
                {
                    string faceValue = input.Substring(0, input.Length - 1);
                    char suitChar = input[input.Length - 1];
                    SuitEnum suit = SuitEnum.Clubs;
                    switch (suitChar)
                    {
                        case '♣':
                            suit = SuitEnum.Clubs;
                            break;
                        case '♦':
                            suit = SuitEnum.Diamonds;
                            break;
                        case '♥':
                            suit = SuitEnum.Hearts;
                            break;
                        case '♠':
                            suit = SuitEnum.Spades;
                            break;
                    }

                    return new Card(faceValue, suit);
                }
                throw new ArgumentException(string.Format($"Invalid card format: {input}"), "input");
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
