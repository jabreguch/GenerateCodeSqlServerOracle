using System.Collections;
using System;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

public static partial class Glo
{

    public static decimal Deci(this IDataReader dr, string TXCOLUMNA)
    {
        decimal XobjValue = Deci(dr[TXCOLUMNA]);
        return XobjValue;
    }

    private static int Num(this object XobjValue)
    {
        if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
        {
            return 0;
        }
        else if (XobjValue == null)
        {
            return 0;
        }
        else if (XobjValue.ToString().Trim().Equals(""))
        {
            return 0;
        }
        return Convert.ToInt32(XobjValue);
    }


    public static decimal Deci(this object XobjValue)
    {
        if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
        {
            return 0;
        }
        else if (XobjValue == null)
        {
            return 0;
        }
        else if (XobjValue.ToString().Trim().Equals(""))
        {
            return 0;
        }
        CultureInfo r = new CultureInfo("es-PE");
        r.NumberFormat.CurrencyDecimalSeparator = ".";
        r.NumberFormat.CurrencyGroupSeparator = ".";
        r.NumberFormat.NumberDecimalSeparator = ".";
        r.NumberFormat.PercentDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = r;
        decimal val = 0;
        Decimal.TryParse(XobjValue.ToString(), out val);
        return val;
        //return (decimal)XobjValue;
    }



    public static int Num(this SqlParameter pr)
    {
        int XobjValue = Numero(pr.Value);
        return XobjValue;
    }

    public static int Num(this IDataReader dr, string TXCOLUMNA)
    {
        int XobjValue = Numero(dr[TXCOLUMNA]);
        return XobjValue;
    }
    public static int Num(this SqlDataReader dr, string TXCOLUMNA)
    {
        int XobjValue = Numero(dr[TXCOLUMNA]);
        return XobjValue;
    }

    public static int Num(this System.Data.DataRowView dr, string TXCOLUMNA)
    {
        int XobjValue = Numero(dr[TXCOLUMNA]);
        return XobjValue;
    }

    public static int Num(this System.Data.DataRow dr, string TXCOLUMNA)
    {
        int XobjValue = Numero(dr[TXCOLUMNA]);
        return XobjValue;
    }

  

    public static int Num(this IDictionary dr, string TXCOLUMNA)
    {
        int XobjValue = Numero(dr[TXCOLUMNA]);
        return XobjValue;
    }

  

    public static int Num(this string tx)
    {
        int XobjValue = Numero(tx);
        return XobjValue;
    }

    private static int Numero(object obj)
    {
        if (obj == System.DBNull.Value) { return 0; }
        else if (obj == null) { return 0; }
        else if (obj.ToString().Trim().Equals("")) { return 0; }
        if (obj.GetType().BaseType.FullName == "System.Enum") { return Convert.ToInt32(obj); }
        return int.Parse(obj.ToString());
    }


}

