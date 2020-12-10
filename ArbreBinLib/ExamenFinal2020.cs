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
                if (p_arbre is null)
                    return;
              
                else
                {
                    if (p_arbre.Key.CompareTo(key) == 1)
                    {
                        key = p_arbre.Key;
                    }
                    parcourir(p_arbre.Gauche, key);
                    parcourir(p_arbre.Droite, key);
                }
            }
        }

        // Récursif direct
        public static Noeud? CheminerR(Noeud? arbre, string chemin)
        {
            Noeud? noeud = null;
            if (arbre is null)
                return noeud;
            else
            {
                foreach (char n in chemin)
                {
                    if (chemin == null)
                        return arbre.Racine;
                    else
                        noeud = obtenirNoeud(arbre, n);
                }


                Noeud obtenirNoeud(Noeud arbre, char n)
                {
                    if (n == 'G')
                    {
                        if (arbre.Gauche != null)
                            noeud = CheminerR(arbre.Gauche, chemin);
                        else
                            noeud = null;
                    }
                    else if (n == 'D')
                    {
                        if (arbre.Droite != null)
                            noeud = CheminerR(arbre.Droite, chemin);
                        else
                            noeud = null;
                    }
                    else
                        throw new ArgumentException("", "Lettre cheminante invalide: " + n);

                    return noeud;
                }
            }

            return noeud;
        }
    }
}
