using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Web;
using System.Reflection;
using System.Data.SqlClient;

public static partial class Glo
{


    public static bool EsNulo(this object XobjValue)
    {
        if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
        {
            return true;
        }
        else if (XobjValue == null)
        {
            return true;
        }
        else if (XobjValue.ToString().Trim().Equals(""))
        {
            return true;
        }
        return false;
    }
    public static bool EsNulo(this string XobjValue)
    {
        if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
        {
            return true;
        }
        else if (XobjValue == null)
        {
            return true;
        }
        else if (XobjValue.ToString().Trim().Equals(""))
        {
            return true;
        }
        return false;
    }

    public static bool NoNulo(this object XobjValue)
    {
        if (object.ReferenceEquals(XobjValue, System.DBNull.Value))
        {
            return false;
        }
        else if (XobjValue == null)
        {
            return false;
        }
        else if (XobjValue.ToString().Trim().Equals(""))
        {
            return false;
        }
        return true;
    }



    public static bool NoNulo(this SqlDataReader dr, string TXCOLUMNA)
    {
        return NoNulo(dr.Text(TXCOLUMNA));
    }







}

