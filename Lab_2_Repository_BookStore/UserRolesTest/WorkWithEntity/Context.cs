namespace WorkWithEntity
{
    using System.Data.Entity;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Sage> Sages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Book>()
                .HasMany(e => e.Clients)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("CartLine").MapLeftKey("ClientRefId").MapRightKey("BookRefId"));

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Sages)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("SageBooks").MapLeftKey("BookRefId").MapRightKey("SageRefId"));
        }
    }
}
