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
public class DaApartamentoInquilino: DaBase {
public int Add (BeApartamentoInquilino c) {
SqlParameter[] pr = new SqlParameter[11];
pr[0] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[0].Direction = ParameterDirection.InputOutput;
pr[0].Value = c.CoCondominio;
pr[1] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[1].Direction = ParameterDirection.InputOutput;
pr[1].Value = c.CoEdificio;
pr[2] = new SqlParameter("CoApartamento", SqlDbType.Int );
pr[2].Direction = ParameterDirection.InputOutput;
pr[2].Value = c.CoApartamento;
pr[3] = new SqlParameter("CoInquilino", SqlDbType.Int );
pr[3].Direction = ParameterDirection.InputOutput;
pr[3].Value = c.CoInquilino;
pr[4] = new SqlParameter("FeIniContrato", SqlDbType.Char,8 );
pr[4].Value = c.FeIniContrato;
pr[5] = new SqlParameter("FeFinContrato", SqlDbType.Char,8 );
pr[5].Value = c.FeFinContrato;
pr[6] = new SqlParameter("FeContrato", SqlDbType.Char,8 );
pr[6].Value = c.FeContrato;
pr[7] = new SqlParameter("BlContrato", SqlDbType.Binary );
pr[7].Value = c.BlContrato;
pr[8] = new SqlParameter("NoArchivo", SqlDbType.VarChar,250 );
pr[8].Value = c.NoArchivo;
pr[9] = new SqlParameter("NoArchivoExtencion", SqlDbType.VarChar,5 );
pr[9].Value = c.NoArchivoExtencion;

pr[10] = new SqlParameter("CoUsuCre", SqlDbType.Int,4 );
pr[10].Value = ApartamentoInquilino.CoUsuCre;
CnHelper.EjecutarQR("ApartamentoinquilinoAdd", pr);
return (int)pr[0].Value;
}

public void Edit (BeApartamentoInquilino c) {
SqlParameter[] pr = new SqlParameter[11];
pr[0] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[0].Value = c.CoCondominio;
pr[1] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[1].Value = c.CoEdificio;
pr[2] = new SqlParameter("CoApartamento", SqlDbType.Int );
pr[2].Value = c.CoApartamento;
pr[3] = new SqlParameter("CoInquilino", SqlDbType.Int );
pr[3].Value = c.CoInquilino;
pr[4] = new SqlParameter("FeIniContrato", SqlDbType.Char,8 );
pr[4].Value = c.FeIniContrato;
pr[5] = new SqlParameter("FeFinContrato", SqlDbType.Char,8 );
pr[5].Value = c.FeFinContrato;
pr[6] = new SqlParameter("FeContrato", SqlDbType.Char,8 );
pr[6].Value = c.FeContrato;
pr[7] = new SqlParameter("BlContrato", SqlDbType.Binary );
pr[7].Value = c.BlContrato;
pr[8] = new SqlParameter("NoArchivo", SqlDbType.VarChar,250 );
pr[8].Value = c.NoArchivo;
pr[9] = new SqlParameter("NoArchivoExtencion", SqlDbType.VarChar,5 );
pr[9].Value = c.NoArchivoExtencion;

pr[10] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[10].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ApartamentoinquilinoEdit", pr);
}

public void EditEst (BeApartamentoInquilino c) {
SqlParameter[] pr = new SqlParameter[6];
pr[0] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[0].Value = c.CoCondominio;
pr[1] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[1].Value = c.CoEdificio;
pr[2] = new SqlParameter("CoApartamento", SqlDbType.Int );
pr[2].Value = c.CoApartamento;
pr[3] = new SqlParameter("CoInquilino", SqlDbType.Int );
pr[3].Value = c.CoInquilino;

pr[31] = new SqlParameter("CoEstReg", SqlDbType.Int );
pr[31].Value = c.CoEstReg;
pr[32] = new SqlParameter("CoUsuMod", SqlDbType.Int,4 );
pr[32].Value = c.CoUsuMod;
CnHelper.EjecutarQR("ApartamentoinquilinoEditEst", pr);
}

public void Del (BeApartamentoInquilino c) {
SqlParameter[] pr = new SqlParameter[5];
pr[0] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[0].Value = c.CoCondominio;
pr[1] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[1].Value = c.CoEdificio;
pr[2] = new SqlParameter("CoApartamento", SqlDbType.Int );
pr[2].Value = c.CoApartamento;
pr[3] = new SqlParameter("CoInquilino", SqlDbType.Int );
pr[3].Value = c.CoInquilino;

pr[31] = new SqlParameter("CoUsuEli", SqlDbType.Int,4 );
pr[31].Value = c.CoUsuEli;
CnHelper.EjecutarQR("ApartamentoinquilinoDel", pr);
}

public SqlDataReader Gets (SqlConnection cn, BeApartamentoInquilino c ) {
SqlParameter[] pr = new SqlParameter[1];
return CnHelper.ObtenerDR(cn,"ApartamentoinquilinoGets", pr);
}

public SqlDataReader Get(SqlConnection cn, BeApartamentoInquilino c){
SqlParameter[] pr = new SqlParameter[5];
pr[0] = new SqlParameter("CoCondominio", SqlDbType.Int );
pr[0].Value = c.CoCondominio;
pr[1] = new SqlParameter("CoEdificio", SqlDbType.Int );
pr[1].Value = c.CoEdificio;
pr[2] = new SqlParameter("CoApartamento", SqlDbType.Int );
pr[2].Value = c.CoApartamento;
pr[3] = new SqlParameter("CoInquilino", SqlDbType.Int );
pr[3].Value = c.CoInquilino;

return CnHelper.ObtenerDR(cn, "ApartamentoinquilinoGet", pr);
}

}