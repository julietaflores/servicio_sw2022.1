ALTER FUNCTION [dbo].[ServicioSP](@IdiomaSigla varchar(10))
RETURNS TABLE
 AS
 RETURN
 SELECT s.servicioId,e.EquivalenciaValor as ServicioNombre,s.servicioURLFoto,s.CategoriaServicioId,s.ServicioUsuario,s.ServicioFechaHoraMod,
 s.ServicioKeyWords,s.ServicioPorcentaje,s.ServicioTarifaMinima,s.ServicioDetalleTipo,s.ServicioSabado,s.ServicioDomingo,s.ServicioHorarioRegularIni,
 s.ServicioHorarioRegularFin,s.ServicioHorarioSabadoIni,s.ServicioHorarioSabadoFin,s.ServicioHorarioDomingoIni,s.ServicioHorarioDomingoFin,
 s.ServicioPersonaEnTurno,s.ServicioDetalleFormulario,s.TipoServicioId
FROM[dbo].[Equivalencia] e with(nolock)
inner join Idioma i with(nolock)
on
e.IdiomaId=i.idiomaId
inner join Objeto o with(nolock)
on 
e.ObjetoId=o.ObjetoId
INNER JOIN [dbo].[Servicio] s with(nolock)
ON e.EquivalenciaObjetoId=s.servicioId
and ObjetoNombre='Servicio'
and IdiomaSigla=@IdiomaSigla