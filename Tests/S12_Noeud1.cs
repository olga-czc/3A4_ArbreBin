
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using ArbreBinLib;
using static ArbreBinLib.Factory;
using System.Linq;

namespace Tests
{

    [TestClass]
    public class S12_Noeud1
    {
        [TestMethod, Timeout(500)]
        [DataRow(1, EspèceDeNoeud.Feuille)]
        [DataRow(2, EspèceDeNoeud.Feuille)]
        [DataRow(3, EspèceDeNoeud.TigeGauche)]
        [DataRow(4, EspèceDeNoeud.TigeDroite)]
        [DataRow(7, EspèceDeNoeud.Embranchement)]
        public void T01_Espèce(int i, EspèceDeNoeud espèce)
        {
            TestUtil.NotImplementedInconclusive(() =>
                AreEqual(espèce, 
                    FactoryPlus.Arbres123()?.ElementAtOrDefault(i-1)?.Espèce)
            );
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "R[1]")]
        [DataRow(2, "R[2]")]
        [DataRow(3, "R[.2]")]
        [DataRow(4, "R[1.]")]
        [DataRow(7, "R[.2.]")]
        public void T02a_ToString_Racine(int i, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString,
                    FactoryPlus.Arbres123()?.ElementAtOrDefault(i-1)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(4, "[2]")]
        [DataRow(8, "[.3]")]
        [DataRow(9, "[2.]")]
        public void T02b_ToString_NonRacine(int i, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString,
                    FactoryPlus.Arbres123()?.ElementAtOrDefault(i - 1)?.Droite?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "R[1|2]")]
        [DataRow(2, "R[2|4]")]
        [DataRow(3, "R[.2|4]")]
        [DataRow(4, "R[1.|2]")]
        [DataRow(7, "R[.2.|4]")]
        public void T02c_ToString_Racine_Et_Valeurs(int i, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString,
                    FactoryPlus.Arbres123AvecValeurs()?.ElementAtOrDefault(i - 1)?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        [DataRow(4, "[2|4]")]
        [DataRow(8, "[.3|6]")]
        [DataRow(9, "[2.|4]")]
        public void T02d_ToString_NonRacine_Et_Valeurs(int i, string toString)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(toString,
                    FactoryPlus.Arbres123AvecValeurs()?.ElementAtOrDefault(i - 1)?.Droite?.ToString());
            });
        }

        [TestMethod, Timeout(500)]
        public void T02e_ToString_GrandsNombres()
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual("R[.12.|24]",
                    FactoryPlus.AddValues(FactoryPlus.Arbres100())
                    ?.ElementAtOrDefault(0)?.ToString());
            });
        }

    }

}
