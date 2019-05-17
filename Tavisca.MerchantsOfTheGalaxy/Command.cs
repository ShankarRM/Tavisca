using System.Collections.Generic;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public abstract class Command
    {
        public IReadOnlyList<Symbol> Symbols { get; set; }

        protected Command(IReadOnlyList<Symbol> symbols)
        {
            Symbols = symbols;
        }
        public override string ToString()
        {
            return string.Join(" ", Symbols);
        }

    }
}