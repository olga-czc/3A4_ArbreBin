
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using ArbreBinLib;
using System.Linq;
using static ArbreBinLib.Factory;
using static ArbreBinLib.FactoryPlus;
using static ArbreBinLib.ArbreBin<int, int>;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using cstjean.info.fg.utilitaire.extensions;
using static Tests.TestUtil;
using System.Data;

namespace Tests
{
    public static partial class DataRows
    {
 
        public static IEnumerable<object?[]> PréOrdre =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, ""),
                DataRow(nameof(Arbres1), 1, "1"),
                DataRow(nameof(Arbres2), 1, "2 1"),
                DataRow(nameof(Arbres2), 2, "1 2"),
                DataRow(nameof(Arbres3), 1, "3 2 1"),
                DataRow(nameof(Arbres3), 2, "3 1 2"),
                DataRow(nameof(Arbres3), 3, "2 1 3"),
                DataRow(nameof(Arbres3), 4, "1 3 2"),
                DataRow(nameof(Arbres3), 5, "1 2 3"),
                DataRow(nameof(ArbresTypiques), 1, "3 2 1 4 5"),
                DataRow(nameof(ArbresTypiques), 2, "6 3 4 1 2 5"),
                DataRow(nameof(ArbresTypiques), 3, "20 5 3 12 8 6 13 25 21 28"),
                DataRow(nameof(ArbresTypiques), 4, "15 12 8 10 14 13 20 16 17 21"),
                DataRow(nameof(ArbresTypiques), 5, "1 2 4 5 7 8 3 6 9"),
            };

        public static IEnumerable<object?[]> EnOrdre =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, ""),
                DataRow(nameof(Arbres1), 1, "1"),
                DataRow(nameof(Arbres2), 1, "1 2"),
                DataRow(nameof(Arbres2), 2, "1 2"),
                DataRow(nameof(Arbres3), 1, "1 2 3"),
                DataRow(nameof(Arbres3), 2, "1 2 3"),
                DataRow(nameof(Arbres3), 3, "1 2 3"),
                DataRow(nameof(Arbres3), 4, "1 2 3"),
                DataRow(nameof(Arbres3), 5, "1 2 3"),
                DataRow(nameof(ArbresTypiques), 1, "1 2 3 4 5"),
                DataRow(nameof(ArbresTypiques), 2, "6 4 1 3 5 2"),
                DataRow(nameof(ArbresTypiques), 3, "3 5 6 8 12 13 20 21 25 28"),
                DataRow(nameof(ArbresTypiques), 4, "8 10 12 13 14 15 16 17 20 21"),
                DataRow(nameof(ArbresTypiques), 5, "4 2 7 5 8 1 3 9 6"),
            };

        public static IEnumerable<object?[]> PostOrdre =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, ""),
                DataRow(nameof(Arbres1), 1, "1"),
                DataRow(nameof(Arbres2), 1, "1 2"),
                DataRow(nameof(Arbres2), 2, "2 1"),
                DataRow(nameof(Arbres3), 1, "1 2 3"),
                DataRow(nameof(Arbres3), 2, "2 1 3"),
                DataRow(nameof(Arbres3), 3, "1 3 2"),
                DataRow(nameof(Arbres3), 4, "2 3 1"),
                DataRow(nameof(Arbres3), 5, "3 2 1"),
                DataRow(nameof(ArbresTypiques), 1, "1 2 5 4 3"),
                DataRow(nameof(ArbresTypiques), 2, "1 4 5 2 3 6"),
                DataRow(nameof(ArbresTypiques), 3, "3 6 8 13 12 5 21 28 25 20"),
                DataRow(nameof(ArbresTypiques), 4, "10 8 13 14 12 17 16 21 20 15"),
                DataRow(nameof(ArbresTypiques), 5, "4 7 8 5 2 9 6 3 1"),
            };

        public static IEnumerable<object?[]> EnLargeur =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, ""),
                DataRow(nameof(Arbres1), 1, "1"),
                DataRow(nameof(Arbres2), 1, "2 1"),
                DataRow(nameof(Arbres2), 2, "1 2"),
                DataRow(nameof(Arbres3), 1, "3 2 1"),
                DataRow(nameof(Arbres3), 2, "3 1 2"),
                DataRow(nameof(Arbres3), 3, "2 1 3"),
                DataRow(nameof(Arbres3), 4, "1 3 2"),
                DataRow(nameof(Arbres3), 5, "1 2 3"),
                DataRow(nameof(ArbresTypiques), 1, "3 2 4 1 5"),
                DataRow(nameof(ArbresTypiques), 2, "6 3 4 2 1 5"),
                DataRow(nameof(ArbresTypiques), 3, "20 5 25 3 12 21 28 8 13 6"),
                DataRow(nameof(ArbresTypiques), 4, "15 12 20 8 14 16 21 10 13 17"),
                DataRow(nameof(ArbresTypiques), 5, "1 2 3 4 5 6 7 8 9"),
            };

    }

    [TestClass]
    public class S31_ArbreBin3
    {

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.PréOrdre), typeof(DataRows))]
        public void T01_PréOrdre(string factoryName, int indice, string préOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(préOrdre, Arbre(factoryName, indice).Lister(PréOrdre));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void T02_EnOrdre(string factoryName, int indice, string enOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(enOrdre, Arbre(factoryName, indice).Lister(EnOrdre));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.PostOrdre), typeof(DataRows))]
        public void T03_PostOrdre(string factoryName, int indice, string postOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(postOrdre, Arbre(factoryName, indice).Lister(PostOrdre));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void T04_EnOrdreInverse(string factoryName, int indice, string enOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(string.Join(' ', enOrdre.Split(' ').Reverse()),
                    Arbre(factoryName, indice).Lister(EnOrdreInverse));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnLargeur), typeof(DataRows))]
        public void T05_EnLargeur(string factoryName, int indice, string enLargeur)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(enLargeur, Arbre(factoryName, indice).Lister(EnLargeur));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void T06_EnOrdreItératif(string factoryName, int indice, string enOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(enOrdre, Arbre(factoryName, indice).Lister(EnOrdreItératif));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Taille), typeof(DataRows))]
        public void T10_TailleV(string factoryName, int taille)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                foreach (var arbre in Arbres(factoryName))
                    AreEqual(taille, TailleV(arbre));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbFeuilles), typeof(DataRows))]
        public void T11_NbFeuillesV(string factoryName, int nbF, params int[] indices)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbres = Arbres(factoryName).ToArray();
                foreach (var i in indices)
                    AreEqual(nbF, NbFeuillesV(arbres[i - 1]));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbToutesEspèces), typeof(DataRows))]
        public void T12_NbToutesEspècesV(string factoryName, int nbE, int nbTG, int nbTD, int nbF, int indice)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual((nbE, nbTG, nbTD, nbF), NbToutesEspècesV(Arbre(factoryName, indice)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void T13_ChercherV(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherV(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }


        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void T20_NoeudsEnOrdre(string factoryName, int indice, string enOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(enOrdre, NoeudsEnOrdre(Arbre(factoryName, indice)).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.PréOrdre), typeof(DataRows))]
        public void T21a_Noeuds(string factoryName, int indice, string préOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(préOrdre, Noeuds(Arbre(factoryName, indice), PréOrdre).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void T21b_Noeuds(string factoryName, int indice, string ordre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(ordre, Noeuds(Arbre(factoryName, indice), EnOrdre).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.PréOrdre), typeof(DataRows))]
        public void T22_NoeudsPréOrdre(string factoryName, int indice, string préOrdre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(préOrdre, NoeudsPréOrdre(Arbre(factoryName, indice)).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void Tb30_ChercherVX(string factoryName, int indice, int cléRecherché, string? toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherVX(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }

        //[TestMethod, Timeout(500)]
        //[DataRow(1, 1)]
        //[DataRow(2, 3)]
        //[DataRow(3, 6)]
        //[DataRow(4, 10)]
        //[DataRow(20, 55, true)]
        //public void Tb31_EnOrdreAbrégé(int compte, int sommeAttendue, bool retour = false)
        //{
        //    TestUtil.NotImplementedInconclusive(() =>
        //    {
        //        int somme = 0;
        //        int compteur = 0;
        //        var arbre = Arbres10().First();
        //        AreEqual(retour, EnOrdreAbrégé(arbre, noeud => { 
        //            somme += noeud.Key; 
        //            compteur++; 
        //            return compteur < compte; 
        //        }));
        //        AreEqual(sommeAttendue, somme);
        //    });
        //}

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void Tb32_ChercherVA(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherVA(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void Tb40_CinqPremiersL(string factoryName, int indice, string enOrdre)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(string.Join(' ', enOrdre.Split(' ').Take(5)), 
                    CinqPremiersL(Arbre(factoryName, indice)).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void Tb41_CinqPremiersVA(string factoryName, int indice, string enOrdre)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(string.Join(' ', enOrdre.Split(' ').Take(5)),
                    CinqPremiersVA(Arbre(factoryName, indice)).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void Tb42_YieldNoeudsEnOrdre(string factoryName, int indice, string enOrdre)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(enOrdre, YieldNoeudsEnOrdre(Arbre(factoryName, indice)).StrKeys());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EnOrdre), typeof(DataRows))]
        public void Tb43_CinqPremiersY(string factoryName, int indice, string enOrdre)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(string.Join(' ', enOrdre.Split(' ').Take(5)),
                    CinqPremiersY(Arbre(factoryName, indice)).StrKeys());
            });
        }
    }
}
