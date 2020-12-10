
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using ArbreBinLib;
using System.Linq;
using static ArbreBinLib.Factory;
using static ArbreBinLib.FactoryPlus;
using System;
using static Tests.TestUtil;
using System.Collections.Generic;
using cstjean.info.fg.utilitaire.extensions;

namespace Tests
{
    public static partial class DataRows
    {
        public static IEnumerable<object?[]> MaxP =>
            new[] 
            {
                DataRow(nameof(ArbreNull), 0),
                DataRow(nameof(ArbresTypiques), 5, 6, 28, 21, 9),
            };
        public static IEnumerable<object?[]> CheminerR =>
            new[]
            {
                DataRow("", 20),
                DataRow("G", 5),
                DataRow("D", 25),
                DataRow("GG", 3),
                DataRow("GD", 12),
                DataRow("DD", 28),
                DataRow("DG", 21),
                DataRow("GDD", 13),
                DataRow("GDG", 8),
                DataRow("GDGG", 6),
                DataRow("GDGGG", null),
                DataRow("GDGGD", null),
                DataRow("GGG", null),
                DataRow("GGD", null),
                DataRow("DDD", null),
                DataRow("DDD", null),
                DataRow("DGGD", null),
            };
        public static IEnumerable<object?[]> CheminerRLettreInvalide =>
            new[]
            {
                DataRow("Z", 'Z'),
                DataRow("X", 'X'),
                DataRow("GA", 'A'),
                DataRow("DB", 'B'),
                DataRow("GGC", 'C'),
                DataRow("GDF", 'F'),
            };
    }

    [TestClass]
    public class S45_ExamenFinal2020
    {

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.MaxP), typeof(DataRows))]
        public void T01a_MaxP(string factoryName, params int[] maximums)
        {
            NotImplementedInconclusive(() =>
            {
                foreach (var (i, arbre) in Arbres(factoryName).Numéroter0())
                    AreEqual(maximums[i], ArbreBin<int, int>.MaxP(arbre));
            });
        }

        [TestMethod, Timeout(500)]
        public void T01b_MaxP_arbres_syntaxiques()
        {
            NotImplementedInconclusive(() =>
            {
                // Mettre en commentaire ci-dessous si non implémentés.
                AreEqual("3", ArbreBin<string, string>.MaxP(ArbresSyntaxiquesTypiques().ElementAt(0)));
                AreEqual("7", ArbreBin<string, string>.MaxP(ArbresSyntaxiquesTypiques().ElementAt(1)));
                AreEqual("9", ArbreBin<string, string>.MaxP(ArbresSyntaxiquesTypiques().ElementAt(2)));
                AreEqual("9", ArbreBin<string, string>.MaxP(ArbresSyntaxiquesTypiques().ElementAt(3)));
                AreEqual("8", ArbreBin<string, string>.MaxP(ArbresSyntaxiquesTypiques().ElementAt(4)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.CheminerR), typeof(DataRows))]
        public void T02a_CheminerR(string chemin, int? clé)
        {
            NotImplementedInconclusive(() =>
            {
                var noeud = ArbreBin<int, int>.CheminerR(ArbresTypiques().ElementAt(2), chemin);
                AreEqual(clé, noeud?.Key);
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.CheminerRLettreInvalide), typeof(DataRows))]
        public void T02b_CheminerR_lettre_invalide(string chemin, char lettreInvalide)
        {
            NotImplementedInconclusive(() =>
            {
                var ex = Throwed<ArgumentException>(
                    () => ArbreBin<int, int>.CheminerR(ArbresTypiques().ElementAt(2), chemin));
                StringAssert.EndsWith(ex?.Message, $"{lettreInvalide}");
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.CheminerR), typeof(DataRows))]
        public void T02c_CheminerR_arbre_null(string chemin, int? _)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var noeud = ArbreBin<int, int>.CheminerR(null, chemin);
                IsNull(noeud);
            });
        }

    }
}

