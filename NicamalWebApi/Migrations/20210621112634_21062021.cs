using Microsoft.EntityFrameworkCore.Migrations;

namespace NicamalWebApi.Migrations
{
    public partial class _21062021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "6f8cd594-11b2-44cd-b6f2-70ac47ab425e");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "90f11f76-2f31-4f93-9b68-597a26ccc210");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "919407aa-c5c2-4d87-9dd8-34c627882df8");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "ab94b6c4-e740-457f-8a75-1ff9f3ba38ca");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "aeb56286-e331-4f57-a04f-e8778200e2c2");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "c9aa9b76-8609-46e8-bb0d-1970bdaaa39d");

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UrlDonation",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "History",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UrlDonation",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { "90f11f76-2f31-4f93-9b68-597a26ccc210", "https://192.168.1.136:5001/usersprofile/dog.png", "dog" },
                    { "6f8cd594-11b2-44cd-b6f2-70ac47ab425e", "https://192.168.1.136:5001/usersprofile/cat.png", "cat" },
                    { "aeb56286-e331-4f57-a04f-e8778200e2c2", "https://192.168.1.136:5001/usersprofile/parrot.png", "parrot" },
                    { "ab94b6c4-e740-457f-8a75-1ff9f3ba38ca", "https://192.168.1.136:5001/usersprofile/rabbit.png", "rabbit" },
                    { "c9aa9b76-8609-46e8-bb0d-1970bdaaa39d", "https://192.168.1.136:5001/usersprofile/panda.png", "panda" },
                    { "919407aa-c5c2-4d87-9dd8-34c627882df8", "https://192.168.1.136:5001/usersprofile/fish.png", "fish" }
                });
        }
    }
}
