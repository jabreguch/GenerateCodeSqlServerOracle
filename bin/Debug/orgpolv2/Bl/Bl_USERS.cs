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
/// t[Jonatan Abregu]	19/11/2018	Created
///</history>
public class BL__USERS: BL_BASE { 
DA__USERS data; 
public int Add ( BE__USERS c) { 
return data.Add(c);
}

public void Edit (BE__USERS c) {
data.Edit(c);
}

public void EditEst (BE__USERS c) {
 data.EditEst(c);
}

public void Del (BE__USERS c) {
data.Del(c);
}

public List<BE__USERS>  Gets (BE__USERS c) {
List<BE__USERS> r = new List<BE__USERS>(); 
OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); 
OracleDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BE__USERS i = new BE__USERS();
i.CUENTA=dr.Text("CUENTA");
i.COD_OP=dr.Num("COD_OP");
i.X_USER_OLD_COUSUMOD=dr.Text("X_USER_OLD_COUSUMOD");
i.USERNAME=dr.Text("USERNAME");
i.IDUSERSDGU=dr.Num("IDUSERSDGU");
i.CLAVE=dr.Text("CLAVE");
i.IDUSERSROP=dr.Num("IDUSERSROP");
i.COD_ENTE=dr.Text("COD_ENTE");
i.AMBITO=dr.Text("AMBITO");
i.COPERFIL=dr.Num("COPERFIL");
i.COD_AREA=dr.Text("COD_AREA");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCerrarDr(cn, dr);
}
}

public BE__USERS Get (BE__USERS c) {
BE__USERS i = null;
OracleConnection cn = new OracleConnection(TX_CADENA_CONEXION); 
OracleDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BE__USERS();
i.CUENTA=dr.Text("CUENTA");
i.COD_OP=dr.Num("COD_OP");
i.X_USER_OLD_COUSUMOD=dr.Text("X_USER_OLD_COUSUMOD");
i.USERNAME=dr.Text("USERNAME");
i.IDUSERSDGU=dr.Num("IDUSERSDGU");
i.CLAVE=dr.Text("CLAVE");
i.IDUSERSROP=dr.Num("IDUSERSROP");
i.COD_ENTE=dr.Text("COD_ENTE");
i.AMBITO=dr.Text("AMBITO");
i.COPERFIL=dr.Num("COPERFIL");
i.COD_AREA=dr.Text("COD_AREA");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCerrarDr(cn, dr);
}
}

public BL__USERS()
{
data = new DA__USERS();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DA__USERS();
}
 disposed = true;
base.Dispose(disposing);
}

~BL__USERS() { Dispose(false); }
}