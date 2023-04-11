using BackEndTestTask.Models.Database;
using BackEndTestTask.Models;

namespace BackEndTestTask.Services.Interfaces
{
    public interface IExceptionJournalService
    {
        Task<ResponseMessage<ExceptionJournal>> GetSingleAsync(string eventId);
        Task<ResponseMessage<IEnumerable<ExceptionJournal>>> GetRangeAsync(int page, int pageSize);
    }
}
