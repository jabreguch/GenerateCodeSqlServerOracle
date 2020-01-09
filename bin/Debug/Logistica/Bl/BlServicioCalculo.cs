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
/// t[Jonatan Abregu]	05/12/2018	Created
///</history>
public class BlServicioCalculo: BlBase { 
DaServicioCalculo data; 
public int Add( BeServicioCalculo c) { 
return data.Add (c);
}

public void Edit (BeServicioCalculo c) {
data.Edit(c);
}

public void EditEst (BeServicioCalculo c) {
 data.EditEst(c);
}

public void Del (BeServicioCalculo c) {
data.Del(c);
}

public List<BeServicioCalculo>  Gets (BeServicioCalculo c) {
List<BeServicioCalculo> r = new List<BeServicioCalculo>(); 
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BeServicioCalculo i = new BeServicioCalculo();
i.CoServicioCalculo=dr.Num("CoServicioCalculo");
i.CoEmpresa=dr.Num("CoEmpresa");
i.CoServicio=dr.Num("CoServicio");
i.CoTipoPredio=dr.Num("CoTipoPredio");
i.CoTipoCalculo=dr.Num("CoTipoCalculo");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BeServicioCalculo Get (BeServicioCalculo c) {
BeServicioCalculo i = null;
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BeServicioCalculo();
i.CoServicioCalculo=dr.Num("CoServicioCalculo");
i.CoEmpresa=dr.Num("CoEmpresa");
i.CoServicio=dr.Num("CoServicio");
i.CoTipoPredio=dr.Num("CoTipoPredio");
i.CoTipoCalculo=dr.Num("CoTipoCalculo");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BlServicioCalculo()
{
data = new DaServicioCalculo();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DaServicioCalculo();
}
 disposed = true;
base.Dispose(disposing);
}

~BlServicioCalculo() { Dispose(false); }
}