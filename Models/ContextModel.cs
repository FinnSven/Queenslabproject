using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Queenslab_Project.Models
{
    public partial class sampleContext : DbContext
    {
        public sampleContext()
        {

        }

        public sampleContext(DbContextOptions<sampleContext> options)
            : base(options)
            {

            }
            public virtual DbSet<Basket> Basket {get; set;}
            public virtual DbSet<CustTrans> CustTrans {get; set;}
            public virtual DbSet<Discount> Discount {get; set;}
            public virtual DbSet<ProductDetails> ProductDetails {get; set;}
            public virtual DbSet<Promo> Promo {get; set;}
            public virtual DbSet<TotalDetail> TotalDetail {get; set;}

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            {
                if (!optionsBuilder.IsConfigured)
                {
// warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
// This above warning was included. I am guessing for clarity. I have not put a connection string just yet. 
                optionsBuilder.UseSqlServer("Server=MSI;Database=sample;Trusted_Connection=True;");
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Basket>(entity =>
                {
                    entity.HasKey(e => e.BasketId);

                    entity.ToTable("tblBasket");

                    entity.Property(e => e.BasketId).HasColumnName("BasketID");

                    entity.Property(e => e.ProductId)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasOne(d => d.Customer)
                        .WithMany(p => p.Basket)
                        .HasForeignKey(d => d.CustomerId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_tblBasket_tblCustomerTrans");

                });

                modelBuilder.Entity<CustTrans>(entity =>
                {
                    entity.HasKey(e => e.CustomerId)
                        .HasName("PK_tblCustTrans");
                    entity.ToTable("tblCustTrans");

                    entity.Property(e => e.CustomerId).ValueGeneratedNever();

                    entity.Property(e=> e.DandtimeInser)
                        .HasColumnName("DandtimeInser")
                        .HasColumnType("datetime");

                    entity.Property(e => e.LoyaltyCard)
                        .IsRequired()
                        .HasMaxLength(50);

                });

                modelBuilder.Entity<Discount>(entity =>
                {
                    entity.HasKey(e=> e.discountId);
                    
                    entity.ToTable("tblDiscount");

                    entity.Property(e => e.discountId).HasMaxLength(50);

                    entity.Property(e => e.DiscountLoyalty)
                        .IsRequired()
                        .HasMaxLength(200);

                    entity.Property(e => e.DandtimeInser)
                        .HasColumnName("DandtimeInser")
                        .HasColumnType("datetime");

                    entity.Property(e => e.EndDate).HasColumnType("datetime");

                    entity.Property(e => e.productId)
                        .IsRequired()
                        .HasMaxLength(50);
                        
                    entity.Property(e => e.StartDate).HasColumnType("datetime");

                    entity.HasOne(d => d.Product)
                        .WithMany(p => p.TblDiscount)
                        .HasForeignKey(d => d.productId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_tblProductDetails_tblDiscount");

                }); 

                modelBuilder.Entity<ProductDetails>(entity =>
                {
                    entity.HasKey(e => e.productId)
                        .HasName("PK_tblProductDetails");
                    
                    entity.ToTable("tblProductDetails");
                    

                    entity.Property(e => e.productId).HasMaxLength(50);

                    entity.Property(e => e.Category)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.Property(e => e.DandtimeInser)
                        .HasColumnType("DandtimeInser")
                        .HasColumnType("datetime");
                    
                    entity.Property(e => e.ProductName)
                        .IsRequired()
                        .HasMaxLength(200);
                });
                
                modelBuilder.Entity<Promo>(entity =>
                {

                    entity.HasKey(e => e.PromoID)
                        .HasName("PK_tblPromo");

                    entity.ToTable("tblPromo");

                    entity.Property(e => e.PromoID).HasMaxLength(50);

                    entity.Property(e => e.Category)
                        .IsRequired()
                        .HasMaxLength(100);
                    entity.Property(e => e.DandtimeInser)
                    .HasColumnName("DandtimeInser")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PromoName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                });

                
            modelBuilder.Entity<TotalDetail>(entity =>
            {
                entity.HasKey(e => e.TotalID);

                entity.ToTable("tblTotalDetail");

                entity.Property(e => e.DandtimeInser)
                    .HasColumnName("DandtimeInser")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TotalDetail)
                    .HasForeignKey(d => d.customerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTotalDetail_tblCustomerTransaction");
            });
            }


    }
}