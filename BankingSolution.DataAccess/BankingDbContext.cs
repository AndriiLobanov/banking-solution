﻿using BankingSolution.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.DataAccess;

public class BankingDbContext : DbContext
{
    public BankingDbContext()
    { }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Transaction>()
            .HasKey(t => t.Id);
    }
}