using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavisca.MerchantsOfTheGalaxy.Test
{
    /// <summary>
    /// Summary description for LexTest
    /// </summary>
    [TestClass]
    public class LexTest
    {
        [TestMethod]
        public void CanGetSymbolForCommodityDeclaration()
        {
            const string sentence = "one Banana is 32 Reais";

            var lex = new Lex();
            lex.Init(sentence);

            lex.ParseToken();
            var classifier = lex.ParseToken();
            lex.ParseToken(); 
            

            Assert.AreEqual(SymbolKind.Commodity, classifier.Kind);
            

        }
        [TestMethod]
        public void CanGetSymbolForUnitDeclaration()
        {
            const string sentence = "one Banana is 32 Reais";

            var lex = new Lex();
            lex.Init(sentence);
            //skip initial tokens uptill value
            lex.ParseToken();
            lex.ParseToken();
            lex.ParseToken();
            lex.ParseToken();
            var unit = lex.ParseToken();
            Assert.AreEqual(SymbolKind.Unit, unit.Kind);

        }
        [TestMethod]
        public void CanGetSymbolForConstants()
        {
            const string sentence = "glob is I";

            var lex = new Lex();
            lex.Init(sentence);

            
            var classifier = lex.ParseToken();
            

            Assert.AreEqual(SymbolKind.Constant, classifier.Kind);
            
        }
    }
}
