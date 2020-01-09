using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	05/12/2018	Created
///</history>
public class DaServicioCalculo: DaBase {
public int Add (BeServicioCalculo c) {
SqlParameter[] pr = new SqlParameter[6];
pr[0] = new SqlParameter("CoServicioCalculo", SqlDbType.Int );
pr[0].Direction = ParameterDirection.InputOutput;
pr[0].Value = c.CoServicioCalculo;
pr[1] = new SqlParameter("CoEmpresa", SqlDbType.Int );
pr[1].Value = c.CoEmpresa;
pr[2] = new SqlParameter("CoServicio", SqlDbType.Int );
pr[2].Value = c.CoServicio;
pr[3] = new SqlParameter("CoTipoPredio", SqlDbType.Int );
pr[3].Value = c.CoTipoPredio;
pr[4] = new SqlParameter("CoTipoCalculo", SqlDbType.Int );
pr[4].Value = c.CoTipoCalculo;

pr[5] = new SqlParameter("CoUsuCre", SqlDbType.Int,4 );
pr[5].Value = ServicioCalculo.CoUsuCre;
CnHelper.EjecutarQR("ServiciocalculoAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BeServicioCalculo c) {
SqlParameter[] pr = new SqlParameter[6];
pr[0] = new SqlParameter("CoServicioCalculo", SqlDbType.Int );
pr[0].Value = c.CoServicioCalculo;
pr[1] = new SqlParameter("CoEmpresa", SqlDbType.Int );
pr[1].Value = c.CoEmpresa;
pr[2] = new SqlParameter("CoServicio", SqlDbType.Int );
pr[2].Value = c.CoServicio;
pr[3] = new SqlParameter("CoTipoPredio", SqlDbType.Int );
pr[3].Value = c.CoTipoPredio;
pr[4] = new SqlParameter("CoTipoCalculo", SqlDbType.Int );
pr[4].Value = c.CoTipoCalculo;

pr[5] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[5].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ServiciocalculoEdit", pr);
}

public void EditEst (BeServicioCalculo c) {
SqlParameter[] pr = new SqlParameter[3];
pr[0] = new SqlParameter("CoServicioCalculo", SqlDbType.Int );
pr[0].Value = c.CoServicioCalculo;

pr[01] = new SqlParameter("CoEstReg", SqlDbType.Int );
pr[01].Value = c.CoEstReg;
pr[02] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[02].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ServiciocalculoEditEst", pr);
}

public void Del (BeServicioCalculo c) {
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoServicioCalculo", SqlDbType.Int );
pr[0].Value = c.CoServicioCalculo;

pr[01] = new SqlParameter("CoUsuEli", SqlDbType.Int,4 );
pr[01].Value = c.CoUsuEli;
CnHelper.EjecutarQR("ServiciocalculoDel", pr);
}

public SqlDataReader Gets (SqlConnection cn, BeServicioCalculo c ) {
SqlParameter[] pr = new SqlParameter[1];
return CnHelper.ObtenerDR(cn,"ServiciocalculoGets", pr);
}

public SqlDataReader Get(SqlConnection cn, BeServicioCalculo c){
SqlParameter[] pr = new SqlParameter[2];
pr[0] = new SqlParameter("CoServicioCalculo", SqlDbType.Int );
pr[0].Value = c.CoServicioCalculo;

return CnHelper.ObtenerDR(cn, "ServiciocalculoGet", pr);
}

}