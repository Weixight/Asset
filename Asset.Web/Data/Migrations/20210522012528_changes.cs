using Microsoft.EntityFrameworkCore.Migrations;

namespace Asset.Web.Data.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LGA",
                columns: table => new
                {
                    lga_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lga_name = table.Column<string>(nullable: true),
                    state_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGA", x => x.lga_id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    state_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    state_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.state_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LGA");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
