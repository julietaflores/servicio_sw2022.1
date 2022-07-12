--alter table dbo.movitem
--add VentaxLote bit null
--go
go
alter table persona
	add [PersonaCorreoValidado] [bit] NULL
go
alter table persona
add
	[PersonaCodigoVerificacion] [varchar](5) NULL
go
alter table persona
add
	[PersonaFacebookUid] [varchar](200) NULL
	go
alter table persona
add
	[PersonaGmailUid] [varchar](200) NULL
	go
alter table persona
add
	[PersonaPhoneUid] [varchar](200) NULL
	go
alter table persona
add
	[PersonaContrasenaActualizada] [bit] NULL
	go
alter table persona
add
	[PersonaIdioma] [varchar](50) NULL
	
	
go
alter table LogSesionesPersona
add	[LogSesionesPersonaVersion] [varchar](200) NULL
go
alter table LogSesionesPersona
add	[LogSesionesPersonaSO] [varchar](200) NULL
go
alter table LogSesionesPersona
add	[LogSesionesPersonaIdioma] [varchar](200) NULL.