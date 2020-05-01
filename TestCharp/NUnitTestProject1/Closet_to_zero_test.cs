using System;
using System.Collections.Generic;
using NUnit.Framework;
using ClosestToZero;
namespace Tests
{

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void If_list_has_a_zero()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int>{0,3,0});
            Assert.AreEqual(closest,0);
        }

        [Test]
        public void If_list_has_a_single_number()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int> { 33 });
            Assert.AreEqual(closest, 33);
        }

        [Test]
        public void if_list_is_empty()
        {
            //Closest_to_zero.ClosestNumberToZero(new List<int> {});
            Assert.Throws<ArgumentException>(() => Closest_to_zero.ClosestNumberToZero(new List<int> { }));

        }

        [Test]
        public void if_list_has_only_postive_numbers()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int> { 21,33,11,1111 });
            Assert.AreEqual(closest, 11);

        }

        [Test]
        public void if_list_has_only_negative_numbers()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int> { -21, -33, -11, -1111 });
            Assert.AreEqual(closest, -11);
        }

        [Test]
        public void if_list_has_a_mix_of_postive_and_negative_numbers()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int> { 21, -33, 11, -1111 });
            Assert.AreEqual(closest, 11);
        }

        [Test]
        public void if_test_has_a_tie_between_positive_and_negative_closest()
        {
            int closest = Closest_to_zero.ClosestNumberToZero(new List<int> { 21, -11, 11, 1111 });
            Assert.AreEqual(closest, 11);
        }
    }
}