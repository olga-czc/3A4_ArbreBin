// Pour étudiants, phase 1
// Ne pas modifier

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static ArbreBinLib.ArbreBin<int, int>;
using SyntaxNode = ArbreBinLib.ArbreBin<string, string>.Noeud; 

namespace ArbreBinLib
{
    public static partial class Factory { }

    public static class FactoryPlus
    {
        public static Noeud? Arbre(string factoryName, int i)
        {
            return MethodByName(factoryName)().ElementAt(i-1);
        }

        public static IEnumerable<Noeud?> Arbres(params string[] factoryNames)
        {
            var arbres = Enumerable.Empty<Noeud?>();
            foreach(var factoryName in factoryNames)
            {
                arbres = arbres.Concat(MethodByName(factoryName)());
            }
            return arbres;
        }

        /// <summary>
        /// Obtient la méthode de Factory ou FactoryPlus qui porte le nom indiqué en argument.
        /// </summary>
        /// <param name="factoryName">Nom de la méthode</param>
        /// <returns>Une méthode qui retourne une énumeration d'arbre</returns>
        private static Func<IEnumerable<Noeud?>> MethodByName(string factoryName)
        {
            var mi = typeof(Factory).GetMethod(factoryName)
                ?? typeof(FactoryPlus).GetMethod(factoryName);
            if (mi == null)
                throw new NotImplementedException(factoryName);
            return () => (IEnumerable<Noeud?>)mi.Invoke(null, new object[] { })!;
        }

        /// <summary>
        /// Génère un arbre aléatoirement de taille (max - min + 1)
        /// </summary>
        /// <param name="rand">Générateur utilisé</param>
        /// <param name="freqEmbranchement">Fréquence relative des embranchements</param>
        /// <param name="freqTigeGauche">Fréquence relative des tiges gauches</param>
        /// <param name="freqTigeDroite">Fréquence relative des tiges droites</param>
        /// <param name="max">Valeur maximale dans l'arbre</param>
        /// <param name="min">Valeur minimale dans l'arbre</param>
        /// <param name="mult">Multiplicateur pour la valeur</param>
        /// <returns>Un arbre binaire aléatoire</returns>
        private static Noeud Arbre(
            this Random rand, 
            int freqEmbranchement, 
            int freqTigeGauche, 
            int freqTigeDroite, 
            int max, 
            int min = 1, 
            int mult = 1)
        {
            if (max < min)
                throw new ArgumentOutOfRangeException();
            
            // Si max == min alors retourner une feuille
            if (max == min) 
                return new Noeud(min*mult);
            
            int freqTotale = freqTigeGauche + freqTigeDroite;
            // S'il y a au moins 3 noeuds, alors on peut envisager un embranchement
            if (max - min > 1)
                freqTotale += freqEmbranchement;
            // Dans le cas rare ou la frequence totale est 0, 
            // alors on choisit tige gauche ou droite au hasard
            if (freqTotale == 0)
            {
                freqTotale = 2;
                freqTigeDroite = freqTigeGauche = 1;
            }

            int n = rand.Next(freqTotale);
            
            // Tige gauche
            if (n < freqTigeGauche)
                return new Noeud(max*mult, gauche: rand.Arbre(freqEmbranchement, freqTigeGauche, freqTigeDroite, max - 1, min));
            n -= freqTigeGauche;
            
            // Tige droite
            if (n < freqTigeDroite)
                return new Noeud(min*mult, droite: rand.Arbre(freqEmbranchement, freqTigeGauche, freqTigeDroite, max, min+1));

            // Embranchement
            int med = rand.Next(min + 1, max);
            return new Noeud(med*mult
            , gauche: rand.Arbre(freqEmbranchement, freqTigeGauche, freqTigeDroite, med - 1, min)
            , droite: rand.Arbre(freqEmbranchement, freqTigeGauche, freqTigeDroite, max, med + 1)
            );
        }

        /// <summary>
        /// Générateur d'arbres binaires
        /// </summary>
        /// <param name="taille">Taille de l'arbre</param>
        /// <param name="cardinalité">Combien d'arbre on génère</param>
        /// <param name="freqEmbranchement">Fréquence relative des embranchements</param>
        /// <param name="freqTigeGauche">Fréquence relative des tiges gauches</param>
        /// <param name="freqTigeDroite">Fréquence relative des tiges droites</param>
        /// <param name="min">Valeur minimale dans l'arbre</param>
        /// <param name="stepTaille">Croissance de la taille en fonction de la cardinalité</param>
        /// <param name="seed">Laisser à null pour une génération différente à chaque appel</param>
        /// <returns>Une séquence d'arbres binaires</returns>
        private static IEnumerable<Noeud?> Arbres(
            int taille, 
            int cardinalité,
            int freqEmbranchement = 5,
            int freqTigeGauche = 1,
            int freqTigeDroite = 1,
            int min = 1,
            int stepTaille = 0,
            int? seed = 1)
        {
            var rand = seed.HasValue ? new Random(seed.Value) : new Random();
            for(int i = 0; i < cardinalité; i++)
            {
                yield return rand.Arbre(freqEmbranchement, freqTigeGauche, freqTigeDroite, min + taille + i*stepTaille - 1, min);
            }
        }

        /// <summary>
        /// Générateur d'arbres binaires
        /// </summary>
        /// <param name="taille">Taille de l'arbre</param>
        /// <param name="cardinalité">Combien d'arbre on génère</param>
        /// <param name="min">Valeur minimale dans l'arbre</param>poiu
        /// <param name="stepTaille">Croissance de la taille en fonction de la cardinalité</param>
        /// <param name="seed">Laisser à null pour une génération différente à chaque appel</param>
        /// <param name="opérateurs">Fréquence et opérateurs possibles</param>
        /// <returns>Une séquence d'arbres binaires</returns>
        private static IEnumerable<SyntaxNode?> ArbresSyntaxiques(
            int taille,
            int cardinalité,
            int min = 1,
            int stepTaille = 0,
            int? seed = 1,
            string? opérateurs = null)
        {
            opérateurs ??= "+++++***-/";

            foreach(var ab in Arbres(taille, cardinalité, 10, 0, 0, min, stepTaille, seed))
            {
                yield return transformer(ab);
            }

            SyntaxNode? transformer(Noeud? arbre)
            {
                if (arbre is null) 
                    return null;
                char opérateur = opérateurs![(arbre.Key + taille + cardinalité) % opérateurs!.Length];
                return arbre.Espèce switch
                {
                    EspèceDeNoeud.Embranchement => new SyntaxNode("" + opérateur, null!, transformer(arbre.Gauche), transformer(arbre.Droite)),
                    EspèceDeNoeud.TigeGauche => new SyntaxNode("" + opérateur, null!, transformer(arbre.Gauche), new SyntaxNode("" + arbre.Key)),
                    EspèceDeNoeud.TigeDroite => new SyntaxNode("" + opérateur, null!, new SyntaxNode("" + arbre.Key), transformer(arbre.Droite)),
                    EspèceDeNoeud.Feuille => new SyntaxNode("" + arbre.Key),
                    _ => throw new InvalidOperationException(),
                };
            }
        }

        public static IEnumerable<Noeud?> Arbres10()
            => Arbres(10, 5, 8);

        public static IEnumerable<Noeud?> Arbres20()
            => Arbres(20, 3, 10);

        public static IEnumerable<Noeud?> ArbresAléatoires()
            => Arbres(5, 6, 8, stepTaille: 2, seed: null);

        public static IEnumerable<Noeud?> Arbres100()
            => Arbres(100, 3, 8);

        public static IEnumerable<Noeud?> Arbres1K()
            => Arbres(1000, 3, 8);

        public static IEnumerable<Noeud?> Arbres100K()
            => Arbres(100000, 1, 8);

        public static IEnumerable<Noeud?> AddValues(IEnumerable<Noeud?> arbres)
        {
            foreach(var arbre in arbres)
            {
                AddValues(arbre);
                yield return arbre;
            }

            static void AddValues(Noeud? arbre)
            {
                if (arbre == null) return;
                arbre.Value = arbre.Key * 2;
                AddValues(arbre.Gauche);
                AddValues(arbre.Droite);
            }
        }

        public static IEnumerable<Noeud?> Arbres123()
            => Arbres(nameof(Factory.Arbres1), nameof(Factory.Arbres2), nameof(Factory.Arbres3));

        public static IEnumerable<Noeud?> Arbres123AvecValeurs()
            => AddValues(Arbres123());

        public static IEnumerable<SyntaxNode?> ArbresSyntaxiquesCroissants()
            => ArbresSyntaxiques(7, 5, 2, 3).Concat(ArbresSyntaxiques(22, 2, 2, 3));

        public static IEnumerable<SyntaxNode?> ArbresSyntaxiques50K()
            => ArbresSyntaxiques(50_000, 1, opérateurs: "++++++----*");

    }
}
