using Domain;
using Presistense.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Presistense { 
    public class DbIntializer(MetroDbContex metroDbContex) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if ((await metroDbContex.Database.GetPendingMigrationsAsync()).Any())
            {
                await metroDbContex.Database.MigrateAsync();
            }
        }
    }
}