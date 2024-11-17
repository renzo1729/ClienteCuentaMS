# Nombre del Proyecto

Microservicios Desarrollo.

## Tabla de Contenidos
- [Instalaci�n](#instalaci�n)

## Instalaci�n

Para configurar y ejecutar el proyecto, sigue los siguientes pasos:

1. **Compilaci�n y despliegue con Docker**:
   Abre una ventana de comandos (cmd), navega hasta la carpeta ra�z de la soluci�n del proyecto y ejecuta el siguiente comando para construir y desplegar los contenedores:
   
   docker-compose up --build
2. Inicializaci�n de la base de datos: En una nueva ventana de comandos (cmd), dir�gete nuevamente a la carpeta ra�z de la soluci�n y ejecuta el siguiente comando para inicializar la base de datos en el contenedor de SQL Server: 
   docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Renzo1129@1129" -d master -i /BaseDatos.sql

Probar con el postman-collection que est� dentro la soluci�n

http://localhost:5001 es para Microservicio PersonClient y http://localhost:5002 es para Microservicio AccountTransaction