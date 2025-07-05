using FileManager.Api.Entites;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using System.Reflection;

namespace FileManager.Api.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):DbContext(options)
{
	public DbSet<UploadedFile> Files { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
		
		base.OnModelCreating(modelBuilder);
		
	}
}
