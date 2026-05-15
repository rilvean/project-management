using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManagement.Domain.Enums;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:project_priority", "medium,low,high")
                .Annotation("Npgsql:Enum:project_status", "active,completed")
                .Annotation("Npgsql:Enum:user_role", "employee,project_manager,admin,supervisor")
                .Annotation("Npgsql:Enum:work_task_status", "active,completed");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    manager_id = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    priority = table.Column<ProjectPriority>(type: "project_priority", nullable: false),
                    status = table.Column<ProjectStatus>(type: "project_status", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<UserRole>(type: "user_role", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_executors",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_executors", x => new { x.project_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_project_executors_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_executors_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    deadline = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: true),
                    status = table.Column<WorkTaskStatus>(type: "work_task_status", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    executor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(0) with time zone", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_tasks_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_tasks_users_executor_id",
                        column: x => x.executor_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_project_executors_user_id",
                table: "project_executors",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_manager_id_title",
                table: "projects",
                columns: new[] { "manager_id", "title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_work_tasks_executor_id",
                table: "work_tasks",
                column: "executor_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_tasks_project_id_title",
                table: "work_tasks",
                columns: new[] { "project_id", "title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_executors");

            migrationBuilder.DropTable(
                name: "work_tasks");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
