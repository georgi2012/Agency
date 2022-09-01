//using Agency.Core;
using Agency.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Agency.Tests
{
    [TestClass]
    public class IOTests
    {
        // The tests are located in the Tests folder
        // Each test has two files - "in" and "out"
        // The "in" file is the input
        // The "out" file is the expected output of your program

        [TestMethod]
        public void Test_001()
        {
            this.ExecuteIOTest("001");
        }

        [TestMethod]
        public void Test_002()
        {
            this.ExecuteIOTest("002");
        }

        [TestMethod]
        public void Test_003()
        {
            this.ExecuteIOTest("003");
        }

        [TestMethod]
        public void Test_004()
        {
            this.ExecuteIOTest("004");
        }

        [TestMethod]
        public void Test_005()
        {
            this.ExecuteIOTest("005");
        }

        [TestMethod]
        public void Test_006()
        {
            this.ExecuteIOTest("006");
        }

        [TestMethod]
        public void Test_007()
        {
            this.ExecuteIOTest("007");
        }

        [TestMethod]
        public void Test_008()
        {
            this.ExecuteIOTest("008");
        }

        [TestMethod]
        public void Test_009()
        {
            this.ExecuteIOTest("009");
        }

        [TestMethod]
        public void Test_010()
        {
            this.ExecuteIOTest("010");
        }

        [TestMethod]
        public void Test_011()
        {
            this.ExecuteIOTest("011");
        }

        [TestMethod]
        public void Test_012()
        {
            this.ExecuteIOTest("012");
        }

        [TestMethod]
        public void Test_013()
        {
            this.ExecuteIOTest("013");
        }

        [TestMethod]
        public void Test_014()
        {
            this.ExecuteIOTest("014");
        }

        [TestMethod]
        public void Test_015()
        {
            this.ExecuteIOTest("015");
        }

        [TestMethod]
        public void Test_016()
        {
            this.ExecuteIOTest("016");
        }

        [TestMethod]
        public void Test_017()
        {
            this.ExecuteIOTest("017");
        }

        [TestMethod]
        public void Test_018()
        {
            this.ExecuteIOTest("018");
        }

        [TestMethod]
        public void Test_019()
        {
            this.ExecuteIOTest("019");
        }

        [TestMethod]
        public void Test_020()
        {
            this.ExecuteIOTest("020");
        }

        [TestMethod]
        public void Test_021()
        {
            this.ExecuteIOTest("021");
        }

        [TestMethod]
        public void Test_022()
        {
            this.ExecuteIOTest("022");
        }

        [TestMethod]
        public void Test_023()
        {
            this.ExecuteIOTest("023");
        }

        [TestMethod]
        public void Test_024()
        {
            this.ExecuteIOTest("024");
        }

        [TestMethod]
        public void Test_025()
        {
            this.ExecuteIOTest("025");
        }

        private void ExecuteIOTest(string testNumber)
        {
            var testInput = new StreamReader("../../Tests/test." + testNumber + ".in.txt");
            var testOutput = new StreamReader("../../Tests/test." + testNumber + ".out.txt");
            var programOutput = new StringWriter();

            Console.SetIn(testInput);
            Console.SetOut(programOutput);

            // An example of how you can set the value of a private field using reflection. 
            var instance = typeof(Engine).GetField("instanceHolder", BindingFlags.Static | BindingFlags.NonPublic);
            instance.SetValue(null, null);

            Engine.Instance.Start();

            var expected = testOutput.ReadToEnd().Trim();
            var actual = programOutput.ToString().Trim();

            Assert.AreEqual(expected, actual);
        }
    }
}
