// Pour Étudiant, phase 4

using System;
using ArbreBinLib;
using cstjean.info.fg.consoleplus;
using static cstjean.info.fg.consoleplus.ConsolePlus;
using static ArbreBinLib.ArbreBin<int, int>;
using cstjean.info.fg.utilitaire.extensions;

namespace SDD
{
    public static partial class Program
    {
        static partial void Menu6()
        {
            if (STEP < 4500) return;

            MenuGénéral.MenuItems.AddRange(new[] {
                MenuItem.Spacer,
                new MenuItem("Stats Examen Final 2020"),
                new MenuItem("EF20 Sans benchmarks", () => MenuArbres.Show(false, 6), false),
                new MenuItem("EF20 Avec benchmarks", () => MenuArbres.Show(true, 6), false),
            });
        }
    }

    public static partial class Stats
    {
        static partial void AfficherStats6<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>
        {
            if (Program.STEP < 4500) return;

            if ( Afficher(nameof(MaxP), () => ArbreBin<T, T>.MaxP(arbre)) )
                WriteLine();

            foreach (string chemin in new [] { "GDG", "", "DX"})
            {
                _ = Afficher($"{nameof(CheminerR)}(\"{chemin}\")", () => ArbreBin<T, T>.CheminerR(arbre, chemin));
            }
            WriteLine();
        }
    }
}
