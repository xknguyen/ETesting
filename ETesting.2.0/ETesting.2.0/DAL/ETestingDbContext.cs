using Microsoft.AspNet.Identity.EntityFramework;

namespace ETesting._2._0.DAL
{
    public class ETestingDbContext : IdentityDbContext
    {
        public ETestingDbContext()
            : base("DefaultConnection")
        {
        }

        public static ETestingDbContext Create()
        {
            return new ETestingDbContext();
        }
    }
}