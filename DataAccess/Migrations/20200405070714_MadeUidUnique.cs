using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class MadeUidUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Uid",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Uid",
                table: "Students",
                column: "Uid",
                unique: true,
                filter: "[Uid] IS NOT NULL");

            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "taaaFn", "saaaLn", "raaaMn", "{00000000-0000-0000-0000-000000000001}", "Female", "2111111" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "sbbbFn", "abbbLn", "qbbbMn", "{00000000-0000-0000-0000-000000000002}", "Male", "5222222" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "acccFn", "qcccLn", "scccMn", "{00000000-0000-0000-0000-000000000003}", "Male", "4333333" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "qdddFn", "paaaLn", "pdddMn", "{00000000-0000-0000-0000-000000000004}", "Female", "3444444" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "peeeFn", "oeeeLn", "oeeeMn", "{00000000-0000-0000-0000-000000000005}", "Female", "1555555" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "offfFn", "nfffLn", "tfffMn", "{00000000-0000-0000-0000-000000000006}", "Male", "6666666" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "fgggFn", "mgggLn", "agggMn", "{00000000-0000-0000-0000-000000000007}", "Female", "8777777" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "ehhhFn", "khhhLn", "bhhhMn", "{00000000-0000-0000-0000-000000000008}", "Female", "9888888" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "diiiFn", "jiiiLn", "ciiiMn", "{00000000-0000-0000-0000-000000000009}", "Male", "7999999" });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { true, "cjjjFn", "ijjjLn", "djjjMn", "{00000000-0000-0000-0000-000000000010}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "bkkkFn", "hkkkLn", "ekkkMn", "{00000000-0000-0000-0000-000000000011}", "Female", null });

            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "alllFn", "glllLn", "nlllMn", "{00000000-0000-0000-0000-000000000012}", "Female", null });

            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "kmmmFn", "fmmmLn", "fmmmMn", "{00000000-0000-0000-0000-000000000013}", "Male", null });

            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "jnnnFn", "ennnLn", "mnnnMn", "{00000000-0000-0000-0000-000000000014}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { true, "ioooFn", "doooLn", "goooMn", "{00000000-0000-0000-0000-000000000015}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "hpppFn", "cpppLn", "hpppMn", "{00000000-0000-0000-0000-000000000016}", "Male", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "gqqqFn", "tqqqLn", "lqqqMn", "{00000000-0000-0000-0000-000000000017}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "nrrrFn", "brrrLn", "irrrMn", "{00000000-0000-0000-0000-000000000018}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "msssFn", "asssLn", "ksssMn", "{00000000-0000-0000-0000-000000000019}", "Female", null });
            
            migrationBuilder.InsertData(
                          schema: "dbo",
                          table: "Students",
                          columns: new[] { "Deleted", "FirstName", "LastName", "MiddleName", "Id", "Sex", "Uid" },
                          values: new object[] { false, "ltttFn", "ltttLn", "jtttMn", "{00000000-0000-0000-0000-000000000020}", "Male", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for(var i = 1; i <= 20; i++)
            {
                var stringId = i.ToString("D2");
                migrationBuilder.DeleteData(
                   schema: "dbo",
                   table: "Students",
                   keyColumn: "Id",
                   keyValue: $"00000000-0000-0000-0000-0000000000{stringId}");
            }

            migrationBuilder.DropIndex(
                name: "IX_Students_Uid",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Uid",
                table: "Students",
                column: "Uid");
        }
    }
}
