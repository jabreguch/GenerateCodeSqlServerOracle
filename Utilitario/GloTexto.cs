using System.Collections;
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
public static partial class Glo
{
    public static bool In<T>(this T obj, params T[] args)
    {
        return args.Contains(obj);
    }

    public static string Mayuscula(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }
        return Convert.ToString(XobjValue).Trim().ToUpper();
    }


    public static string Minuscula(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }
        return Convert.ToString(XobjValue).Trim().ToLower();

    }



    public static string NombresAltasYBajas(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }
        StringBuilder s = new StringBuilder();
        String[] cadenas = Convert.ToString(XobjValue).Minuscula().Split(Convert.ToChar(" "));
        for (int i = 0; i < cadenas.Length; i++)
        {
            if (cadenas[i].Length <= 2 || cadenas[i].Trim() == "del")
            {
                s.Append(string.Concat(cadenas[i].Trim(), " "));
            }
            else
            {
                s.Append(string.Concat(cadenas[i].Substring(0, 1).ToUpper(), cadenas[i].Substring(1).Trim(), " "));

            }
        }
        return s.ToString().Trim();
    }

    public static string PriLetraMayuscula(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }
        return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(XobjValue.Text().ToLower());

    }

    public static string SinAcento(this object XobjValue)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }

        string TX_BUSCA = (string)XobjValue;
        TX_BUSCA = TX_BUSCA.Replace("Á", "A");
        TX_BUSCA = TX_BUSCA.Replace("É", "E");
        TX_BUSCA = TX_BUSCA.Replace("Í", "I");
        TX_BUSCA = TX_BUSCA.Replace("Ó", "O");
        TX_BUSCA = TX_BUSCA.Replace("Ú", "U");
        //ÀÈÌÒÙ
        TX_BUSCA = TX_BUSCA.Replace("À", "A");
        TX_BUSCA = TX_BUSCA.Replace("È", "E");
        TX_BUSCA = TX_BUSCA.Replace("Ì", "I");
        TX_BUSCA = TX_BUSCA.Replace("Ò", "O");
        TX_BUSCA = TX_BUSCA.Replace("Ù", "U");

        TX_BUSCA = TX_BUSCA.Replace("á", "a");
        TX_BUSCA = TX_BUSCA.Replace("é", "e");
        TX_BUSCA = TX_BUSCA.Replace("í", "i");
        TX_BUSCA = TX_BUSCA.Replace("ó", "o");
        TX_BUSCA = TX_BUSCA.Replace("ú", "u");

        XobjValue = TX_BUSCA;
        return Convert.ToString(XobjValue).Trim();
    }



    public static string Text(this object XobjValue, int QT_CARACTER = 0)
    {
        if (XobjValue.NoNulo() == false)
        {
            return null;
        }


        if (QT_CARACTER > 0 & Convert.ToString(XobjValue).Length > QT_CARACTER)
        {
            XobjValue = Convert.ToString(XobjValue).Substring(0, QT_CARACTER) + "..";
        }

        return Convert.ToString(XobjValue).Trim();

    }



    public static string Text(this SqlParameter pr, string TXCOLUMNA)
    {
        string XobjValue = Text(pr.Value);
        return XobjValue;
    }

    public static string Text(this DateTime? pr, string TXFORMATO)
    {
        if (pr != null)
        {
            return pr.Value.ToString(TXFORMATO);

        }
        else { return null; }
    }

    public static string Text(this IDataReader dr, string TXCOLUMNA)
    {
        string XobjValue = Text(dr[TXCOLUMNA]);
        return XobjValue;
    }

    public static string Text(this SqlDataReader dr, string TXCOLUMNA)
    {
        string XobjValue = Text(dr[TXCOLUMNA]);
        return XobjValue;
    }


    public static string Text(this System.Data.DataRowView dr, string TXCOLUMNA)
    {
        string XobjValue = Text(dr[TXCOLUMNA]);
        return XobjValue;
    }

    public static string Text(this System.Data.DataRow dr, string TXCOLUMNA)
    {
        string XobjValue = Text(dr[TXCOLUMNA]);
        return XobjValue;
    }


    public static string Text(this IDictionary dr, string TXCOLUMNA)
    {
        string XobjValue = Text(dr[TXCOLUMNA]);
        return XobjValue;
    }

    public static string CerosIzquierda(this object obj, int QT_CEROS)
    {
        string tx = Text(obj);
        if (tx == null)
        {
            string temp = "";
            return string.Concat(temp.PadLeft(QT_CEROS, Convert.ToChar("0")), "0");
        }
        else
        {
            return tx.PadLeft(QT_CEROS, Convert.ToChar("0"));
        }
    }
}

