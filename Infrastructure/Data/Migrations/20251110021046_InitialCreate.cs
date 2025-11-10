using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InmobiliariaAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inquilinos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inquilinos", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "propietarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_propietarios", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipos_inmueble",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_inmueble", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usos_inmueble",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usos_inmueble", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inmuebles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    direccion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ambientes = table.Column<int>(type: "int", nullable: false),
                    superficie = table.Column<int>(type: "int", nullable: false),
                    latitud = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    longitud = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    id_tipo = table.Column<int>(type: "int", nullable: false),
                    id_uso = table.Column<int>(type: "int", nullable: false),
                    disponible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_propietario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inmuebles", x => x.id);
                    table.ForeignKey(
                        name: "FK_inmuebles_propietarios_id_propietario",
                        column: x => x.id_propietario,
                        principalTable: "propietarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inmuebles_tipos_inmueble_id_tipo",
                        column: x => x.id_tipo,
                        principalTable: "tipos_inmueble",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inmuebles_usos_inmueble_id_uso",
                        column: x => x.id_uso,
                        principalTable: "usos_inmueble",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contratos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_finalizacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    monto_alquiler = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_inmueble = table.Column<int>(type: "int", nullable: false),
                    id_inquilino = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contratos", x => x.id);
                    table.ForeignKey(
                        name: "FK_contratos_inmuebles_id_inmueble",
                        column: x => x.id_inmueble,
                        principalTable: "inmuebles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contratos_inquilinos_id_inquilino",
                        column: x => x.id_inquilino,
                        principalTable: "inquilinos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imagenes_inmuebles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    url = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_inmueble = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagenes_inmuebles", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagenes_inmuebles_inmuebles_id_inmueble",
                        column: x => x.id_inmueble,
                        principalTable: "inmuebles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    detalle = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_pago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_contrato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pagos_contratos_id_contrato",
                        column: x => x.id_contrato,
                        principalTable: "contratos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "propietarios",
                columns: new[] { "id", "activo", "apellido", "dni", "email", "nombre", "password", "telefono" },
                values: new object[,]
                {
                    { 1, true, "Gómez", 20123456, "carlos.gomez@example.com", "Carlos", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000001" },
                    { 2, true, "Fernández", 20987654, "maria.fernandez@example.com", "María", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000002" },
                    { 3, true, "Pérez", 19543210, "lucia.perez@example.com", "Lucía", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000003" },
                    { 4, true, "Rodríguez", 21456789, "javier.rodriguez@example.com", "Javier", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000004" },
                    { 5, true, "Lopez", 22345678, "ana.lopez@example.com", "Ana", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000005" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contratos_id_inmueble",
                table: "contratos",
                column: "id_inmueble");

            migrationBuilder.CreateIndex(
                name: "IX_contratos_id_inquilino",
                table: "contratos",
                column: "id_inquilino");

            migrationBuilder.CreateIndex(
                name: "IX_imagenes_inmuebles_id_inmueble",
                table: "imagenes_inmuebles",
                column: "id_inmueble");

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_id_propietario",
                table: "inmuebles",
                column: "id_propietario");

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_id_tipo",
                table: "inmuebles",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_id_uso",
                table: "inmuebles",
                column: "id_uso");

            migrationBuilder.CreateIndex(
                name: "IX_inquilinos_dni",
                table: "inquilinos",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inquilinos_email",
                table: "inquilinos",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pagos_id_contrato",
                table: "pagos",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "IX_propietarios_dni",
                table: "propietarios",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_propietarios_email",
                table: "propietarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagenes_inmuebles");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "contratos");

            migrationBuilder.DropTable(
                name: "inmuebles");

            migrationBuilder.DropTable(
                name: "inquilinos");

            migrationBuilder.DropTable(
                name: "propietarios");

            migrationBuilder.DropTable(
                name: "tipos_inmueble");

            migrationBuilder.DropTable(
                name: "usos_inmueble");
        }
    }
}
