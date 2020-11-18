
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

namespace Tests
{
    public static partial class DataRows
    {
        public static IEnumerable<object?[]> Taille =>
            new []
            {
                DataRow(nameof(ArbreNull), 0),
                DataRow(nameof(Arbres1), 1),
                DataRow(nameof(Arbres2), 2),
                DataRow(nameof(Arbres3), 3),
                DataRow(nameof(Arbres10), 10),
                DataRow(nameof(Arbres100), 100),
                DataRow(nameof(Arbres1K), 1000),
            };

        public static IEnumerable<object?[]> NbFeuilles =>
            new[]
            {
                DataRow(nameof(ArbreNull), 0, 1),
                DataRow(nameof(Arbres1), 1, 1, 2),
                DataRow(nameof(Arbres2), 1, 1, 2),
                DataRow(nameof(Arbres3), 1, 1, 2, 4, 5),
                DataRow(nameof(Arbres3), 2, 3),
                DataRow(nameof(ArbresTypiques), 2, 1, 2),
                DataRow(nameof(ArbresTypiques), 5, 3),
                DataRow(nameof(ArbresTypiques), 4, 4, 5),
                DataRow(nameof(Arbres100), 36, 1),
                DataRow(nameof(Arbres1K), 380, 1),
            };

        public static IEnumerable<object?[]> NbToutesEspèces =>
            new[]
            {
                DataRow(nameof(ArbreNull), 0, 0, 0, 0, 1),
                DataRow(nameof(Arbres1), 0, 0, 0, 1, 1),
                DataRow(nameof(Arbres1), 0, 0, 0, 1, 2),
                DataRow(nameof(Arbres2), 0, 1, 0, 1, 1),
                DataRow(nameof(Arbres2), 0, 0, 1, 1, 2),
                DataRow(nameof(Arbres3), 0, 2, 0, 1, 1),
                DataRow(nameof(Arbres3), 0, 1, 1, 1, 2),
                DataRow(nameof(Arbres3), 1, 0, 0, 2, 3),
                DataRow(nameof(Arbres3), 0, 1, 1, 1, 4),
                DataRow(nameof(Arbres3), 0, 0, 2, 1, 5),
                DataRow(nameof(ArbresTypiques), 1, 1, 1, 2, 1),
                DataRow(nameof(ArbresTypiques), 1, 1, 2, 2, 2),
                DataRow(nameof(ArbresTypiques), 4, 1, 0, 5, 3),
                DataRow(nameof(ArbresTypiques), 3, 1, 2, 4, 4),
                DataRow(nameof(ArbresTypiques), 3, 1, 1, 4, 5),
                DataRow(nameof(Arbres100), 35, 15, 14, 36, 1),
                DataRow(nameof(Arbres1K), 379, 113, 128, 380, 1),
            };

        public static IEnumerable<object?[]> Hauteur =>
            new[]
            {
                DataRow(nameof(ArbreNull), 0, 1),
                DataRow(nameof(Arbres1), 1, 1, 2),
                DataRow(nameof(Arbres2), 2, 1, 2),
                DataRow(nameof(Arbres3), 3, 1, 2, 4, 5),
                DataRow(nameof(Arbres3), 2, 3),
                DataRow(nameof(ArbresTypiques), 3, 1),
                DataRow(nameof(ArbresTypiques), 4, 2, 4, 5),
                DataRow(nameof(ArbresTypiques), 5, 3),
                DataRow(nameof(Arbres100), 14, 1),
                DataRow(nameof(Arbres1K), 34, 1),
            };

        public static IEnumerable<object?[]> Largeurs =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, ""),
                DataRow(nameof(Arbres1), 1, "1"),
                DataRow(nameof(Arbres2), 1, "1 1"),
                DataRow(nameof(Arbres2), 2, "1 1"),
                DataRow(nameof(Arbres3), 1, "1 1 1"),
                DataRow(nameof(Arbres3), 2, "1 1 1"),
                DataRow(nameof(Arbres3), 3, "1 2"),
                DataRow(nameof(Arbres3), 4, "1 1 1"),
                DataRow(nameof(Arbres3), 5, "1 1 1"),
                DataRow(nameof(ArbresTypiques), 1, "1 2 2"),
                DataRow(nameof(ArbresTypiques), 2, "1 1 2 2"),
                DataRow(nameof(ArbresTypiques), 3, "1 2 4 2 1"),
                DataRow(nameof(ArbresTypiques), 4, "1 2 4 3"),
                DataRow(nameof(ArbresTypiques), 5, "1 2 3 3"),
                DataRow(nameof(Arbres100), 1, "1 2 4 5 8 7 11 14 16 10 10 7 4 1"),
            };

        public static IEnumerable<object?[]> Chercher =>
            new[]
            {
                DataRow(nameof(ArbreNull), 1, 1, null),
                DataRow(nameof(Arbres1), 2, 1, null),
                DataRow(nameof(Arbres1), 2, 2, "R[2]"),
                DataRow(nameof(Arbres1), 2, 3, null),
                DataRow(nameof(Arbres2), 1, 1, "[1]"),
                DataRow(nameof(Arbres3), 1, 2, "[.2]"),
                DataRow(nameof(Arbres3), 2, 2, "[2]"),
                DataRow(nameof(Arbres3), 3, 2, "R[.2.]"),
                DataRow(nameof(Arbres3), 4, 2, "[2]"),
                DataRow(nameof(Arbres3), 5, 2, "[2.]"),
                DataRow(nameof(ArbresTypiques), 1, 2, "[.2]"),
                DataRow(nameof(ArbresTypiques), 2, 3, "[.3.]"),
                DataRow(nameof(ArbresTypiques), 3, 0, null),
                DataRow(nameof(ArbresTypiques), 3, 6, "[6]"),
                DataRow(nameof(ArbresTypiques), 3, 7, null),
                DataRow(nameof(ArbresTypiques), 3, 8, "[.8]"),
                DataRow(nameof(ArbresTypiques), 3, 10, null),
                DataRow(nameof(ArbresTypiques), 3, 20, "R[.20.]"),
                DataRow(nameof(ArbresTypiques), 3, 21, "[21]"),
                DataRow(nameof(ArbresTypiques), 3, 22, null),
                DataRow(nameof(ArbresTypiques), 3, 25, "[.25.]"),
                DataRow(nameof(ArbresTypiques), 3, 26, null),
                DataRow(nameof(ArbresTypiques), 3, 30, null),
                DataRow(nameof(ArbresTypiques), 5, 3, "[3.]"),
                DataRow(nameof(ArbresTypiques), 5, 5, "[.5.]"),
            };
    }

    [TestClass]
    public class S21_ArbreBin2
    {
        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Taille), typeof(DataRows))]
        public void T01a_TailleR(string factoryName, int taille)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                foreach (var arbre in Arbres(factoryName))
                    AreEqual(taille, TailleR(arbre));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Taille), typeof(DataRows))]
        public void T01b_TailleP(string factoryName, int taille)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                foreach (var arbre in Arbres(factoryName))
                    AreEqual(taille, TailleP(arbre));
            });
        }


        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbFeuilles), typeof(DataRows))]
        public void T02a_NbFeuillesR(string factoryName, int nbF, params int[] indices)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbres = Arbres(factoryName).ToArray();
                foreach (var i in indices)
                    AreEqual(nbF, NbFeuillesR(arbres[i-1]));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbFeuilles), typeof(DataRows))]
        public void T02b_NbFeuillesP(string factoryName, int nbF, params int[] indices)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbres = Arbres(factoryName).ToArray();
                foreach (var i in indices)
                    AreEqual(nbF, NbFeuillesP(arbres[i - 1]));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbFeuilles), typeof(DataRows))]
        public void T03a_NbEspèce(string factoryName, int nbF, params int[] indices)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbres = Arbres(factoryName).ToArray();
                foreach (var i in indices)
                    AreEqual(nbF, NbEspèce(arbres[i - 1], EspèceDeNoeud.Feuille));
            });
        }


        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbToutesEspèces), typeof(DataRows))]
        public void T03b_NbEspèce(string factoryName, int nbE, int nbTG, int nbTD, int nbF, int indice)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbre = Arbre(factoryName, indice);
                AreEqual(nbE, NbEspèce(arbre, EspèceDeNoeud.Embranchement));
                AreEqual(nbTG, NbEspèce(arbre, EspèceDeNoeud.TigeGauche));
                AreEqual(nbTD, NbEspèce(arbre, EspèceDeNoeud.TigeDroite));
                AreEqual(nbF, NbEspèce(arbre, EspèceDeNoeud.Feuille));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbToutesEspèces), typeof(DataRows))]
        public void T04a_NbToutesEspècesNR(string factoryName, int nbE, int nbTG, int nbTD, int nbF, int indice)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual((nbE, nbTG, nbTD, nbF), NbToutesEspècesNR(Arbre(factoryName, indice)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.NbToutesEspèces), typeof(DataRows))]
        public void T04b_NbToutesEspècesR(string factoryName, int nbE, int nbTG, int nbTD, int nbF, int indice)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual((nbE, nbTG, nbTD, nbF), NbToutesEspècesR(Arbre(factoryName, indice)));
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(nameof(ArbreNull), 1, "")]
        [DataRow(nameof(Arbres1), 1, "1")]
        [DataRow(nameof(Arbres1), 2, "2")]
        [DataRow(nameof(Arbres2), 1, "1")]
        [DataRow(nameof(Arbres2), 2, "2")]
        [DataRow(nameof(Arbres3), 1, "1")]
        [DataRow(nameof(Arbres3), 2, "2")]
        [DataRow(nameof(Arbres3), 3, "1 3")]
        [DataRow(nameof(Arbres3), 4, "2")]
        [DataRow(nameof(Arbres3), 5, "3")]
        [DataRow(nameof(ArbresTypiques), 1, "1 5")]
        [DataRow(nameof(ArbresTypiques), 2, "1 5")]
        [DataRow(nameof(ArbresTypiques), 3, "3 6 13 21 28")]
        [DataRow(nameof(ArbresTypiques), 4, "10 13 17 21")]
        [DataRow(nameof(ArbresTypiques), 5, "4 7 8 9")]
        public void T05_Feuilles(string factoryName, int indice, string feuilles)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(feuilles, Feuilles(Arbre(factoryName, indice)).Select(n => n.Key).Trier().EnTexte());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Hauteur), typeof(DataRows))]
        public void T06_Hauteur(string factoryName, int hauteur, params int[] indices)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbres = Arbres(factoryName).ToArray();
                foreach (var i in indices)
                    AreEqual(hauteur, Hauteur(arbres[i - 1]));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Largeurs), typeof(DataRows))]
        public void T07a_Largeurs(string factoryName, int indice, string largeurs)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(largeurs, Largeurs(Arbre(factoryName, indice)).EnTexte());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Largeurs), typeof(DataRows))]
        public void T07b_Largeur(string factoryName, int indice, string largeurs)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var largeur = largeurs
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.Parse(n))
                    .DefaultIfEmpty()
                    .Max();
                AreEqual(largeur, Largeur(Arbre(factoryName, indice)));
            });
        }


        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void T08a_ChercherPc(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherPc(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void T08b_ChercherPi(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherPa(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void T08c_Chercher(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, Chercher(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.Chercher), typeof(DataRows))]
        public void Tb09_ChercherR(string factoryName, int indice, int cléRecherché, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString, ChercherR(Arbre(factoryName, indice), cléRecherché)?.ToString());
            });
        } 
    }
}
