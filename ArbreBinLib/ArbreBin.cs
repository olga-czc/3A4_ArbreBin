using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArbreBinLib
{
    public static partial class ArbreBin<TKey, TValue>
        where TKey: notnull, IComparable<TKey>
    {

        public class Noeud
        {
            // -----Auto propriétés-----
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Noeud? Gauche { get; set; }
            public Noeud? Droite { get; set; }
            public Noeud? Parent { get; set; }

            // -----Constructeur-----
            public Noeud(
                TKey key,
                TValue value = default,
                Noeud? gauche = null,
                Noeud? droite = null)
            {
                Key = key;
                Value = value;
                Gauche = gauche;
                if (Gauche != null)
                    Gauche.Parent = this;
                Droite = droite;
                if (Droite != null)
                    Droite.Parent = this;
                Parent = null;
            }

            //-----Affichage-----
            public override string ToString() => throw new NotImplementedException();

            //-----Propriétés calculables-----
            public EspèceDeNoeud Espèce => throw new NotImplementedException();

        }
    }
}
