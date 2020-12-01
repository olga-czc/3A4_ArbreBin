using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            public Noeud Racine
            {

                get
                {
                    while (Parent != null)
                        return Parent;

                    return null;
                }
            }
            public int Profondeur
            {
                get
                {
                    int count = 0;

                    if (Parent == null)
                        return 0;
                    else
                    {
                        while (Parent != null)
                            count++;
                    }
                    return count;
                }
            }
            public Noeud PlusGauche
            {
                get
                {
                    while (Gauche != null)
                        return Gauche;

                    return null;
                }
            }
            public Noeud PlusDroite
            {
                get
                {
                    while (Droite != null)
                        return Droite;

                    return null;
                }
            }


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
                get
                {
                    if (Gauche != null && Droite != null)
                        return EspèceDeNoeud.Embranchement;
                    else if (Gauche != null && Droite == null)
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


        public static int TailleR(Noeud? arbre) => arbre == null ? 0 : (TailleR(arbre.Gauche) + TailleR(arbre.Droite) + 1);


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

        public static int NbFeuillesR(Noeud? arbre) => arbre is null ? 0 :
            arbre.Gauche is null && arbre.Droite is null ? 1 :
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
                    if (noeud.Espèce == espèce)
                        nbEspèce++;
                    parcourir(noeud.Gauche, espèce);
                    parcourir(noeud.Droite, espèce);
                }
            }
        }
        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
            NbToutesEspècesNR(Noeud? arbre)
        {
            return (NbEspèce(arbre, EspèceDeNoeud.Embranchement), NbEspèce(arbre, EspèceDeNoeud.TigeGauche),
                NbEspèce(arbre, EspèceDeNoeud.TigeDroite), NbEspèce(arbre, EspèceDeNoeud.Feuille));
        }

        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
           NbToutesEspècesR(Noeud? arbre)
        {
            int nbEmbranchements = 0;
            int nbTigesGauches = 0;
            int nbTigesDroites = 0;
            int nbFeuilles = 0;
            parcourir(arbre);
            return (nbEmbranchements, nbTigesGauches, nbTigesDroites, nbFeuilles);

            void parcourir(Noeud? noeud)
            {
                if (noeud is null)
                    return;

                else
                {
                    if (noeud.Espèce == EspèceDeNoeud.Embranchement)
                        nbEmbranchements++;
                    if (noeud.Espèce == EspèceDeNoeud.TigeGauche)
                        nbTigesGauches++;
                    if (noeud.Espèce == EspèceDeNoeud.TigeDroite)
                        nbTigesDroites++;
                    if (noeud.Espèce == EspèceDeNoeud.Feuille)
                        nbFeuilles++;
                    parcourir(noeud.Gauche);
                    parcourir(noeud.Droite);
                }
            }
        }


        public static IEnumerable<Noeud> Feuilles(Noeud? arbre)
        {
            List<Noeud> feuilles = new List<Noeud>();
            parcourir(arbre);
            return feuilles;

            void parcourir(Noeud? noeud)
            {
                if (noeud != null)
                {
                    if (noeud.Espèce == EspèceDeNoeud.Feuille)
                        feuilles.Add(noeud);
                    parcourir(noeud.Gauche);
                    parcourir(noeud.Droite);
                }
            }
        }


        // ---------------- 3 - Dimensions ---------------

        public static int Hauteur(Noeud? arbre)
        {
            if (arbre == null)
                return 0;

            else
            {
                int hGauche = Hauteur(arbre.Gauche);
                int hDroite = Hauteur(arbre.Droite);

                if (hGauche > hDroite)
                    return (hGauche + 1);
                else
                    return (hDroite + 1);
            }
        }

        public static int[] Largeurs(Noeud? arbre)
        {
            List<int> listeLargeurs = new List<int>();

            int hauteur = Hauteur(arbre);

            for (int i = 1; i <= hauteur; i++)
            {
                listeLargeurs.Add(obtLargeur(arbre, i));
            }

            int obtLargeur(Noeud noeud, int level)
            {
                if (noeud == null)
                {
                    return 0;
                }

                if (level == 1)
                {
                    return 1;
                }
                else if (level > 1)
                {
                    return obtLargeur(noeud.Gauche, level - 1)
                        + obtLargeur(noeud.Droite, level - 1);
                }
                return 0;
            }
            return listeLargeurs.ToArray();
        }

        public static int Largeur(Noeud? arbre) => arbre is null ? 0 : Largeurs(arbre).Max();  // table.Max(x => x.Status)


        // ---------------- 4 - Recherche -----------------

        public static Noeud? ChercherPc(Noeud? arbre, TKey clé)
        {
            Noeud noeud = null;
            parcourir(arbre, clé);
            return noeud;

            void parcourir(Noeud? p_arbre, TKey key)
            {
                if (p_arbre is null) return;
                else
                {
                    if (p_arbre.Key.CompareTo(key) == 0)
                    {
                        noeud = p_arbre;
                    }
                    parcourir(p_arbre.Gauche, key);
                    parcourir(p_arbre.Droite, key);
                }
            }

        }

        public static Noeud? ChercherPa(Noeud? arbre, TKey clé)
        {
            Noeud noeud = null;
            parcourir(arbre, clé);
            return noeud;

            void parcourir(Noeud? p_arbre, TKey key)
            {
                if (p_arbre is null) return;
                else
                {
                    if (p_arbre.Key.CompareTo(key) == 0)
                    {
                        noeud = p_arbre;

                        return;
                    }
                    parcourir(p_arbre.Gauche, key);
                    parcourir(p_arbre.Droite, key);
                }
            }
        }

        public static Noeud? Chercher(Noeud? arbre, TKey clé)
        {
            Noeud noeud = null;
            parcourir(arbre, clé);
            return noeud;

            void parcourir(Noeud? p_arbre, TKey key)
            {
                if (p_arbre is null) return;
                else
                {
                    if (p_arbre.Key.CompareTo(key) == 0)
                    {
                        noeud = p_arbre;

                        return;
                    }
                    parcourir(p_arbre.Gauche, key);
                    parcourir(p_arbre.Droite, key);
                }
            }
        }

        public static Noeud? ChercherR(Noeud? arbre, TKey clé) => arbre is null ?
            null : Chercher(arbre, clé);


        public static void PréOrdre(Noeud? noeud, Action<Noeud> visiter)
        {
            if (noeud is null) return;
            visiter(noeud);
            PréOrdre(noeud.Gauche, visiter);
            PréOrdre(noeud.Droite, visiter);
        }

        public static void EnOrdre(Noeud? noeud, Action<Noeud> visiter)
        {
            if (noeud is null) return;
            EnOrdre(noeud.Gauche, visiter);
            visiter(noeud);
            EnOrdre(noeud.Droite, visiter);
        }

        public static void PostOrdre(Noeud? noeud, Action<Noeud> visiter)
        {
            if (noeud is null) return;
            PostOrdre(noeud.Gauche, visiter);
            PostOrdre(noeud.Droite, visiter);
            visiter(noeud);
        }

        public static void EnOrdreInverse(Noeud? noeud, Action<Noeud> visiter)
        {
            if (noeud is null) return;
            EnOrdreInverse(noeud.Droite, visiter);
            visiter(noeud);
            EnOrdreInverse(noeud.Gauche, visiter);
        }

        public static void EnLargeur(Noeud? arbre, Action<Noeud> visiter) => throw new NotImplementedException();

        public static void EnOrdreItératif(Noeud? arbre, Action<Noeud> visiter)
        {
            if (arbre is null) return;

            Stack<Noeud> s = new Stack<Noeud>();

            while (arbre != null || s.Count > 0)
            {
                if (arbre != null)
                {
                    s.Push(arbre);
                    arbre = arbre.Gauche;
                }
                else
                {
                    arbre = s.Pop();
                    visiter(arbre);
                    arbre = arbre.Droite;
                }
            }
        }


        public static bool EnOrdreAbrégé(Noeud? noeud, Action<Noeud> visiter) => throw new NotImplementedException();


        public static int TailleV(Noeud? arbre)
        {
            int compte = 0;
            PréOrdre(arbre, _ => { if (arbre.Gauche is null && arbre.Droite is null) { compte++; } });
            return compte;
        }

        public static int NbFeuillesV(Noeud? arbre)
        {
            int compte = 0;
            PréOrdre(arbre, _ => compte++);
            return compte;

        }

        public static (int NbEmbranchements, int NbTigesGauches, int NbTigesDroites, int NbFeuilles)
            NbToutesEspècesV(Noeud? arbre) => throw new NotImplementedException();

        public static Noeud? ChercherV(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherVX(Noeud? arbre, TKey clé) => throw new NotImplementedException();

        public static Noeud? ChercherVA(Noeud? arbre, TKey clé) => throw new NotImplementedException();


        public static List<Noeud> NoeudsEnOrdre(Noeud? arbre) => throw new NotImplementedException();

        public static List<Noeud> Noeuds(Noeud? arbre, Action<Noeud?, Action<Noeud>> parcours) => throw new NotImplementedException();

        public static List<Noeud> NoeudsPréOrdre(Noeud? arbre) => throw new NotImplementedException();

        public static IEnumerable<Noeud> YieldNoeudsEnOrdre(Noeud? arbre) => throw new NotImplementedException();


        public static IEnumerable<Noeud> CinqPremiersL(Noeud? arbre) => throw new NotImplementedException();

        public static IEnumerable<Noeud> CinqPremiersVA(Noeud? arbre) => throw new NotImplementedException();

        public static IEnumerable<Noeud> CinqPremiersY(Noeud? arbre) => throw new NotImplementedException();

    }
}
