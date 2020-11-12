// Pour étudiants, phase 1
// Ne pas modifier

using cstjean.info.fg.utilitaire.extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArbreBinLib
{
    public static class Graphique
    {
        public static bool ShowDefaultValues { get; set; } = false;

        /// <summary>
        /// Représentation d'une arborescence sous forme d'une string d'une seule ligne.
        /// </summary>
        /// <param name="arbre">Arbre</param>
        /// <param name="estRacine">Champ privé, ne pas utiliser</param>
        /// <returns>Représentation uniligne</returns>
        public static string 
            ToString1D<K,V>(this ArbreBin<K,V>.Noeud? arbre, bool estRacine = true)
            where K: notnull, IComparable<K>
        {
            if (arbre == null) 
                return estRacine ? "null" : "";
            string mayBeValue = ShowDefaultValues || !Equals(arbre.Value, default(V)) ? "|" + arbre.Value : "";
            return string.Format("({0}{1}{2}{3})",
                ToString1D(arbre.Gauche, false),
                arbre.Key,
                mayBeValue,
                ToString1D(arbre.Droite, false)
            ); 
        }


        /// <summary>
        ///     Donne une représentation 2D d'un arbre binaire
        /// </summary>
        /// <param name="arbre">L'arbre binaire a représenter</param>
        /// <param name="largeurMax">Facultatif: Largeur maximale à utiliser</param>
        /// <param name="utf8">Vrai pour utiliser les caractères utf8, sinon ASCII</param>
        /// <param name="profondeurMax">Profondeur maximale de l'arbre à afficher</param>
        /// <param name="largeurNoeud">Largeur d'un noeud (typiquement 3 à 7)</param>
        /// <param name="marge">Marge horizontale à ajouter devant et derrière</param>
        /// <returns>String multilignes</returns>
        /// 
        public static string ToString2D<K,V>(
            this ArbreBin<K, V>.Noeud? arbre, 
            int? largeurMax = null, 
            bool utf8 = true, 
            int? profondeurMax = null, 
            int? largeurNoeud = null,
            int? marge = null
        )
            where K : notnull, IComparable<K>
        {
            if (arbre is null) return "";

            // Hauteur et largeur du canevas
            int hauteurCan = _hauteur(arbre) * 2;
            int largeurCan = (int)Math.Min(Math.Pow(2, hauteurCan - 1) * 7, 1000);
            
            // Canevas initialisé avec des espaces
            var canevas = new char[hauteurCan, largeurCan];
            for (int i = 0; i < hauteurCan; i++)
                for (int j = 0; j < largeurCan; j++)
                    canevas[i, j] = ' ';

            // Dessiner l'arbre dans le canevas
            int width = largeurNoeud ?? 3;
            int offset = marge ?? width;
            int maxDepth = profondeurMax ?? int.MaxValue;
            var largeurDessin = _print_t(canevas, arbre, false, offset, 0, utf8, maxDepth, width);

            // Convertir le dessin en string
            var largeurString = Math.Min(largeurDessin + 2*offset, largeurMax ?? int.MaxValue);
            return _toString(canevas, hauteurCan, largeurString);

            // --------------------------------------------

            static string _toString(char[,] canevas, int hauteurStr, int largeurStr)
            {
                var dessin = new StringBuilder();
                for (int i = 0; i < hauteurStr; i++)
                {
                    var lineB = new StringBuilder();
                    for (int j = 0; j < largeurStr; j++)
                    {
                        _ = lineB.Append(canevas[i, j]);
                    }
                    string line = lineB.ToString().TrimEnd();
                    if (line != "")
                        _ = dessin.AppendLine(line);
                }
                return dessin.ToString();
            }

            static int _hauteur(ArbreBin<K, V>.Noeud? arbre)
            {
                if (arbre == null)
                    return 0;
                return 1 + Math.Max(_hauteur(arbre.Gauche), _hauteur(arbre.Droite));
            }

            static int _print_t(char[,] s, ArbreBin<K, V>.Noeud? tree, bool is_left, int offset, int depth, bool utf8, int maxDepth = int.MaxValue, int width = 3)
            {

                if (tree == null || depth > maxDepth)
                    return 0;

                int largeurCan = s.GetLength(1);
  
                var (bar, downRight, downLeft, upRight, upLeft, upLeftRight) = utf8 
                    ? ('━', '┏', '┓', '┗', '┛', '┻')
                    : ('-', '+', '+', '+', '+', '+');
                string mayBeValue = ShowDefaultValues || !Equals(tree.Value, default(V)) ? "|" + tree.Value : "";
                string keyval = $" {tree.Key}{mayBeValue} ";

                int left = _print_t(s, tree.Gauche, true, offset, depth + 1, utf8, maxDepth, width);
                int right = _print_t(s, tree.Droite, false, offset + left + width, depth + 1, utf8, maxDepth, width);

                if (depth < maxDepth)
                {
                    for (int i = 0; i < keyval.Length; i++)
                        s[2 * depth, Math.Max(0, Math.Min(largeurCan - 1, offset + left + i + width/2 - keyval.Length/2))] = keyval[i];
                }

                if (depth > 0 && is_left)
                {
                    for (int i = 0; i < width + right; i++)
                    {
                        s[2 * depth - 1, offset + left + width / 2 + i] = bar;
                    }

                    s[2 * depth - 1, offset + left + width / 2] = downRight;
                    s[2 * depth - 1, offset + left + width + right + width / 2] = upLeft;
                }
                else if (depth > 0 && !is_left)
                {
                    for (int i = 0; i < left + width; i++)
                        s[2 * depth - 1, offset - width / 2 + i] = bar;

                    s[2 * depth - 1, offset + left + width / 2] = downLeft;
                    ref var startLoc = ref s[2 * depth - 1, offset - width / 2 - 1];
                    startLoc = startLoc == upLeft ? upLeftRight : upRight;
                }

                return left + width + right;
            }
        }

        static public string Lister<K,V>(this ArbreBin<K, V>.Noeud? arbre, Action<ArbreBin<K, V>.Noeud?, Action<ArbreBin<K, V>.Noeud>> parcourir)
            where K : notnull, IComparable<K>
        {
            var liste = new List<ArbreBin<K, V>.Noeud>();
            parcourir(arbre, n => liste.Add(n));
            return StrKeys(liste);
        }

        static public string StrKeys<K,V>(this IEnumerable<ArbreBin<K, V>.Noeud> items)
            where K : notnull, IComparable<K>
        => items.Select(n => n.Key).EnTexte().Raccourcir(70);
    }
}
