﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedeSocial.Infrastructure.Context;

#nullable disable

namespace RedeSocial.Infrastructure.Migrations
{
    [DbContext(typeof(RedeSocialDbContext))]
    [Migration("20221220225011_PbInfraOk")]
    partial class PbInfraOk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RedeSocial.Domain.Entities.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PerfilId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.HasIndex("PostId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Perfil", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Perfils_");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PerfilId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Relacionamento", b =>
                {
                    b.Property<Guid>("PerfilIdA")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PerfilIdB")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PerfilIdA", "PerfilIdB");

                    b.HasIndex("PerfilIdB");

                    b.ToTable("Relacionamentos");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Comentario", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedeSocial.Domain.Entities.Post", "Post")
                        .WithMany("Comentarios")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Perfil");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Perfil", "Perfil")
                        .WithMany("Posts")
                        .HasForeignKey("PerfilId");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Relacionamento", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Perfil", "PerfilA")
                        .WithMany("RelacionamentosB")
                        .HasForeignKey("PerfilIdA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedeSocial.Domain.Entities.Perfil", "PerfilB")
                        .WithMany("RelacionamentosA")
                        .HasForeignKey("PerfilIdB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PerfilA");

                    b.Navigation("PerfilB");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Perfil", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("RelacionamentosA");

                    b.Navigation("RelacionamentosB");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
