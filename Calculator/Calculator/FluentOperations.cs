using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class FluentOperations
    {
        public double SomeComplexOperation(int x)
        {
            return Decrement(Increment(Decrement(x)));
        }

        private int Increment(int i)
        {
            return ++i;
        }

        private int Decrement(int i)
        {
            return --i;
        }
    }
}
