using Microsoft.EntityFrameworkCore.Migrations;

namespace NicamalWebApi.Migrations
{
    public partial class _24062021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "30636762-60ac-46b1-b462-0fb5daafaf71");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "54e11b40-f776-4712-9c2f-a8b2ac19b8fc");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "7a5fd117-8967-4d5a-aaf3-743c69c5645b");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "8284543b-303c-4430-b4f9-8d46eee25665");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "d096a7e9-0e4a-4b6b-aff7-78357f7ea86f");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "eeca8313-0a38-49a9-b19c-665a93096241");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { "dcc9262f-58b4-4b99-827b-c2cdd1dda9ef", "https://192.168.1.136:5001/usersprofile/dog.png", "dog" },
                    { "a2131ecd-adf4-49d5-a39e-8e82aed4e600", "https://192.168.1.136:5001/usersprofile/cat.png", "cat" },
                    { "8a3345de-b204-4ba6-a243-642b820a8cdf", "https://192.168.1.136:5001/usersprofile/parrot.png", "parrot" },
                    { "0b1ef7ea-be99-40d9-a547-5df8e3db344d", "https://192.168.1.136:5001/usersprofile/rabbit.png", "rabbit" },
                    { "97dc20a3-7964-4113-8e44-c59bc66f76ec", "https://192.168.1.136:5001/usersprofile/panda.png", "panda" },
                    { "db64b274-7374-4858-b9f5-1fb763764d45", "https://192.168.1.136:5001/usersprofile/fish.png", "fish" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "0b1ef7ea-be99-40d9-a547-5df8e3db344d");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "8a3345de-b204-4ba6-a243-642b820a8cdf");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "97dc20a3-7964-4113-8e44-c59bc66f76ec");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "a2131ecd-adf4-49d5-a39e-8e82aed4e600");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "db64b274-7374-4858-b9f5-1fb763764d45");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "dcc9262f-58b4-4b99-827b-c2cdd1dda9ef");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { "30636762-60ac-46b1-b462-0fb5daafaf71", "https://192.168.1.136:5001/usersprofile/dog.png", "dog" },
                    { "7a5fd117-8967-4d5a-aaf3-743c69c5645b", "https://192.168.1.136:5001/usersprofile/cat.png", "cat" },
                    { "54e11b40-f776-4712-9c2f-a8b2ac19b8fc", "https://192.168.1.136:5001/usersprofile/parrot.png", "parrot" },
                    { "d096a7e9-0e4a-4b6b-aff7-78357f7ea86f", "https://192.168.1.136:5001/usersprofile/rabbit.png", "rabbit" },
                    { "8284543b-303c-4430-b4f9-8d46eee25665", "https://192.168.1.136:5001/usersprofile/panda.png", "panda" },
                    { "eeca8313-0a38-49a9-b19c-665a93096241", "https://192.168.1.136:5001/usersprofile/fish.png", "fish" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Users_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
