﻿using gomoku.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace gomoku.ai
{
    public static class Evaluation
    {
        
        public const int WinningValue = 15000;
        private static readonly char freeCell = Color.Undefined.ToChar();
        private static readonly char white = Color.White.ToChar();
        private static readonly char black = Color.Black.ToChar();
        private static readonly IDictionary<Regex, int> Combitations = new Dictionary<Regex, int>()
        {
            { Five(black), 15000 },
            { Five(white), -15000 },
            { FourOpen(black), 5000},
            { FourOpen(white), -5000},
            { FourClosedRight(black), 1000},
            { FourClosedRight(white), -1000},
            { FourClosedLeft(black), 1000},
            { FourClosedLeft(white), -1000},
            { FourHoleLeft(black), 1000},
            { FourHoleCenter(black), 1000},
            { FourHoleRight(black), 1000},
            { FourHoleLeft(white), -1000},
            { FourHoleCenter(white), -1000},
            { FourHoleRight(white), -1000},
            { Three(black), 1000 },
            { ThreeHoleLeft(black), 1000 },
            { ThreeHoleRight(black), 1000 },
            { ThreeLeft(black), 700 },
            { ThreeRight(black), 700 },
            { ThreeClosedLeft(black), 500 },
            { ThreeClosedRight(black), 500 },
            { Two(black), 100 },
            { TwoHole(black), 100 },
            { TwoClosedLeft(black), 50 },
            { TwoClosedRight(black), 50 },
            { Three(white), -1000 },
            { ThreeHoleLeft(white), -1000 },
            { ThreeHoleRight(white), -1000 },
            { ThreeLeft(white), -700 },
            { ThreeRight(white), -700 },
            { ThreeClosedLeft(white), -500 },
            { ThreeClosedRight(white), -500 },
            { Two(white), -100 },
            { TwoHole(white), -100 },
            { TwoClosedLeft(white), -50 },
            { TwoClosedRight(white), -50 },
        };

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

            foreach (var combination in Combitations)
            {
                var count = combination.Key.Matches(allStrings).Count;
                value += count * combination.Value;
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
        private static Regex TwoClosedLeft(char c) => new Regex($@"[^{c}{freeCell}]{c}{c}{freeCell}{freeCell}{freeCell}");
        
        // ---XX0
        private static Regex TwoClosedRight(char c) => new Regex($@"{freeCell}{freeCell}{freeCell}{c}{c}[^{c}{freeCell}]");
    }
}
