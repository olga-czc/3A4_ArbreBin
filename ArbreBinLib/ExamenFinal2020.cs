using System;
using System.Collections.Generic;
using System.Text;

namespace ArbreBinLib
{
    public static partial class ArbreBin<TKey, TValue>
    {
        // Technique du parcours, retour default si arbre null
        public static TKey MaxP(Noeud? arbre)
        {
            TKey cleMax = default;
            parcourir(arbre, cleMax);
            return cleMax;

            void parcourir(Noeud? p_arbre, TKey key)
            {
                //List<TKey> liste = new List<TKey> { };

                if (p_arbre is null)
                    return;
                else
                {
                    if (p_arbre.Key.CompareTo(key) == 1)
                    {
                        // liste.Add(p_arbre.Key);
                        //  liste.Sort();
                        // key = liste[liste.Count - 1];
                        
                        key = p_arbre.Key;
                    }
                    parcourir(p_arbre.Gauche, key);
                    parcourir(p_arbre.Droite, key);
                }
            }
        }

        // Récursif direct
        public static Noeud? CheminerR(Noeud? arbre, string chemin) => throw new NotImplementedException();
    }
}
