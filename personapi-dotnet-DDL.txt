-- Crear el esquema si no existe
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'person_db')
BEGIN
    EXEC('CREATE SCHEMA person_db');
END;
GO

USE master; -- Cambiar al contexto de la base de datos master para asegurarnos de que tengamos los permisos adecuados

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'person_db')
BEGIN
    CREATE DATABASE person_db;
END;
GO

USE person_db;


-- Cambiar el modo de compatibilidad para evitar errores en la creaci�n de tablas

ALTER DATABASE [person_db] SET COMPATIBILITY_LEVEL = 120;

-- Crear el esquema si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'person_db')
BEGIN
    EXEC('CREATE SCHEMA person_db')
END

GO

USE [person_db];


-- Tabla `persona`
CREATE TABLE persona (
  cc INT NOT NULL,
  nombre NVARCHAR(45) NOT NULL,
  apellido NVARCHAR(45) NOT NULL,
  genero CHAR(1) NOT NULL,
  edad INT NULL,
  PRIMARY KEY (cc)
);


-- Tabla `profesion`
CREATE TABLE profesion (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nom NVARCHAR(90) NOT NULL,
  des NVARCHAR(MAX) NULL
);

-- Tabla `estudios`
CREATE TABLE estudios (
  id INT IDENTITY(1,1) PRIMARY KEY, -- Definir id como la clave primaria
  id_prof INT NOT NULL,
  cc_per INT NOT NULL,
  fecha DATE NULL,
  univer NVARCHAR(50) NULL,
  CONSTRAINT estudio_persona_fk FOREIGN KEY (cc_per) REFERENCES persona (cc),
  CONSTRAINT estudio_profesion_fk FOREIGN KEY (id_prof) REFERENCES profesion (id)
);

-- Crear un índice único para las claves foráneas
CREATE UNIQUE INDEX UQ_estudios_cc_per_id_prof ON estudios (id_prof, cc_per);


-- Tabla `telefono`
CREATE TABLE telefono (
  num NVARCHAR(15) NOT NULL,
  oper NVARCHAR(45) NOT NULL,
  duenio INT NOT NULL,
  PRIMARY KEY (num),
  CONSTRAINT telefono_persona_fk FOREIGN KEY (duenio) REFERENCES persona (cc)
);

-- Restaurar el modo de compatibilidad y verificar restricciones
ALTER DATABASE [person_db] SET COMPATIBILITY_LEVEL = 130;

-- Asignar propiedad al usuario sa
ALTER AUTHORIZATION ON DATABASE::person_db TO sa;

