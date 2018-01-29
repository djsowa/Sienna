using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Command;
using SIENN.Services.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;

namespace SIENN.Services.ControllerServices.Crud
{
    public abstract class GenericCrudControlerService<TResultModel, TRequestModel, TEntity> 
                        : ICrudControllerService<TResultModel, TRequestModel, TEntity>  where TEntity : BaseEntity
                                                                                        where TResultModel : class
                                                                                        where TRequestModel : class
    {
        protected readonly StoreDbContext Context;
        protected readonly IMapper Mapper;

        protected readonly GenericCrudCommand<TEntity> CrudCommand;
        public GenericCrudControlerService(StoreDbContext context, IMapper mapper, IGenericRepository<TEntity> repository)
        {
            Context = context;
            Mapper = mapper;
            CrudCommand = new GenericCrudCommand<TEntity>(Context, repository);
        }

        public virtual TResultModel FromEntityToResult(TEntity entity)
        {
            return Mapper.Map<TEntity, TResultModel>(entity);
        }

        public virtual TEntity FromRequestToEntity(TRequestModel model, int id = 0)
        {
            var entity = Mapper.Map<TRequestModel, TEntity>(model);

            if (id > 0)
                entity.Id = id;

            return entity;
        }

        public virtual TEntity FromResultToEntity(TResultModel model, int id = 0)
        {
            var entity = Mapper.Map<TResultModel, TEntity>(model);

            if (id > 0)
                entity.Id = id;

            return entity;
        }

        public virtual async Task<Tuple<int, TResultModel>> AddAsync(TRequestModel request)
        {
            var requestedEntity = FromRequestToEntity(request);
            int entityId = await CrudCommand?.AddAsync(requestedEntity);

            if (entityId == 0)
                return null;

            TEntity createdEntity = await CrudCommand.GetAsync(entityId);

            var result = new Tuple<int, TResultModel>(entityId, FromEntityToResult(createdEntity));

            return result;
        }

        public virtual async Task<TResultModel> GetAsync(int id)
        {
            var entity = await CrudCommand.GetAsync(id);

            if (entity == null)
                return null;

            return FromEntityToResult(entity);
        }

        public virtual async Task<GenericList<TResultModel>> List(int skip, int take)
        {
            var totalCount = await CrudCommand.CountAsync();

            if (totalCount == 0)
                return null;

            var entityList = await CrudCommand.GetRangeAsync(skip, take);

            if (entityList == null || !entityList.Any())
                return null;

            var result = new GenericList<TResultModel>();

            result.TotalCount = totalCount;

            result.Items.AddRange(entityList.Select(FromEntityToResult));

            return result;
        }

        public virtual async Task<bool> RemoveAsync(int id)
        {
            var entityToRemove = await CrudCommand.GetAsync(id);

            if (entityToRemove == null)
                return false;

            var wasDeleted = await CrudCommand.RemoveAsync(entityToRemove);

            return wasDeleted;
        }

        public virtual async Task<TResultModel> UpdateAsync(int id, TRequestModel request)
        {
            await CrudCommand.UpdateAsync(FromRequestToEntity(request, id));

            var updatedEntity = await CrudCommand.GetAsync(id);

            return FromEntityToResult(updatedEntity);
        }
    }
}