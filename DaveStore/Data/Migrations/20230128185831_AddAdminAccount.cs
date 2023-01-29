using DaveStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaveStore.Data.Migrations
{
    public partial class AddAdminAccount : Migration
    {
        private const string ADMIN_USER_GUID = "828d77a5-bbd9-4040-b75c-563ba59bf844";
        private const string ADMIN_ROLE_GUID = "ce362e35-f5f1-4325-804d-ac705718ab44";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var passwordHash = hasher.HashPassword(null, "Password100!");

            migrationBuilder.Sql(@$"INSERT INTO AspNetUsers(Id, UserName, NormalizedUserName,Email,EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount,NormalizedEmail,PasswordHash,SecurityStamp,FirstName)
            VALUES('{ADMIN_USER_GUID}', 'admin@davestore.com', 'ADMIN@DAVESTORE.COM', 'admin@davestore.com', 0, 0, 0, 0, 0, 'ADMIN@DAVESTORE.COM', '{passwordHash}', '', 'Admin')");

            migrationBuilder.Sql(
                $"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");

            migrationBuilder.Sql(
                $"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}', '{ADMIN_ROLE_GUID}')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId = '{ADMIN_ROLE_GUID}'");
            migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");
            migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");
        }
    }
}