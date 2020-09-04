using gomoku.core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace gomoku.test
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void FiguringOutWinnerRow()
        {
            var game = new Game();
            
            for (int i = 0; i < 4; i++)
            {
                game.MakeMove(0, i);
                game.MakeMove(1, i);
            }
            
            Assert.IsFalse(game.IsFinished);
            Assert.AreEqual(Status.Free, game.Winner);

            game.MakeMove(0, 4);

            Assert.IsTrue(game.IsFinished);
            Assert.AreEqual(Status.Black, game.Winner);
        }

        [TestMethod]
        public void FiguringOutWinnerColumn()
        {
            var game = new Game();
            
            for (int i = 0; i < 4; i++)
            {
                game.MakeMove(i, 0);
                game.MakeMove(i, 1);
            }
            
            game.MakeMove(0, 4);
            
            Assert.IsFalse(game.IsFinished);
            Assert.AreEqual(Status.Free, game.Winner);
            
            game.MakeMove(4, 1);

            Assert.IsTrue(game.IsFinished);
            Assert.AreEqual(Status.White, game.Winner);
        }

        [TestMethod]
        public void FiguringOutWinnerDiagonal()
        {
            var game = new Game();

            for (int i = 0; i < 4; i++)
            {
                game.MakeMove(i, i);
                game.MakeMove(i + 1, i);
            }

            Assert.IsFalse(game.IsFinished);
            Assert.AreEqual(Status.Free, game.Winner);

            game.MakeMove(4, 4);

            Assert.IsTrue(game.IsFinished);
            Assert.AreEqual(Status.Black, game.Winner);
        }

        [TestMethod]
        public void SixInLineDoesNotWin()
        {
            var game = new Game();

            for (int i = 0; i < 4; i++)
            {
                game.MakeMove(i, i);
                game.MakeMove(i + 1, i);
            }

            game.MakeMove(5, 5);
            game.MakeMove(6, 5);
            game.MakeMove(4, 4);

            Assert.IsFalse(game.IsFinished);
            Assert.AreEqual(Status.Free, game.Winner);
        }

    }
}
