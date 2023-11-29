using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Views.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            Create View vm_PersonOrders
            As
            Select p.Name , COUNT(*) [Count] from Persons p
            Inner join Orders o on p.personId  = o.PersonId
            Group By p.Name
            order by [Count] Desc
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop View vm_PersonOrders");
        }
    }
}
