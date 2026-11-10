using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ComputerShopApi.Models;

public partial class ComputershopContext : DbContext
{
    public ComputershopContext()
    {
    }

    public ComputershopContext(DbContextOptions<ComputershopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Osystem> Osystems { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=computershop;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.7-mysql"));

}
