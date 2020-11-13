using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ArbreBinLib
{
    public static partial class ArbreBin<TKey, TValue>
        where TKey : notnull, IComparable<TKey>
    {

        public class Noeud
        {
            // -----Auto propriétés-----
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Noeud? Gauche { get; set; }
            public Noeud? Droite { get; set; }
            public Noeud? Parent { get; set; }

            // -----Constructeur-----
            public Noeud(
                TKey key,
                TValue value = default,
                Noeud? gauche = null,
                Noeud? droite = null)
            {
                Key = key;
                Value = value;
                Gauche = gauche;
                if (Gauche != null)
                    Gauche.Parent = this;
                Droite = droite;
                if (Droite != null)
                    Droite.Parent = this;
                Parent = null;
            }

            //-----Affichage-----
            public override string ToString()
            {
                if (Parent == null)
                {
                    if (Value == null || Value.Equals(0))
                    {
                        if (Droite == null && Gauche == null)
                            return ($"R[{Key}]");
                        else if (Droite != null && Gauche == null)
                            return ($"R[{Key}.]");
                        else if (Droite == null && Gauche != null)
                            return ($"R[.{Key}]");
                        else
                            return ($"R[.{Key}.]");
                    }
                    else 
                    {
                        if (Droite == null && Gauche == null)
                            return ($"R[{Key}|{Value}]");
                        else if (Droite != null && Gauche == null)
                            return ($"R[{Key}.|{Value}]");
                        else if (Droite == null && Gauche != null)
                            return ($"R[.{Key}|{Value}]");
                        else
                            return ($"R[.{Key}.|{Value}]");
                    }
                }
                else
                {
                    if (Value == null || Value.Equals(0))
                    {
                        if (Droite == null && Gauche == null)
                            return ($"[{Key}]");
                        else if (Droite != null && Gauche == null)
                            return ($"[{Key}.]");
                        else if (Droite == null && Gauche != null)
                            return ($"[.{Key}]");
                        else
                            return ($"[.{Key}.]");
                    }
                    else 
                    {
                        if (Droite == null && Gauche == null)
                            return ($"[{Key}|{Value}]");
                        else if (Droite != null && Gauche == null)
                            return ($"[{Key}.|{Value}]");
                        else if (Droite == null && Gauche != null)
                            return ($"[.{Key}|{Value}]");
                        else
                            return ($"[.{Key}.|{Value}]");
                    }
                }
            }

            //-----Propriétés calculables-----
            public EspèceDeNoeud Espèce
            {
                get { if (Gauche != null && Droite != null) 
                        return EspèceDeNoeud.Embranchement;
                      else if( Gauche != null && Droite == null)
                        return EspèceDeNoeud.TigeGauche;
                      else if (Gauche == null && Droite != null)
                        return EspèceDeNoeud.TigeDroite;
                      else return EspèceDeNoeud.Feuille;
                }
            }

        }
    }
}
