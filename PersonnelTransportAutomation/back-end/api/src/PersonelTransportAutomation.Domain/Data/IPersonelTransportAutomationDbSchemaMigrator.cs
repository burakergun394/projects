using System.Threading.Tasks;

namespace PersonelTransportAutomation.Data;

public interface IPersonelTransportAutomationDbSchemaMigrator
{
    Task MigrateAsync();
}
