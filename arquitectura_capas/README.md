# Proyecto Hospital — Arquitectura por capas en C#

Proyecto basado en la guía **“LAS CAPAS EN C#”** (SENA), adaptado a:

- **C# / ASP.NET Core Razor Pages** (Visual Studio Code + C# Dev Kit)
- **SQL Server 2022 en Docker** (misma idea de la guía: SQL Server + procedimientos almacenados)
- **DBeaver Community** como cliente para administrar la base de datos

## Estructura de la solución

```
arquitectura_capas/
├── CEntidades/          → Capa de entidades (clases encapsuladas)
│   ├── CECitas.cs
│   ├── CEPaciente.cs
│   └── CEMedico.cs
├── CDatos/              → Capa de datos (conexión + procedimientos almacenados)
│   ├── CConexion.cs
│   ├── CDCitas.cs
│   ├── CDPacientes.cs
│   └── CDMedicos.cs
├── CReglas/             → Capa de negocio (reglas y validaciones)
│   ├── CRegCitas.cs
│   ├── CRegPacientes.cs
│   └── CRegMedicos.cs
├── CPresentacion/       → Capa de presentación (Razor Pages)
│   ├── Pages/Citas
│   ├── Pages/Pacientes
│   └── Pages/Medicos
├── database/
│   └── hospital.sql     → Tablas, procedimientos y datos de ejemplo
└── arquitectura_capas.slnx
```

Flujo de una petición:

```
Razor Page → CReglas (negocio) → CDatos (SQL Server) → CEntidades
```

## 1. Base de datos

La base **hospital** ya está creada en un contenedor Docker llamado `hospital-sqlserver`.

| Dato | Valor |
|---|---|
| Host | `localhost` |
| Puerto | `1433` |
| Base de datos | `hospital` |
| Usuario | `sa` |
| Contraseña | `Hospital#2026` |

Comandos útiles:

```bash
# Ver / iniciar / detener el contenedor
docker ps -a --filter name=hospital-sqlserver
docker start hospital-sqlserver
docker stop hospital-sqlserver

# Abrir una consola SQL dentro del contenedor
docker exec -it hospital-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'Hospital#2026' -C -d hospital
```

### Volver a crear la base desde cero

```bash
docker cp database/hospital.sql hospital-sqlserver:/tmp/hospital.sql
docker exec hospital-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'Hospital#2026' -C -i /tmp/hospital.sql
```

> El script elimina y vuelve a crear las tablas `paciente`, `medico` y `cita`,
> sus procedimientos almacenados y los datos de ejemplo.

### Procedimientos almacenados

| Procedimiento | Función |
|---|---|
| `Actualizar_cita` | Inserta o actualiza una cita |
| `anula_cita` | Anulación lógica de una cita (`activo = 0`) |
| `Consultar_cita` | Consulta una cita con datos del paciente y del médico |
| `Consultar_citas` | Lista todas las citas |
| `Actualizar_paciente` / `Anular_paciente` / `Consultar_paciente(s)` | CRUD de pacientes |
| `Actualizar_medico` / `Anular_medico` / `Consultar_medico(s)` | CRUD de médicos |

## 2. DBeaver Community

La conexión **“Hospital (SQL Server - Docker)”** ya está registrada en DBeaver.

1. Abra DBeaver Community.
2. En el panel izquierdo verá la conexión **Hospital (SQL Server - Docker)**.
3. Al conectarse, escriba la contraseña: `Hospital#2026`.
4. El driver JDBC de SQL Server ya fue descargado, por lo que no debería pedirlo.
5. El script de la base también está disponible en `Scripts → hospital.sql`.

Si DBeaver estaba abierto mientras se creó la conexión, ciérrelo y ábralo de nuevo.

## 3. Ejecutar el proyecto en Visual Studio Code

Requisitos (ya instalados en este equipo):

- .NET SDK 10
- Extensión **C# Dev Kit** (`ms-dotnettools.csdevkit`)
- Extensión **C#** (`ms-dotnettools.csharp`)

Pasos:

1. Abra la carpeta `arquitectura_capas` en Visual Studio Code.
2. Espere a que C# Dev Kit cargue la solución.
3. Presione **F5** y elija **Hospital Capas (Razor Pages)**.
4. Se abrirá `http://localhost:5080`.

También puede ejecutarlo desde la terminal:

```bash
cd arquitectura_capas
dotnet run --project CPresentacion --urls http://localhost:5080
```

> Antes de ejecutar, asegúrese de que el contenedor esté iniciado:
> `docker start hospital-sqlserver`

## 4. Páginas disponibles

| Ruta | Descripción |
|---|---|
| `/` | Panel con resumen y próximas citas |
| `/Citas` | Listado, búsqueda y anulación de citas |
| `/Citas/Create` | Registrar una cita |
| `/Citas/Edit/{codigo}` | Editar una cita |
| `/Pacientes` | Listado y gestión de pacientes |
| `/Medicos` | Listado y gestión de médicos |

## 5. Conceptos de la guía aplicados

- **Encapsulamiento:** atributos privados y propiedades públicas en las entidades.
- **Capa de datos:** `CConexion` abre la conexión y cada clase usa `SqlCommand`,
  `SqlDataAdapter` y `DataSet` para ejecutar los procedimientos almacenados.
- **Capa de negocio:** valida los datos y actúa como tránsito entre presentación y datos.
- **Capa de presentación:** Razor Pages captura y muestra la información al usuario.
- **Inyección de dependencias:** la cadena de conexión se define en
  `CPresentacion/appsettings.json` y las capas se registran en `Program.cs`.
- **CRUD completo:** guardar, consultar, editar y anular (borrado lógico)
  para citas, pacientes y médicos.

## Notas

- Las contraseñas están en texto plano porque es un proyecto académico local.
  En un entorno real se deben usar *user secrets*, variables de entorno o un
  usuario de base de datos con permisos mínimos.
- El contenedor usa SQL Server 2022 Developer, gratuito para desarrollo.
- Si el contenedor se detiene, la información se conserva en el volumen
  Docker `hospital_mssql`.
