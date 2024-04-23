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


IMPORTANTE:

### Pasos para ejecutar el programa:

1. **Ejecutar el DDL en SQL Server Management Studio:**
   - Antes de comenzar, asegúrate de tener acceso a SQL Server Management Studio.
   - Abre SQL Server Management Studio y ejecuta el script DDL proporcionado para configurar la base de datos.
   
2. **Clonar el repositorio en Visual Studio Community:**
   - Abre Visual Studio Community.
   - Clona el repositorio del proyecto desde GitHub u otra plataforma utilizando la opción de clonación de repositorios.
   
3. **Realizar cambios en los archivos mencionados:**
   - Abre el proyecto clonado en Visual Studio Community.
   - Realiza los siguientes cambios en los archivos mencionados:

    a. **Archivo `appsettings.json`:**
       - Abre el archivo `appsettings.json`.
       - Busca la cadena de conexión que se encuentra bajo la clave `"ConexionBD"`.
       - Modifica el valor de `"MARTINPC"` con la dirección del servidor de base de datos correspondiente.
   
    b. **Archivo `personDbContext.cs` dentro de las carpetas Models/Entities:**
       - Navega hasta la carpeta Models/Entities en el proyecto.
       - Abre el archivo `personDbContext.cs`.
       - Busca la cadena de conexión y modifica el valor de `"MARTINPC"` con la dirección del servidor de base de datos correspondiente.

   Nota: Asegúrate de cambiar solo el servidor en ambas cadenas de conexión, manteniendo el resto de la cadena sin modificar.

Una vez completados estos pasos, el programa estará configurado correctamente para conectarse a la base de datos con el servidor especificado.
Y listo, puedes correr el programa


# Pasos seguidos para la realización del Laboratorio 1

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
