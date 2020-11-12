// Pour étudiant, phase 1

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using cstjean.info.fg.utilitaire.extensions;
using ArbreBinLib;

// NB Ceci n'améliore pas le temps d'exécution...
// [assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Tests
{
    public static partial class TestUtil
    {
        public static object?[] DataRow(params object?[] args)
            => args;


        public static void NotImplementedInconclusive(params Action[] actions)
        {
            try
            {
                foreach(var action in actions)
                    action();
            }
            catch (NotImplementedException ex)
            {
                Debug.WriteLine(ex.Message);
                Inconclusive(ex.Message);
            }
        }

        public static void CheckParents<T>(ArbreBin<T, T>.Noeud? arbre, ArbreBin<T, T>.Noeud? parent = null)
            where T: notnull, IComparable<T>
        {
            if (arbre == null) return;
            IsTrue(arbre.Parent == parent);
            CheckParents(arbre.Gauche, arbre);
            CheckParents(arbre.Droite, arbre);
        }

        public static ArbreBin<T, T>.Noeud? CheckParentsBy<T>(ArbreBin<T, T>.Noeud? arbre)
            where T: notnull, IComparable<T>
        {
            CheckParents(arbre);
            return arbre;
        }

        public static void CheckFactory<T>(Func<IEnumerable<ArbreBin<T,T>.Noeud?>> factoryMethod, int i, string arbre1D)
            where T: notnull, IComparable<T>
        {
            NotImplementedInconclusive(() =>
                AreEqual(arbre1D, CheckParentsBy(
                    factoryMethod().ElementAtOrDefault(i - 1))
                    .ToString1D().Raccourcir(70)
                )
            );
        }

        public static void CheckFactory<T>(Func<ArbreBin<T, T>.Noeud?> factoryMethod, string arbre1D)
            where T: notnull, IComparable<T>
        {
            NotImplementedInconclusive(() =>
                AreEqual(arbre1D, CheckParentsBy(
                    factoryMethod()).ToString1D().Raccourcir(70))
            );
        }

        //public static void Throws<T>(Func<object?> fn)
        //    where T : Exception
        //{
        //    _ = Throwed<T>(fn);
        //}

        public static void Throws<T>(Action fn)
            where T : Exception
        {
            _ = Throwed<T>(fn);
        }

        //public static T? Throwed<T>(Func<object?> fn)
        //    where T : Exception
        //{
        //    return Throwed<T>(() => { _ = fn(); });
        //}

        public static T? Throwed<T>(Action fn)
            where T : Exception
        {
            try
            {
                fn();
                Fail($"Should throw exception of type {typeof(T).Name}");
            }
            catch (NotImplementedException)
            {
                Inconclusive();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                Fail($"Expecting exception of type {typeof(T).Name} but got {ex.GetType().Name}: {ex.Message}");
            }
            return default;
        }
    }
}