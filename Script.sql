USE [EstadoCuenta_DB]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 5/4/2024 23:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[IdCompra] [int] IDENTITY(1,1) NOT NULL,
	[NumeroTarjeta] [varchar](13) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Descripcion] [varchar](300) NULL,
	[Monto] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatosConfigurables]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatosConfigurables](
	[IdDatosConfigurables] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[Valor] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDatosConfigurables] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatosTarjeta]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatosTarjeta](
	[NumeroTarjeta] [varchar](13) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[SaldoActual] [money] NOT NULL,
	[Limite] [money] NOT NULL,
	[SaldoDisponible] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[NumeroTarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[IdPago] [int] IDENTITY(1,1) NOT NULL,
	[NumeroTarjeta] [varchar](13) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Monto] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_ComprasTarjeta] FOREIGN KEY([NumeroTarjeta])
REFERENCES [dbo].[DatosTarjeta] ([NumeroTarjeta])
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_ComprasTarjeta]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_PagosTarjeta] FOREIGN KEY([NumeroTarjeta])
REFERENCES [dbo].[DatosTarjeta] ([NumeroTarjeta])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_PagosTarjeta]
GO
/****** Object:  StoredProcedure [dbo].[GuardarCompra]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GuardarCompra]
	-- Add the parameters for the stored procedure here
	@Monto money,
	@NumeroTarjeta varchar(13),
	@Descripcion varchar(200),
	@Fecha date
AS
BEGIN
	DECLARE @saldoDisponible money;

	SET @saldoDisponible = (select SaldoDisponible from DatosTarjeta)
	IF(@saldoDisponible >= @Monto) 
		INSERT INTO Compras (Monto, Descripcion, Fecha, NumeroTarjeta) values (@Monto, @Descripcion, @Fecha, @NumeroTarjeta)

		UPDATE DatosTarjeta set SaldoActual = SaldoActual + @Monto, SaldoDisponible = SaldoDisponible - @Monto where NumeroTarjeta = @NumeroTarjeta


END
GO
/****** Object:  StoredProcedure [dbo].[GuardarPago]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GuardarPago] 
	-- Add the parameters for the stored procedure here
	@Monto money,
	@NumeroTarjeta varchar(13),
	@Fecha date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO Pagos (Monto, Fecha, NumeroTarjeta) values (@Monto, @Fecha, @NumeroTarjeta)

	UPDATE DatosTarjeta set SaldoActual = SaldoActual - @Monto, SaldoDisponible = SaldoDisponible + @Monto where NumeroTarjeta = @NumeroTarjeta
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerInformacionTarjeta]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerInformacionTarjeta] 
	-- Add the parameters for the stored procedure here
	@NumeroTarjeta varchar(13)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @interes money;
	DECLARE @cuotaMinima money;
	DECLARE @TotalInteres money;

	DECLARE @interesConfigurable money;
	DECLARE @PorcentajeSaldoMinimo money;
	

	SET @interesConfigurable = (select valor from DatosConfigurables where Nombre = 'interes')
	SET @PorcentajeSaldoMinimo = (select valor from DatosConfigurables where Nombre = 'porcentajeSaldoMinimo')

	SET @interes = (select SaldoActual * @interesConfigurable from DatosTarjeta where NumeroTarjeta = @NumeroTarjeta)
    SET @cuotaMinima = (select SaldoActual * @PorcentajeSaldoMinimo from DatosTarjeta where NumeroTarjeta = @NumeroTarjeta)
	SET @TotalInteres = (select @interes + SaldoActual from DatosTarjeta where NumeroTarjeta = @NumeroTarjeta)


	SELECT @cuotaMinima as CuotaMinima, SaldoActual, @TotalInteres as TotalInteres, Nombre, NumeroTarjeta, Limite, SaldoDisponible from DatosTarjeta where NumeroTarjeta = @NumeroTarjeta


END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTransacciones]    Script Date: 5/4/2024 23:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerTransacciones]
	-- Add the parameters for the stored procedure here
	@NumeroTarjeta varchar(13),
	@Mes int
AS
BEGIN
	CREATE TABLE #Transacciones(
		Tipo varchar(1),
		Monto money,
		Descripcion varchar(300),
		Fecha date,
		NumeroTarjeta varchar(13)
	)
	INSERT INTO #Transacciones SELECT 'C', Monto, Descripcion, Fecha, NumeroTarjeta from Compras where MONTH(Fecha) = @Mes and NumeroTarjeta = @NumeroTarjeta;
	INSERT INTO #Transacciones SELECT 'P', Monto, 'Pago', Fecha, NumeroTarjeta from Pagos where MONTH(Fecha) = @Mes and NumeroTarjeta = @NumeroTarjeta;

	SELECT * FROM #Transacciones
	DROP table #Transacciones
END
GO
