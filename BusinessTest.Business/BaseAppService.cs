using BusinessTest.Data.Repositories;

namespace BusinessTest.Business
{
    public class BaseAppService
    {
        protected internal IUnitOfWork UnitOfWork { get; set; }
        public BaseAppService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}