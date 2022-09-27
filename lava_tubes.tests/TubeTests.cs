using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace lava_tubes.tests
{
    [TestClass]
    public class TubeTests
    {
        Stream GoodInput;
        Stream BadInput1;
        Stream BadInput2;
        Stream EmptyInput;


        [TestInitialize]
        public void StartUp()
        {
            string g_input = $"12345954321{Environment.NewLine}23459595432{Environment.NewLine}34595459543{Environment.NewLine}45954345954{Environment.NewLine}59543234595{Environment.NewLine}95432123459{Environment.NewLine}59543234595{Environment.NewLine}45954345954{Environment.NewLine}34595459543{Environment.NewLine}23459595432{Environment.NewLine}12345954321";
            string b_input1 = $"12345954321{Environment.NewLine}2345959543{Environment.NewLine}34595459543{Environment.NewLine}45954345954{Environment.NewLine}59543234595{Environment.NewLine}95432123459{Environment.NewLine}59543234595{Environment.NewLine}45954345954{Environment.NewLine}34595459543{Environment.NewLine}23459595432{Environment.NewLine}12345954321";
            string b_input2 = $"12345954321{Environment.NewLine}2345959543a{Environment.NewLine}34595459543{Environment.NewLine}45954345954{Environment.NewLine}59543234595{Environment.NewLine}95432123459{Environment.NewLine}59543234595{Environment.NewLine}45954345954{Environment.NewLine}34595459543{Environment.NewLine}23459595432{Environment.NewLine}12345954321";
            string empty_input = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}";

            GoodInput = new MemoryStream();
            var writer = new StreamWriter(GoodInput);
            writer.Write(g_input);
            writer.Flush();
            GoodInput.Position = 0;

            BadInput1 = new MemoryStream();
            writer = new StreamWriter(BadInput1);
            writer.Write(b_input1);
            writer.Flush();
            BadInput1.Position = 0;

            BadInput2 = new MemoryStream();
            writer = new StreamWriter(BadInput2);
            writer.Write(b_input2);
            writer.Flush();
            BadInput2.Position = 0;

            EmptyInput = new MemoryStream();
            writer = new StreamWriter(EmptyInput);
            writer.Write(empty_input);
            writer.Flush();
            EmptyInput.Position = 0;

            Tube.Tubes.Clear();
            Tube.IsComplete = false;
        }

        [TestMethod]
        public void InitializeTubes_With_Valid_Existing_Input()
        {
            // Act + Assert
            try
            {
                Tube.InitializeTubes(GoodInput);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(10,Tube.SumOfRisks());
        }

        [TestMethod]
        public void InitializeTubes_With_Empty_Existing_Input()
        {
            // Act + Assert
            try
            {
                Tube.InitializeTubes(EmptyInput);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(0,Tube.SumOfRisks());
        }

        [TestMethod]
        public void InitializeTubes_With_Invalid_Existing_Input()
        {
            // Act + Assert
            try
            {
                Tube.InitializeTubes(BadInput1);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(0,Tube.SumOfRisks());
        }

        [TestMethod]
        public void InitializeTubes_With_Invalid_Existing_Input2()
        {
            // Act + Assert
            try
            {
                Tube.InitializeTubes(BadInput2);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(0, Tube.SumOfRisks());
        }

        [TestMethod]
        public void InitializeTubes_Without_Existing_Input()
        {
            // Arrange
            string input = "404.txt";

            // Act + Assert
            try
            {
                Tube.InitializeTubes(input);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            Assert.AreEqual(0,Tube.SumOfRisks());
        }
    }
}