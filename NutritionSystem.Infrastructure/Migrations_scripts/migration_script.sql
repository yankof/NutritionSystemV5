CREATE DATABASE [PacienteDB]
GO
USE [PacienteDB]
GO

-- Contenido de script_tablas.sql
BEGIN TRANSACTION;

IF OBJECT_ID('[dbo].[Persona]', 'U') IS NOT NULL
DROP TABLE [dbo].[Persona]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Persona]
(
    [Id] UNIQUEIDENTIFIER not null PRIMARY KEY, -- Primary Key column    
    Nombre VARCHAR(90),
    ApPaterno VARCHAR(60), 
    ApMaterno VARCHAR(60),
    Mail VARCHAR(60),
    CI VARCHAR(15),
    Nit VARCHAR(15),
    Direccion VARCHAR(60),
    Telefono VARCHAR(60),
    Celular VARCHAR(60),
    FechaCreacion date
);
GO
IF OBJECT_ID('[dbo].[Nutricionista]', 'U') IS NOT NULL
DROP TABLE [dbo].[Nutricionista]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Nutricionista]
(
    [IdNutricionista] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column    
    Titulo varchar(60),
    Turno  varchar(60),
    Activo varchar(10),
    fechaCreacion DATE,
    
);
GO
ALTER TABLE Nutricionista ADD FOREIGN KEY (IdNutricionista) REFERENCES Persona
GO
-- Create a new table called '[Pacient]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Pacient]', 'U') IS NOT NULL
DROP TABLE [dbo].[Pacient]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Pacient]
(
    [IdPacient] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column
    Activo varchar(10),
    FechaCreacion DATE,
);
GO
ALTER TABLE Pacient ADD FOREIGN KEY (idPacient) REFERENCES Persona
-- Create a new table called '[Consulta]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Consulta]', 'U') IS NOT NULL
DROP TABLE [dbo].[Consulta]
GO
-- Create the table in the specified schema

CREATE TABLE [dbo].[Consulta]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column
    [Descripcion] VARCHAR(50),
    [IdNutricionista] UNIQUEIDENTIFIER,
    [IdPacient] UNIQUEIDENTIFIER,
    FechaCreacion date,
    Estatus varchar(10),
);
GO

-- ALTER TABLE Consulta ADD FOREIGN KEY (idPacient) REFERENCES Pacient
-- ALTER TABLE Consulta ADD FOREIGN KEY (IdNutricionista) REFERENCES Nutricionista
GO
-- Create a new table called '[Evaluacion]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Evaluacion]', 'U') IS NOT NULL
DROP TABLE [dbo].[Evaluacion]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Evaluacion]
(
    IdEvaluacion UNIQUEIDENTIFIER not null PRIMARY KEY,
    Descripcion varchar(255),
    TipoEvaluacion varchar(5),
    FechaCreacion date,
    --HoraCreacion varchar(10),
    Activo VARCHAR(10),
    IdConsulta UNIQUEIDENTIFIER,
    --TipoStatus varchar(10),
);
GO
ALTER TABLE Evaluacion ADD FOREIGN KEY (IdConsulta) REFERENCES Consulta
GO
-- Create a new table called '[Diagnostico]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Diagnostico]', 'U') IS NOT NULL
DROP TABLE [dbo].[Diagnostico]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Diagnostico]
(
    [IdConsulta] UNIQUEIDENTIFIER NOT NULL,
    [TipoDiagnostico] VARCHAR(50),
    Valor varchar(200),
    TipoStatus varchar(10),
);
GO
ALTER TABLE Diagnostico ADD FOREIGN KEY (IdConsulta) REFERENCES Consulta
GO
-- Create a new table called '[Plan]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Plan]', 'U') IS NOT NULL
DROP TABLE [dbo].[Plan]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Plan]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column
    TipoPlan varchar(100),
    Descripcion varchar(200),
    DiasTratamiento int,
    IdConsulta UNIQUEIDENTIFIER not null,
    TipoStatus varchar(10),
    FechaCreacion date,
        
);
GO
ALTER TABLE [Plan] ADD FOREIGN KEY (IdConsulta) REFERENCES Consulta
GO
-- Create a new table called '[HistorialPaciente]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[HistorialPaciente]', 'U') IS NOT NULL
DROP TABLE [dbo].[HistorialPaciente]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[HistorialPaciente]
(
    [IdHistorial] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column
    IdPaciente UNIQUEIDENTIFIER,
    IdEvaluacion UNIQUEIDENTIFIER,
    FechaCreacion date,
    Valor nvarchar(100),
    Resultado  nvarchar(200),
);
GO
-- Create a new table called '[Reserva]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Reserva]', 'U') IS NOT NULL
DROP TABLE [dbo].[Reserva]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Reserva]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, -- Primary Key column    
    FechaCreacion   date,-- : Datetime
    HoraCreacion    varchar(10),-- : timeStamp
    Activo  varchar(10),-- : string
    FechaModificacion   date,-- : Datetime
    HoraModificacion    varchar(10),-- : TimeStamp
);
GO
IF SCHEMA_ID(N'outbox') IS NULL EXEC(N'CREATE SCHEMA [outbox];');
GO
-- Create a new table called '[outbox].[outboxMessage]' in schema '[outbox]'
-- Drop the table if it already exists
IF OBJECT_ID('[outbox].[outboxMessage]', 'U') IS NOT NULL
DROP TABLE [outbox].[outboxMessage]
GO
CREATE TABLE [outbox].[outboxMessage] (
    [outboxId] uniqueidentifier NOT NULL,
    [content] nvarchar(max) NULL,
    [type] nvarchar(max) NOT NULL,
    [created] datetime2 NOT NULL,
    [processed] bit NOT NULL,
    [processedOn] datetime2 NULL,
    [correlationId] nvarchar(max) NULL,
    [traceId] nvarchar(max) NULL,
    [spanId] nvarchar(max) NULL,
    CONSTRAINT [PK_outboxMessage] PRIMARY KEY ([outboxId])
);

COMMIT;
GO