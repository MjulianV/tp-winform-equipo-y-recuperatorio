# Catálogo de artículos

Aplicación Windows Forms para .NET Framework 4.8 y SQL Server.

## Ejecutar en una PC con la base instalada

1. Abrir la solución en Visual Studio 2022.
2. En App.config, ajustar únicamente Data Source de la conexión CatalogoDB al servidor o instancia que usa esa PC. Se conserva la configuración original .\SQLEXPRESS01.
3. Comprobar que la base se llama CATALOGO_P3_DB y contiene ARTICULOS, MARCAS, CATEGORIAS e IMAGENES.
4. Ejecutar el proyecto. La aplicación no instala SQL Server ni crea bases automáticamente.

## Funciones

- Búsqueda mientras se escribe, por código, nombre u otros campos. Acepta coincidencias parciales y no distingue mayúsculas.
- Nuevo abre un formulario con marcas y categorías obtenidas de SQL Server.
- Guardar valida campos obligatorios y precio, comprueba códigos duplicados y guarda con parámetros SQL.
- Después de guardar, se recarga la base, se limpia la búsqueda y se selecciona el artículo guardado.
- Editar permite actualizar el artículo seleccionado.
- Eliminar pide confirmación y elimina el artículo y sus imágenes en una transacción.
- Actualizar permite volver a cargar el catálogo después de un error de conexión.

## Comprobación con base de datos

1. Buscar S01 por Código y Galaxy por Nombre.
2. Crear un artículo con un código nuevo, nombre, marca, categoría y precio.
3. Confirmar que aparece seleccionado en el listado sin presionar Actualizar.
4. Cerrar y abrir el programa para verificar que quedó guardado.
5. Intentar repetir el código: debe indicar que ya existe.
6. Cancelar un alta: no debe guardar cambios.

La compilación y las pruebas de búsqueda y validación se verificaron sin instalar una base. El guardado real y la persistencia deben comprobarse en una PC con conexión a la base.

## Observación sobre los datos de ejemplo

El SQL proporcionado asigna al Moto G IdMarca=1 e IdCategoria=5, pero solo crea categorías con Id de 1 a 4. Si se pretendía asignar Motorola y Celulares, corresponderían IdMarca=5 e IdCategoria=1. El script original no fue modificado ni ejecutado.
