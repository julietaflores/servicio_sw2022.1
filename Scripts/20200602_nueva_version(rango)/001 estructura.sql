alter table servicio
add

	[ServicioDetalleTipo] [bit] NULL,
	[ServicioSabado] [bit] NULL,
	[ServicioDomingo] [bit] NULL,
	[ServicioHorarioRegularIni] [datetime] NULL,
	[ServicioHorarioRegularFin] [datetime] NULL,
	[ServicioHorarioSabadoIni] [datetime] NULL,
	[ServicioHorarioSabadoFin] [datetime] NULL,
	[ServicioHorarioDomingoIni] [datetime] NULL,
	[ServicioHorarioDomingoFin] [datetime] NULL,
	[ServicioPersonaEnTurno] [decimal](12, 0) NULL,
	[ServicioDetalleFormulario] [nvarchar](max) NULL
	go
	ALTER TABLE ServicioDetalle WITH CHECK ADD
 CONSTRAINT FK_ServicioDetalle_Servicio FOREIGN KEY (ServicioId) REFERENCES Servicio (ServicioId)

	
CREATE TABLE [dbo].[ServicioDetalle](
	[ServicioId] [decimal](12, 0) NOT NULL,
	[ServicioDetalleId] [decimal](12, 0) NOT NULL,
	[ServicioDetalleDescripcion] [varchar](100) NULL,
	[ServicioDetallePrecioUnitario] [int] NULL,
	[ServicioDetalleCostoInicial] [int] NULL,
	[ServicioDetalleCostoFinal] [int] NULL,
 CONSTRAINT [PK_ServicioDetalle] PRIMARY KEY CLUSTERED 
(
	[ServicioId] ASC,
	[ServicioDetalleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[RequiereServicioDetalle](
	[RequiereServicioId] [varchar](200) NOT NULL,
	[RequiereServicioDetalleId] [int] NOT NULL,
	[ServicioDetalleId] [decimal](12, 0) NULL,
	[ServicioDetalleCantidad] [int] NULL,
	[ServicioDetallePUFecha] [int] NULL,
	[ServicioDetalleDatos] [nvarchar](max) NULL,
 CONSTRAINT [PK_RequiereServicioDetalle] PRIMARY KEY CLUSTERED 
(
	[RequiereServicioId] ASC,
	[RequiereServicioDetalleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE RequiereServicioDetalle WITH CHECK ADD
 CONSTRAINT FK_RequiereServicioDetalle_RequiereServicio 
 FOREIGN KEY (RequiereServicioId) REFERENCES RequiereServicio  (RequiereServicioId)