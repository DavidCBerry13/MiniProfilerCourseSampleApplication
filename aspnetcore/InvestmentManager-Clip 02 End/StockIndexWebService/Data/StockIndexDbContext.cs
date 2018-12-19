using Microsoft.EntityFrameworkCore;
using StockIndexWebService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockIndexWebService.Data
{
    public class StockIndexDbContext : DbContext
    {

        public StockIndexDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<StockIndex> StockIndexes { get; set; }

        public DbSet<StockIndexPrice> StockIndexPrices { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ConfigureStockIndex(modelBuilder);
            this.ConfigureStockIndexPrices(modelBuilder);
        }


        private void ConfigureStockIndex(ModelBuilder modelBuilder)
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


        private void ConfigureStockIndexPrices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockIndexPrice>()
                .ToTable("StockIndexPrices")
                .HasKey(p => new { p.IndexCode, p.TradeDate });

            modelBuilder.Entity<StockIndexPrice>().Property(p => p.IndexCode)
                .HasColumnName("IndexCode");
            modelBuilder.Entity<StockIndexPrice>().Property(p => p.AdjustedClosePrice)
                .HasColumnName("AdjClosePrice");

            modelBuilder.Entity<StockIndexPrice>()
                .HasOne<StockIndex>(x => x.Index);


            // https://localhost:44325/api/StockIndexPrices/DJIA?tradeDate=2018-10-17
        }

    }
}
