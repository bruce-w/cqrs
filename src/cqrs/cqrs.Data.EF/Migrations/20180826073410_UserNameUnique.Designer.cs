﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cqrs.Data.Sql.EF;

namespace cqrs.Data.Sql.EF.Migrations
{
    [DbContext(typeof(AuctionContext))]
    [Migration("20180826073410_UserNameUnique")]
    partial class UserNameUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cqrs.Domain.Entities.Auction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CloseDate");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("LotId");

                    b.Property<string>("SellerId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("LotId");

                    b.HasIndex("SellerId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("cqrs.Domain.Entities.Bid", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuctionId");

                    b.Property<string>("BidderId");

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BidderId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("cqrs.Domain.Entities.Lot", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("cqrs.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("cqrs.Domain.Entities.Auction", b =>
                {
                    b.HasOne("cqrs.Domain.Entities.Lot", "Lot")
                        .WithMany()
                        .HasForeignKey("LotId");

                    b.HasOne("cqrs.Domain.Entities.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.OwnsOne("cqrs.Domain.ValueObjects.Money", "InitialAmount", b1 =>
                        {
                            b1.Property<string>("AuctionId");

                            b1.Property<decimal>("Amount");

                            b1.Property<int>("Currency");

                            b1.ToTable("Auctions");

                            b1.HasOne("cqrs.Domain.Entities.Auction")
                                .WithOne("InitialAmount")
                                .HasForeignKey("cqrs.Domain.ValueObjects.Money", "AuctionId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("cqrs.Domain.Entities.Bid", b =>
                {
                    b.HasOne("cqrs.Domain.Entities.Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId");

                    b.HasOne("cqrs.Domain.Entities.User", "Bidder")
                        .WithMany()
                        .HasForeignKey("BidderId");

                    b.OwnsOne("cqrs.Domain.ValueObjects.Money", "Amount", b1 =>
                        {
                            b1.Property<string>("BidId");

                            b1.Property<decimal>("Amount");

                            b1.Property<int>("Currency");

                            b1.ToTable("Bids");

                            b1.HasOne("cqrs.Domain.Entities.Bid")
                                .WithOne("Amount")
                                .HasForeignKey("cqrs.Domain.ValueObjects.Money", "BidId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}