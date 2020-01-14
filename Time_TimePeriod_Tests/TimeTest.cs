using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_and_TimePeriod;

namespace Time_TimePeriod_Tests
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        [DataRow(29, 62,78, 5, 2, 18)]
        [DataRow(-9, -120, -45, 9, 0, 45)]
        [DataRow(24, 60, 60, 0, 0, 0)]
        public void Test_Time_Constructor1(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            
            Assert.AreEqual(t1,t2);
        }

        [TestMethod]
        [DataRow("78:98:1560",6,38,0)]
        [DataRow("-26:-90:-5", 2, 30, 5)]
        [DataRow("aa:bb:cc", 0, 0, 0)]
        [DataRow("**:__:$$", 0, 0, 0)]
        [DataRow("**-__+$$", 0, 0, 0)]
        public void Test_Time_Constructor2(string str, int h, int m, int s)
        {
            Time t1 = new Time(str);
            Time t2 = new Time(h, m, s);

            Assert.AreEqual(t1,t2);
        }

        [TestMethod]
        [DataRow("29:62:78", "5:2:18",true)]
        [DataRow("-26:-90:-5","2:30:5",true)]
        public void Test_Time_Equals(string str1, string str2,bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1.Equals(t2), expected);
        }

        [TestMethod]
        [DataRow("29:62:78", "5:2:18", 0)]
        [DataRow("29:62:78", "5:2:17", 1)]
        [DataRow("29:62:78", "5:2:19", -1)]
        public void Test_Time_Compare(string str1, string str2, int expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1.CompareTo(t2), expected);
        }

        [TestMethod]
        [DataRow("29:62:78", "5:2:18", true)]
        [DataRow("15:42:23", "14:60:18", false)]
        public void Test_Time_OperatorEqual(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 == t2, expected);
        }

        [TestMethod]
        [DataRow("29:62:78", "5:2:18", false)]
        [DataRow("15:42:23", "14:60:18", true)]
        public void Test_Time_OperatorNotEqual(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 != t2, expected);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", true)]
        [DataRow("00:00:00", "00:00:00", false)]
        [DataRow("03:23:01", "02:00:00", true)]
        [DataRow("15:42:23", "14:58:18", true)]
        [DataRow("02:00:00", "03:23:01", false)]
        public void Test_Time_OperatorMoreThan(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 > t2, expected);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", false)]
        [DataRow("00:00:00", "00:00:00", false)]
        [DataRow("03:23:01", "02:00:00", false)]
        [DataRow("15:42:23", "14:58:18", false)]
        [DataRow("02:00:00", "03:23:01", true)]
        public void Test_Time_OperatorLessThan(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 < t2, expected);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", true)]
        [DataRow("00:00:00", "00:00:00", true)]
        [DataRow("03:23:01", "02:00:00", true)]
        [DataRow("15:42:23", "14:58:18", true)]
        [DataRow("02:00:00", "03:23:01", false)]
        public void Test_Time_OperatorMoreThanOrEqual(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 >= t2, expected);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", false)]
        [DataRow("00:00:00", "00:00:00", true)]
        [DataRow("03:23:01", "02:00:00", false)]
        [DataRow("15:42:23", "14:58:18", false)]
        [DataRow("02:00:00", "03:23:01", true)]
        public void Test_Time_OperatorLessThanOrEqual(string str1, string str2, bool expected)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(t1 <= t2, expected);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("03:23:01", "02:00:00", "05:23:01")]
        [DataRow("15:42:23", "14:58:18", "29:100:41")]
        [DataRow("02:00:00", "03:23:01", "05:23:01")]
        public void Test_Time_OperatorPlus(string str1, string str2, string str3)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            Time t3 = new Time(str3);

            Assert.AreEqual(t1 + t2, t3);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("03:23:01", "02:00:00", "01:23:01")]
        [DataRow("15:42:23", "14:58:18", "0:44:5")]
        [DataRow("02:00:00", "03:23:01", "22:36:59")]

        public void Test_Time_OperatorMinus(string str1, string str2, string str3)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            Time t3 = new Time(str3);

            Assert.AreEqual(t1 - t2, t3);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("03:23:01", "02:00:00", "05:23:01")]
        [DataRow("15:42:23", "14:58:18", "29:100:41")]
        [DataRow("02:00:00", "03:23:01", "05:23:01")]
        public void Test_Time_Plus(string str1, string str2, string str3)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);
            Time t3 = new Time(str3);

            Assert.AreEqual(t1.Plus(t2), t3);
        }

        [TestMethod]
        [DataRow("00:00:01", "00:00:00", "00:00:01")]
        [DataRow("00:00:00", "00:00:00", "00:00:00")]
        [DataRow("03:23:01", "02:00:00", "01:23:01")]
        [DataRow("15:42:23", "14:58:18", "0:44:5")]
        [DataRow("02:00:00", "03:23:01", "22:36:59")]

        public void Test_Time_Minus(string str1, string str2, string str3)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);
            Time t3 = new Time(str3);

            Assert.AreEqual(t1.Minus(t2), t3);
        }
    }
}
