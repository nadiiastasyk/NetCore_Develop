using BowlingGame;
using Xunit;

namespace BowlingGameTests
{
    public class GameTests
    {
        private readonly Game _game;

        public GameTests()
        {
            _game = new Game();
        }

        [Fact]
        public void Game_FullGameZeros_Success()
        {
            RollMany(20, 0);

            Assert.Equal(_game.Score(), 0);
        }

        [Fact]
        public void Game_FullGameOnes_Success()
        {
            RollMany(20, 1);

            Assert.Equal(_game.Score(), 20);
        }

        [Fact]
        public void Game_OneSpareWithZeroes_Success()
        {
            RollSpare();
            RollMany(18, 0);

            Assert.Equal(_game.Score(), 10);
        }

        [Fact]
        public void Game_OneSpareWithOnes_Success()
        {
            RollSpare();
            RollMany(18, 1);

            Assert.Equal(_game.Score(), 29);
        }

        [Fact]
        public void Game_OneStrikeWithOnes_Success()
        {
            _game.Roll(10);
            _game.Roll(5);
            _game.Roll(3);
            RollMany(17, 0);

            Assert.Equal(_game.Score(), 26);
        }

        [Fact]
        public void Game_PerfectGame_Success()
        {
            RollMany(12, 10);

            Assert.Equal(_game.Score(), 300);
        }

        [Fact]
        public void Game_AllSpare_Success()
        {
            RollMany(21, 5);

            Assert.Equal(_game.Score(), 155);
        }

        private void RollSpare()
        {
            _game.Roll(5);
            _game.Roll(5);
        }

        private void RollMany(int times, int pins)
        {
            for (int i = 0; i < times; i++)
            {
                _game.Roll(pins);
            }
        }
    }
}
