# PersonAPI-dotnet - GRUPO 5
# Taller - Laboratorio 1: Creación de una API con ASP.NET Core y SQL Server

Este archivo proporciona una guía paso a paso sobre cómo se llevó a cabo el laboratorio.

## Herramientas necesarias

- Visual Studio Community 2022 instalado con los siguientes componentes:
   - Desarrollo ASP.NET y web
   - Almacenamiento y procesamiento de datos
   - Plantillas de proyecto y elementos de .Net Framework
- SQL Server 2019 Express en modo básico.
- SQL Server Management Studio 18.

## Paso 1: 

1. Crear un repositorio público en GitHub con el nombre "personapi-dotnet" según las instrucciones y clonarlo en Visual Studio Community. Esto permitirá mantener un control de versiones del laboratorio.

## Paso 2:

1. Con las herramientas de SQL descargadas, abrir SQL Server Management Studio y crear la base de datos "person_db". Esta base de datos contendrá las tablas "estudios", "profesion", "persona" y "teléfono", asignándoles propiedades "sa" para la base creada.

## Paso 3:

1. Crear un nuevo proyecto utilizando la plantilla "Aplicación web de ASP.NET Core (Modelo-Vista-Controlador)".
2. Nombrar la aplicación igual que el repositorio.
3. Verificar que al crear el proyecto esté sin autenticación y sin configuración HTTPS.

## Paso 4:

1. En el menú "Herramientas", buscar la opción "Administrar paquetes NuGet".
2. Instalar los siguientes paquetes:
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Tools

## Paso 5:

1. Abrir la consola de paquetes NuGet y ejecutar el siguiente comando:
  Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Entities
2. Se crearán las clases entidad a partir de las tablas existentes en la base de datos y el contexto.
3. Agregar la cadena de conexión en appsettings.json.

## Paso 6:

1. Crear las interfaces, repositorios y controladores necesarios para la aplicación.

## Paso 7:

1. Desplegar la aplicación en el entorno de preferencia. En este caso, se decidió desplegar en Docker.

## Paso 8:

1. Realizar "push" al repositorio Git.
2. Crear un TAG en el repositorio para marcar el estado actual del proyecto.
