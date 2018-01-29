using System.Collections.Generic;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Search
{
    public interface IStoreControllerService<TViewModel, TViewFilterModel, TEntity>   where TEntity : BaseEntity
                                                                                      where TViewModel : class
                                                                                      where TViewFilterModel : class
    {
        Task<GenericList<TViewModel>> GetView(TViewFilterModel filter);
        TEntity FromViewResultToEntity(TViewModel model, int id = 0);
        TViewModel FromEntityToViewResult(TEntity entity);
    }
}