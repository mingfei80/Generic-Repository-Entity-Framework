using Microsoft.EntityFrameworkCore;

namespace AveryLam.EFCore.Data;

public class AveryLamDbContext: DbContext
{
    public AveryLamDbContext(DbContextOptions<AveryLamDbContext> options) : base(options)
    { }
}
