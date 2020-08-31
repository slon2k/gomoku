using gomoku.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace gomoku.ai
{
    public static class Evaluation
    {
        
        public static int Evaluate(Board board)
        {
            int value = 0;
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
        
        private static IDictionary<string, int> Patterns = new Dictionary<string, int> 
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
            { "XX-XX", -500},
            { "XXX-X", -500},
            
            //Three
            { "--000--", -500},
            { "-0-00-", -500},
            { "-00-0-", -500},
            { "--XXX--", 500},
            { "-X-XX-", -500},
            { "-X-XX-", -500},
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
            { "0XX---", 50 },
        };

        

    }
}
