using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iitu.web.example.Migrations
{
    public partial class dataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Author", "CreatedAt", "Genre", "Name", "Poster" },
                values: new object[] { -1, "Todd Phillips", new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crime , Drama , Thriller", "Joker", "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/joker_dabf394a-d4f2-4b68-90e2-011ed6b54012_poster.png?d=270x360&q=20" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Author", "CreatedAt", "Genre", "Name", "Poster" },
                values: new object[] { -2, "David Leitch", new DateTime(2019, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action , Adventure", "Fast & Furious Presents: Hobbs & Shaw", "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/fast-furious-presents-hobbs-shaw_14d1ab4f-c90c-46d1-9e04-f7d69f285ebd_poster.png?d=270x360&q=20" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Author", "CreatedAt", "Genre", "Name", "Poster" },
                values: new object[] { -3, "Jon Favreau", new DateTime(2019, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure , Animation , Drama , Family , Musical", "The Lion King", "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/the-lion-king_3904aadc-3a07-4836-892f-763b2dfdeea3_poster.png?d=270x360&q=20" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Author", "CreatedAt", "Genre", "Name", "Poster" },
                values: new object[] { -4, "Joachim Rønning", new DateTime(2019, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure , Family , Fantasy", "Maleficent: Mistress of Evil", "https://dz7u9q3vpd4eo.cloudfront.net/admin-uploads/posters/mxt_movies_poster/maleficent-mistress-of-evil_c8507e61-a6b3-404d-b8c5-df6f74bc62be_poster.png?d=270x360&q=20" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
