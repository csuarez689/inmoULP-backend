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
                    dni = table.Column<string>(type: "varchar(15)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                    dni = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
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
                    latitud = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitud = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo_id = table.Column<int>(type: "int", nullable: false),
                    uso_id = table.Column<int>(type: "int", nullable: false),
                    propietario_id = table.Column<int>(type: "int", nullable: false),
                    disponible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inmuebles", x => x.id);
                    table.ForeignKey(
                        name: "FK_inmuebles_propietarios_propietario_id",
                        column: x => x.propietario_id,
                        principalTable: "propietarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inmuebles_tipos_inmueble_tipo_id",
                        column: x => x.tipo_id,
                        principalTable: "tipos_inmueble",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inmuebles_usos_inmueble_uso_id",
                        column: x => x.uso_id,
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
                    inmueble_id = table.Column<int>(type: "int", nullable: false),
                    inquilino_id = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contratos", x => x.id);
                    table.ForeignKey(
                        name: "FK_contratos_inmuebles_inmueble_id",
                        column: x => x.inmueble_id,
                        principalTable: "inmuebles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contratos_inquilinos_inquilino_id",
                        column: x => x.inquilino_id,
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
                    inmueble_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagenes_inmuebles", x => x.id);
                    table.ForeignKey(
                        name: "FK_imagenes_inmuebles_inmuebles_inmueble_id",
                        column: x => x.inmueble_id,
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
                    contrato_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pagos_contratos_contrato_id",
                        column: x => x.contrato_id,
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
                    { 1, true, "Gómez", "20123456", "carlos.gomez@example.com", "Carlos", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000001" },
                    { 2, true, "Fernández", "20987654", "maria.fernandez@example.com", "María", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000002" },
                    { 3, true, "Pérez", "19543210", "lucia.perez@example.com", "Lucía", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000003" },
                    { 4, true, "Rodríguez", "21456789", "javier.rodriguez@example.com", "Javier", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000004" },
                    { 5, true, "Lopez", "22345678", "ana.lopez@example.com", "Ana", "7WJ3b3aActlm6nINXM8wAEI1UCovzqb3l6epF7wdZ+c=", "2664000005" }
                });

            migrationBuilder.InsertData(
                table: "tipos_inmueble",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "Casa" },
                    { 2, "Departamento" },
                    { 3, "Local Comercial" },
                    { 4, "Oficina" }
                });

            migrationBuilder.InsertData(
                table: "usos_inmueble",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 1, "Residencial" },
                    { 2, "Comercial" }
                });

            migrationBuilder.InsertData(
                table: "inmuebles",
                columns: new[] { "id", "ambientes", "direccion", "disponible", "latitud", "longitud", "precio", "propietario_id", "superficie", "tipo_id", "uso_id" },
                values: new object[,]
                {
                    { 1, 4, "Av. España 102", true, "-33.296512", "-66.335421", 120000m, 1, 140, 1, 1 },
                    { 2, 3, "Belgrano 450", true, "-33.297845", "-66.333210", 98000m, 1, 95, 2, 1 },
                    { 3, 2, "Junín 780", false, "-33.298762", "-66.331567", 75000m, 1, 70, 2, 1 },
                    { 4, 5, "Mitre 1200", true, "-33.299843", "-66.336789", 145000m, 1, 180, 1, 1 },
                    { 5, 3, "Colón 350", false, "-33.301256", "-66.334123", 210000m, 1, 110, 3, 2 },
                    { 6, 4, "Buenos Aires 89", true, "-33.305612", "-66.337456", 152000m, 2, 160, 1, 1 },
                    { 7, 3, "Chacabuco 560", true, "-33.304123", "-66.332345", 99000m, 2, 100, 2, 1 },
                    { 8, 2, "San Martín 910", false, "-33.302981", "-66.329876", 82000m, 2, 75, 2, 1 },
                    { 9, 5, "Rivadavia 2100", false, "-33.306745", "-66.340012", 165000m, 2, 200, 1, 1 },
                    { 10, 4, "Pringles 45", true, "-33.303567", "-66.327543", 230000m, 2, 150, 3, 2 },
                    { 11, 3, "Pueyrredón 320", true, "-33.312345", "-66.341234", 138000m, 3, 120, 1, 1 },
                    { 12, 2, "Ituzaingó 654", true, "-33.310234", "-66.338765", 87000m, 3, 68, 2, 1 },
                    { 13, 4, "Catamarca 870", false, "-33.311789", "-66.336543", 149000m, 3, 140, 1, 1 },
                    { 14, 1, "Lafinur 120", true, "-33.309432", "-66.333210", 110000m, 3, 55, 4, 2 },
                    { 15, 3, "Ayacucho 410", false, "-33.308765", "-66.331098", 96000m, 3, 105, 2, 1 },
                    { 16, 4, "Independencia 1500", true, "-33.318765", "-66.346543", 155000m, 4, 150, 1, 1 },
                    { 17, 3, "Balcarce 620", true, "-33.316234", "-66.342101", 93000m, 4, 98, 2, 1 },
                    { 18, 2, "Los Puquios 45", false, "-33.320987", "-66.347890", 78000m, 4, 65, 2, 1 },
                    { 19, 5, "Illia 3450", false, "-33.317654", "-66.349321", 172000m, 4, 210, 1, 1 },
                    { 20, 4, "Ruta 3 Km 5", true, "-33.322345", "-66.351234", 240000m, 4, 190, 3, 2 },
                    { 21, 3, "Esteban Adaro 890", true, "-33.325678", "-66.353456", 142000m, 5, 115, 1, 1 },
                    { 22, 2, "Concarán 120", true, "-33.324321", "-66.356789", 88000m, 5, 82, 2, 1 },
                    { 23, 1, "Fraga 630", false, "-33.326543", "-66.358901", 105000m, 5, 50, 4, 2 },
                    { 24, 4, "El Trapiche 270", true, "-33.327890", "-66.360123", 158000m, 5, 160, 1, 1 },
                    { 25, 3, "Potrero de los Funes 410", false, "-33.329012", "-66.362345", 215000m, 5, 130, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "imagenes_inmuebles",
                columns: new[] { "id", "inmueble_id", "url" },
                values: new object[,]
                {
                    { 1, 1, "/uploads/inmuebles/test_casa.jpg" },
                    { 2, 2, "/uploads/inmuebles/test_casa.jpg" },
                    { 3, 3, "/uploads/inmuebles/test_casa.jpg" },
                    { 4, 4, "/uploads/inmuebles/test_casa.jpg" },
                    { 5, 5, "/uploads/inmuebles/test_casa.jpg" },
                    { 6, 6, "/uploads/inmuebles/test_casa.jpg" },
                    { 7, 7, "/uploads/inmuebles/test_casa.jpg" },
                    { 8, 8, "/uploads/inmuebles/test_casa.jpg" },
                    { 9, 9, "/uploads/inmuebles/test_casa.jpg" },
                    { 10, 10, "/uploads/inmuebles/test_casa.jpg" },
                    { 11, 11, "/uploads/inmuebles/test_casa.jpg" },
                    { 12, 12, "/uploads/inmuebles/test_casa.jpg" },
                    { 13, 13, "/uploads/inmuebles/test_casa.jpg" },
                    { 14, 14, "/uploads/inmuebles/test_casa.jpg" },
                    { 15, 15, "/uploads/inmuebles/test_casa.jpg" },
                    { 16, 16, "/uploads/inmuebles/test_casa.jpg" },
                    { 17, 17, "/uploads/inmuebles/test_casa.jpg" },
                    { 18, 18, "/uploads/inmuebles/test_casa.jpg" },
                    { 19, 19, "/uploads/inmuebles/test_casa.jpg" },
                    { 20, 20, "/uploads/inmuebles/test_casa.jpg" },
                    { 21, 21, "/uploads/inmuebles/test_casa.jpg" },
                    { 22, 22, "/uploads/inmuebles/test_casa.jpg" },
                    { 23, 23, "/uploads/inmuebles/test_casa.jpg" },
                    { 24, 24, "/uploads/inmuebles/test_casa.jpg" },
                    { 25, 25, "/uploads/inmuebles/test_casa.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contratos_inmueble_id",
                table: "contratos",
                column: "inmueble_id");

            migrationBuilder.CreateIndex(
                name: "IX_contratos_inquilino_id",
                table: "contratos",
                column: "inquilino_id");

            migrationBuilder.CreateIndex(
                name: "IX_imagenes_inmuebles_inmueble_id",
                table: "imagenes_inmuebles",
                column: "inmueble_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_propietario_id",
                table: "inmuebles",
                column: "propietario_id");

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_tipo_id",
                table: "inmuebles",
                column: "tipo_id");

            migrationBuilder.CreateIndex(
                name: "IX_inmuebles_uso_id",
                table: "inmuebles",
                column: "uso_id");

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
                name: "IX_pagos_contrato_id",
                table: "pagos",
                column: "contrato_id");

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
