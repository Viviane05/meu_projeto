using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace meu_projeto.Web.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela já existe antes de criar
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Countries')
                BEGIN
                    CREATE TABLE [Countries] (
                        [Id] int NOT NULL IDENTITY(1,1),
                        [Name] nvarchar(50) NOT NULL,
                        CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
                    );
                END
            ");

            // Se precisar adicionar dados iniciais (opcional)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Countries)
                BEGIN
                    INSERT INTO Countries (Name) VALUES 
                    ('Brasil'),
                    ('Estados Unidos'),
                    ('Canadá');
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Opção segura para remoção (se necessário)
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Countries')
                BEGIN
                    DROP TABLE [Countries];
                END
            ");
        }
    }
}