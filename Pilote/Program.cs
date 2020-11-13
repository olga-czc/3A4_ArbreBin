// Pour Étudiant, phase 1

using System;
using System.Text;

using static cstjean.info.fg.consoleplus.ConsolePlus;

namespace SDD
{
    public static partial class Program
    {
        public const string Nom = "Olga Cazacioc";
        public const string Initiales = "OC";

        public static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 15, Console.LargestWindowHeight - 6);
            // Certains étudiants n'ont pas une console UTF8 par défaut
            // Mais on a besoin d'UTF8 pour mieux afficher les arbres en 2D.
            Console.OutputEncoding = UTF8Encoding.UTF8;
            IndentationGénérale = 2;

            Menu2();
            Menu3();
            Menu4();
            Menu5();
            Menu6();

            MenuGénéral.Show();
        }

        static partial void Menu2();
        static partial void Menu3();
        static partial void Menu4();
        static partial void Menu5();
        static partial void Menu6();

        static partial void RecoverSTEP(ref double STEP);

        public static double STEP
        {
            get
            {
                double step = 1e20;
                RecoverSTEP(ref step);
                return step;
            }
        }

    }
}
