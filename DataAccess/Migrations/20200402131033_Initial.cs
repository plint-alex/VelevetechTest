using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sex = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 40, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 60, nullable: false),
                    Uid = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RefreshToken = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Sex",
                table: "Students",
                column: "Sex");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Uid",
                table: "Students",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login");


            //migrationBuilder.Sql(
            //  "CREATE FULLTEXT INDEX ON [dbo].[Students](FirstName, LastName, MiddleName) " +
            //  "KEY INDEX PK_Students ON(TextCatalog) " +
            //  "WITH(CHANGE_TRACKING AUTO) ", true);

            migrationBuilder.InsertData(
                           schema: "dbo",
                           table: "Users",
                           columns: new[] { "Id", "Login", "Password" },
                           values: new object[] { "00000000-0000-0000-0000-000000000001", "admin", MD5Hash("admin") });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("DROP FULLTEXT INDEX ON [dbo].[Students]", true);

            migrationBuilder.DeleteData(
            schema: "dbo",
            table: "Users",
            keyColumn: "Id",
            keyValue: "00000000-0000-0000-0000-000000000001");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Users");
        }

        private static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
