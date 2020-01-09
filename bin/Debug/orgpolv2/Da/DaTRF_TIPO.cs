using Oracle.DataAccess.Types;
using Oracle.DataAccess.Types;
using System.Data;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	04/12/2018	Created
///</history>
public class DA_TRF_TIPO: DA_BASE {
public int Add (BE_TRF_TIPO c) {
OracleParameter[] pr = new OracleParameter[9];
pr[0] = new OracleParameter("TXVALOR", OracleDbType.Varchar2,150 );
pr[0].Value = c.TXVALOR;
pr[1] = new OracleParameter("TXSIGLA", OracleDbType.Varchar2,5 );
pr[1].Value = c.TXSIGLA;
pr[2] = new OracleParameter("TXDESCRIPCION", OracleDbType.Varchar2,150 );
pr[2].Value = c.TXDESCRIPCION;
pr[3] = new OracleParameter("IDTIPOREF", OracleDbType.Int32 );
pr[3].Value = c.IDTIPOREF;
pr[4] = new OracleParameter("NUORDEN", OracleDbType.Int32 );
pr[4].Value = c.NUORDEN;
pr[5] = new OracleParameter("IDGRUPO", OracleDbType.Int32 );
pr[0].Direction = ParameterDirection.InputOutput;
pr[5].Value = c.IDGRUPO;
pr[6] = new OracleParameter("IDTIPO", OracleDbType.Int32 );
pr[1].Direction = ParameterDirection.InputOutput;
pr[6].Value = c.IDTIPO;
pr[7] = new OracleParameter("TXTIPO", OracleDbType.Varchar2,300 );
pr[2].Direction = ParameterDirection.InputOutput;
pr[7].Value = c.TXTIPO;

pr[8] = new OracleParameter("IDUsuCre", OracleDbType.Int32,4 );
pr[8].Value = TRF_TIPO.IDUsuCre;
CnHelper.EjecutarQR("TrftipoAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BE_TRF_TIPO c) {
OracleParameter[] pr = new OracleParameter[9];
pr[0] = new OracleParameter("TXVALOR", OracleDbType.Varchar2,150 );
pr[0].Value = c.TXVALOR;
pr[1] = new OracleParameter("TXSIGLA", OracleDbType.Varchar2,5 );
pr[1].Value = c.TXSIGLA;
pr[2] = new OracleParameter("TXDESCRIPCION", OracleDbType.Varchar2,150 );
pr[2].Value = c.TXDESCRIPCION;
pr[3] = new OracleParameter("IDTIPOREF", OracleDbType.Int32 );
pr[3].Value = c.IDTIPOREF;
pr[4] = new OracleParameter("NUORDEN", OracleDbType.Int32 );
pr[4].Value = c.NUORDEN;
pr[5] = new OracleParameter("IDGRUPO", OracleDbType.Int32 );
pr[5].Value = c.IDGRUPO;
pr[6] = new OracleParameter("IDTIPO", OracleDbType.Int32 );
pr[6].Value = c.IDTIPO;
pr[7] = new OracleParameter("TXTIPO", OracleDbType.Varchar2,300 );
pr[7].Value = c.TXTIPO;

pr[8] = new OracleParameter("IDUsuMod", OracleDbType.Int32,4 );
pr[8].Value = c.IDUsuMod;
CnHelper.EjecutarQR("TrftipoEdit", pr);
}

public void EditEst (BE_TRF_TIPO c) {
OracleParameter[] pr = new OracleParameter[5];
pr[0] = new OracleParameter("IDGRUPO", OracleDbType.Int32 );
pr[0].Value = c.IDGRUPO;
pr[1] = new OracleParameter("IDTIPO", OracleDbType.Int32 );
pr[1].Value = c.IDTIPO;
pr[2] = new OracleParameter("TXTIPO", OracleDbType.Varchar2,300 );
pr[2].Value = c.TXTIPO;

pr[21] = new OracleParameter("IDEstReg", OracleDbType.Int32 );
pr[21].Value = c.IDEstReg;
pr[22] = new OracleParameter("IDUsuMod", OracleDbType.Int32,4 );
pr[22].Value = c.IDUsuMod;
CnHelper.EjecutarQR("TrftipoEditEst", pr);
}

public void Del (BE_TRF_TIPO c) {
OracleParameter[] pr = new OracleParameter[4];
pr[0] = new OracleParameter("IDGRUPO", OracleDbType.Int32 );
pr[0].Value = c.IDGRUPO;
pr[1] = new OracleParameter("IDTIPO", OracleDbType.Int32 );
pr[1].Value = c.IDTIPO;
pr[2] = new OracleParameter("TXTIPO", OracleDbType.Varchar2,300 );
pr[2].Value = c.TXTIPO;

pr[21] = new OracleParameter("IDUsuEli", OracleDbType.Int32,4 );
pr[21].Value = c.IDUsuEli;
CnHelper.EjecutarQR("TrftipoDel", pr);
}

public OracleDataReader Gets (OracleConnection cn, BE_TRF_TIPO c ) {
OracleParameter[] pr = new OracleParameter[1];
return CnHelper.ObtenerDR(cn,"TrftipoGets", pr);
}

public OracleDataReader Get(OracleConnection cn, BE_TRF_TIPO c){
OracleParameter[] pr = new OracleParameter[4];
pr[0] = new OracleParameter("IDGRUPO", OracleDbType.Int32 );
pr[0].Value = c.IDGRUPO;
pr[1] = new OracleParameter("IDTIPO", OracleDbType.Int32 );
pr[1].Value = c.IDTIPO;
pr[2] = new OracleParameter("TXTIPO", OracleDbType.Varchar2,300 );
pr[2].Value = c.TXTIPO;

return CnHelper.ObtenerDR(cn, "TrftipoGet", pr);
}

}