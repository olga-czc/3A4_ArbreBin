using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ArbreBinLib;
using static Tests.TestUtil;
using System.Linq;

namespace Tests
{

    [TestClass]
    public class S11_Factory1
    {
        [TestMethod, Timeout(500)]
        public void T00_ArbreNull()
        {
            CheckFactory(Factory.ArbreNull, 1, "null");
        }


        [TestMethod, Timeout(500)]
        [DataRow(1, "(1)")]
        [DataRow(2, "(2)")]
        public void T01_Arbres1(int i, string arbre1D)
        {
            CheckFactory(Factory.Arbres1, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "((1)2)")]
        [DataRow(2, "(1(2))")]
        public void T02_Arbres2(int i, string arbre1D)
        {
            CheckFactory(Factory.Arbres2, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "(((1)2)3)")]
        [DataRow(2, "((1(2))3)")]
        [DataRow(3, "((1)2(3))")]
        [DataRow(4, "(1((2)3))")]
        [DataRow(5, "(1(2(3)))")]
        public void T03_Arbres3(int i, string arbre1D)
        {
            CheckFactory(Factory.Arbres3, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "(((1)2)3(4(5)))")]
        [DataRow(2, "(6((4(1))3((5)2)))")]
        [DataRow(3, "(((3)5(((6)8)12(13)))20((21)25(28)))")]
        [DataRow(4, "(((8(10))12((13)14))15((16(17))20(21)))")]
        [DataRow(5, "(((4)2((7)5(8)))1(3((9)6)))")]
        public void T04_ArbresTypiques(int i, string arbre1D)
        {
            CheckFactory(Factory.ArbresTypiques, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "((1)2((((3)4)5(6(7)))8((9)10)))")]
        [DataRow(2, "((1)2((3(4))5(((6)7)8(9(10)))))")]
        public void Tx10_Arbres10(int i, string arbre1D)
        {
            // Ce test devrait passer automatiquement quand vous aurez codé ArbresTypiques
            // Vous n'avez pas à code la méthode Arbres10
            NotImplementedInconclusive(
                () => Factory.ArbresTypiques().FirstOrDefault(),
                () => CheckFactory(FactoryPlus.Arbres10, i, arbre1D));
        }

    }

}