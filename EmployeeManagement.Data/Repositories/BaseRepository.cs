using Calisan_Yonetim_Core.Models;

public abstract class BaseRepository<T> where T : class
{
    protected readonly CalisanYonetimDbContext _context;

    protected BaseRepository(CalisanYonetimDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
} 