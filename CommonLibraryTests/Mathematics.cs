/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     CommonLibraryTests
 * FILE:        CommonLibraryTests/Mathematics.cs
 * PURPOSE:     Tests for the Math Tools
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ExtendedSystemObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLibraryTests
{
    /// <summary>
    ///     Test some image related stuff
    /// </summary>
    [TestClass]
    public class Mathematics
    {
        /// <summary>
        ///     Test the custom DirectBitmap and how it works
        /// </summary>
        [TestMethod]
        public void Fractures()
        {
            //TODO more Tests!
            var one = new ExtendedMath.Fraction(1, 2, 2);
            Assert.AreEqual(5, one.ExponentNumerator, string.Concat("Test Failed: ", one.ExponentNumerator));
            var two = new ExtendedMath.Fraction(1, 2, 2);
            Assert.AreEqual(5, two.ExponentNumerator, string.Concat("Test Failed: ", two.ExponentNumerator));
            var result = one + two;

            Assert.AreEqual(1, result.Numerator, string.Concat("Test Failed: ", result.Numerator));
            Assert.AreEqual(1, result.Denominator, string.Concat("Test Failed: ", result.Denominator));
            Assert.AreEqual(5, result.Exponent, string.Concat("Test Failed: ", result.Exponent));
            Assert.AreEqual(5, result.ExponentNumerator, string.Concat("Test Failed: ", result.ExponentNumerator));
            Assert.AreEqual(5, result.Decimal, string.Concat("Test Failed: ", result.Decimal));

            result = one - two;

            Assert.AreEqual(1, result.Numerator, string.Concat("Test Failed: ", result.Numerator));
            Assert.AreEqual(1, result.Denominator, string.Concat("Test Failed: ", result.Denominator));
            Assert.AreEqual(0, result.Exponent, string.Concat("Test Failed: ", result.Exponent));
            Assert.AreEqual(0, result.ExponentNumerator, string.Concat("Test Failed: ", result.ExponentNumerator));
            Assert.AreEqual(0, result.Decimal, string.Concat("Test Failed: ", result.Decimal));

            one = new ExtendedMath.Fraction(14, 2);
            Assert.AreEqual(1, one.Numerator, string.Concat("Test Failed: ", result.Numerator));
            Assert.AreEqual(1, one.Denominator, string.Concat("Test Failed: ", result.Denominator));
            Assert.AreEqual(7, one.Exponent, string.Concat("Test Failed: ", result.Exponent));
            Assert.AreEqual(7, one.ExponentNumerator, string.Concat("Test Failed: ", one.ExponentNumerator));
            Assert.AreEqual(7, one.Decimal, string.Concat("Test Failed: ", result.Decimal));


            one = new ExtendedMath.Fraction(14, 8);
            Assert.AreEqual(3, one.Numerator, string.Concat("Test Failed: ", result.Numerator));
            Assert.AreEqual(4, one.Denominator, string.Concat("Test Failed: ", result.Denominator));
            Assert.AreEqual(1, one.Exponent, string.Concat("Test Failed: ", result.Exponent));
            Assert.AreEqual(7, one.ExponentNumerator, string.Concat("Test Failed: ", one.ExponentNumerator));
            Assert.AreEqual((decimal)1.75, one.Decimal, string.Concat("Test Failed: ", result.Decimal));
        }
    }
}
