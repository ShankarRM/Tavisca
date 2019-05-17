using System.Collections.Generic;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class CommodityDeclaration : Command
    {
        public CommodityDeclaration(IReadOnlyList<Symbol> symbols) : base(symbols)
        {
        }
    }
}