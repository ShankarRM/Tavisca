using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Lex
    {
        private string[] tokens;
        private int _currentPosition;
        public Lex(){}

        public void Init(string commandText)
        {
            tokens = ExtractTokens(commandText);
            _currentPosition = 0;
        }

        private static string[] ExtractTokens(string commandText)
        {
            return commandText.Split(' ');
        }
        //Identifying kind of token
        public Symbol ParseToken()
        {
            Symbol symbol;

            if (tokens.Length == _currentPosition)
                return null;

            var token = tokens[_currentPosition];

            switch (token)
            {
                case Keywords.Operators.Is:
                    symbol = new Symbol(token, SymbolKind.Operator);
                    break;
                case Keywords.Qualifiers.Query:
                    symbol = new Symbol(token, SymbolKind.Query);
                    break;
                case Keywords.RomanSymbols.I:
                case Keywords.RomanSymbols.V:
                case Keywords.RomanSymbols.X:
                case Keywords.RomanSymbols.L:
                case Keywords.RomanSymbols.C:
                case Keywords.RomanSymbols.D:
                case Keywords.RomanSymbols.M:
                    symbol = new Roman(token);
                    break;
                case Keywords.Statements.How:
                    symbol = new Symbol(token, SymbolKind.Statement);
                    break;
                case Keywords.SubStatements.Many:
                case Keywords.SubStatements.Much:
                    symbol = new Symbol(token, SymbolKind.SubStatement);
                    break;
                default:
                    symbol = EvaluateConstantsAndCommodity(token);
                    break;
            }
            _currentPosition++;
            return symbol;
        }

        private Symbol EvaluateConstantsAndCommodity(string token)
        {
            double argsValue;
            if (double.TryParse(token, out argsValue))
                return new Symbol(token, SymbolKind.Value);

            var previousArg = _currentPosition == 0 ? null : tokens[_currentPosition - 1];

            if (previousArg == null)
                return new Constant(token);

            var nextArg = _currentPosition == tokens.Length - 1 ? null : tokens[_currentPosition + 1]; 

            if (nextArg == null || (IsSubstatement(previousArg) && IsOperator(nextArg)))
                return new Unit(token);

            if (!IsSubstatement(previousArg) && IsOperator(nextArg))
                return new Commodity(token);

            if (IsQuery(nextArg) && IsManyStatement())
                return new Commodity(token);

            return new Constant(token);
        }
        #region Check token type
        private bool IsQuery(string token)
        {
            return token == Keywords.Qualifiers.Query;
        }

        private bool IsManyStatement()
        {
            return tokens.Contains(Keywords.SubStatements.Many);
        }

        private bool IsOperator(string token)
        {
            return token == Keywords.Operators.Is;
        }

        private bool IsSubstatement(string token)
        {
            return token== Keywords.SubStatements.Many
                   || token == Keywords.SubStatements.Much; 
        }
        #endregion
    }
}
