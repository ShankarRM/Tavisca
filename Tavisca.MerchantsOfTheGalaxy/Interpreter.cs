using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Interpreter
    {
        private readonly Lex _lex;

        public Processor Processor { get; private set; }

        public Interpreter()
        {
            try
            {
                _lex = new Lex();
                Processor = new Processor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public CommandResult ParseAndExecute(string commandText)
        {
            try
            {
                 _lex.Init(commandText);
                
                var command = Parse();

                if (command is ConstantDeclaration)
                    return Processor.Execute(command as ConstantDeclaration);
                if (command is CommodityDeclaration)
                    return Processor.Execute(command as CommodityDeclaration);
                if (command is Query)
                    return Processor.Execute(command as Query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new CommandResult() {
                ResultText= "I have no idea what you are talking about",
                Sucess=false
            };
        }

        private Command Parse()
        {
            var tokens = GetSymbolsList();
            if (IsConstantDeclaration(tokens))
                return new ConstantDeclaration(tokens);

            if (IsClassifierDeclaration(tokens))
                return new CommodityDeclaration(tokens);

            if (IsQueryCommand(tokens))
                return new Query(tokens);
            return null;
        }

        private bool IsQueryCommand(IReadOnlyList<Symbol> tokens)
        {
            return tokens.First().Kind == SymbolKind.Statement
                   && tokens[1].Kind == SymbolKind.SubStatement
                   && tokens.Any(s => s.Kind == SymbolKind.Constant)
                   && tokens.Any(s => s.Kind == SymbolKind.Operator)
                   && tokens.Last().Kind == SymbolKind.Query;
        }

        private bool IsClassifierDeclaration(IReadOnlyList<Symbol> tokens)
        {
            return tokens.First().Kind == SymbolKind.Constant
                   && tokens.Any(s => s.Kind == SymbolKind.Commodity)
                   && tokens.Any(s => s.Kind == SymbolKind.Operator)
                   && tokens.Any(s => s.Kind == SymbolKind.Value)
                   && tokens.Last().Kind == SymbolKind.Unit;
        }

        private bool IsConstantDeclaration(IReadOnlyList<Symbol> tokens)
        {
            return tokens.Count == 3
                   && tokens.First().Kind == SymbolKind.Constant
                   && tokens.Any(s => s.Kind == SymbolKind.Operator)
                   && tokens.Last().Kind == SymbolKind.RomanSymbol;
        }

        private IReadOnlyList<Symbol> GetSymbolsList()
        {
            var symbols = new List<Symbol>();

            var lastSymbol = _lex.ParseToken();

            while (lastSymbol != null)
            {
                symbols.Add(lastSymbol);
                lastSymbol = _lex.ParseToken();
            }

            return new ReadOnlyCollection<Symbol>(symbols);
        }
    }
}
