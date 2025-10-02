using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBook.Migrations
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
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    Genre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Books_Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Authors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Authors_AuthorId",
                table: "Books_Authors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Authors_BookId",
                table: "Books_Authors",
                column: "BookId");

            // =========================
            // ======= SEED DATA =======
            // =========================

            // Publishers (10)
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NXB Trẻ" },
                    { 2, "NXB Giáo Dục" },
                    { 3, "NXB Kim Đồng" },
                    { 4, "NXB Văn Học" },
                    { 5, "NXB Hội Nhà Văn" },
                    { 6, "NXB Lao Động" },
                    { 7, "NXB Khoa Học" },
                    { 8, "NXB Thanh Niên" },
                    { 9, "NXB Chính Trị" },
                    { 10, "NXB Phụ Nữ" }
                });

            // Authors (10)
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Nguyễn Nhật Ánh" },
                    { 2, "Tô Hoài" },
                    { 3, "Nam Cao" },
                    { 4, "Nguyễn Du" },
                    { 5, "Xuân Quỳnh" },
                    { 6, "Nguyễn Minh Châu" },
                    { 7, "Kim Lân" },
                    { 8, "Vũ Trọng Phụng" },
                    { 9, "Nguyễn Huy Thiệp" },
                    { 10, "Hồ Xuân Hương" }
                });

            // Users (2)
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin", "123456", "Admin" },
                    { 2, "user1", "123456", "User" }
                });

            // Sử dụng mốc thời gian cố định cho DateAdded/DateRead
            var seedDate = new DateTime(2025, 9, 29, 0, 0, 0, DateTimeKind.Utc);

            // Books (10)
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Title", "Description", "IsRead", "DateRead", "Rate", "Genre", "CoverUrl", "DateAdded", "PublisherID" },
                values: new object[,]
                {
                    { 1, "Cho tôi xin một vé đi tuổi thơ", null, true, seedDate, 5, "Tiểu thuyết", null, seedDate, 1 },
                    { 2, "Dế Mèn phiêu lưu ký", null, true, seedDate, 5, "Thiếu nhi", null, seedDate, 3 },
                    { 3, "Chí Phèo", null, true, seedDate, 5, "Hiện thực", null, seedDate, 4 },
                    { 4, "Truyện Kiều", null, true, seedDate, 5, "Thơ", null, seedDate, 2 },
                    { 5, "Thuyền ngoài xa", null, false, null, null, "Truyện ngắn", null, seedDate, 5 },
                    { 6, "Vợ nhặt", null, true, seedDate, 4, "Hiện thực", null, seedDate, 1 },
                    { 7, "Số đỏ", null, false, null, null, "Trào phúng", null, seedDate, 6 },
                    { 8, "Tắt đèn", null, true, seedDate, 4, "Hiện thực", null, seedDate, 7 },
                    { 9, "Lão Hạc", null, true, seedDate, 5, "Hiện thực", null, seedDate, 8 },
                    { 10, "Hồn Trương Ba, da hàng thịt", null, true, seedDate, 5, "Kịch", null, seedDate, 9 }
                });

            // Books_Authors (10 liên kết)
            migrationBuilder.InsertData(
                table: "Books_Authors",
                columns: new[] { "Id", "BookId", "AuthorId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 6 },
                    { 6, 6, 7 },
                    { 7, 7, 8 },
                    { 8, 8, 3 },
                    { 9, 9, 3 },
                    { 10, 10, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xoá dữ liệu seed theo thứ tự khoá ngoại
            for (var i = 1; i <= 10; i++)
            {
                migrationBuilder.DeleteData(table: "Books_Authors", keyColumn: "Id", keyValue: i);
            }

            for (var i = 1; i <= 10; i++)
            {
                migrationBuilder.DeleteData(table: "Books", keyColumn: "Id", keyValue: i);
            }

            migrationBuilder.DeleteData(table: "Users", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Users", keyColumn: "Id", keyValue: 2);

            for (var i = 1; i <= 10; i++)
            {
                migrationBuilder.DeleteData(table: "Authors", keyColumn: "Id", keyValue: i);
            }

            for (var i = 1; i <= 10; i++)
            {
                migrationBuilder.DeleteData(table: "Publishers", keyColumn: "Id", keyValue: i);
            }

            migrationBuilder.DropTable(
                name: "Books_Authors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
