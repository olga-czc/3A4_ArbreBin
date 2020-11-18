
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using ArbreBinLib;
using System.Linq;

using static ArbreBinLib.Factory;
using static ArbreBinLib.FactoryPlus;
using static ArbreBinLib.ArbreBin<int, int>;

namespace Tests
{
    [TestClass]
    public partial class S22_Noeud2
    {
        [TestMethod, Timeout(500)]
        [DataRow(1, 5, 3)]
        [DataRow(2, 6, 6)]
        [DataRow(5, 9, 1)]
        public void T01_Racine(int indice, int maxNoeud, int cléRacine)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbre = Arbre(nameof(ArbresTypiques), indice);
                for (int clé = 1; clé <= maxNoeud; clé++)
                {
                    AreEqual(cléRacine, Chercher(arbre, clé)?.Racine.Key);
                }
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(20, 0)]
        [DataRow(5, 1)]
        [DataRow(3, 2)]
        [DataRow(12, 2)]
        [DataRow(8, 3)]
        [DataRow(6, 4)]
        // Dans l'arbre typique #3
        public void T02_Profondeur(int cléNoeud, int profondeur)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbre = Arbre(nameof(ArbresTypiques), 3);
                AreEqual(profondeur, Chercher(arbre, cléNoeud)?.Profondeur);
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(nameof(Arbres1), 1, 1, 1)]
        [DataRow(nameof(Arbres1), 2, 2, 2)]
        [DataRow(nameof(Arbres2), 1, 1, 2)]
        [DataRow(nameof(Arbres2), 2, 1, 2)]
        [DataRow(nameof(ArbresTypiques), 1, 1, 5)]
        [DataRow(nameof(ArbresTypiques), 2, 6, 2)]
        [DataRow(nameof(ArbresTypiques), 3, 3, 28)]
        [DataRow(nameof(ArbresTypiques), 4, 8, 21)]
        [DataRow(nameof(ArbresTypiques), 5, 4, 6)]
        public void T03_PlusGaucheDroite(string factoryName, int indice, int plusGauche, int plusDroite)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(plusGauche, Arbre(factoryName, indice)?.PlusGauche.Key);
                AreEqual(plusDroite, Arbre(factoryName, indice)?.PlusDroite.Key);
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(20, 20, 0)]
        [DataRow(5, 20, 1)]
        [DataRow(5, 5, 0)]
        [DataRow(3, 20, 2)]
        [DataRow(3, 5, 1)]
        [DataRow(3, 3, 0)]
        [DataRow(12, 5, 1)]
        [DataRow(8, 5, 2)]
        [DataRow(6, 5, 3)]
        [DataRow(6, 12, 2)]
        // Si pas un ancêtre, il faut retourner null
        [DataRow(12, 6, null)]
        [DataRow(12, 25, null)]
        public void T04_ProfondeurRelative(int clé, int cléRelative, int? profondeurRelative)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                var arbre = Arbre(nameof(ArbresTypiques), 3);
                AreEqual(profondeurRelative, 
                    Chercher(arbre, clé)?.ProfondeurRelative(Chercher(arbre, cléRelative)!));
            });
        }

    }

}
