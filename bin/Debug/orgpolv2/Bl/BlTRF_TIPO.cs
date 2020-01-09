using System.Collections.Generic;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Web.Mvc;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	04/12/2018	Created
///</history>
public class BL_TRF_TIPO: BL_BASE { 
DA_TRF_TIPO data; 
public int Add ( BE_TRF_TIPO c) { 
return data.Add(c);
}

public void Edit (BE_TRF_TIPO c) {
data.Edit(c);
}

public void EditEst (BE_TRF_TIPO c) {
 data.EditEst(c);
}

public void Del (BE_TRF_TIPO c) {
data.Del(c);
}

public List<BE_TRF_TIPO>  Gets (BE_TRF_TIPO c) {
List<BE_TRF_TIPO> r = new List<BE_TRF_TIPO>(); 
OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); 
OracleDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BE_TRF_TIPO i = new BE_TRF_TIPO();
i.TXVALOR=dr.Text("TXVALOR");
i.TXSIGLA=dr.Text("TXSIGLA");
i.TXDESCRIPCION=dr.Text("TXDESCRIPCION");
i.IDTIPOREF=dr.Num("IDTIPOREF");
i.NUORDEN=dr.Num("NUORDEN");
i.IDGRUPO=dr.Num("IDGRUPO");
i.IDTIPO=dr.Num("IDTIPO");
i.TXTIPO=dr.Text("TXTIPO");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCerrarDr(cn, dr);
}
}

public BE_TRF_TIPO Get (BE_TRF_TIPO c) {
BE_TRF_TIPO i = null;
OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); 
OracleDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BE_TRF_TIPO();
i.TXVALOR=dr.Text("TXVALOR");
i.TXSIGLA=dr.Text("TXSIGLA");
i.TXDESCRIPCION=dr.Text("TXDESCRIPCION");
i.IDTIPOREF=dr.Num("IDTIPOREF");
i.NUORDEN=dr.Num("NUORDEN");
i.IDGRUPO=dr.Num("IDGRUPO");
i.IDTIPO=dr.Num("IDTIPO");
i.TXTIPO=dr.Text("TXTIPO");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCerrarDr(cn, dr);
}
}

public BL_TRF_TIPO()
{
data = new DA_TRF_TIPO();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DA_TRF_TIPO();
}
 disposed = true;
base.Dispose(disposing);
}

~BL_TRF_TIPO() { Dispose(false); }
}