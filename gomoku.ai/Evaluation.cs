using gomoku.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace gomoku.ai
{
    public static class Evaluation
    {
        
        private const int WinningValue = 15000;
        private static readonly char freeCell = Color.Undefined.ToChar();
        private static readonly char white = Color.White.ToChar();
        private static readonly char black = Color.Black.ToChar();

        public static int Evaluate(Board board)
        {
            string allStrings = string.Join(string.Empty, board.AllStrings);
            int value = 0;


            if (Five(black).IsMatch(allStrings))
            {
                return WinningValue;
            }

            if (Five(white).IsMatch(allStrings))
            {
                return -WinningValue;
            }

            if (board.Turn == Color.Black && (
                FourOpen(black).IsMatch(allStrings) || 
                FourClosedLeft(black).IsMatch(allStrings) || 
                FourClosedRight(black).IsMatch(allStrings) ||
                FourHoleLeft(black).IsMatch(allStrings) ||
                FourHoleRight(black).IsMatch(allStrings) ||
                FourHoleCenter(black).IsMatch(allStrings)))
            {
                return WinningValue;
            }

            if (board.Turn == Color.White && (
                FourOpen(white).IsMatch(allStrings) || 
                FourClosedLeft(white).IsMatch(allStrings) || 
                FourClosedRight(white).IsMatch(allStrings) ||
                FourHoleLeft(white).IsMatch(allStrings) ||
                FourHoleRight(white).IsMatch(allStrings) ||
                FourHoleCenter(white).IsMatch(allStrings)))
            {
                return -WinningValue;
            }


            foreach (var str in board.AllStrings)
            {
                foreach (var pattern in Patterns)
                {
                    if (str.Contains(pattern.Key))
                    {
                        value += pattern.Value;
                    }
                }
            }
            return value;
        }

        // XXXXX
        private static Regex Five(char c) => new Regex($@"[^{c}]{c}{c}{c}{c}{c}[^{c}]");

        // -XXXX-
        private static Regex FourOpen(char c) => new Regex($@"{freeCell}{c}{c}{c}{c}{freeCell}");
        
        // 0XXXX-
        private static Regex FourClosedLeft(char c) => new Regex($@"[^{c}{freeCell}]{c}{c}{c}{c}{freeCell}");

        // -XXXX0
        private static Regex FourClosedRight(char c) => new Regex($@"{freeCell}{c}{c}{c}{c}[^{c}{freeCell}]");

        // 0XXX-X
        private static Regex FourHoleRight(char c) => new Regex($@"[^{c}]{c}{c}{c}{freeCell}{c}[^{c}]");
        
        // X-XXX
        private static Regex FourHoleLeft(char c) => new Regex($@"[^{c}]{c}{freeCell}{c}{c}{c}[^{c}]");

        // XX-XX
        private static Regex FourHoleCenter(char c) => new Regex($@"[^{c}]{c}{c}{freeCell}{c}{c}[^{c}]");

        // --XXX--
        private static Regex Three(char c) => new Regex($@"{freeCell}{freeCell}{c}{c}{c}{freeCell}{freeCell}");

        // -X-XX-
        private static Regex ThreeHoleLeft(char c) => new Regex($@"{freeCell}{c}{freeCell}{c}{c}{freeCell}");

        // -XX-X-
        private static Regex ThreeHoleRight(char c) => new Regex($@"{freeCell}{c}{c}{freeCell}{c}{freeCell}");

        // 0-XXX--
        private static Regex ThreeLeft(char c) => new Regex($@"[^{c}{freeCell}]{freeCell}{c}{c}{c}{freeCell}{freeCell}");

        // --XXX-0
        private static Regex ThreeRight(char c) => new Regex($@"{freeCell}{freeCell}{c}{c}{c}{freeCell}[^{c}{freeCell}]");

        // 0XXX--
        private static Regex ThreeClosedLeft(char c) => new Regex($@"[^{c}{freeCell}]{c}{c}{c}{freeCell}{freeCell}");

        // --XXX0
        private static Regex ThreeClosedRight(char c) => new Regex($@"{freeCell}{freeCell}{c}{c}{c}[^{c}{freeCell}]");

        // --XX--
        private static Regex Two(char c) => new Regex($@"{freeCell}{freeCell}{c}{c}{freeCell}{freeCell}");
        
        // -X-X-
        private static Regex TwoHole(char c) => new Regex($@"{freeCell}{c}{freeCell}{c}{freeCell}");

        // 0XX---
        private static Regex TwoHoleLeft(char c) => new Regex($@"[^{c}{freeCell}]{c}{c}{freeCell}{freeCell}{freeCell}");
        
        // ---XX0
        private static Regex TwoHoleRight(char c) => new Regex($@"{freeCell}{freeCell}{freeCell}{c}{c}[^{c}{freeCell}]");

        private static readonly IDictionary<string, int> Patterns = new Dictionary<string, int>()
        {
            //Five in line
            { "+00000+", -10000 },
            { "+00000X", -10000 },
            { "+00000-", -10000 },
            { "X00000+", -10000 },
            { "X00000X", -10000 },
            { "X00000-", -10000 },
            { "-00000+", -10000 },
            { "-00000X", -10000 },
            { "-00000-", -10000 },

            { "+XXXXX+", 10000 },
            { "+XXXXX0", 10000 },
            { "+XXXXX-", 10000 },
            { "0XXXXX+", 10000 },
            { "0XXXXX0", 10000 },
            { "0XXXXX-", 10000 },
            { "-XXXXX+", 10000 },
            { "-XXXXX0", 10000 },
            { "-XXXXX-", 10000 },

            //Four in line
            { "-0000-", -1000 },
            { "-XXXX-", 1000 },
            { "X0000-", -500 },
            { "0XXXX-", 500 },
            { "+0000-", -500 },
            { "+XXXX-", 500 },
            { "-0000+", -500 },
            { "-XXXX+", 500 },
            { "-0000X", -500 },
            { "-XXXX0", 500 },
            
            //Four with a hole
            { "0-000", -500},
            { "00-00", -500},
            { "000-0", -500},
            { "X-XXX", 500},
            { "XX-XX", 500},
            { "XXX-X", 500},
            
            //Three
            { "--000--", -500},
            { "-0-00-", -500},
            { "-00-0-", -500},
            { "--XXX--", 500},
            { "-X-XX-", 500},
            { "-XX-X-", 500},
            { "X-000--", -400 },
            { "+-000--", -400 },
            { "--000-X", -400 },
            { "--000-+", -400 },
            { "0-XXX--", 400 },
            { "+-XXX--", 400 },
            { "--XXX-0", 400 },
            { "--XXX-+", 400 },
            { "X000--", -250},
            { "+000--", -250},
            { "--000X", -250},
            { "--000+", -250},
            { "0XXX--", 250},
            { "+XXX--", 250},
            { "--XXX0", 250},
            { "--XXX+", 250},
            //Two
            { "--00--", -200 },
            { "--XX--", 200 },
            { "-0-0-", -100 },
            { "-X-X-", 100 },
            { "---00X", -50 },
            { "X00---", -50 },
            { "---XX0", 50 },
            { "0XX---", 50 }
        };
    }
}
