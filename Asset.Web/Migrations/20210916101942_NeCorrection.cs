using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Asset.Web.Migrations
{
    public partial class NeCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "MyEarning",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueAmount = table.Column<decimal>(nullable: false),
                    ValueDate = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Corpid = table.Column<int>(nullable: false),
                    CorpEarningid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEarning", x => x.id);
                    table.ForeignKey(
                        name: "FK_MyEarning_OurCorpEarningSetup_CorpEarningid",
                        column: x => x.CorpEarningid,
                        principalTable: "OurCorpEarningSetup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MyEarning_corpRegs_Corpid",
                        column: x => x.Corpid,
                        principalTable: "corpRegs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            
    
            


           

          

            migrationBuilder.CreateIndex(
                name: "IX_MyEarning_CorpEarningid",
                table: "MyEarning",
                column: "CorpEarningid");

            migrationBuilder.CreateIndex(
                name: "IX_MyEarning_Corpid",
                table: "MyEarning",
                column: "Corpid");

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivationMail");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoleMenuPermission");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssetLiabilityView");

            migrationBuilder.DropTable(
                name: "AssetNumber");

            migrationBuilder.DropTable(
                name: "AssetTypes");

            migrationBuilder.DropTable(
                name: "CorpEarningSetUp");

            migrationBuilder.DropTable(
                name: "EarninWeights");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "KopAssets");

            migrationBuilder.DropTable(
                name: "LGA");

            migrationBuilder.DropTable(
                name: "Liabilities");

            migrationBuilder.DropTable(
                name: "LiabilityTypes");

            migrationBuilder.DropTable(
                name: "MyEarning");

            migrationBuilder.DropTable(
                name: "ServiceTbl");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "AspNetNavigationMenu");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OurCorpEarningSetup");

            migrationBuilder.DropTable(
                name: "AverageMaintanableEarningsWeighteds");

            migrationBuilder.DropTable(
                name: "corpRegs");
        }
    }
}
