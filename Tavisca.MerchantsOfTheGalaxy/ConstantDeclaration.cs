using System.Collections.Generic;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class ConstantDeclaration : Command
    {
        public ConstantDeclaration(IReadOnlyList<Symbol> symbols) : base(symbols)
        {
        }
    }
}