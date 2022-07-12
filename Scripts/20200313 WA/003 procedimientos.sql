ALTER Procedure [dbo].[ObtenerImporte_y_Calificacion](@ServAsigId varchar(50))
as 
begin
SELECT sum(sac.ServAsigCostoValor)ImporteProveedor,p.PostCalificacion FROM ServAsig sa
inner join Post p
on sa.ServAsigId=p.ServAsigId
inner join ServAsigCosto sac
on 
sa.ServAsigId=sac.ServAsigId
and sac.ConceptoCostoId in (3,5)
and sa.ServAsigId=@ServAsigId
group by p.PostCalificacion
end


ALTER  procedure [dbo].[ValidarExistePersonaV2] 
 @PersonaCorreo varchar(100),
 @TipoLoginId decimal(12,2),
 @PersonaCodigoTelefono varchar(40),
 @PersonaTelefono varchar(40) 
as
begin

if ((@PersonaCorreo!='')or @PersonaCorreo is not null)
begin
select top 1 PersonaId from persona where PersonaCorreo=@PersonaCorreo
end



if (((@PersonaTelefono!='')or @PersonaTelefono is not null)and (@TipoLoginId=3))
begin
set @PersonaCodigoTelefono='+'+@PersonaCodigoTelefono
select top 1 PersonaId from persona where ( PersonaTelefono=@PersonaTelefono and PersonaCodigoTelefono=@PersonaCodigoTelefono and TipoLoginId=3)

end
else 
begin
if ((@PersonaTelefono!='')or @PersonaTelefono is not null)
select top 0 PersonaId  from Persona
end
end

ALTER  procedure [dbo].[ValidarExistePersonaV1] 
 @PersonaUID varchar(40) 
as
begin
select top 1 p.PersonaId from persona p
where ((p.personaFacebookUid=@PersonaUID) or (p.PersonaGmailUid=@PersonaUID) or  (p.PersonaPhoneUid=@PersonaUID))

end

