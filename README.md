# Backend Store API

## Ejecutar la API

1. Verifica que SQL Server este disponible en `localhost:1433`.
2. Usa la cadena de conexion configurada en `appsettings.json` o cambia sus valores.
3. Desde esta carpeta ejecuta:

```powershell
dotnet run --launch-profile http
```

La URL base para Postman es `http://localhost:5141`. Si usas el perfil HTTPS,
usa `https://localhost:7056` y desactiva temporalmente la verificacion SSL en
Postman para el certificado de desarrollo.

## Postman

Importa `postman/backend.postman_collection.json`. La coleccion usa la variable
`baseUrl` y guarda automaticamente `brandId`, `beerId` e `inventoryId` al crear
recursos. Ejecuta las carpetas en este orden:

1. `01 - Brands`
2. `02 - Beers`
3. `03 - Inventory`
4. `04 - Otros endpoints`
5. `05 - Limpieza opcional`

La ultima carpeta elimina los registros creados por la prueba. Ejecutala solo
cuando ya no necesites revisar las respuestas.

Los endpoints de marcas, cervezas e inventario requieren que SQL Server este
encendido y que la base de datos exista. Si la base no existe, aplica las
migraciones con:

```powershell
dotnet ef database update
```

## Rutas principales

| Metodo | Ruta | Uso |
| --- | --- | --- |
| GET | `/api/Brand` | Listar marcas |
| POST | `/api/Brand` | Crear marca |
| PUT | `/api/Brand/{id}` | Actualizar marca |
| DELETE | `/api/Brand/{id}` | Eliminar marca |
| GET | `/api/Brand/{id}/beers` | Cervezas de una marca |
| GET | `/api/Beer` | Listar cervezas |
| POST | `/api/Beer` | Crear cerveza |
| PUT | `/api/Beer/{id}` | Actualizar cerveza |
| DELETE | `/api/Beer/{id}` | Eliminar cerveza |
| GET | `/api/Inventory` | Listar inventario |
| POST | `/api/Inventory` | Crear inventario |
| PUT | `/api/Inventory/{id}` | Actualizar inventario |
| DELETE | `/api/Inventory/{id}` | Eliminar inventario |

Los cuerpos se envian como `raw` JSON con `Content-Type: application/json`.
