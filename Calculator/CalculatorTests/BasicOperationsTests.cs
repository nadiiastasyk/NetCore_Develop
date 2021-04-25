using Calculator;
using Xunit;

namespace CalculatorTests
{
    public class BasicOperationsTests
    {
        [Theory]
        [InlineData(3, 5, 8)]
        [InlineData(-5, 5, 0)]
        [InlineData(102312, 12498, 114810)]
        public void Add_TwoPositiveNumbers_ResultIsEqualToSumOfNumbers(int firstNumber, int secondNumber, int sum)
        {
            // Arrange
            BasicOperations sut = new BasicOperations();

            // Act
            int result = sut.Add(firstNumber, secondNumber);

            // Assert
            Assert.Equal(sum, result);
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(5, 5, 0)]
        [InlineData(114810, 12498, 102312)]
        public void Substract_TwoPositiveNumbers_ResultIsEqualToSubstractionOfNumbers(int firstNumber, int secondNumber, int substract)
        {
            // Arrange
            BasicOperations sut = new BasicOperations();

            // Act
            int result = sut.Substract(firstNumber, secondNumber);

            // Assert
            Assert.Equal(substract, result);
        }
    }
}
