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
     

        public static int TailleP(Noeud? arbre)
        {
            int taille = 0;
            parcourir(arbre);
            return taille;

            void parcourir(Noeud? noeud)
            {
                if (noeud is null) return;
                taille++;
                parcourir(noeud.Gauche);
                parcourir(noeud.Droite);
            }
        }

        public static int NbFeuillesR(Noeud? arbre) => arbre is null? 0:
            arbre.Gauche is null && arbre.Droite is null? 1 :
            (NbFeuillesR(arbre.Gauche) + NbFeuillesR(arbre.Droite));
      

        public static int NbFeuillesP(Noeud? arbre)
        {
            int nbFeuilles = 0;
            parcourir(arbre);
            return nbFeuilles;

            void parcourir(Noeud? noeud)
            {
                if (noeud is null)
                    return;
         
                else
                {
                    if (noeud.Espèce == EspèceDeNoeud.Feuille)
                        nbFeuilles++;
                    parcourir(noeud.Gauche);
                    parcourir(noeud.Droite);
                }
            
            }
        }


        public static int NbEspèce(Noeud? arbre, EspèceDeNoeud espèce)
        {
            int nbEspèce = 0;
            parcourir(arbre, espèce);
            return nbEspèce;

            void parcourir(Noeud? noeud, EspèceDeNoeud espèce)
            {
                if (noeud is null) return;
                else
                {
                    if (espèce == EspèceDeNoeud.Feuille)
                    {
                        //if (noeud.Gauche is null && noeud.Droite is null)
                        //{
                        //    nbEspèce = 1;
                        //    return;
                        //}
                        //else
                        //{
                            if (arbre.Espèce == EspèceDeNoeud.Feuille)
                                nbEspèce++;
                            parcourir(noeud.Gauche, espèce);
                            parcourir(noeud.Droite, espèce);
                        //}
                    }
                    else if (espèce == EspèceDeNoeud.Embranchement)
                    {
                        if (arbre.Espèce == EspèceDeNoeud.Embranchement)
                            nbEspèce++;
                        parcourir(noeud.Gauche, espèce);
                        parcourir(noeud.Droite, espèce);
                    }
                    else if (espèce == EspèceDeNoeud.TigeDroite)
                    {
                        if (arbre.Espèce == EspèceDeNoeud.TigeDroite)
                            nbEspèce++;
                        parcourir(noeud.Gauche, espèce);
                        parcourir(noeud.Droite, espèce);
                    }
                    else if (espèce == EspèceDeNoeud.TigeGauche)
                    {
                        if (arbre.Espèce == EspèceDeNoeud.TigeGauche)
                            nbEspèce++;
                        parcourir(noeud.Gauche, espèce);
                        parcourir(noeud.Droite, espèce);
                    }
                }
            }
        }
        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
            NbToutesEspècesNR(Noeud? arbre) => throw new NotImplementedException();

        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
           NbToutesEspècesR(Noeud? arbre) => throw new NotImplementedException();

        public static IEnumerable<Noeud> Feuilles(Noeud? arbre) => throw new NotImplementedException();
    

        // ---------------- 3 - Dimensions ---------------

        public static int Hauteur(Noeud? arbre) 
        {

            if(arbre == null)
            return 0;

            return (Hauteur(arbre.Gauche) + Hauteur(arbre.Droite) + 1);
        }

        public static int[] Largeurs(Noeud? arbre) => throw new NotImplementedException();

        public static int Largeur(Noeud? arbre) => throw new NotImplementedException();


        // ---------------- 4 - Recherche -----------------

        public static Noeud? ChercherPc(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherPa(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? Chercher(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherR(Noeud? arbre, TKey clé) => throw new NotImplementedException();

      
    }
}
