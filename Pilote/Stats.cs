// Pour Étudiant, phase 1

#define HIDE_NOT_IMPLEMENTED

using System;
using System.Linq;
using static System.ConsoleColor;
using ArbreBinLib;
using cstjean.info.fg.consoleplus;
using static cstjean.info.fg.consoleplus.ConsolePlus;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SDD
{
    public static partial class Stats
    {
        private static int Indice = 0;

        public static void Afficher<T>(int indice, ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>
        {
            Indice = indice;

            // Méthodes et propriétés nodales
            if (arbre != null && MenuArbres.StatLevels.Contains(1))
            {
                var aff = false;
                aff |= Afficher("Espèce(racine)", () => arbre.Espèce);
                aff |= Afficher("ToString(racine)", () => arbre.ToString());
                if (arbre.Droite != null)
                    aff |= Afficher("ToString(racine.Droite)", () => arbre.Droite.ToString());
                if (aff) WriteLine();
            }

            if (MenuArbres.StatLevels.Contains(2))
                AfficherStats2(arbre);
            if (MenuArbres.StatLevels.Contains(3))
                AfficherStats3(arbre);
            if (MenuArbres.StatLevels.Contains(4))
                AfficherStats4(arbre);
            if (MenuArbres.StatLevels.Contains(5))
                AfficherStats5(arbre);
            if (MenuArbres.StatLevels.Contains(6))
                AfficherStats6(arbre);
        }

        static partial void AfficherStats2<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>;
        static partial void AfficherStats3<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>;
        static partial void AfficherStats4<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>;
        static partial void AfficherStats5<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>;
        static partial void AfficherStats6<T>(ArbreBin<T, T>.Noeud? arbre)
            where T : notnull, IComparable<T>;


        /// <returns>vrai si quelque chose s'affiche</returns>
        static bool Afficher<T>(string titre, Func<T> fn, Func<T, string>? format = null)
        {
            try
            {
                using var colors = new ConfigColors(
                    valeur: (Magenta, null));
                _ = ConsolePlus.AfficherCalcul
                (
                    titre + $"  [{Indice}|{Program.Initiales}] " 
                    , calcul: () => InvokeWithTimeout(fn, 1000)
                    , alignement:(45,-15)
                    , duréeMaxMs: 50
                    , format: format
                    , noBenchmark: MenuArbres.NoBenchmark
#if HIDE_NOT_IMPLEMENTED
                    , doNotCatch: new Type[] { typeof(NotImplementedException) }
#endif              
                );
                return true;
            }
            catch (NotImplementedException) { return false; }
        }


        static T InvokeWithTimeout<T>(Func<T> fn, int timeoutMilliseconds)
        {
            T retour = default;

            var task = Task.Run( () =>
            {
                retour = fn();
            });

            try
            {
                if (!task.Wait(timeoutMilliseconds))
                {
                    throw new TimeoutException($"Délai d'exécution expiré: {timeoutMilliseconds}ms");
                }
            }
            catch(AggregateException ex)
            {
                if (ex.InnerExceptions.Count != 1) throw;
                throw ex.InnerExceptions[0];
            }
            return retour!;
        }

    }
}
