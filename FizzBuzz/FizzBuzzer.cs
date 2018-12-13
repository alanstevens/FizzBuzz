using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FizzBuzz
{
    internal class FizzBuzzer
    {
        public IEnumerable<int> ReturnNumbers1to100()
        {
            for (var i = 1; i < 101; i++) yield return i;
        }

        public IEnumerable<string> DoFizzBuzz(int upperLimit = 100)
        {
            if (upperLimit < 1) throw new ArgumentException("Upper limit must be greater than zero.");
            for (var i = 1; i <= upperLimit; i++)
                yield return GetFizzBuzzValue(i);
        }

        public string GetFizzBuzzValue(int num)
        {
            if (IsFizzBuzz(num)) return "FizzBuzz";
            if (IsFizz(num)) return "Fizz";
            if (IsBuzz(num)) return "Buzz";
            return num.ToString();
        }

        public bool IsFizzBuzz(int num)
        {
            if (num % 3 == 0 && num % 5 == 0) return true;
            return false;
        }

        public bool IsFizz(int num)
        {
            if (num % 3 == 0) return true;
            return false;
        }

        public bool IsBuzz(int num)
        {
            if (num % 5 == 0) return true;
            return false;
        }
    }

    public class FizzBuzzTests
    {
        public FizzBuzzTests(ITestOutputHelper output)
        {
            _output = output;
            _fizzBuzzer = new FizzBuzzer();
        }

        private readonly ITestOutputHelper _output;

        private readonly FizzBuzzer _fizzBuzzer;

        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(9)]
        [InlineData(12)]
        [InlineData(18)]
        [InlineData(21)]
        [InlineData(24)]
        [InlineData(27)]
        [InlineData(33)]
        [InlineData(36)]
        [InlineData(39)]
        [InlineData(42)]
        [InlineData(48)]
        [InlineData(51)]
        [InlineData(54)]
        [InlineData(57)]
        [InlineData(63)]
        [InlineData(66)]
        [InlineData(69)]
        [InlineData(72)]
        [InlineData(78)]
        [InlineData(81)]
        [InlineData(84)]
        [InlineData(87)]
        [InlineData(93)]
        [InlineData(96)]
        [InlineData(99)]
        public void should_return_fizz(int val)
        {
            _fizzBuzzer.IsFizz(val).Should().BeTrue();
            _fizzBuzzer.GetFizzBuzzValue(val).Should().Be("Fizz");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(25)]
        [InlineData(35)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(55)]
        [InlineData(65)]
        [InlineData(70)]
        [InlineData(80)]
        [InlineData(85)]
        [InlineData(95)]
        [InlineData(100)]
        public void should_return_buzz(int val)
        {
            _fizzBuzzer.IsBuzz(val).Should().BeTrue();
            _fizzBuzzer.GetFizzBuzzValue(val).Should().Be("Buzz");
        }

        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(45)]
        [InlineData(60)]
        [InlineData(75)]
        [InlineData(90)]
        public void should_return_fizzbuzz(int val)
        {
            _fizzBuzzer.IsFizzBuzz(val).Should().BeTrue();
            _fizzBuzzer.GetFizzBuzzValue(val).Should().Be("FizzBuzz");
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(150)]
        [InlineData(200)]
        public void should_print_fizzbuzz_for_an_arbitrary_upper_bound(int val)
        {
            var nums = _fizzBuzzer.DoFizzBuzz(val).ToArray();
            nums.Length.Should().Be(val);
            foreach (var num in nums)
                _output.WriteLine(num);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(11)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(19)]
        public void should_return_self(int val)
        {
            _fizzBuzzer.IsFizzBuzz(val).Should().BeFalse();
            _fizzBuzzer.IsFizz(val).Should().BeFalse();
            _fizzBuzzer.IsBuzz(val).Should().BeFalse();
            _fizzBuzzer.GetFizzBuzzValue(val).Should().Be(val.ToString());
        }

        [Fact]
        public void should_print_one_to_100()
        {
            var nums = _fizzBuzzer.ReturnNumbers1to100().ToArray();
            nums.First().Should().Be(1);
            nums.Last().Should().Be(100);
            foreach (var num in nums)
                _output.WriteLine(num.ToString());
        }

        [Fact]
        public void should_throw_argument_exception()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => _fizzBuzzer.DoFizzBuzz(0).ToArray());
            ex.Message.Should().Be("Upper limit must be greater than zero.", ex.Message);
        }
    }
}