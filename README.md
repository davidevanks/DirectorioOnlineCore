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
