using System.Collections.Generic;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Search
{
    public interface IProductSearchControllerService<TResultModel, TFilterModel>   where TResultModel : class
                                                                                   where TFilterModel : class
    {
        Task<GenericList<TResultModel>> GetAsync(TFilterModel filter);
        Task<GenericList<TResultModel>> GetAvailableAsync(int skip, int take);
        Product FromResultToEntity(TResultModel model, int id = 0);
        TResultModel FromEntityToResult(Product entity);
    }
}