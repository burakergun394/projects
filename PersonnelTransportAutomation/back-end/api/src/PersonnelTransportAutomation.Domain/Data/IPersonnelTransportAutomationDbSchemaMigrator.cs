using System.Threading.Tasks;

namespace PersonnelTransportAutomation.Data;

public interface IPersonnelTransportAutomationDbSchemaMigrator
{
    Task MigrateAsync();
}
