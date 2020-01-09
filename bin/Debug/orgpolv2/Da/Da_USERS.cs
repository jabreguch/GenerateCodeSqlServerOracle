using Oracle.DataAccess.Types;
using Oracle.DataAccess.Types;
using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19/11/2018	Created
///</history>
public class DA__USERS: DA_BASE {
public int Add (BE__USERS c) {
OracleParameter[] pr = new OracleParameter[14];
pr[0] = new OracleParameter("CUENTA", OracleDbType.Varchar2,50 );
pr[0].Value = c.CUENTA;
pr[1] = new OracleParameter("COD_OP", OracleDbType.Int32 );
pr[1].Value = c.COD_OP;
pr[2] = new OracleParameter("X_USER_OLD_COUSUMOD", OracleDbType.Varchar2,50 );
pr[2].Value = c.X_USER_OLD_COUSUMOD;
pr[3] = new OracleParameter("FEC_VEN_CTA", OracleDbType.Date );
pr[3].Value = c.FEC_VEN_CTA;
pr[4] = new OracleParameter("USERNAME", OracleDbType.Varchar2,100 );
pr[4].Value = c.USERNAME;
pr[5] = new OracleParameter("IDUSERSDGU", OracleDbType.Int32 );
pr[5].Value = c.IDUSERSDGU;
pr[6] = new OracleParameter("CLAVE", OracleDbType.Varchar2,50 );
pr[6].Value = c.CLAVE;
pr[7] = new OracleParameter("IDUSERSROP", OracleDbType.Int32 );
pr[0].Direction = ParameterDirection.InputOutput;
pr[7].Value = c.IDUSERSROP;
pr[8] = new OracleParameter("COD_ENTE", OracleDbType.Varchar2,4 );
pr[8].Value = c.COD_ENTE;
pr[9] = new OracleParameter("FEC_VEN_PSW", OracleDbType.Date );
pr[9].Value = c.FEC_VEN_PSW;
pr[10] = new OracleParameter("AMBITO", OracleDbType.Varchar2,6 );
pr[10].Value = c.AMBITO;
pr[11] = new OracleParameter("COPERFIL", OracleDbType.Int32 );
pr[11].Value = c.COPERFIL;
pr[12] = new OracleParameter("COD_AREA", OracleDbType.Varchar2,4 );
pr[12].Value = c.COD_AREA;

pr[13] = new OracleParameter("CoUsuCre", OracleDbType.Int32,4 );
pr[13].Value = _USERS.CoUsuCre;
CnHelper.EjecutarQR("UsersAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BE__USERS c) {
OracleParameter[] pr = new OracleParameter[14];
pr[0] = new OracleParameter("CUENTA", OracleDbType.Varchar2,50 );
pr[0].Value = c.CUENTA;
pr[1] = new OracleParameter("COD_OP", OracleDbType.Int32 );
pr[1].Value = c.COD_OP;
pr[2] = new OracleParameter("X_USER_OLD_COUSUMOD", OracleDbType.Varchar2,50 );
pr[2].Value = c.X_USER_OLD_COUSUMOD;
pr[3] = new OracleParameter("FEC_VEN_CTA", OracleDbType.Date );
pr[3].Value = c.FEC_VEN_CTA;
pr[4] = new OracleParameter("USERNAME", OracleDbType.Varchar2,100 );
pr[4].Value = c.USERNAME;
pr[5] = new OracleParameter("IDUSERSDGU", OracleDbType.Int32 );
pr[5].Value = c.IDUSERSDGU;
pr[6] = new OracleParameter("CLAVE", OracleDbType.Varchar2,50 );
pr[6].Value = c.CLAVE;
pr[7] = new OracleParameter("IDUSERSROP", OracleDbType.Int32 );
pr[7].Value = c.IDUSERSROP;
pr[8] = new OracleParameter("COD_ENTE", OracleDbType.Varchar2,4 );
pr[8].Value = c.COD_ENTE;
pr[9] = new OracleParameter("FEC_VEN_PSW", OracleDbType.Date );
pr[9].Value = c.FEC_VEN_PSW;
pr[10] = new OracleParameter("AMBITO", OracleDbType.Varchar2,6 );
pr[10].Value = c.AMBITO;
pr[11] = new OracleParameter("COPERFIL", OracleDbType.Int32 );
pr[11].Value = c.COPERFIL;
pr[12] = new OracleParameter("COD_AREA", OracleDbType.Varchar2,4 );
pr[12].Value = c.COD_AREA;

pr[13] = new OracleParameter("CoUsuMod", OracleDbType.Int32,4 );
pr[13].Value = c.CoUsuMod;
CnHelper.EjecutarQR("UsersEdit", pr);
}

public void EditEst (BE__USERS c) {
OracleParameter[] pr = new OracleParameter[3];
pr[0] = new OracleParameter("IDUSERSROP", OracleDbType.Int32 );
pr[0].Value = c.IDUSERSROP;

pr[01] = new OracleParameter("CoEstReg", OracleDbType.Int32 );
pr[01].Value = c.CoEstReg;
pr[02] = new OracleParameter("CoUsuMod", OracleDbType.Int32,4 );
pr[02].Value = c.CoUsuMod;
CnHelper.EjecutarQR("UsersEditEst", pr);
}

public void Del (BE__USERS c) {
OracleParameter[] pr = new OracleParameter[2];
pr[0] = new OracleParameter("IDUSERSROP", OracleDbType.Int32 );
pr[0].Value = c.IDUSERSROP;

pr[01] = new OracleParameter("CoUsuEli", OracleDbType.Int32,4 );
pr[01].Value = c.CoUsuEli;
CnHelper.EjecutarQR("UsersDel", pr);
}

public OracleDataReader Gets (OracleConnection cn, BE__USERS c ) {
OracleParameter[] pr = new OracleParameter[1];
return CnHelper.ObtenerDR(cn,"UsersGets", pr);
}

public OracleDataReader Get(OracleConnection cn, BE__USERS c){
OracleParameter[] pr = new OracleParameter[2];
pr[0] = new OracleParameter("IDUSERSROP", OracleDbType.Int32 );
pr[0].Value = c.IDUSERSROP;

return CnHelper.ObtenerDR(cn, "UsersGet", pr);
}

}