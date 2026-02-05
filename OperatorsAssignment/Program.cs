﻿using System;

namespace U1B2_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== CAP_U1B2: Complex Numbers Boot Camp ===\n");
            
            Console.WriteLine("=== PART A: Construction + Display ===");

            
            //   a = 3 + 4i
            //   b = -2 + 5i
            
            var a = new Complex(3, 4);
            var b = new Complex(-2, 5);
            Console.WriteLine(a);
            Console.WriteLine(b);

            
            Console.WriteLine("\n=== PART B: Magnitude + Conjugate ===");
            
            Console.WriteLine($"|a| = {a.Magnitude()}");

            
            Console.WriteLine($"conj(b) = {b.Conjugate()}");
            
            Console.WriteLine("\n=== PART C: Add/Subtract (methods) ===");

           
            var sum = a.Add(b);
            var diff = a.Subtract(b);
            Console.WriteLine($"a + b (method) = {sum}");
            Console.WriteLine($"a - b (method) = {diff}");
            
            Console.WriteLine("\n=== PART D: Multiply + Operator Overloads ===");
            
            var product = a.Multiply(b);
            Console.WriteLine($"a * b (method) = {product}");

          
            Console.WriteLine($"a + b (operator) = {a + b}");
            Console.WriteLine($"a - b (operator) = {a - b}");
            Console.WriteLine($"a * b (operator) = {a * b}");
            
            Console.WriteLine("\n=== PART E: Division ===");

            var q1 = a.Divide(b);
            Console.WriteLine($"a / b (method) = {q1}");
            var q2 = a / b;
            Console.WriteLine($"a / b (operator) = {q2}");
            
            Console.WriteLine("\n=== PART F: Equality ===");
            
            var c = new Complex(1.0, 9.0);
            Console.WriteLine($"(a + b) == c ? { (a + b) == c }");
            
            Console.WriteLine("\n=== PART G: Mini Test Suite ===");


            RunAllTests();

            Console.WriteLine("\n=== Done (for now). If something broke, it’s probably math being dramatic. ===\n");
        }

       
        static void RunAllTests()
        {
            var a = new Complex(3, 4);
            var b = new Complex(-2, 5);

            AssertTrue(a.ToString() == "3 + 4i", "ToString A");
            AssertTrue(b.ToString() == "-2 + 5i", "ToString B");

            AssertNear(a.Magnitude(), 5.0, 1e-9, "Magnitude");

            AssertTrue(b.Conjugate().ToString() == "-2 - 5i", "Conjugate");

            AssertTrue((a.Add(b)).ToString() == "1 + 9i", "Add method");
            AssertTrue((a - b).ToString() == "5 - 1i", "Subtract operator");

            AssertTrue((a.Multiply(b)).ToString() == "-26 + 7i", "Multiply method");
            AssertTrue((a * b).ToString() == "-26 + 7i", "Multiply operator");

            var c = new Complex(1.0, 9.0);
            AssertTrue((a + b) == c, "Equality with EPS");
            
            var one = b / b;
            AssertTrue(one == new Complex(1, 0), "Division (b/b)");

           
            try
            {
                var z = new Complex(0, 0);
                var boom = a / z;
                Console.WriteLine("FAIL: DivideByZero (no exception)");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("PASS: DivideByZero");
            }
        }

        static void AssertTrue(bool condition, string testName)
        {
            Console.WriteLine((condition ? "PASS: " : "FAIL: ") + testName);
        }

        static void AssertNear(double actual, double expected, double eps, string testName)
        {
            bool ok = Math.Abs(actual - expected) <= eps;
            Console.WriteLine((ok ? "PASS: " : "FAIL: ") + testName + $" (got {actual}, expected {expected})");
        }
    }

    public class Complex
    {
        private double _real;
        private double _imag;

        public Complex(double real, double imag)
        {
            _real = real;
            _imag = imag;
        }

        public double Real => _real;
        public double Imag => _imag;

        public override string ToString()
        {
            
            if (_imag >= 0)
            {
                return $"{_real} + {_imag}i";
            }
            else
            {
                return $"{_real} - {Math.Abs(_imag)}i";
            }
        }

      
        public double Magnitude()
        {
            return Math.Sqrt(_real * _real + _imag * _imag);
        }

        public Complex Conjugate()
        {
            return new Complex(_real, -_imag);
        }

      
        public Complex Add(Complex other)
        {
            return new Complex(_real + other._real, _imag + other._imag);
        }
        
        public Complex Subtract(Complex other)
        {
            return new Complex(_real - other._real, _imag - other._imag);
        }
        
        public Complex Multiply(Complex other)
        {
            double real = _real * other._real - _imag * other._imag;
            double imag = _real * other._imag + _imag * other._real;
            return new Complex(real, imag);
        }
        
        public Complex Divide(Complex other)
        {
            double denom = other._real * other._real + other._imag * other._imag;
            if (denom == 0.0)
            {
                throw new DivideByZeroException("Cannot divide by 0 + 0i");
            }

            double real = (_real * other._real + _imag * other._imag) / denom;
            double imag = (_imag * other._real - _real * other._imag) / denom;
            return new Complex(real, imag);
        }
        
        public static Complex operator +(Complex a, Complex b) => a.Add(b);
        public static Complex operator -(Complex a, Complex b) => a.Subtract(b);
        public static Complex operator *(Complex a, Complex b) => a.Multiply(b);
        public static Complex operator /(Complex a, Complex b) => a.Divide(b);
        
        private const double EPS = 1e-9;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            if (obj is Complex other)
            {
                return Math.Abs(_real - other._real) <= EPS && Math.Abs(_imag - other._imag) <= EPS;
            }
            return false;
        }

        public override int GetHashCode()
        {
            double r = Math.Round(_real, 9);
            double i = Math.Round(_imag, 9);
            return HashCode.Combine(r, i);
        }

        public static bool operator ==(Complex? a, Complex? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Complex? a, Complex? b) => !(a == b);
    }
}