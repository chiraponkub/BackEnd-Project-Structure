using Microsoft.EntityFrameworkCore;
using VShow_CoreLibs.Entity.Tables;

namespace VShow_BackEnd.Entity
{
    /// <summary>
    /// สำหรับกำหนด Model ที่จะมาทำ DbSet ไว้ที่นี่
    /// </summary>
    public partial class DBConnect : DbContext
    {
        public virtual DbSet<AccountProvider> AccountProvider { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
    }
}
