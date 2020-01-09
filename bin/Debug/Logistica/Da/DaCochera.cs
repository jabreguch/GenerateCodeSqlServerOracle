using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19-11-2018	Created
///</history>
public class DaCochera: DaBase {
public int Add (BeCochera c) {
SqlParameter[] pr = new SqlParameter[7];
pr[0] = new SqlParameter("CoCochera", SqlDbType.Int );
pr[0].Direction = ParameterDirection.InputOutput;
pr[0].Value = c.CoCochera;
pr[1] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[1].Value = c.CoCondominio;
pr[2] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[2].Value = c.CoEdificio;
pr[3] = new SqlParameter("NuCochera", SqlDbType.VarChar,10 );
pr[3].Value = c.NuCochera;
pr[4] = new SqlParameter("NuArea", SqlDbType.Int );
pr[4].Value = c.NuArea;
pr[5] = new SqlParameter("NuPiso", SqlDbType.Int );
pr[5].Value = c.NuPiso;

pr[6] = new SqlParameter("CoUsuCre", SqlDbType.Int,4 );
pr[6].Value = Cochera.CoUsuCre;
CnHelper.EjecutarQR("CocheraAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BeCochera c) {
SqlParameter[] pr = new SqlParameter[7];
pr[0] = new SqlParameter("CoCochera", SqlDbType.Int );
pr[0].Value = c.CoCochera;
pr[1] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[1].Value = c.CoCondominio;
pr[2] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[2].Value = c.CoEdificio;
pr[3] = new SqlParameter("NuCochera", SqlDbType.VarChar,10 );
pr[3].Value = c.NuCochera;
pr[4] = new SqlParameter("NuArea", SqlDbType.Int );
pr[4].Value = c.NuArea;
pr[5] = new SqlParameter("NuPiso", SqlDbType.Int );
pr[5].Value = c.NuPiso;

pr[6] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[6].Value = c.CoUsuMod;
CnHelper.EjecutarQR("CocheraEdit", pr);
}

public void EditEst (BeCochera c) {
SqlParameter[] pr = new SqlParameter[3];
pr[0] = new SqlParameter("CoCochera", SqlDbType.Int );
pr[0].Value = c.CoCochera;

pr[01] = new SqlParameter("CoEstReg", SqlDbType.Int );
pr[01].Value = c.CoEstReg;
pr[02] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[02].Value = c.CoUsuMod;
CnHelper.EjecutarQR("CocheraEditEst", pr);
}

public void Del (BeCochera c) {
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoCochera", SqlDbType.Int );
pr[0].Value = c.CoCochera;

pr[01] = new SqlParameter("CoUsuEli", SqlDbType.Int,4 );
pr[01].Value = c.CoUsuEli;
CnHelper.EjecutarQR("CocheraDel", pr);
}

public SqlDataReader Gets (SqlConnection cn, BeCochera c ) {
SqlParameter[] pr = new SqlParameter[1];
return CnHelper.ObtenerDR(cn,"CocheraGets", pr);
}

public SqlDataReader Get(SqlConnection cn, BeCochera c){
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoCochera", SqlDbType.Int );
pr[0].Value = c.CoCochera;

return CnHelper.ObtenerDR(cn, "CocheraGet", pr);
}

}