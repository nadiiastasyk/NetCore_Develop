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

        public int Fibonacci(int n)
        {
            int a = 0;
            int b = 1;

            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }

            return a;
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
