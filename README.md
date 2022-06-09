# DirectorioOnlineCore
DirectorioOnlineCore

# Instrucciones para actualizar el modelo con database first

Hago esto ya que el identity lo ocupe desde la perspectica Data base first y no quiero que se sobreescriba.

ejecutar en consola nuget packet:
Scaffold-DbContext -Connection name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force -Schema "bs"
-------------------------------------------------------------------------
Luego en el contexto heredar de:
- public partial class DirectorioOnlineCoreContext : IdentityDbContext


luego agregar esto en la primer linea luego del método OnModelCreating:

  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
# Carpeta para imagenes
En el proyecto no inclui las imagenes que guardan los usuarios. como parte de la configuracion se debe crear siempre la carpeta en el wwwroot del
proyecto llamada "ImagesBusiness"
