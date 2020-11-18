// Pour Étudiant, phase 2

using System;
using System.Linq;
using ArbreBinLib;
using cstjean.info.fg.consoleplus;
using static cstjean.info.fg.consoleplus.ConsolePlus;
using cstjean.info.fg.utilitaire.extensions;
using static ArbreBinLib.ArbreBin<int, int>;

namespace SDD
{
    public static partial class Program
    {

        static partial void Menu2()
        {
            if (STEP <= 2000) return;

            MenuGénéral.MenuItems.AddRange(new[] {
                MenuItem.Spacer,
                new MenuItem("Stats 2"),
                new MenuItem("2 Sans benchmarks", () => MenuArbres.Show(false, 2), false),
                new MenuItem("2 Avec benchmarks", () => MenuArbres.Show(true, 2), false),
            });
        }
    }


    public static partial class Stats 
    {
        static partial void AfficherStats2<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>
        {
            if (Program.STEP <= 2000) return;

            static T From(int i) => (T)Convert.ChangeType(i, typeof(T));

            if (false
            | Afficher(nameof(TailleR), () => ArbreBin<T, T>.TailleR(arbre))
            | Afficher(nameof(TailleP), () => ArbreBin<T, T>.TailleP(arbre))
            | Afficher(nameof(NbFeuillesR), () => ArbreBin<T, T>.NbFeuillesR(arbre))
            | Afficher(nameof(NbFeuillesP), () => ArbreBin<T, T>.NbFeuillesP(arbre))
            ) WriteLine();

            if (false
            | Afficher($"{nameof(NbEspèce)}(Feuilles      )", () => ArbreBin<T, T>.NbEspèce(arbre, EspèceDeNoeud.Feuille))
            | Afficher($"{nameof(NbEspèce)}(Embranchements)", () => ArbreBin<T, T>.NbEspèce(arbre, EspèceDeNoeud.Embranchement))
            | Afficher(nameof(NbToutesEspècesNR) + " => (E,TG,TD,F)", () => ArbreBin<T, T>.NbToutesEspècesNR(arbre))
            | Afficher(nameof(NbToutesEspècesR) + " => (E,TG,TD,F)", () => ArbreBin<T, T>.NbToutesEspècesR(arbre))
            | Afficher(nameof(Feuilles), () => ArbreBin<T, T>.Feuilles(arbre), feuilles => feuilles.Select(f => f.Key).Trier().EnTexte().Raccourcir(39))
            ) WriteLine();

            if (false
            | Afficher(nameof(Hauteur), () => ArbreBin<T, T>.Hauteur(arbre))
            | Afficher(nameof(Largeurs), () => ArbreBin<T, T>.Largeurs(arbre), largeurs => largeurs.EnTexte().Raccourcir(39))
            | Afficher(nameof(Largeur), () => ArbreBin<T, T>.Largeur(arbre))
            ) WriteLine();

            if (false
            | Afficher($"{nameof(ChercherPc)}(0)", () => ArbreBin<T, T>.ChercherPc(arbre, From(0)))
            | Afficher($"{nameof(ChercherPc)}(2)", () => ArbreBin<T, T>.ChercherPc(arbre, From(2)))
            | Afficher($"{nameof(ChercherPa)}(0)", () => ArbreBin<T, T>.ChercherPa(arbre, From(0)))
            | Afficher($"{nameof(ChercherPa)}(2)", () => ArbreBin<T, T>.ChercherPa(arbre, From(2)))
            | Afficher($"{nameof(Chercher)}(0)", () => ArbreBin<T, T>.Chercher(arbre, From(0)))
            | Afficher($"{nameof(Chercher)}(2)", () => ArbreBin<T, T>.Chercher(arbre, From(2)))
            | Afficher($"[B] {nameof(ChercherR)}(0)", () => ArbreBin<T, T>.ChercherR(arbre, From(0)))
            | Afficher($"[B] {nameof(ChercherR)}(2)", () => ArbreBin<T, T>.ChercherR(arbre, From(2)))
            ) WriteLine();

            if (arbre != null)
                if (false
                | Afficher($"{nameof(Noeud.Racine)}(2)", () => ArbreBin<T, T>.Chercher(arbre, From(2))?.Racine)
                | Afficher($"{nameof(Noeud.Profondeur)}(2)", () => ArbreBin<T, T>.Chercher(arbre, From(2))?.Profondeur)
                | Afficher(nameof(Noeud.PlusGauche), () => arbre.PlusGauche)
                | Afficher(nameof(Noeud.PlusDroite), () => arbre.PlusDroite)
                | Afficher($"{nameof(Noeud.PlusDroite)}(2)", () => ArbreBin<T, T>.Chercher(arbre, From(2))?.PlusDroite)
                | Afficher($"{nameof(Noeud.ProfondeurRelative)}(2,racine)", () => ArbreBin<T, T>.Chercher(arbre, From(2))?.ProfondeurRelative(arbre))
                | Afficher($"{nameof(Noeud.ProfondeurRelative)}(3,1)     ", () => ArbreBin<T, T>.Chercher(arbre, From(3))?.ProfondeurRelative(ArbreBin<T, T>.Chercher(arbre, From(1))))
                ) WriteLine();
        }
    }
}
