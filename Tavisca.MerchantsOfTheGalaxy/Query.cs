using System.Collections.Generic;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Query : Command
    {
        public Query(IReadOnlyList<Symbol> symbols) : base(symbols)
        {
        }
    }
}