create FUNCTION [dbo].[ServicioDetalleSP](@IdiomaSigla varchar(10))
RETURNS TABLE
 AS
 RETURN
 SELECT sd.ServicioId,sd.ServicioDetalleId,e.EquivalenciaValor as ServicioDetalleDescripcion,
 sd.ServicioDetallePrecioUnitario,sd.ServicioDetalleCostoInicial,sd.ServicioDetalleCostoFinal
FROM[dbo].[Equivalencia] e with(nolock)
inner join Idioma i with(nolock)
on
e.IdiomaId=i.idiomaId
inner join Objeto o with(nolock)
on 
e.ObjetoId=o.ObjetoId
INNER JOIN [dbo].[ServicioDetalle] sd with(nolock)
ON e.EquivalenciaObjetoId=sd.servicioId 
and ObjetoNombre='ServicioDetalle'
and IdiomaSigla=@IdiomaSigla
