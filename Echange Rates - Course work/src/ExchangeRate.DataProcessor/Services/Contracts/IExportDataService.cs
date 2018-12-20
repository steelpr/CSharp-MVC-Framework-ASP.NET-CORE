using System.Threading.Tasks;

namespace ExchangeRate.DataProcessor.Services.Contracts
{
    public interface IExportDataService
    {
        Task ExportCurrency();

        Task ExportHardware();

        Task ExportGame();

        Task ExportIt();
    }
}
