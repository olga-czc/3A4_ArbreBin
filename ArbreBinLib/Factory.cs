using System;
using System.Collections.Generic;
using System.Text;
using static ArbreBinLib.ArbreBin<int, int>;
using SyntaxNode = ArbreBinLib.ArbreBin<string, string>.Noeud;


namespace ArbreBinLib
{
    public static partial class Factory
    {
        public static IEnumerable<Noeud?> ArbreNull()
        {
            yield return null;

        }
        public static IEnumerable<Noeud?> Arbres1()
        {
            yield return new Noeud(1);
            yield return new Noeud(2);
        }
        public static IEnumerable<Noeud?> Arbres2()
        {
            yield return new Noeud(2, gauche: new Noeud(1));
            yield return new Noeud(1, droite: new Noeud(2));
        }
        public static IEnumerable<Noeud?> Arbres3()
        {
            yield return new Noeud(3, gauche: new Noeud(2, gauche : new Noeud(1)));
            yield return new Noeud(3, gauche: new Noeud(1, droite: new Noeud(2)));
            yield return new Noeud(2, gauche: new Noeud(1), droite: new Noeud(3));
            yield return new Noeud(1, droite: new Noeud(3, gauche: new Noeud(2)));
            yield return new Noeud(1, droite: new Noeud(2, droite: new Noeud(3)));
        }
        public static IEnumerable<Noeud?> ArbresTypiques()
        {
            {
                yield return new Noeud(3, gauche: new Noeud(2, gauche: new Noeud(1)), droite: new Noeud(4, droite: new Noeud(5)));
                yield return new Noeud(6, droite: new Noeud(3, gauche: new Noeud(4, droite: new Noeud(1)), droite: new Noeud(2, gauche: new Noeud(5))));
                yield return new Noeud(20, droite: new Noeud(25, gauche: new Noeud(21), droite: new Noeud(28)), gauche: new Noeud(5, gauche: new Noeud(3),
                    droite: new Noeud(12, droite: new Noeud(13), gauche: new Noeud(8, gauche: new Noeud(6)))));
                yield return new Noeud(15, droite: new Noeud(20, gauche: new Noeud(16, droite: new Noeud(17)), droite: new Noeud(21)), gauche: new Noeud(12, gauche: new Noeud(8, droite: new Noeud(10)),
                   droite: new Noeud(14, gauche: new Noeud(13))));
                yield return new Noeud(1, droite: new Noeud(3, droite: new Noeud(6, gauche: new Noeud(9))), gauche: new Noeud(2, gauche: new Noeud(4), droite: new Noeud(5, droite: new Noeud(8), gauche: new Noeud(7))));


            }
        }

        public static IEnumerable<SyntaxNode?> ArbresSyntaxiquesTypiques()
        {
            yield return new SyntaxNode("3");
            yield return new SyntaxNode("*", droite: new SyntaxNode("3"), gauche: new SyntaxNode("7"));
            yield return new SyntaxNode("+", droite: new SyntaxNode("7"), gauche: new SyntaxNode("*", droite: new SyntaxNode("9"), gauche: new SyntaxNode("+", droite: new SyntaxNode("3"), gauche: new SyntaxNode("2"))));
            yield return new SyntaxNode("+", droite: new SyntaxNode("*", droite: new SyntaxNode("2"), gauche: new SyntaxNode("+", droite: new SyntaxNode("9"), gauche: new SyntaxNode("5"))), gauche: new SyntaxNode("3"));
            yield return new SyntaxNode("*", droite: new SyntaxNode("+", droite: new SyntaxNode("5"), gauche: new SyntaxNode("-", droite: new SyntaxNode("4"), gauche: new SyntaxNode("8"))), gauche: new SyntaxNode("+", droite: new SyntaxNode("3"), gauche: new SyntaxNode("2")));

        }
        public static IEnumerable<SyntaxNode?> ArbresSyntaxiquesSpéciaux()
        {
            yield return null;
            yield return new SyntaxNode("deux");
            yield return new SyntaxNode("*", droite: new SyntaxNode("3"));
            yield return new SyntaxNode("+", gauche: new SyntaxNode("7"));
            yield return new SyntaxNode("%", droite: new SyntaxNode("4"), gauche: new SyntaxNode("17"));
            yield return new SyntaxNode("/", droite: new SyntaxNode("0"), gauche: new SyntaxNode("3"));
            yield return new SyntaxNode("/", droite: new SyntaxNode("0"), gauche: new SyntaxNode("0"));
        }

    }
}
