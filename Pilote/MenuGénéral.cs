// Pour Étudiant, phase 1

using System.Collections.Generic;
using static System.ConsoleColor;
using cstjean.info.fg.consoleplus;

namespace SDD
{
    public static partial class MenuGénéral
    {
        public static readonly List<MenuItem> MenuItems = new List<MenuItem>
        {
            new MenuItem("Visualiser (arborescences seulement)", () => MenuArbres.Show(false), false),
            MenuItem.Spacer,
            new MenuItem("Stats 1"),
            new MenuItem("1 Sans benchmarks", () => MenuArbres.Show(false, 1), false),
            new MenuItem("1 Avec benchmarks", () => MenuArbres.Show(true, 1), false),
        };

        public static void Show()
        {            
            ConsoleMenu.Show
            (
                $"Arbre Binaire --- {Program.Nom}", Green, Black, Red, Magenta, null, null
                , MenuItems.ToArray()
            ); ;
        }
    }
}
