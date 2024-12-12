using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace YourProjectName.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Ensure the data file is reset before each test
            if (File.Exists("info.txt"))
            {
                File.Delete("info.txt");
            }
        }

        [TestMethod]
        public void TestStudentCanEnterStatus()
        {
            // Arrange
            var input = new StringReader("John\n3\n1\nFeeling okay\n2\n");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.StudentInteraction();

            // Assert
            var lines = File.ReadAllLines("info.txt");
            Assert.AreEqual("John", lines[0]);
            Assert.AreEqual("ok", lines[1]);
            Assert.AreEqual("Feeling okay", lines[2]);
            Assert.AreEqual("2", lines[3]);
            Assert.AreEqual("No meeting booked", lines[4]);
        }

        [TestMethod]
        public void TestPSCanCheckStudentStatus()
        {
            // Arrange
            var data = "John\nok\nFeeling okay\n2\nNo meeting booked";
            Program.SaveToFile(data);

            var input = new StringReader("12345\n1\n");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.PSInteraction();

            // Assert
            var outputString = output.ToString();
            Assert.IsTrue(outputString.Contains("Access granted. Welcome to the system, Nia Lee."));
            Assert.IsTrue(outputString.Contains("John"));
            Assert.IsTrue(outputString.Contains("ok"));
            Assert.IsTrue(outputString.Contains("Feeling okay"));
        }

        [TestMethod]
        public void TestSTCanCheckProgress()
        {
            // Arrange
            var data = "John\nok\nFeeling okay\n2\nNo meeting booked";
            Program.SaveToFile(data);

            var input = new StringReader("67890\n");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Program.STInteraction();

            // Assert
            var outputString = output.ToString();
            Assert.IsTrue(outputString.Contains("Access granted. Welcome to our system, Andy Grey."));
            Assert.IsTrue(outputString.Contains("John"));
            Assert.IsTrue(outputString.Contains("ok"));
            Assert.IsTrue(outputString.Contains("Feeling okay"));
        }

        [TestMethod]
        public void TestDataPersistence()
        {
            // Arrange
            var data = "John\nok\nFeeling okay\n2\nNo meeting booked";
            Program.SaveToFile(data);

            // Act
            Program.SaveToFile(data);

            // Assert
            var lines = File.ReadAllLines("info.txt");
            Assert.AreEqual("John", lines[0]);
            Assert.AreEqual("ok", lines[1]);
            Assert.AreEqual("Feeling okay", lines[2]);
            Assert.AreEqual("2", lines[3]);
            Assert.AreEqual("No meeting booked", lines[4]);
        }
    }
}