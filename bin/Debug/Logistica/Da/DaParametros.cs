using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	12/12/2018	Created
///</history>
public class DaParametros: DaBase {
public int Add (BeParametros c) {
SqlParameter[] pr = new SqlParameter[8];
pr[0] = new SqlParameter("CoParametro", SqlDbType.Int );
pr[0].Value = c.CoParametro;
pr[1] = new SqlParameter("CoEmpresa", SqlDbType.Int );
pr[1].Value = c.CoEmpresa;
pr[2] = new SqlParameter("NoCampo", SqlDbType.VarChar,80 );
pr[0].Direction = ParameterDirection.InputOutput;
pr[2].Value = c.NoCampo;
pr[3] = new SqlParameter("NuOrden", SqlDbType.Int );
pr[3].Value = c.NuOrden;
pr[4] = new SqlParameter("NoValor", SqlDbType.VarChar,50 );
pr[4].Value = c.NoValor;
pr[5] = new SqlParameter("NoNombre", SqlDbType.VarChar,200 );
pr[5].Value = c.NoNombre;
pr[6] = new SqlParameter("NoSigla", SqlDbType.VarChar,15 );
pr[6].Value = c.NoSigla;

pr[7] = new SqlParameter("CoUsuCre", SqlDbType.Int,4 );
pr[7].Value = Parametros.CoUsuCre;
CnHelper.EjecutarQR("ParametrosAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BeParametros c) {
SqlParameter[] pr = new SqlParameter[8];
pr[0] = new SqlParameter("CoParametro", SqlDbType.Int );
pr[0].Value = c.CoParametro;
pr[1] = new SqlParameter("CoEmpresa", SqlDbType.Int );
pr[1].Value = c.CoEmpresa;
pr[2] = new SqlParameter("NoCampo", SqlDbType.VarChar,80 );
pr[2].Value = c.NoCampo;
pr[3] = new SqlParameter("NuOrden", SqlDbType.Int );
pr[3].Value = c.NuOrden;
pr[4] = new SqlParameter("NoValor", SqlDbType.VarChar,50 );
pr[4].Value = c.NoValor;
pr[5] = new SqlParameter("NoNombre", SqlDbType.VarChar,200 );
pr[5].Value = c.NoNombre;
pr[6] = new SqlParameter("NoSigla", SqlDbType.VarChar,15 );
pr[6].Value = c.NoSigla;

pr[7] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[7].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ParametrosEdit", pr);
}

public void EditEst (BeParametros c) {
SqlParameter[] pr = new SqlParameter[3];
pr[0] = new SqlParameter("NoCampo", SqlDbType.VarChar,80 );
pr[0].Value = c.NoCampo;

pr[01] = new SqlParameter("CoEstReg", SqlDbType.Int );
pr[01].Value = c.CoEstReg;
pr[02] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[02].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ParametrosEditEst", pr);
}

public void Del (BeParametros c) {
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("NoCampo", SqlDbType.VarChar,80 );
pr[0].Value = c.NoCampo;

pr[01] = new SqlParameter("CoUsuEli", SqlDbType.Int,4 );
pr[01].Value = c.CoUsuEli;
CnHelper.EjecutarQR("ParametrosDel", pr);
}

public SqlDataReader Gets (SqlConnection cn, BeParametros c ) {
SqlParameter[] pr = new SqlParameter[1];
return CnHelper.ObtenerDR(cn,"ParametrosGets", pr);
}

public SqlDataReader Get(SqlConnection cn, BeParametros c){
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("NoCampo", SqlDbType.VarChar,80 );
pr[0].Value = c.NoCampo;

return CnHelper.ObtenerDR(cn, "ParametrosGet", pr);
}

}