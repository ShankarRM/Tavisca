using System;

namespace Tavisca.MerchantsOfTheGalaxy
{
    public class Symbol
    {
        
        public String Name { get; private set; }
        public SymbolKind Kind { get; private set; }

        public Symbol(String name, SymbolKind kind)
        {
            Name = name;
            Kind = kind;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public double ToDouble()
        {
            double result;
            if (!double.TryParse(Name, out result))
                throw new Exception("Symbol is not a valid double");

            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            return obj != null
                && obj.GetType() == GetType()
                && ((Symbol)obj).Name == Name;
        }
    }
}