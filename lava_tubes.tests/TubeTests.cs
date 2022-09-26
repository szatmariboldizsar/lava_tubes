using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace lava_tubes.tests
{
    [TestClass]
    public class TubeTests
    {
        Stream GoodInput;
        Stream BadInput;
        [TestInitialize]
        public void TestInitialize()
        {
            string g_input = $"12659{Environment.NewLine}26595{Environment.NewLine}65956{Environment.NewLine}59562{Environment.NewLine}95621";
            string b_input = $"12659{Environment.NewLine}2595{Environment.NewLine}65956{Environment.NewLine}59562{Environment.NewLine}95621";

            GoodInput = new MemoryStream();
            var writer = new StreamWriter(GoodInput);
            writer.Write(g_input);
            writer.Flush();
            GoodInput.Position = 0;

            BadInput = new MemoryStream();
            writer = new StreamWriter(BadInput);
            writer.Write(b_input);
            writer.Flush();
            BadInput.Position = 0;
        }
        [TestMethod]
        public void Initializations_With_Valid_Existing_File()
        {
            // Act + Assert
            try
            {
                Tube.InitializeTubes(GoodInput);
                Basin.InitializeBasins();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(Tube.SumOfRisks(), 52);
            Assert.AreEqual(Basin.MultipleOfSizes(), 100);
        }
        [TestMethod]
        public void InitializeTubes_Without_Existing_File()
        {
            // Arrange
            string input = "404.txt";
            
            //delegate initialize += Tube.InitializeTubes();

            // Act + Assert
            //Assert.ThrowsException(Tube.InitializeTubes(input));
        }
        
    }
}