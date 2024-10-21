﻿// <auto-generated />
using System;
using BancoChu.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BancoChu.Infra.Migrations
{
    [DbContext(typeof(BancoChuContext))]
    [Migration("20241020194925_Transacao-Sucesso_Mensagem")]
    partial class TransacaoSucesso_Mensagem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BancoChu.Domain.Entidades.Conta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AgenciaOrigem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumeroContaOrigem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("numeric");

                    b.Property<string>("Titular")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("BancoChu.Domain.Entidades.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContaDestinoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContaOrigemId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mensagem")
                        .HasColumnType("text");

                    b.Property<bool>("Sucesso")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ContaDestinoId");

                    b.HasIndex("ContaOrigemId", "ContaDestinoId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("BancoChu.Domain.Entidades.Transacao", b =>
                {
                    b.HasOne("BancoChu.Domain.Entidades.Conta", "ContaDestino")
                        .WithMany()
                        .HasForeignKey("ContaDestinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BancoChu.Domain.Entidades.Conta", "ContaOrigem")
                        .WithMany()
                        .HasForeignKey("ContaOrigemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ContaDestino");

                    b.Navigation("ContaOrigem");
                });
#pragma warning restore 612, 618
        }
    }
}
