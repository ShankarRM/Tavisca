using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Processor
    {
        public Dictionary<Constant, Roman> ConstantsTable { get; private set; }

        public Dictionary<Commodity, List<Unit>> CommodityTable { get; private set; }

        public Processor()
        {
            ConstantsTable = new Dictionary<Constant,Roman>();
            CommodityTable = new Dictionary<Commodity, List<Unit>>();
        }

        public CommandResult Execute(ConstantDeclaration declaration)
        {
            var constantSymbol = (Constant)declaration.Symbols.Single(s => s is Constant);

            if (ConstantsTable.ContainsKey(constantSymbol))
                throw new Exception("Already defined");

            var romanSymbol = (Roman)declaration.Symbols.Single(s => s is Roman);

            ConstantsTable.Add(constantSymbol, romanSymbol);

            return new CommandResult
            {
                ResultText = String.Format("Information Registred: \"{0}\"", declaration),
                Sucess = true
            };
        }

        public CommandResult Execute(CommodityDeclaration declaration)
        {
            var classifier = (Commodity)declaration.Symbols.Single(s => s is Commodity);
            var unit = (Unit)declaration.Symbols.Single(s => s is Unit);
            var value = declaration.Symbols.Single(s => s.Kind == SymbolKind.Value).ToDouble();

            unit.Value = CalculateUnitFactor(declaration.Symbols.OfType<Constant>(), value);

            if (!CommodityTable.ContainsKey(classifier))
                CommodityTable.Add(classifier, new List<Unit>());

            if (!CommodityTable[classifier].Contains(unit))
                CommodityTable[classifier].Add(unit);
            else
                throw new Exception("Already defined");

            return new CommandResult
            {
                ResultText = String.Format("Information Registred: \"{0}\"", declaration),
                Sucess = true
            };
        }

        public CommandResult Execute(Query query)
        {
            var queryType = query.Symbols.Single(s => s.Kind == SymbolKind.SubStatement).Name;

            string messageText;

            var constants = query.Symbols.OfType<Constant>().ToList();
            var value = GetDecimalValue(constants);
            var constantsName = string.Join(" ", constants.Select(c => c.ToString()));

            if (queryType == Keywords.SubStatements.Much)
                messageText = string.Format("{0} is {1}", constantsName, value);

            else
            {
                var commodity = (Commodity)query.Symbols.Single(s => s is Commodity);
                var unit = (Unit)query.Symbols.Single(s => s is Unit);

                value *= CommodityTable[commodity].Find(u => u.Equals(unit)).Value;

                messageText = string.Format("{0} {1} is {2} {3}", constantsName, commodity, value, unit);
            }

            return new CommandResult
            {
                ResultText = messageText,
                Sucess = true
            };

        }

        private double GetDecimalValue(IEnumerable<Constant> constants)
        {
            try
            {
                var romanSymbols = new StringBuilder();

                constants.Select(c => ConstantsTable[c])
                         .ToList().ForEach(r => romanSymbols.Append(r));

                var romanNumber = romanSymbols.ToString();

                return RomanToDecimalConverter.Convert(romanNumber);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public double CalculateUnitFactor(IEnumerable<Constant> constants, double value)
        {
            return value / GetDecimalValue(constants);
        }

    }
}