using System;
using System.Collections.Generic;
using System.Text;
using static ArbreBinLib.ArbreBin<int, int>;

namespace ArbreBinLib
{
    public static partial class Factory
    {
        public static IEnumerable<Noeud?> ArbreNull() => throw new NotImplementedException();
        public static IEnumerable<Noeud?> Arbres1() => throw new NotImplementedException();
        public static IEnumerable<Noeud?> Arbres2() => throw new NotImplementedException();
        public static IEnumerable<Noeud?> Arbres3() => throw new NotImplementedException();
        public static IEnumerable<Noeud?> ArbresTypiques() => throw new NotImplementedException();
    }
}
