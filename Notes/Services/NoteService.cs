using Notes.Repositories;

namespace Notes.Services
{
    public class NoteService : INoteService
    {
        private NoteReporitory _reporitory;
        
        public NoteService()
        {
            _reporitory = new NoteReporitory();
        }
        public string FirstOneTitle()
        {
            return _reporitory.FirstOneTitle();
        }
    }
}