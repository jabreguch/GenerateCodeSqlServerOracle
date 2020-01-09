using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	19-11-2018	Created
///</history>
public class BePeriodo: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CoPeriodo")] public  int  CoPeriodo { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoCondominio")] public  int  CoCondominio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoEdificio")] public  int  CoEdificio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FePeriodo")] public  string  FePeriodo { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BePeriodo() { Dispose(false); }
}