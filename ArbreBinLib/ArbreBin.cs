using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArbreBinLib
{
    public static partial class ArbreBin<TKey, TValue>
        where TKey : notnull, IComparable<TKey>
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

            // ----Propriétés additionnelles -----
            public Noeud Racine => throw new NotImplementedException();
            public int Profondeur => throw new NotImplementedException();
            public Noeud PlusGauche => throw new NotImplementedException();
            public Noeud PlusDroite => throw new NotImplementedException();


            //-----Affichage-----
            public override string ToString()
            {
                if (Parent == null)
                {
                    if (Value == null || Value.Equals(0))
                    {
                        if (Droite == null && Gauche == null)
                            return ($"R[{Key}]");
                        else if (Droite != null && Gauche == null)
                            return ($"R[{Key}.]");
                        else if (Droite == null && Gauche != null)
                            return ($"R[.{Key}]");
                        else
                            return ($"R[.{Key}.]");
                    }
                    else 
                    {
                        if (Droite == null && Gauche == null)
                            return ($"R[{Key}|{Value}]");
                        else if (Droite != null && Gauche == null)
                            return ($"R[{Key}.|{Value}]");
                        else if (Droite == null && Gauche != null)
                            return ($"R[.{Key}|{Value}]");
                        else
                            return ($"R[.{Key}.|{Value}]");
                    }
                }
                else
                {
                    if (Value == null || Value.Equals(0))
                    {
                        if (Droite == null && Gauche == null)
                            return ($"[{Key}]");
                        else if (Droite != null && Gauche == null)
                            return ($"[{Key}.]");
                        else if (Droite == null && Gauche != null)
                            return ($"[.{Key}]");
                        else
                            return ($"[.{Key}.]");
                    }
                    else 
                    {
                        if (Droite == null && Gauche == null)
                            return ($"[{Key}|{Value}]");
                        else if (Droite != null && Gauche == null)
                            return ($"[{Key}.|{Value}]");
                        else if (Droite == null && Gauche != null)
                            return ($"[.{Key}|{Value}]");
                        else
                            return ($"[.{Key}.|{Value}]");
                    }
                }
            }

            //-----Propriétés calculables-----
            public EspèceDeNoeud Espèce
            {
                get { if (Gauche != null && Droite != null) 
                        return EspèceDeNoeud.Embranchement;
                      else if( Gauche != null && Droite == null)
                        return EspèceDeNoeud.TigeGauche;
                      else if (Gauche == null && Droite != null)
                        return EspèceDeNoeud.TigeDroite;
                      else return EspèceDeNoeud.Feuille;
                }
            }

            // --------------- Méthodes additionnelles ---------

            public int? ProfondeurRelative(Noeud? ancêtre) => throw new NotImplementedException();
        }
        
        // -------------- 1 - Dénombrement de base ----------------
 

        public static int TailleR(Noeud? arbre) => arbre == null? 0: (TailleR(arbre.Gauche) + TailleR(arbre.Droite) + 1);
     

        public static int TailleP(Noeud? arbre) => throw new NotImplementedException();

        public static int NbFeuillesR(Noeud? arbre) => throw new NotImplementedException();

        public static int NbFeuillesP(Noeud? arbre) => throw new NotImplementedException();


        public static int NbEspèce(Noeud? arbre, EspèceDeNoeud espèce) => throw new NotImplementedException();

        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
            NbToutesEspècesNR(Noeud? arbre) => throw new NotImplementedException();

        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
           NbToutesEspècesR(Noeud? arbre) => throw new NotImplementedException();

        public static IEnumerable<Noeud> Feuilles(Noeud? arbre) => throw new NotImplementedException();
    

        // ---------------- 3 - Dimensions ---------------

        public static int Hauteur(Noeud? arbre) => throw new NotImplementedException();

        public static int[] Largeurs(Noeud? arbre) => throw new NotImplementedException();

        public static int Largeur(Noeud? arbre) => throw new NotImplementedException();


        // ---------------- 4 - Recherche -----------------

        public static Noeud? ChercherPc(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherPa(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? Chercher(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherR(Noeud? arbre, TKey clé) => throw new NotImplementedException();

      
    }
}
