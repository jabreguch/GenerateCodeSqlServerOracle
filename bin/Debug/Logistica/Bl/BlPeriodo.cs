using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System;
using  Microsoft.AspNetCore.Mvc.Rendering;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19-11-2018	Created
///</history>
public class BlPeriodo: BlBase { 
DaPeriodo data; 
public int Add( BePeriodo c) { 
return data.Add (c);
}

public void Edit (BePeriodo c) {
data.Edit(c);
}

public void EditEst (BePeriodo c) {
 data.EditEst(c);
}

public void Del (BePeriodo c) {
data.Del(c);
}

public List<BePeriodo>  Gets (BePeriodo c) {
List<BePeriodo> r = new List<BePeriodo>(); 
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BePeriodo i = new BePeriodo();
i.CoPeriodo=dr.Num("CoPeriodo");
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.FePeriodo=dr.Text("FePeriodo");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BePeriodo Get (BePeriodo c) {
BePeriodo i = null;
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BePeriodo();
i.CoPeriodo=dr.Num("CoPeriodo");
i.CoCondominio=dr.Num("CoCondominio");
i.CoEdificio=dr.Num("CoEdificio");
i.FePeriodo=dr.Text("FePeriodo");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BlPeriodo()
{
data = new DaPeriodo();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DaPeriodo();
}
 disposed = true;
base.Dispose(disposing);
}

~BlPeriodo() { Dispose(false); }
}