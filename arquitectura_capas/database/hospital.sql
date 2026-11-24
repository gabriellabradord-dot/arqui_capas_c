IF DB_ID('hospital') IS NULL
BEGIN
    CREATE DATABASE hospital;
END;
GO

USE hospital;
GO

IF OBJECT_ID('dbo.cita', 'U') IS NOT NULL DROP TABLE dbo.cita;
IF OBJECT_ID('dbo.medico', 'U') IS NOT NULL DROP TABLE dbo.medico;
IF OBJECT_ID('dbo.paciente', 'U') IS NOT NULL DROP TABLE dbo.paciente;
GO

CREATE TABLE dbo.paciente
(
    id_paciente  NVARCHAR(10) NOT NULL CONSTRAINT PK_paciente PRIMARY KEY,
    Tip_doc      NVARCHAR(20) NOT NULL,
    nom_paciente NVARCHAR(50) NOT NULL,
    dir_paciente NVARCHAR(50) NULL,
    tel_paciente NVARCHAR(10) NULL,
    cel_paciente NVARCHAR(10) NULL,
    activo       BIT NOT NULL CONSTRAINT DF_paciente_activo DEFAULT (1)
);
GO

CREATE TABLE dbo.medico
(
    id_medico   NVARCHAR(10) NOT NULL CONSTRAINT PK_medico PRIMARY KEY,
    Tip_doc     NVARCHAR(20) NOT NULL,
    nom_medico  NVARCHAR(50) NOT NULL,
    dir_medico  NVARCHAR(50) NULL,
    tel_medico  NVARCHAR(10) NULL,
    cel_medico  NVARCHAR(10) NULL,
    especialidad NVARCHAR(50) NULL,
    activo      BIT NOT NULL CONSTRAINT DF_medico_activo DEFAULT (1)
);
GO

CREATE TABLE dbo.cita
(
    Cod_cita        NVARCHAR(10) NOT NULL CONSTRAINT PK_cita PRIMARY KEY,
    fecha           DATE NOT NULL,
    hora            TIME(7) NOT NULL,
    id_paciente     NVARCHAR(10) NOT NULL,
    id_medico       NVARCHAR(10) NOT NULL,
    valor           INT NOT NULL,
    diagnostico     NVARCHAR(MAX) NULL,
    Nom_acompanante NVARCHAR(50) NULL,
    activo          BIT NOT NULL CONSTRAINT DF_cita_activo DEFAULT (1),
    CONSTRAINT FK_cita_paciente FOREIGN KEY (id_paciente) REFERENCES dbo.paciente (id_paciente),
    CONSTRAINT FK_cita_medico   FOREIGN KEY (id_medico)   REFERENCES dbo.medico (id_medico)
);
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_cita
    @Cod_cita NVARCHAR(10),
    @fecha DATE,
    @hora TIME(7),
    @id_paciente NVARCHAR(10),
    @id_medico NVARCHAR(10),
    @valor INT,
    @diagnostico NVARCHAR(MAX),
    @Nom_acompanante NVARCHAR(50),
    @activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.cita WHERE Cod_cita = @Cod_cita)
    BEGIN
        UPDATE dbo.cita
           SET fecha = @fecha,
               hora = @hora,
               id_paciente = @id_paciente,
               id_medico = @id_medico,
               valor = @valor,
               diagnostico = @diagnostico,
               Nom_acompanante = @Nom_acompanante,
               activo = @activo
         WHERE Cod_cita = @Cod_cita;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.cita
            (Cod_cita, fecha, hora, id_paciente, id_medico, valor, diagnostico, Nom_acompanante, activo)
        VALUES
            (@Cod_cita, @fecha, @hora, @id_paciente, @id_medico, @valor, @diagnostico, @Nom_acompanante, @activo);
    END;
END;
GO

REATE OR ALTER PROCEDURE dbo.anula_cita
    @pcod_cita NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.cita SET activo = 0 WHERE Cod_cita = @pcod_cita;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_cita
    @Cod_cita NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.Cod_cita, c.fecha, c.hora, c.id_paciente, p.nom_paciente,
           c.id_medico, m.nom_medico, c.valor, c.diagnostico,
           c.Nom_acompanante, c.activo
      FROM dbo.cita c
      INNER JOIN dbo.paciente p ON p.id_paciente = c.id_paciente
      INNER JOIN dbo.medico m   ON m.id_medico = c.id_medico
     WHERE c.Cod_cita = @Cod_cita;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_citas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.Cod_cita, c.fecha, c.hora, c.id_paciente, p.nom_paciente,
           c.id_medico, m.nom_medico, c.valor, c.diagnostico,
           c.Nom_acompanante, c.activo
      FROM dbo.cita c
      INNER JOIN dbo.paciente p ON p.id_paciente = c.id_paciente
      INNER JOIN dbo.medico m   ON m.id_medico = c.id_medico
     ORDER BY c.fecha DESC, c.hora DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_paciente
    @id_paciente NVARCHAR(10),
    @Tip_doc NVARCHAR(20),
    @nom_paciente NVARCHAR(50),
    @dir_paciente NVARCHAR(50),
    @tel_paciente NVARCHAR(10),
    @cel_paciente NVARCHAR(10),
    @activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.paciente WHERE id_paciente = @id_paciente)
    BEGIN
        UPDATE dbo.paciente
           SET Tip_doc = @Tip_doc,
               nom_paciente = @nom_paciente,
               dir_paciente = @dir_paciente,
               tel_paciente = @tel_paciente,
               cel_paciente = @cel_paciente,
               activo = @activo
         WHERE id_paciente = @id_paciente;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.paciente
            (id_paciente, Tip_doc, nom_paciente, dir_paciente, tel_paciente, cel_paciente, activo)
        VALUES
            (@id_paciente, @Tip_doc, @nom_paciente, @dir_paciente, @tel_paciente, @cel_paciente, @activo);
    END;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Anular_paciente
    @id_paciente NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.paciente SET activo = 0 WHERE id_paciente = @id_paciente;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_paciente
    @id_paciente NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_paciente, Tip_doc, nom_paciente, dir_paciente, tel_paciente, cel_paciente, activo
      FROM dbo.paciente
     WHERE id_paciente = @id_paciente;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_pacientes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_paciente, Tip_doc, nom_paciente, dir_paciente, tel_paciente, cel_paciente, activo
      FROM dbo.paciente
     ORDER BY nom_paciente;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Actualizar_medico
    @id_medico NVARCHAR(10),
    @Tip_doc NVARCHAR(20),
    @nom_medico NVARCHAR(50),
    @dir_medico NVARCHAR(50),
    @tel_medico NVARCHAR(10),
    @cel_medico NVARCHAR(10),
    @especialidad NVARCHAR(50),
    @activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.medico WHERE id_medico = @id_medico)
    BEGIN
        UPDATE dbo.medico
           SET Tip_doc = @Tip_doc,
               nom_medico = @nom_medico,
               dir_medico = @dir_medico,
               tel_medico = @tel_medico,
               cel_medico = @cel_medico,
               especialidad = @especialidad,
               activo = @activo
         WHERE id_medico = @id_medico;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.medico
            (id_medico, Tip_doc, nom_medico, dir_medico, tel_medico, cel_medico, especialidad, activo)
        VALUES
            (@id_medico, @Tip_doc, @nom_medico, @dir_medico, @tel_medico, @cel_medico, @especialidad, @activo);
    END;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Anular_medico
    @id_medico NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.medico SET activo = 0 WHERE id_medico = @id_medico;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_medico
    @id_medico NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_medico, Tip_doc, nom_medico, dir_medico, tel_medico, cel_medico, especialidad, activo
      FROM dbo.medico
     WHERE id_medico = @id_medico;
END;
GO

CREATE OR ALTER PROCEDURE dbo.Consultar_medicos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_medico, Tip_doc, nom_medico, dir_medico, tel_medico, cel_medico, especialidad, activo
      FROM dbo.medico
     ORDER BY nom_medico;
END;
GO

INSERT INTO dbo.paciente (id_paciente, Tip_doc, nom_paciente, dir_paciente, tel_paciente, cel_paciente, activo)
VALUES
    ('PAC001', 'CC', 'María Fernanda Gómez', 'Calle 10 # 5-23', '6012345678', '3105558877', 1),
    ('PAC002', 'CC', 'Carlos Andrés Ramírez', 'Carrera 8 # 12-45', '6019876543', '3204441122', 1),
    ('PAC003', 'TI', 'Lucía Torres', 'Avenida 3 # 20-10', '6013334455', '3157778899', 1);

INSERT INTO dbo.medico (id_medico, Tip_doc, nom_medico, dir_medico, tel_medico, cel_medico, especialidad, activo)
VALUES
    ('MED001', 'CC', 'Dr. Juan Pablo Herrera', 'Calle 50 # 20-15', '6015556677', '3001112233', 'Medicina general', 1),
    ('MED002', 'CC', 'Dra. Ana Lucía Rojas', 'Carrera 15 # 45-08', '6017778899', '3012223344', 'Pediatría', 1),
    ('MED003', 'CC', 'Dr. Felipe Mendoza', 'Calle 80 # 12-30', '6018889900', '3023334455', 'Cardiología', 1);

INSERT INTO dbo.cita (Cod_cita, fecha, hora, id_paciente, id_medico, valor, diagnostico, Nom_acompanante, activo)
VALUES
    ('CIT001', DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), '09:00', 'PAC001', 'MED001', 85000, 'Control general', 'Jorge Gómez', 1),
    ('CIT002', DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), '11:30', 'PAC002', 'MED003', 150000, 'Dolor en el pecho', NULL, 1),
    ('CIT003', DATEADD(DAY, -5, CAST(GETDATE() AS DATE)), '15:00', 'PAC003', 'MED002', 95000, 'Control de crecimiento', 'Diana Torres', 0);
GO

PRINT 'Base de datos hospital creada correctamente.';
GO
