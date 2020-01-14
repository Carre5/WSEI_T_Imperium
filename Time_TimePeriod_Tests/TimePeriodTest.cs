using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_and_TimePeriod;

namespace Time_TimePeriod_Tests
{
    [TestClass]
    public class TimePeriodTest
    {
        [TestMethod]
        [DataRow(29, "0:0:29")]
        [DataRow(3665, "1:1:5")]
        [DataRow(150, "0:2:30")]
        public void Test_TimePeriod_Constructor_1(long seconds, string expected)
        {
            TimePeriod tp1 = new TimePeriod(seconds);
            TimePeriod tp2 = new TimePeriod(expected);
            Assert.AreEqual(tp1, tp2);
        }

        [TestMethod]
        [DataRow("10:20:00", "5:10:00", "5:10:00")]
        [DataRow("1:1:1", "2:2:2", "1:1:1")]
        public void Test_TimePeriod_Constructor_2(string s1, string s2, string expected)
        {
            Time t1 = new Time(s1);
            Time t2 = new Time(s2);

            TimePeriod tp1 = new TimePeriod(t1, t2);

            Assert.AreEqual(tp1, new TimePeriod(expected));
        }

        [TestMethod]
        [DataRow(29, 62, "30:2:00")]
        [DataRow(56, 43, "56:43:00")]
        [DataRow(15, 78, "16:18:00")]
        public void Test_TimePeriod_Constructor_3(int h, int m, string expected)
        {
            TimePeriod tp1 = new TimePeriod(h, m);
            TimePeriod tp2 = new TimePeriod(expected);
            Assert.AreEqual(tp1, tp2);
        }

        [TestMethod]
        [DataRow(29, 62, 78, "30:3:18")]
        [DataRow(150, 15, 27, "150:15:27")]
        [DataRow(10, 10, 10, "10:10:10")]
        public void Test_TimePeriod_Constructor_4(int h, int m, int s, string expected)
        {
            TimePeriod tp1 = new TimePeriod(h, m, s);
            TimePeriod tp2 = new TimePeriod(expected);
            Assert.AreEqual(tp1, tp2);
        }

        [TestMethod]
        [DataRow("30:3:18", 29, 62, 78)]
        [DataRow("150:15:27", 150, 15, 27)]
        [DataRow("10:10:10", 10, 10, 10)]
        public void Test_TimePeriod_Constructor_5(string input, int eh, int em, int es)
        {
            TimePeriod tp1 = new TimePeriod(input);
            TimePeriod tp2 = new TimePeriod(eh, em, es);
            Assert.AreEqual(tp1, tp2);
        }

        [TestMethod]
        [DataRow("20:3:18", "30:3:18","50:6:36")]
        [DataRow("10:10:10","150:15:27","160:25:37")]
        [DataRow("10:10:10", "10:10:10", "20:20:20")]
        public void Test_TimePeriod_Constructor_6(string s1, string s2, string expected)
        {
            Time t1 = new Time(s1);
            TimePeriod tp1 = new TimePeriod(s2);
            TimePeriod tp2 = new TimePeriod(expected);

            Assert.AreEqual(new TimePeriod(t1,tp1), tp2);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", true)]
        [DataRow("70:89:18", "30:3:18", false)]
        [DataRow("10:10:10", "30:3:18", false)]
        public void Test_TimePeriod_Equals(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual(tp1.Equals(tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", 0)]
        [DataRow("70:54:18", "30:3:18", 1)]
        [DataRow("10:89:18", "30:3:18", -1)]
        public void Test_TimePeriod_CompareTo(string s1, string s2, int expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual(tp1.CompareTo(tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", false)]
        [DataRow("10:89:18", "30:3:18", true)]
        [DataRow("70:54:18", "30:3:18", false)]
        public void Test_TimePeriod_LessThan_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 < tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", true)]
        [DataRow("10:89:18", "30:3:18", true)]
        [DataRow("70:54:18", "30:3:18", false)]
        public void Test_TimePeriod_LessOrEqual_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 <= tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", true)]
        [DataRow("70:54:18", "30:3:18", false)]
        [DataRow("10:89:18", "30:3:18", false)]
        public void Test_TimePeriod_Equal_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 == tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", false)]
        [DataRow("70:54:18", "30:3:18", true)]
        [DataRow("10:89:18", "30:3:18", true)]
        public void Test_TimePeriod_NotEqual_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 != tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", true)]
        [DataRow("70:54:18", "30:3:18", true)]
        [DataRow("10:89:18", "30:3:18", false)]
        public void Test_TimePeriod_MoreOrEqual_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 >= tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", false)]
        [DataRow("70:54:18", "30:3:18", true)]
        [DataRow("10:89:18", "30:3:18", false)]
        public void Test_TimePeriod_More_operator(string s1, string s2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);

            Assert.AreEqual((tp1 > tp2), expected);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", "60:6:36")]
        [DataRow("70:54:18", "30:3:18", "100:57:36")]
        [DataRow("10:89:18", "30:3:18", "41:32:36")]
        public void Test_TimePeriod_Plus_1(string s1, string s2, string s3)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);
            TimePeriod tp3 = new TimePeriod(s3);

            Assert.AreEqual(tp1.Plus(tp2), tp3);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", "60:6:36")]
        [DataRow("70:54:18", "30:3:18", "100:57:36")]
        [DataRow("10:89:18", "30:3:18", "41:32:36")]
        public void Test_TimePeriod_Plus_2(string s1, string s2, string s3)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);
            TimePeriod tp3 = new TimePeriod(s3);

            Assert.AreEqual(tp1.Plus(tp1, tp2), tp3);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", "0:0:0")]
        [DataRow("70:54:18", "30:3:18", "40:51:0")]
        public void Test_TimePeriod_Minus(string s1, string s2, string s3)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);
            TimePeriod tp3 = new TimePeriod(s3);

            Assert.AreEqual(tp1.Minus(tp2), tp3);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", "60:6:36")]
        [DataRow("70:54:18", "30:3:18", "100:57:36")]
        [DataRow("10:89:18", "30:3:18", "41:32:36")]
        public void Test_TimePeriod_Plus_operator(string s1, string s2, string s3)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);
            TimePeriod tp3 = new TimePeriod(s3);

            Assert.AreEqual(tp1 + tp2, tp3);
        }

        [TestMethod]
        [DataRow("30:3:18", "30:3:18", "0:0:0")]
        [DataRow("70:54:18", "30:3:18", "40:51:0")]
        public void Test_TimePeriod_Minus_operator(string s1, string s2, string s3)
        {
            TimePeriod tp1 = new TimePeriod(s1);
            TimePeriod tp2 = new TimePeriod(s2);
            TimePeriod tp3 = new TimePeriod(s3);

            Assert.AreEqual(tp1 - tp2, tp3);
        }
    }
}
