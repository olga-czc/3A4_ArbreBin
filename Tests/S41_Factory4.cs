
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ArbreBinLib;
using static Tests.TestUtil;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class S41_Factory4
    {
        [TestMethod, Timeout(500)]
        [DataRow(1, "(3)")]
        [DataRow(2, "((7)*(3))")]
        [DataRow(3, "((((2)+(3))*(9))+(7))")]
        [DataRow(4, "((3)+(((5)+(9))*(2)))")]
        [DataRow(5, "(((2)+(3))*(((8)-(4))+(5)))")]
        public void T01_ArbresSyntaxiquesTypiques(int i, string arbre1D)
        {
            CheckFactory(Factory.ArbresSyntaxiquesTypiques, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "null"                          )]
        [DataRow(2, "(deux)"       )]
        [DataRow(3, "(*(3))"       )]
        [DataRow(4, "((7)+)"       )]
        [DataRow(5, "((17)%(4))"   )]
        [DataRow(6, "((3)/(0))")]
        [DataRow(7, "((0)/(0))")]

        public void T02_ArbresSyntaxiquesSpéciaux(int i, string arbre1D)
        {
            CheckFactory(Factory.ArbresSyntaxiquesSpéciaux, i, arbre1D);
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "((2)*(((4)*(6))/(8)))")]
        [DataRow(2, "((((2)*((4)*(5)))-((7)+(9)))+(11))")]
        public void T03_ArbresSyntaxiquesCroissants(int i, string arbre1D)
        {
            // Il faut d'abord implémenter la méthode des arbres syntaxiques typiques
            NotImplementedInconclusive(
                () => Factory.ArbresSyntaxiquesTypiques().First(), 
                () => CheckFactory(FactoryPlus.ArbresSyntaxiquesCroissants, i, arbre1D));
        }

        [TestMethod, Timeout(500)]
        [DataRow(1, "((((((((((((((1)-(2))-(((4)+(...277322...98)))*(50000)))))))))))))))))")]
        public void T04_ArbresSyntaxiques50K(int i, string arbre1D)
        {
            // Il faut d'abord implémenter la méthode des arbres syntaxiques typiques
            NotImplementedInconclusive(
                () => Factory.ArbresSyntaxiquesTypiques().First(),
                () => CheckFactory(FactoryPlus.ArbresSyntaxiques50K, i, arbre1D));
        }

    }

}