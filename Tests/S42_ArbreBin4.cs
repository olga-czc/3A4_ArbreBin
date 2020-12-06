
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

using ArbreBinLib;
using System.Linq;
using static ArbreBinLib.Factory;
using static ArbreBinLib.FactoryPlus;
using System;
using static Tests.TestUtil;
using System.Collections.Generic;
using System.Data;

namespace Tests
{
    public static partial class DataRows
    {
        public static IEnumerable<object?[]> SyntaxePostfixée =>
            new[]
            {
                DataRow(1, "3"),
                DataRow(2, "7 3 *"),
                DataRow(3, "2 3 + 9 * 7 +"),
                DataRow(4, "3 5 9 + 2 * +"),
                DataRow(5, "2 3 + 8 4 - 5 + *"),
            };

        public static IEnumerable<object?[]> SyntaxePréfixée =>
            new[]
            {
                DataRow(1, "3"),
                DataRow(2, "(* 7 3)"),
                DataRow(3, "(+ (* (+ 2 3) 9) 7)"),
                DataRow(4, "(+ 3 (* (+ 5 9) 2))"),
                DataRow(5, "(* (+ 2 3) (+ (- 8 4) 5))"),
            };

        public static IEnumerable<object?[]> SyntaxeInfixée =>
            new[]
            {
                DataRow(1, "3"),
                DataRow(2, "7 * 3"),
                DataRow(3, "((2 + 3) * 9) + 7"),
                DataRow(4, "3 + ((5 + 9) * 2)"),
                DataRow(5, "(2 + 3) * ((8 - 4) + 5)"),
            };

        public static IEnumerable<object?[]> EvalTypiques =>
            new[]
            {
                DataRow(1, 3),
                DataRow(2, 21),
                DataRow(3, 52),
                DataRow(4, 31),
                DataRow(5, 45),
            };

        public static IEnumerable<object?[]> EvalIntCroissants =>
            new[]
            {
                DataRow(1, 6),
                DataRow(2, 35),
                DataRow(3, -169),
                DataRow(4, -50),
                DataRow(5, 186),
                DataRow(6, 0),
                DataRow(7, -299),
            };

        public static IEnumerable<object?[]> EvalDoubleCroissants =>
            new[]
            {
                DataRow(1, 6),
                DataRow(2, 35),
                DataRow(3, -169),
                DataRow(4, -50),
                DataRow(5, 186),
                DataRow(6, 1.6174948240165633E-05),
                DataRow(7, -465.4335664335664),
            };

        public static IEnumerable<object?[]> EvalSpéciauxErreurs =>
            new[]
            {
                DataRow(1, typeof(ArgumentNullException), "ne peut pas être vide"),
                DataRow(2, typeof(FormatException), "nombre invalide", "@ R[deux]"),
                DataRow(3, typeof(SyntaxErrorException), "manque argument de gauche", "@ R[*.]"),
                DataRow(4, typeof(SyntaxErrorException), "manque argument de droite", "@ R[.+]"),
                DataRow(5, typeof(SyntaxErrorException), "opérateur inconnu", "@ R[.%.]"),
            };

        public static IEnumerable<object?[]> EvalSpéciauxOk =>
            new[]
            {
                DataRow(6, double.PositiveInfinity),
                DataRow(7, double.NaN),
            };
    }

    [TestClass]
    public class S42_ArbreBin4
    {

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.SyntaxePostfixée), typeof(DataRows))]
        public void T01_SyntaxePostfixée(int indice, string ordre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(ordre, ArbreBin<string, string>.SyntaxePostfixée(
                    ArbresSyntaxiquesTypiques().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.SyntaxePréfixée), typeof(DataRows))]
        public void T02_SyntaxePréfixée(int indice, string ordre)
        {
            TestUtil.NotImplementedInconclusive(() =>
            {
                AreEqual(ordre, ArbreBin<string,string>.SyntaxePréfixée(
                    ArbresSyntaxiquesTypiques().ElementAt(indice-1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.SyntaxeInfixée), typeof(DataRows))]
        public void T03_SyntaxeInfixée(int indice, string ordre)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(ordre, ArbreBin<string, string>.SyntaxeInfixée(
                    ArbresSyntaxiquesTypiques().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalTypiques), typeof(DataRows))]
        public void T11a_EvalInt_Typiques(int indice, int eval)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(eval, ArbreBin<string, string>.EvalInt(
                    ArbresSyntaxiquesTypiques().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalIntCroissants), typeof(DataRows))]
        public void T11b_EvalInt_Croissants(int indice, int eval)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(eval, ArbreBin<string, string>.EvalInt(
                    ArbresSyntaxiquesCroissants().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalTypiques), typeof(DataRows))]
        public void T12a_EvalDouble_Typiques(int indice, int eval)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(eval, ArbreBin<string, string>.EvalDouble(
                    ArbresSyntaxiquesTypiques().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalDoubleCroissants), typeof(DataRows))]
        public void T12b_EvalDouble_Croissants(int indice, double eval)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(eval, ArbreBin<string, string>.EvalDouble(
                    ArbresSyntaxiquesCroissants().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalSpéciauxOk), typeof(DataRows))]
        public void T12c_EvalDouble_SpéciauxOk(int indice, double eval)
        {
            NotImplementedInconclusive(() =>
            {
                AreEqual(eval, ArbreBin<string, string>.EvalDouble(
                    ArbresSyntaxiquesSpéciaux().ElementAt(indice - 1)));
            });
        }

        [TestMethod, Timeout(500)]
        [DynamicData(nameof(DataRows.EvalSpéciauxErreurs), typeof(DataRows))]
        public void T12d_EvalDouble_SpéciauxErreurs(int indice, Type tException, params string[] messages)
        {
            var ex = Throwed<Exception>(
                () => ArbreBin<string, string>.EvalDouble(
                        ArbresSyntaxiquesSpéciaux().ElementAt(indice - 1)));
            IsNotNull(ex);
            if (ex != null)
            {
                AreEqual(tException, ex.GetType());
                foreach (var message in messages)
                    StringAssert.Contains(ex.Message.ToLower(), message.ToLower());
            }
        }


    }
}
