using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;
using SIENN.Services.Models;

namespace SIENN.Services.ControllerServices.Crud
{
    public interface ICrudControllerService<TResultModel, TRequestModel, TEntity> where TEntity : BaseEntity
                                                                                  where TResultModel : class
                                                                                  where TRequestModel : class
    {
        Task<TResultModel> GetAsync(int id);
        Task<GenericList<TResultModel>> List(int skip, int take);
        Task<Tuple<int, TResultModel>> AddAsync(TRequestModel request);
        Task<TResultModel> UpdateAsync(int id, TRequestModel request);
        Task<bool> RemoveAsync(int id);
        TEntity FromResultToEntity(TResultModel model, int id = 0);
        TEntity FromRequestToEntity(TRequestModel model, int id = 0);
        TResultModel FromEntityToResult(TEntity entity);
    }
}