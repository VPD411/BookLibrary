using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.src.Infrastructures.DataAccess.Data.Configurations;

internal static class CustomModelBuilder
{
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfiguration(new UserEntityConfiguration())
            .ApplyConfiguration(new BookEntityConfiguration())
            .ApplyConfiguration(new ReviewEntityConfiguration());
    }
}
