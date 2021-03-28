namespace test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class employees : DbContext
    {
        public employees()
            : base("name=employees")
        {
        }

        public virtual DbSet<deps> deps { get; set; }
        public virtual DbSet<persons> persons { get; set; }
        public virtual DbSet<posts> posts { get; set; }
        public virtual DbSet<status> status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<deps>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<persons>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<persons>()
                .Property(e => e.second_name)
                .IsUnicode(false);

            modelBuilder.Entity<persons>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<posts>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<status>()
                .Property(e => e.name)
                .IsUnicode(false);
        }
    }
}
