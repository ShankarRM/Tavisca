namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Unit:Symbol
    {
        public Unit(string name)
            : base(name, SymbolKind.Unit)
        {
        }

        public double Value { get;  set; }
    }
}