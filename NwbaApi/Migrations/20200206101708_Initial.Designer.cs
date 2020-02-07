﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NwbaApi.Data;

namespace NwbaApi.Migrations
{
    [DbContext(typeof(NwbaContext))]
    [Migration("20200206101708_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NwbaApi.Models.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.Property<int?>("BillPayID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PayeeID")
                        .HasColumnType("int");

                    b.HasKey("AccountNumber");

                    b.HasIndex("BillPayID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PayeeID");

                    b.ToTable("Accounts");

                    b.HasCheckConstraint("CH_Account_Balance", "Balance >= 0");
                });

            modelBuilder.Entity("NwbaApi.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int>("PostCode")
                        .HasColumnType("int")
                        .HasMaxLength(4);

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("AddressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("NwbaApi.Models.BillPay", b =>
                {
                    b.Property<int>("BillPayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("BillPayStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PayeeID")
                        .HasColumnType("int")
                        .HasMaxLength(30);

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BillPayID");

                    b.HasIndex("PayeeID");

                    b.ToTable("BillPays");
                });

            modelBuilder.Entity("NwbaApi.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tfn")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("CustomerID");

                    b.HasIndex("AddressID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NwbaApi.Models.Login", b =>
                {
                    b.Property<string>("LoginID")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("LoginID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Logins");

                    b.HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8");

                    b.HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                });

            modelBuilder.Entity("NwbaApi.Models.Payee", b =>
                {
                    b.Property<int>("PayeeID")
                        .HasColumnType("int");

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PayeeID");

                    b.HasIndex("AddressID");

                    b.ToTable("Payees");
                });

            modelBuilder.Entity("NwbaApi.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("DestinationAccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TransactionTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("TransactionID");

                    b.HasIndex("AccountNumber");

                    b.ToTable("Transactions");

                    b.HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
                });

            modelBuilder.Entity("NwbaApi.Models.Account", b =>
                {
                    b.HasOne("NwbaApi.Models.BillPay", null)
                        .WithMany("Accounts")
                        .HasForeignKey("BillPayID");

                    b.HasOne("NwbaApi.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NwbaApi.Models.Payee", null)
                        .WithMany("Accounts")
                        .HasForeignKey("PayeeID");
                });

            modelBuilder.Entity("NwbaApi.Models.BillPay", b =>
                {
                    b.HasOne("NwbaApi.Models.Payee", "Payee")
                        .WithMany()
                        .HasForeignKey("PayeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NwbaApi.Models.Customer", b =>
                {
                    b.HasOne("NwbaApi.Models.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NwbaApi.Models.Login", b =>
                {
                    b.HasOne("NwbaApi.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NwbaApi.Models.Payee", b =>
                {
                    b.HasOne("NwbaApi.Models.Address", null)
                        .WithMany("Payees")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NwbaApi.Models.Transaction", b =>
                {
                    b.HasOne("NwbaApi.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
