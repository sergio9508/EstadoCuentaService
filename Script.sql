CREATE DATABASE EstadoCuenta_DB;

USE EstadoCuenta_DB;
GO


CREATE TABLE DatosTarjeta(
	NumeroTarjeta varchar(13) NOT NULL PRIMARY KEY,
	Nombre varchar(300) NOT NULL,
	SaldoActual money NOT NULL,
	Limite money NOT NULL,
	SaldoDisponible money NULL,
	) 
GO
CREATE TABLE Compras(
	IdCompra int IDENTITY(1,1) NOT NULL ,
	NumeroTarjeta varchar(13) NOT NULL,
	Fecha date NOT NULL,
	Descripcion varchar(300) NULL,
	Monto money NOT NULL,
	PRIMARY KEY(IdCompra),
	CONSTRAINT FK_ComprasTarjeta FOREIGN KEY (NumeroTarjeta)
	REFERENCES DatosTarjeta(NumeroTarjeta)
)
GO

CREATE TABLE Pagos(
	IdPago int IDENTITY(1,1) NOT NULL ,
	NumeroTarjeta varchar(13) NOT NULL,
	Fecha date NOT NULL,
	Monto money NOT NULL,
	PRIMARY KEY(IdPago),
	CONSTRAINT FK_PagosTarjeta FOREIGN KEY (NumeroTarjeta)
	REFERENCES DatosTarjeta(NumeroTarjeta)
)

CREATE TABLE DatosConfigurables(
	IdDatosConfigurables int IDENTITY(1,1) NOT NULL,
	Nombre varchar(200) NOT NULL, 
	Descripcion varchar(200) NOT NULL,
	Valor varchar(200) NOT NULL,
	PRIMARY KEY (IdDatosConfigurables)
)
