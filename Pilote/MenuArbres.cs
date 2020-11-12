// Pour Étudiant, phase 1

using System;
using System.Collections.Generic;
using static System.ConsoleColor;
using ArbreBinLib;
using cstjean.info.fg.consoleplus;
using static cstjean.info.fg.consoleplus.ConsolePlus;
using cstjean.info.fg.utilitaire.extensions;

namespace SDD
{
    public static partial class MenuArbres
    {
        public static bool NoBenchmark { get; private set; } = false;
        public static int[] StatLevels { get; private set; } = new int[0];

        public static readonly List<MenuItem> MenuItems = new List<MenuItem>
        {   MenuItem.Rien

            , new MenuItem("Vide", () => Visualiser(Factory.ArbreNull()))
            , new MenuItem("Taille 1", () => Visualiser(Factory.Arbres1()))
            , new MenuItem("Taille 2", () => Visualiser(Factory.Arbres2()))
            , new MenuItem("Taille 3", () => Visualiser(Factory.Arbres3()))
            , new MenuItem("Typiques", () => Visualiser(Factory.ArbresTypiques()))

            , MenuItem.Spacer

            , new MenuItem("Tailles 1 + 2 + 3", () => Visualiser(FactoryPlus.Arbres123()))
            , new MenuItem("Tailles 1 + 2 + 3 (avec valeurs)", () => Visualiser(FactoryPlus.Arbres123AvecValeurs()))
            , new MenuItem("Taille 10", () => Visualiser(FactoryPlus.Arbres10()))
            , new MenuItem("Taille 20", () => Visualiser(FactoryPlus.Arbres20()))
            , new MenuItem("Taille 100", () => Visualiser(FactoryPlus.Arbres100(), 4))
            , new MenuItem("Taille 1K", () => Visualiser(FactoryPlus.Arbres1K(), 4))
            , new MenuItem("Taille 100K", () => Visualiser(FactoryPlus.Arbres100K(), 3))
            , new MenuItem("Aléatoires", () => Visualiser(FactoryPlus.ArbresAléatoires()))
        };

        public static void Show(bool avecBenchmark, params int[] statLevels)
        {
            NoBenchmark = !avecBenchmark;
            StatLevels = statLevels;

            ConsoleMenu.Show
            (
                $"{ConsoleMenu.CurrentMenuSelectionTitle} --- {Program.Nom}"
                , Green, Black, Red, Magenta, null, null
                , MenuItems.ToArray()
            );
        }

        public static void Visualiser<T>(IEnumerable<ArbreBin<T, T>.Noeud?> arbres, int? profondeurMax = null)
            where T : notnull, IComparable<T>
        {
            int padding = 90;
            foreach (var (i, arbre) in arbres.Numéroter(1))
            {
                // Entête
                Write((White, Magenta), "".PadRight(padding)); WriteLine();
                Write((Black, Magenta), $"  {i}.  [{Program.Initiales}]  {arbre.ToString1D().Raccourcir(padding - 20)}".PadRight(padding)); WriteLine();
                Write((White, Magenta), "".PadRight(padding)); WriteLine();

                // Arborescence
                WriteLine();
                WriteLine(DarkCyan, arbre.ToString2D(utf8: true, profondeurMax: profondeurMax, marge: 3));

                Stats.Afficher(i, arbre);
            }
        }
    }
}
