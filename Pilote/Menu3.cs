// Pour Étudiant, phase 3

using System;
using ArbreBinLib;
using cstjean.info.fg.consoleplus;
using static cstjean.info.fg.consoleplus.ConsolePlus;
using static ArbreBinLib.ArbreBin<int, int>;

namespace SDD
{
    public static partial class Program
    {
        static partial void Menu3()
        {
            if (STEP <= 3000) return;

            MenuGénéral.MenuItems.AddRange(new[] {
                MenuItem.Spacer,
                new MenuItem("Stats 3"),
                new MenuItem("3 Sans benchmarks", () => MenuArbres.Show(false, 3), false),
                new MenuItem("3 Avec benchmarks", () => MenuArbres.Show(true, 3), false),
            });

            // Exemple de code pour le PPT
            if (Convert.ToBoolean(0))
            {
                var arbre = new Noeud(1);
                PréOrdre(arbre, n => n.Value++);
            }

        }
    }

    public static partial class Stats
    {

        static partial void AfficherStats3<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>
        {
            if (Program.STEP <= 3000) return;

            static T From(int i) => (T)Convert.ChangeType(i, typeof(T));

            if (false
            | Afficher(nameof(PréOrdre), () => arbre.Lister(ArbreBin<T, T>.PréOrdre))
            | Afficher(nameof(EnOrdre), () => arbre.Lister(ArbreBin<T, T>.EnOrdre))
            | Afficher(nameof(PostOrdre), () => arbre.Lister(ArbreBin<T, T>.PostOrdre))
            | Afficher(nameof(EnOrdreInverse), () => arbre.Lister(ArbreBin<T, T>.EnOrdreInverse))
            | Afficher(nameof(EnLargeur), () => arbre.Lister(ArbreBin<T, T>.EnLargeur))
            | Afficher(nameof(EnOrdreItératif), () => arbre.Lister(ArbreBin<T, T>.EnOrdreItératif))
            ) WriteLine();

            if (false
            | (Afficher(nameof(TailleV), () => ArbreBin<T, T>.TailleV(arbre))
                && Afficher(nameof(TailleR), () => ArbreBin<T, T>.TailleR(arbre)))
            | (Afficher(nameof(NbFeuillesV), () => ArbreBin<T, T>.NbFeuillesV(arbre))
                && Afficher(nameof(NbFeuillesR), () => ArbreBin<T, T>.NbFeuillesR(arbre)))
            | (Afficher(nameof(NbToutesEspècesV), () => ArbreBin<T, T>.NbToutesEspècesV(arbre))
                && Afficher(nameof(NbToutesEspècesR), () => ArbreBin<T, T>.NbToutesEspècesR(arbre)))
            | (Afficher(nameof(ChercherV) + "(2)", () => ArbreBin<T, T>.ChercherV(arbre, From(2)))
                && Afficher(nameof(Chercher) + "(2)", () => ArbreBin<T, T>.Chercher(arbre, From(2))) )
            | Afficher("[B] " + nameof(ChercherVX) + "(2)", () => ArbreBin<T, T>.ChercherVX(arbre, From(2)))
            | Afficher("[B] " + nameof(ChercherVA) + "(2)", () => ArbreBin<T, T>.ChercherVA(arbre, From(2)))
            ) WriteLine();

            if (false
            | Afficher(nameof(NoeudsEnOrdre), () => ArbreBin<T, T>.NoeudsEnOrdre(arbre).StrKeys())
            | Afficher($"{nameof(Noeuds)}({nameof(EnOrdreInverse)})", () => ArbreBin<T, T>.Noeuds(arbre, ArbreBin<T, T>.EnOrdreInverse).StrKeys())
            | Afficher(nameof(NoeudsPréOrdre), () => ArbreBin<T, T>.NoeudsPréOrdre(arbre).StrKeys())
            | Afficher("[B] " + nameof(YieldNoeudsEnOrdre), () => ArbreBin<T, T>.YieldNoeudsEnOrdre(arbre).StrKeys())
            ) WriteLine();

            if (false
            | Afficher("[B] " + nameof(CinqPremiersL), () => ArbreBin<T, T>.CinqPremiersL(arbre).StrKeys())
            | Afficher("[B] " + nameof(CinqPremiersVA), () => ArbreBin<T, T>.CinqPremiersVA(arbre).StrKeys())
            | Afficher("[B] " + nameof(CinqPremiersY), () => ArbreBin<T, T>.CinqPremiersY(arbre).StrKeys())
            ) WriteLine();
        }
    }
}
