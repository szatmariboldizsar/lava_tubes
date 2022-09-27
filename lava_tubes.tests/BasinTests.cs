using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lava_tubes.tests
{
    [TestClass]
    public class BasinTests
    {
        Stream GoodInput;
        Stream BadInput;
        Stream EmptyInput;

        [TestInitialize()]
        public void StartUp()
        {
            string g_input = $"12345954321{Environment.NewLine}23459595432{Environment.NewLine}34595459543{Environment.NewLine}45954345954{Environment.NewLine}59543234595{Environment.NewLine}95432123459{Environment.NewLine}59543234595{Environment.NewLine}45954345954{Environment.NewLine}34595459543{Environment.NewLine}23459595432{Environment.NewLine}12345954321";
            string b_input = $"1234554321{Environment.NewLine}2345595432{Environment.NewLine}3455459543{Environment.NewLine}4554345954{Environment.NewLine}5543234595{Environment.NewLine}5432123459{Environment.NewLine}5543234595{Environment.NewLine}4554345954{Environment.NewLine}3455459543{Environment.NewLine}2345595432{Environment.NewLine}1234554321";
            string empty_input = $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}";

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

            EmptyInput = new MemoryStream();
            writer = new StreamWriter(EmptyInput);
            writer.Write(empty_input);
            writer.Flush();
            EmptyInput.Position = 0;

            Tube.Tubes.Clear();
            Tube.IsComplete = false;
            Basin.Basins.Clear();
        }

        [TestMethod]
        public void InitializeBasin_With_Valid_Input()
        {
            // Arrange
            Tube.InitializeTubes(GoodInput);

            // Act
            Basin.InitializeBasins();

            //Assert
            Assert.AreEqual(9225,Basin.ProductOfSizes());
        }

        [TestMethod]
        public void InitializeBasin_With_Empty_Input()
        {
            // Arrange
            Tube.InitializeTubes(EmptyInput);

            // Act
            Basin.InitializeBasins();

            //Assert
            Assert.AreEqual(0,Basin.ProductOfSizes());
        }

        [TestMethod]
        public void InitializeBasin_Without_InitializeTubes()
        {
            // Act
            Basin.InitializeBasins();

            //Assert
            Assert.AreEqual(0,Basin.ProductOfSizes());
        }

        [TestMethod]
        public void InitializeBasin_With_Invalid_Input()
        {
            // Arrange
            Tube.InitializeTubes(BadInput);

            // Act + Assert
            try
            {
                Basin.InitializeBasins();
            }
            catch (Exception)
            {

                Assert.Fail();
            }
            
            Assert.AreEqual(0, Basin.ProductOfSizes());
        }
    }
}
