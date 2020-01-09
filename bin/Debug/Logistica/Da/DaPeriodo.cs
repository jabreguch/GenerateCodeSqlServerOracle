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
public class DaPeriodo: DaBase {
public int Add (BePeriodo c) {
SqlParameter[] pr = new SqlParameter[5];
pr[0] = new SqlParameter("CoPeriodo", SqlDbType.Int );
pr[0].Direction = ParameterDirection.InputOutput;
pr[0].Value = c.CoPeriodo;
pr[1] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[1].Value = c.CoCondominio;
pr[2] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[2].Value = c.CoEdificio;
pr[3] = new SqlParameter("FePeriodo", SqlDbType.Char,8 );
pr[3].Value = c.FePeriodo;

pr[4] = new SqlParameter("CoUsuCre", SqlDbType.Int,4 );
pr[4].Value = Periodo.CoUsuCre;
CnHelper.EjecutarQR("PeriodoAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BePeriodo c) {
SqlParameter[] pr = new SqlParameter[5];
pr[0] = new SqlParameter("CoPeriodo", SqlDbType.Int );
pr[0].Value = c.CoPeriodo;
pr[1] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[1].Value = c.CoCondominio;
pr[2] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[2].Value = c.CoEdificio;
pr[3] = new SqlParameter("FePeriodo", SqlDbType.Char,8 );
pr[3].Value = c.FePeriodo;

pr[4] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[4].Value = c.CoUsuMod;
CnHelper.EjecutarQR("PeriodoEdit", pr);
}

public void EditEst (BePeriodo c) {
SqlParameter[] pr = new SqlParameter[3];
pr[0] = new SqlParameter("CoPeriodo", SqlDbType.Int );
pr[0].Value = c.CoPeriodo;

pr[01] = new SqlParameter("CoEstReg", SqlDbType.Int );
pr[01].Value = c.CoEstReg;
pr[02] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[02].Value = c.CoUsuMod;
CnHelper.EjecutarQR("PeriodoEditEst", pr);
}

public void Del (BePeriodo c) {
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoPeriodo", SqlDbType.Int );
pr[0].Value = c.CoPeriodo;

pr[01] = new SqlParameter("CoUsuEli", SqlDbType.Int,4 );
pr[01].Value = c.CoUsuEli;
CnHelper.EjecutarQR("PeriodoDel", pr);
}

public SqlDataReader Gets (SqlConnection cn, BePeriodo c ) {
SqlParameter[] pr = new SqlParameter[1];
return CnHelper.ObtenerDR(cn,"PeriodoGets", pr);
}

public SqlDataReader Get(SqlConnection cn, BePeriodo c){
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoPeriodo", SqlDbType.Int );
pr[0].Value = c.CoPeriodo;

return CnHelper.ObtenerDR(cn, "PeriodoGet", pr);
}

}