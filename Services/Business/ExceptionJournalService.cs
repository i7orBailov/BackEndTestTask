using BackEndTestTask.Models;
using BackEndTestTask.Models.Database;
using BackEndTestTask.Services.Interfaces;
using BackEndTestTask.Models.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BackEndTestTask.Services.Business
{
    public class ExceptionJournalService : IExceptionJournalService
    {
        private readonly IBaseRepository<ExceptionJournal> _repository;

        public ExceptionJournalService(IBaseRepository<ExceptionJournal> repository)
        {
            _repository = repository;
        }
        
        public async Task<ResponseMessage<ExceptionJournal>> GetSingleAsync(string eventId)
        {
            var journals = await _repository.GetFilteredAsync(filter: n => n.EventId == eventId);
            var journal = journals.SingleOrDefault();
            return new ResponseMessage<ExceptionJournal>(isSuccessful: true, data: journal);
        }

        public async Task<ResponseMessage<IEnumerable<ExceptionJournal>>> GetRangeAsync(int page, int pageSize)
        {
            if (page < 1)
                throw new SecureException($"Error: {nameof(page)} value can not be less than 1");

            var journals = await _repository.GetPagedAsync(page, pageSize);
            return new ResponseMessage<IEnumerable<ExceptionJournal>>(isSuccessful: true, data: journals);
        }
    }
}
