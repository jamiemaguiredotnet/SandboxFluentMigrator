using Common;
using FluentMigrator;

namespace SandboxMigrations.Migrations._2023._02
{
    [CustomMigration(0007, 2023, 02, 18, 08, 00, "Jamie Maguire")]
    public class M0007_Sandbox_AlterUserAddAge : Migration
    {
        public override void Up()
        {
            Execute.Script(Directory.GetCurrentDirectory() + @"\..\..\..\..\SandboxMigrations\Scripts\2023\02\0007\AlterUserAddAge.SQL");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
