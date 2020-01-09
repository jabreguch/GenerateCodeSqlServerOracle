using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	12/12/2018	Created
///</history>
public class BeParametros: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CoParametro")] public  int  CoParametro { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoEmpresa")] public  int  CoEmpresa { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoCampo")] public  string  NoCampo { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NuOrden")] public  int  NuOrden { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoValor")] public  string  NoValor { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoNombre")] public  string  NoNombre { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoSigla")] public  string  NoSigla { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BeParametros() { Dispose(false); }
}