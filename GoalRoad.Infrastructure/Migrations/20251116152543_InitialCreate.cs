using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalRoad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GR_CATEGORIA",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescricaoCategoria = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_CATEGORIA", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "GR_ENDERECO",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_ENDERECO", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "GR_TECNOLOGIA",
                columns: table => new
                {
                    IdTecnologia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTecnologia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescricaoTecnologia = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LogoTecnologia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_TECNOLOGIA", x => x.IdTecnologia);
                });

            migrationBuilder.CreateTable(
                name: "GR_CARREIRA",
                columns: table => new
                {
                    IdCarreira = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TituloCarreira = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescricaoCarreira = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LogoCarreira = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IdCategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_CARREIRA", x => x.IdCarreira);
                    table.ForeignKey(
                        name: "FK_GR_CARREIRA_GR_CATEGORIA_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "GR_CATEGORIA",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "GR_USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEmUsuario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEndereco = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_USUARIO", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_GR_USUARIO_GR_ENDERECO_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "GR_ENDERECO",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "GR_ROADMAP",
                columns: table => new
                {
                    IdCarreira = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_ROADMAP", x => x.IdCarreira);
                    table.ForeignKey(
                        name: "FK_GR_ROADMAP_GR_CARREIRA_IdCarreira",
                        column: x => x.IdCarreira,
                        principalTable: "GR_CARREIRA",
                        principalColumn: "IdCarreira",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GR_FEED",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_FEED", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_GR_FEED_GR_USUARIO_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "GR_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GR_ROADMAP_TECNOLOGIA",
                columns: table => new
                {
                    IdRoadMap = table.Column<int>(type: "int", nullable: false),
                    IdTecnologia = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_ROADMAP_TECNOLOGIA", x => new { x.IdRoadMap, x.IdTecnologia });
                    table.ForeignKey(
                        name: "FK_GR_ROADMAP_TECNOLOGIA_GR_ROADMAP_IdRoadMap",
                        column: x => x.IdRoadMap,
                        principalTable: "GR_ROADMAP",
                        principalColumn: "IdCarreira",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GR_ROADMAP_TECNOLOGIA_GR_TECNOLOGIA_IdTecnologia",
                        column: x => x.IdTecnologia,
                        principalTable: "GR_TECNOLOGIA",
                        principalColumn: "IdTecnologia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GR_FEED_ITEM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    TipoItem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FonteId = table.Column<int>(type: "int", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdTecnologia = table.Column<int>(type: "int", nullable: true),
                    Relevancia = table.Column<double>(type: "float", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GR_FEED_ITEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GR_FEED_ITEM_GR_FEED_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "GR_FEED",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GR_FEED_ITEM_GR_TECNOLOGIA_IdTecnologia",
                        column: x => x.IdTecnologia,
                        principalTable: "GR_TECNOLOGIA",
                        principalColumn: "IdTecnologia",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GR_CARREIRA_IdCategoria",
                table: "GR_CARREIRA",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_GR_FEED_ITEM_IdTecnologia",
                table: "GR_FEED_ITEM",
                column: "IdTecnologia");

            migrationBuilder.CreateIndex(
                name: "IX_GR_FEED_ITEM_IdUsuario",
                table: "GR_FEED_ITEM",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_GR_ROADMAP_TECNOLOGIA_IdRoadMap_StepOrder",
                table: "GR_ROADMAP_TECNOLOGIA",
                columns: new[] { "IdRoadMap", "StepOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_GR_ROADMAP_TECNOLOGIA_IdTecnologia",
                table: "GR_ROADMAP_TECNOLOGIA",
                column: "IdTecnologia");

            migrationBuilder.CreateIndex(
                name: "IX_GR_USUARIO_IdEndereco",
                table: "GR_USUARIO",
                column: "IdEndereco",
                unique: true,
                filter: "[IdEndereco] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GR_FEED_ITEM");

            migrationBuilder.DropTable(
                name: "GR_ROADMAP_TECNOLOGIA");

            migrationBuilder.DropTable(
                name: "GR_FEED");

            migrationBuilder.DropTable(
                name: "GR_ROADMAP");

            migrationBuilder.DropTable(
                name: "GR_TECNOLOGIA");

            migrationBuilder.DropTable(
                name: "GR_USUARIO");

            migrationBuilder.DropTable(
                name: "GR_CARREIRA");

            migrationBuilder.DropTable(
                name: "GR_ENDERECO");

            migrationBuilder.DropTable(
                name: "GR_CATEGORIA");
        }
    }
}
