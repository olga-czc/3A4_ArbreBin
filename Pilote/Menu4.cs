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
        static partial void Menu4()
        {
            if (STEP <= 4000) return;

            MenuGénéral.MenuItems.AddRange(new[] {
                MenuItem.Spacer,
                new MenuItem("Stats 4"),
                new MenuItem("4 Sans benchmarks", () => MenuArbres.Show(false, 4), false),
                new MenuItem("4 Avec benchmarks", () => MenuArbres.Show(true, 4), false),
            });

            MenuArbres.MenuItems.AddRange(new MenuItem[] {
                MenuItem.Spacer,
                new MenuItem("Arbres Syntaxiques"),
                new MenuItem("AS typiques",
                    () => MenuArbres.Visualiser(Factory.ArbresSyntaxiquesTypiques())),
                new MenuItem("AS spéciaux",
                    () => MenuArbres.Visualiser(Factory.ArbresSyntaxiquesSpéciaux())),
                new MenuItem("AS croissants",
                    () => MenuArbres.Visualiser(FactoryPlus.ArbresSyntaxiquesCroissants())),
                new MenuItem("AS 50K",
                    () => MenuArbres.Visualiser(FactoryPlus.ArbresSyntaxiques50K(), 4)),
            });

        }
    }

    public static partial class Stats
    {

        static partial void AfficherStats4<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>
        {
            if (Program.STEP <= 4000) return;

            if ( Afficher(nameof(SyntaxePostfixée), () => ArbreBin<T, T>.SyntaxePostfixée(arbre).Raccourcir(60))
                && Afficher(nameof(PostOrdre), () => arbre.Lister(ArbreBin<T, T>.PostOrdre))
                ) WriteLine();

            if (Afficher(nameof(SyntaxePréfixée), () => ArbreBin<T, T>.SyntaxePréfixée(arbre).Raccourcir(60))
                && Afficher(nameof(PréOrdre), () => arbre.Lister(ArbreBin<T, T>.PréOrdre))
                ) WriteLine();

            if (Afficher(nameof(SyntaxeInfixée), () => ArbreBin<T, T>.SyntaxeInfixée(arbre).Raccourcir(60))
                && Afficher(nameof(EnOrdre), () => arbre.Lister(ArbreBin<T, T>.EnOrdre))
                ) WriteLine();

            if (false
                | Afficher(nameof(EvalInt), () => ArbreBin<T,T>.EvalInt(arbre))
                | Afficher(nameof(EvalDouble), () => ArbreBin<T, T>.EvalDouble(arbre))
                ) WriteLine();
        }
    }
}
