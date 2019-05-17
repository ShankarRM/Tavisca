using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavisca.MerchantsOfTheGalaxy.Test
{
    [TestClass]
    public class CommandAnalyerTest
    {
        [TestMethod]
        public void CanParseConstants()
        {
            var interpreter = new Interpreter();
            interpreter.ParseAndExecute("glob is I");
            var globSymbol = new Constant("glob");
            var iSymbol = new Roman("I");

            Assert.IsTrue(interpreter.Processor.ConstantsTable.ContainsKey(globSymbol));
            Assert.AreEqual(iSymbol, interpreter.Processor.ConstantsTable[globSymbol]);
        }
        [TestMethod]
        public void CanParseCommodity()
        {
            var interpreter = new Interpreter();
            interpreter.ParseAndExecute("glob is I");
            interpreter.ParseAndExecute("glob glob Silver is 34 Credits");

            var silverSymbol = new Commodity("Silver");
            var creditsSymbol = new Unit("Credits");
            
            Assert.IsTrue(interpreter.Processor.CommodityTable.ContainsKey(silverSymbol));
            Assert.IsTrue(interpreter.Processor.CommodityTable[silverSymbol].Contains(creditsSymbol));
            Assert.AreEqual(17, interpreter.Processor.CommodityTable[silverSymbol].Find(s => s.Equals(creditsSymbol)).Value);

        }

        [TestMethod]
        public void CanIdentifyUnkown()
        {
            var interpreter = new Interpreter();
            var result = interpreter.ParseAndExecute("how much wood could a	wood chuck chuck if a wood chuck could chuck wood?");

            Assert.AreEqual("I have no idea what you are talking about", result.ResultText);

        }
        
    }
}
