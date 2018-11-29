using StockIndexWebService.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StockIndexWebService.Data
{
    public class StockIndexDbContext : DbContext
    {

        public StockIndexDbContext(String connectionString) : base(connectionString)
        {

        }


        public DbSet<StockIndex> StockIndexes { get; set; }

        public DbSet<StockIndexPrice> StockIndexPrices { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureStockIndex(modelBuilder);
            this.ConfigureStockIndexPrices(modelBuilder);
        }


        private void ConfigureStockIndex(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockIndex>()
                .ToTable("StockIndexes")
                .HasKey(ix => ix.Code);

            modelBuilder.Entity<StockIndex>().Property(p => p.Code)
                .HasColumnName("IndexCode");
            modelBuilder.Entity<StockIndex>().Property(p => p.Name)
                .HasColumnName("IndexName");
            modelBuilder.Entity<StockIndex>().Property(p => p.ShortDisplayName)
                .HasColumnName("ShortDisplayName");
        }


        private void ConfigureStockIndexPrices(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockIndexPrice>()
                .ToTable("StockIndexPrices")
                .HasKey(p => new { p.IndexCode, p.TradeDate });

            modelBuilder.Entity<StockIndexPrice>().Property(p => p.IndexCode)
                .HasColumnName("IndexCode");
            modelBuilder.Entity<StockIndexPrice>().Property(p => p.AdjustedClosePrice)
                .HasColumnName("AdjClosePrice");

            modelBuilder.Entity<StockIndexPrice>()
                .HasRequired<StockIndex>(x => x.Index);
        }

    }
}
