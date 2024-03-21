﻿// <auto-generated />
using Ability.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ability.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240320151938_Second")]
    partial class Second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ability.Web.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnOrder(3);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Ability.Web.Models.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PedidoItens");
                });

            modelBuilder.Entity("Ability.Web.Models.Pessoa", b =>
                {
                    b.Property<int>("PessoaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PessoaId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(3);

                    b.Property<int>("Idade")
                        .HasColumnType("int")
                        .HasColumnOrder(4);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnOrder(2);

                    b.HasKey("PessoaId");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Ability.Web.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Ability.Web.Models.PedidoItem", b =>
                {
                    b.HasOne("Ability.Web.Models.Pedido", "Pedido")
                        .WithMany("PedidoItens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ability.Web.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Ability.Web.Models.Pedido", b =>
                {
                    b.Navigation("PedidoItens");
                });
#pragma warning restore 612, 618
        }
    }
}
