using Microsoft.EntityFrameworkCore.Migrations;

namespace NicamalWebApi.Migrations
{
    public partial class _14062021_2109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "303f6a44-8c3d-4a8b-9887-0cdb42650225");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "3d1d371f-cb2a-4abd-af27-26ad5ef1bcb8");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "468b0c87-cb18-4212-832d-d675cd4c3ac0");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "63d1d7a3-d803-438b-a882-fa7ae39a8e4f");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "6fc84df8-4968-4f20-96c0-41be4ffd5f75");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: "769a8853-b48e-4120-b0a7-dbf9544c4068");

            migrationBuilder.AddColumn<string>(
                name: "Applied",
                table: "Sanctions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Applied",
                table: "Sanctions");

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { "3d1d371f-cb2a-4abd-af27-26ad5ef1bcb8", "https://192.168.1.136:5001/usersprofile/dog.png", "dog" },
                    { "303f6a44-8c3d-4a8b-9887-0cdb42650225", "https://192.168.1.136:5001/usersprofile/cat.png", "cat" },
                    { "769a8853-b48e-4120-b0a7-dbf9544c4068", "https://192.168.1.136:5001/usersprofile/parrot.png", "parrot" },
                    { "468b0c87-cb18-4212-832d-d675cd4c3ac0", "https://192.168.1.136:5001/usersprofile/rabbit.png", "rabbit" },
                    { "63d1d7a3-d803-438b-a882-fa7ae39a8e4f", "https://192.168.1.136:5001/usersprofile/panda.png", "panda" },
                    { "6fc84df8-4968-4f20-96c0-41be4ffd5f75", "https://192.168.1.136:5001/usersprofile/fish.png", "fish" }
                });
        }
    }
}
