﻿/*
 * This file is part of the Bucket package. Borrow and improve from https://github.com/maxhauser/semver.
 *
 * (c) Yu Meng Han <menghanyu1994@gmail.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: https://github.com/getbucket/semver/wiki
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bucket.Semver.Tests
{
    [TestClass]
    public class TestsSemVersion
    {
        [TestMethod]
        public void CompareTestWithStrings1()
        {
            Assert.IsTrue(SemVersion.Equals("1.0.0", "1"));
        }

        [TestMethod]
        public void CompareTestWithStrings2()
        {
            var v = new SemVersion(1, 0, 0);
            Assert.IsTrue(v < "1.1");
        }

        [TestMethod]
        public void CompareTestWithStrings3()
        {
            var v = new SemVersion(1, 2);
            Assert.IsTrue(v > "1.0.0");
        }

        [TestMethod]
        public void CreateVersionTest()
        {
            var v = new SemVersion(1, 2, 3, "a", "b");

            Assert.AreEqual(1, v.Major);
            Assert.AreEqual(2, v.Minor);
            Assert.AreEqual(3, v.Patch);
            Assert.AreEqual("a", v.Prerelease);
            Assert.AreEqual("b", v.Build);
        }

        [TestMethod]
        public void CreateVersionTestWithNulls()
        {
            var v = new SemVersion(1, 2, 3, null, null);

            Assert.AreEqual(1, v.Major);
            Assert.AreEqual(2, v.Minor);
            Assert.AreEqual(3, v.Patch);
            Assert.AreEqual(string.Empty, v.Prerelease);
            Assert.AreEqual(string.Empty, v.Build);
        }

        [TestMethod]
        public void CreateVersionTestWithSystemVersion1()
        {
            var nonSemanticVersion = new Version(0, 0);
            var v = new SemVersion(nonSemanticVersion);

            Assert.AreEqual(0, v.Major);
            Assert.AreEqual(0, v.Minor);
            Assert.AreEqual(0, v.Patch);
            Assert.AreEqual(string.Empty, v.Build);
            Assert.AreEqual(string.Empty, v.Prerelease);
        }

        [TestMethod]
        public void CreateVersionTestWithSystemVersion2()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
#pragma warning disable CA1806
                new SemVersion(null);
#pragma warning restore CA1806
            });
        }

        [TestMethod]
        public void CreateVersionTestWithSystemVersion3()
        {
            var nonSemanticVersion = new Version(1, 2, 3, 0);
            var v = new SemVersion(nonSemanticVersion);

            Assert.AreEqual(1, v.Major);
            Assert.AreEqual(2, v.Minor);
            Assert.AreEqual(3, v.Patch);
            Assert.AreEqual(0, v.Revision);
            Assert.AreEqual(string.Empty, v.Build);
            Assert.AreEqual(string.Empty, v.Prerelease);
        }

        [TestMethod]
        public void CreateVersionTestWithSystemVersion4()
        {
            var nonSemanticVersion = new Version(1, 2, 4, 3);
            var v = new SemVersion(nonSemanticVersion);

            Assert.AreEqual(1, v.Major);
            Assert.AreEqual(2, v.Minor);
            Assert.AreEqual(4, v.Patch);
            Assert.AreEqual(3, v.Revision);
            Assert.AreEqual(string.Empty, v.Build);
            Assert.AreEqual(string.Empty, v.Prerelease);
        }

        [TestMethod]
        public void ParseTest1()
        {
            var version = SemVersion.Parse("1.2.45-alpha+nightly.23");

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(45, version.Patch);
            Assert.AreEqual("alpha", version.Prerelease);
            Assert.AreEqual("nightly.23", version.Build);
        }

        [TestMethod]
        public void ParseTest2()
        {
            var version = SemVersion.Parse("1");

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(0, version.Minor);
            Assert.AreEqual(0, version.Patch);
            Assert.AreEqual(string.Empty, version.Prerelease);
            Assert.AreEqual(string.Empty, version.Build);
        }

        [TestMethod]
        public void ParseTest3()
        {
            var version = SemVersion.Parse("1.2.45-alpha-beta+nightly.23.43-bla");

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(45, version.Patch);
            Assert.AreEqual("alpha-beta", version.Prerelease);
            Assert.AreEqual("nightly.23.43-bla", version.Build);
        }

        [TestMethod]
        public void ParseTest4()
        {
            var version = SemVersion.Parse("2.0.0+nightly.23.43-bla");

            Assert.AreEqual(2, version.Major);
            Assert.AreEqual(0, version.Minor);
            Assert.AreEqual(0, version.Patch);
            Assert.AreEqual(string.Empty, version.Prerelease);
            Assert.AreEqual("nightly.23.43-bla", version.Build);
        }

        [TestMethod]
        public void ParseTest5()
        {
            var version = SemVersion.Parse("2.0+nightly.23.43-bla");

            Assert.AreEqual(2, version.Major);
            Assert.AreEqual(0, version.Minor);
            Assert.AreEqual(0, version.Patch);
            Assert.AreEqual(string.Empty, version.Prerelease);
            Assert.AreEqual("nightly.23.43-bla", version.Build);
        }

        [TestMethod]
        public void ParseTest6()
        {
            var version = SemVersion.Parse("2.1-alpha");

            Assert.AreEqual(2, version.Major);
            Assert.AreEqual(1, version.Minor);
            Assert.AreEqual(0, version.Patch);
            Assert.AreEqual("alpha", version.Prerelease);
            Assert.AreEqual(string.Empty, version.Build);
        }

        [TestMethod]
        public void ParseTest7()
        {
            Assert.ThrowsException<ArgumentException>(() => SemVersion.Parse("ui-2.1-alpha"));
        }

        [TestMethod]
        public void ParseTestStrict1()
        {
            var version = SemVersion.Parse("1.3.4", true);

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(3, version.Minor);
            Assert.AreEqual(4, version.Patch);
            Assert.AreEqual(string.Empty, version.Prerelease);
            Assert.AreEqual(string.Empty, version.Build);
        }

        [TestMethod]
        public void ParseTestStrict2()
        {
            Assert.ThrowsException<InvalidOperationException>(() => SemVersion.Parse("1", true));
        }

        [TestMethod]
        public void ParseTestStrict3()
        {
            Assert.ThrowsException<InvalidOperationException>(() => SemVersion.Parse("1.3", true));
        }

        [TestMethod]
        public void ParseTestStrict4()
        {
            Assert.ThrowsException<InvalidOperationException>(() => SemVersion.Parse("1.3-alpha", true));
        }

        [TestMethod]
        public void TryParseTest1()
        {
            SemVersion v;
            Assert.IsTrue(SemVersion.TryParse("1.2.45-alpha-beta+nightly.23.43-bla", out v));
        }

        [TestMethod]
        public void TryParseTest2()
        {
            SemVersion v;
            Assert.IsFalse(SemVersion.TryParse("ui-2.1-alpha", out v));
        }

        [TestMethod]
        public void TryParseTest3()
        {
            SemVersion v;
            Assert.IsFalse(SemVersion.TryParse(string.Empty, out v));
        }

        [TestMethod]
        public void TryParseTest4()
        {
            SemVersion v;
            Assert.IsFalse(SemVersion.TryParse(null, out v));
        }

        [TestMethod]
        public void TryParseTest5()
        {
            SemVersion v;
            Assert.IsTrue(SemVersion.TryParse("1.2", out v, false));
        }

        [TestMethod]
        public void TryParseTest6()
        {
            SemVersion v;
            Assert.IsFalse(SemVersion.TryParse("1.2", out v, true));
        }

        [TestMethod]
        public void ToStringTest()
        {
            var version = new SemVersion(1, 2, 0, "beta", "dev-mha.120");

            Assert.AreEqual("1.2.0-beta+dev-mha.120", version.ToString());
        }

        [TestMethod]
        public void EqualTest1()
        {
            var v1 = new SemVersion(1, 2, build: "nightly");
            var v2 = new SemVersion(1, 2, build: "nightly");

            var r = v1.Equals(v2);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void EqualTest2()
        {
            var v1 = new SemVersion(1, 2, prerelease: "alpha", build: "dev");
            var v2 = new SemVersion(1, 2, prerelease: "alpha", build: "dev");

            var r = v1.Equals(v2);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void EqualTest3()
        {
            var v1 = SemVersion.Parse("1.2-nightly+dev");
            var v2 = SemVersion.Parse("1.2.0-nightly");

            var r = v1.Equals(v2);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EqualTest4()
        {
            var v1 = SemVersion.Parse("1.2-nightly");
            var v2 = SemVersion.Parse("1.2.0-nightly2");

            var r = v1.Equals(v2);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EqualTest5()
        {
            var v1 = SemVersion.Parse("1.2.1");
            var v2 = SemVersion.Parse("1.2.0");

            var r = v1.Equals(v2);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EqualTest6()
        {
            var v1 = SemVersion.Parse("1.4.0");
            var v2 = SemVersion.Parse("1.2.0");

            var r = v1.Equals(v2);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EqualByReferenceTest()
        {
            var v1 = SemVersion.Parse("1.2-nightly");

#pragma warning disable S1764
            var r = v1.Equals(v1);
#pragma warning restore S1764
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void CompareTest1()
        {
            var v1 = SemVersion.Parse("1.0.0");
            var v2 = SemVersion.Parse("2.0.0");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest2()
        {
            var v1 = SemVersion.Parse("1.0.0-beta+dev.123");
            var v2 = SemVersion.Parse("1-beta+dev.123");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(0, r);
        }

        [TestMethod]
        public void CompareTest3()
        {
            var v1 = SemVersion.Parse("1.0.0-alpha+dev.123");
            var v2 = SemVersion.Parse("1-beta+dev.123");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest4()
        {
            var v1 = SemVersion.Parse("1.0.0-alpha");
            var v2 = SemVersion.Parse("1.0.0");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest5()
        {
            var v1 = SemVersion.Parse("1.0.0");
            var v2 = SemVersion.Parse("1.0.0-alpha");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void CompareTest6()
        {
            var v1 = SemVersion.Parse("1.0.0");
            var v2 = SemVersion.Parse("1.0.1-alpha");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest7()
        {
            var v1 = SemVersion.Parse("0.0.1");
            var v2 = SemVersion.Parse("0.0.1+build.12");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest8()
        {
            var v1 = SemVersion.Parse("0.0.1+build.13");
            var v2 = SemVersion.Parse("0.0.1+build.12.2");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void CompareTest9()
        {
            var v1 = SemVersion.Parse("0.0.1-13");
            var v2 = SemVersion.Parse("0.0.1-b");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest10()
        {
            var v1 = SemVersion.Parse("0.0.1+uiui");
            var v2 = SemVersion.Parse("0.0.1+12");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void CompareTest11()
        {
            var v1 = SemVersion.Parse("0.0.1+bu");
            var v2 = SemVersion.Parse("0.0.1");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void CompareTest12()
        {
            var v1 = SemVersion.Parse("0.1.1+bu");
            var v2 = SemVersion.Parse("0.2.1");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest13()
        {
            var v1 = SemVersion.Parse("0.1.1-gamma.12.87");
            var v2 = SemVersion.Parse("0.1.1-gamma.12.88");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest14()
        {
            var v1 = SemVersion.Parse("0.1.1-gamma.12.87");
            var v2 = SemVersion.Parse("0.1.1-gamma.12.87.1");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest15()
        {
            var v1 = SemVersion.Parse("0.1.1-gamma.12.87.99");
            var v2 = SemVersion.Parse("0.1.1-gamma.12.87.X");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareTest16()
        {
            var v1 = SemVersion.Parse("0.1.1-gamma.12.87");
            var v2 = SemVersion.Parse("0.1.1-gamma.12.87.X");

            var r = v1.CompareTo(v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void CompareNullTest()
        {
            var v1 = SemVersion.Parse("0.0.1+bu");
            var r = v1.CompareTo(null);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void TestHashCode()
        {
            var v1 = SemVersion.Parse("1.0.0-1+b");
            var v2 = SemVersion.Parse("1.0.0-1+c");

            var h1 = v1.GetHashCode();
            var h2 = v2.GetHashCode();

            Assert.AreNotEqual(h1, h2);
        }

        [TestMethod]
        public void TestStringConversion()
        {
            SemVersion v = "1.0.0";
            Assert.AreEqual(1, v.Major);
        }

        [TestMethod]
        public void TestUntypedCompareTo()
        {
            var v1 = new SemVersion(1);
            var c = v1.CompareTo((object)v1);

            Assert.AreEqual(0, c);
        }

        [TestMethod]
        public void StaticEqualsTest1()
        {
            var v1 = new SemVersion(1, 2, 3);
            var v2 = new SemVersion(1, 2, 3);

            var r = SemVersion.Equals(v1, v2);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void StaticEqualsTest2()
        {
            var r = SemVersion.Equals(null, null);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void StaticEqualsTest3()
        {
            var v1 = new SemVersion(1);

            var r = SemVersion.Equals(v1, null);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void StaticCompareTest1()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(2);

            var r = SemVersion.Compare(v1, v2);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void StaticCompareTest2()
        {
            var v1 = new SemVersion(1);

            var r = SemVersion.Compare(v1, null);
            Assert.AreEqual(1, r);
        }

        [TestMethod]
        public void StaticCompareTest3()
        {
            var v1 = new SemVersion(1);

            var r = SemVersion.Compare(null, v1);
            Assert.AreEqual(-1, r);
        }

        [TestMethod]
        public void StaticCompareTest4()
        {
            var r = SemVersion.Compare(null, null);
            Assert.AreEqual(0, r);
        }

        [TestMethod]
        public void EqualsOperatorTest()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(1);

            var r = v1 == v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void UnequalOperatorTest()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(2);

            var r = v1 != v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GreaterOperatorTest()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(2);

            var r = v2 > v1;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GreaterOperatorTest2()
        {
            var v1 = new SemVersion(1, 0, 0, "alpha");
            var v2 = new SemVersion(1, 0, 0, "rc");

            var r = v2 > v1;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GreaterOperatorTest3()
        {
            var v1 = new SemVersion(1, 0, 0, "-ci.1");
            var v2 = new SemVersion(1, 0, 0, "alpha");

            var r = v2 > v1;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GreaterOrEqualOperatorTest1()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(1);

            var r = v1 >= v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void GreaterOrEqualOperatorTest2()
        {
            var v1 = new SemVersion(2);
            var v2 = new SemVersion(1);

            var r = v1 >= v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void LessOperatorTest()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(2);

            var r = v1 < v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void LessOperatorTest2()
        {
            var v1 = new SemVersion(1, 0, 0, "alpha");
            var v2 = new SemVersion(1, 0, 0, "rc");

            var r = v1 < v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void LessOperatorTest3()
        {
            var v1 = new SemVersion(1, 0, 0, "-ci.1");
            var v2 = new SemVersion(1, 0, 0, "alpha");

            var r = v1 < v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void LessOrEqualOperatorTest1()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(1);

            var r = v1 <= v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void LessOrEqualOperatorTest2()
        {
            var v1 = new SemVersion(1);
            var v2 = new SemVersion(2);

            var r = v1 <= v2;
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void TestChangeMajor()
        {
            var v1 = new SemVersion(1, 2, 3, "alpha", "dev");
            var v2 = v1.Change(major: 5);

            Assert.AreEqual(5, v2.Major);
            Assert.AreEqual(2, v2.Minor);
            Assert.AreEqual(3, v2.Patch);
            Assert.AreEqual("alpha", v2.Prerelease);
            Assert.AreEqual("dev", v2.Build);
        }

        [TestMethod]
        public void TestChangeMinor()
        {
            var v1 = new SemVersion(1, 2, 3, "alpha", "dev");
            var v2 = v1.Change(minor: 5);

            Assert.AreEqual(1, v2.Major);
            Assert.AreEqual(5, v2.Minor);
            Assert.AreEqual(3, v2.Patch);
            Assert.AreEqual("alpha", v2.Prerelease);
            Assert.AreEqual("dev", v2.Build);
        }

        [TestMethod]
        public void TestChangePatch()
        {
            var v1 = new SemVersion(1, 2, 3, "alpha", "dev");
            var v2 = v1.Change(patch: 5);

            Assert.AreEqual(1, v2.Major);
            Assert.AreEqual(2, v2.Minor);
            Assert.AreEqual(5, v2.Patch);
            Assert.AreEqual("alpha", v2.Prerelease);
            Assert.AreEqual("dev", v2.Build);
        }

        [TestMethod]
        public void TestChangePrerelease()
        {
            var v1 = new SemVersion(1, 2, 3, "alpha", "dev");
            var v2 = v1.Change(prerelease: "beta");

            Assert.AreEqual(1, v2.Major);
            Assert.AreEqual(2, v2.Minor);
            Assert.AreEqual(3, v2.Patch);
            Assert.AreEqual("beta", v2.Prerelease);
            Assert.AreEqual("dev", v2.Build);
        }

        [TestMethod]
        [DataRow("1.0.0", "1.0.0", "==", true)]
        [DataRow("1.0.0", "1", "==", true)]
        [DataRow("1.0.0", "1.0.0-dev", "==", false)]
        [DataRow("1.15.0", "1.13.0-dev", ">", true)]
        [DataRow("1.10.0", "1.2.0-dev", ">", true)]
        [DataRow("1.10.0", "1.10.0", ">=", true)]
        [DataRow("1.13.0-alpha", "1.13.0-dev", ">", true)]
        [DataRow("0.0.6-alpha.1", "0.0.6-alpha.2", "<", true)]
        [DataRow("0.0.6-alpha1", "0.0.6-alpha.2", "<", true)]
        [DataRow("0.0.6-alpha.1", "0.0.6-alpha2", "<", true)]
        [DataRow("0.0.6-alpha2", "0.0.6-alpha1", ">", true)]
        [DataRow("0.0.6-alpha", "0.0.6-alpha.1", "<", true)]
        [DataRow("1.0.0-alpha", "1.0.0-alpha.1", "<", true)]
        [DataRow("1.0.0-alpha.1", "1.0.0-alpha.beta", "<", true)]
        [DataRow("1.0.0-alpha.beta", "1.0.0-beta", "<", true)]
        [DataRow("1.0.0-beta", "1.0.0-beta.2", "<", true)]
        [DataRow("1.0.0-beta.2", "1.0.0-beta.11", "<", true)]
        [DataRow("1.0.0-beta.11", "1.0.0-rc", "<", true)]
        [DataRow("1.0.0-beta.11", "1.0.0-rc.1", "<", true)]
        [DataRow("1.0.0-rc.1", "1.0.0", "<", true)]
        [DataRow("1.10.0-foo", "1.10.0-dev", "<", true)]
        [DataRow("1.10.0-beta", "1.10.0-alpha", ">", true)]
        [DataRow("1.10.0-pl", "1.10.0-stable", "<", true)]
        [DataRow("1.13.0", "1.12.9-dev", "<", false)]
        [DataRow("1.13.0", "1.13.0", "<=", true)]
        [DataRow("1.0.0", "2.0.0", "!=", true)]
        [DataRow("1.0.0", "2", "!=", true)]
        public void TestCompareWithOperator(string left, string right, string @operator, bool expected)
        {
            Assert.AreEqual(expected, SemVersion.Compare(left, right, @operator));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompareWithOperatorFaild()
        {
            SemVersion.Compare("1.0", "2.0", "faild operator");
        }
    }
}
