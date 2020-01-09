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
/// t[Jonatan Abregu]	12/12/2018	Created
///</history>
public class BlParametros: BlBase { 
DaParametros data; 
public int Add( BeParametros c) { 
return data.Add (c);
}

public void Edit (BeParametros c) {
data.Edit(c);
}

public void EditEst (BeParametros c) {
 data.EditEst(c);
}

public void Del (BeParametros c) {
data.Del(c);
}

public List<BeParametros>  Gets (BeParametros c) {
List<BeParametros> r = new List<BeParametros>(); 
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Gets(cn, c);
while (dr.Read()) {
BeParametros i = new BeParametros();
i.CoParametro=dr.Num("CoParametro");
i.CoEmpresa=dr.Num("CoEmpresa");
i.NoCampo=dr.Text("NoCampo");
i.NuOrden=dr.Num("NuOrden");
i.NoValor=dr.Text("NoValor");
i.NoNombre=dr.Text("NoNombre");
i.NoSigla=dr.Text("NoSigla");
r.Add(i);
}
return r;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BeParametros Get (BeParametros c) {
BeParametros i = null;
SqlConnection cn = new SqlConnection(TX_CADENA_CONEXION); 
SqlDataReader dr = null;
try {
dr = data.Get(cn, c);
if (dr.Read()) {
i = new BeParametros();
i.CoParametro=dr.Num("CoParametro");
i.CoEmpresa=dr.Num("CoEmpresa");
i.NoCampo=dr.Text("NoCampo");
i.NuOrden=dr.Num("NuOrden");
i.NoValor=dr.Text("NoValor");
i.NoNombre=dr.Text("NoNombre");
i.NoSigla=dr.Text("NoSigla");
}
return i;
} catch (Exception ex) {
throw ex;
} finally {
pCloseDR(cn, dr);
}
}

public BlParametros()
{
data = new DaParametros();
 }

//Destruir Objetos
bool disposed = false;
protected override void Dispose(bool disposing) {
if (disposed) return;
if (disposing) {
data = new DaParametros();
}
 disposed = true;
base.Dispose(disposing);
}

~BlParametros() { Dispose(false); }
}