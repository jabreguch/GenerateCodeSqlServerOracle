using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	05/12/2018	Created
///</history>
public class BeServicioCalculo: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CoServicioCalculo")] public  int  CoServicioCalculo { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoEmpresa")] public  int  CoEmpresa { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoServicio")] public  int  CoServicio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoTipoPredio")] public  int  CoTipoPredio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoTipoCalculo")] public  int  CoTipoCalculo { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BeServicioCalculo() { Dispose(false); }
}