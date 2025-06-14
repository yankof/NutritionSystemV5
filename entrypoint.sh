#!/bin/bash
# Iniciar SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Esperar a que SQL Server esté listo
sleep 30

# Ejecutar el script SQL
/opt/mssql-tools/bin/sqlcmd \
    -S localhost -U SA -P "$SA_PASSWORD" \
    -i /sqlscripts/migration_script.sql

# Mantener el contenedor en ejecución
tail -f /dev/null