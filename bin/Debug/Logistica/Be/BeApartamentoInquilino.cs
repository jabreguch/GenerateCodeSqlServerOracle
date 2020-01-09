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
public class BeApartamentoInquilino: BeBase {
[DataMember(EmitDefaultValue = false, Name = "CoCondominio")] public  int  CoCondominio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoEdificio")] public  int  CoEdificio { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoApartamento")] public  int  CoApartamento { get; set; }
[DataMember(EmitDefaultValue = false, Name = "CoInquilino")] public  int  CoInquilino { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FeIniContrato")] public  string  FeIniContrato { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FeFinContrato")] public  string  FeFinContrato { get; set; }
[DataMember(EmitDefaultValue = false, Name = "FeContrato")] public  string  FeContrato { get; set; }
[DataMember(EmitDefaultValue = false, Name = "BlContrato")] public  byte()  BlContrato { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoArchivo")] public  string  NoArchivo { get; set; }
[DataMember(EmitDefaultValue = false, Name = "NoArchivoExtencion")] public  string  NoArchivoExtencion { get; set; }
 bool disposed = false;
protected override void Dispose(bool disposing){
if (disposed) return;
if (disposing){
}
disposed = true;
}
~BeApartamentoInquilino() { Dispose(false); }
}