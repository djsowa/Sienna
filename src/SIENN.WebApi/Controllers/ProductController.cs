using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.Services.Command;
using SIENN.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SIENN.DbAccess.Repositories;
using SIENN.Services.ControllerServices;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseCrudController<ProductModel, ProductBaseModel, Product>
    {
        public ProductController(ICrudControllerService<ProductModel, ProductBaseModel, Product> crudControllerService) : base(crudControllerService)
        {

        }
    }
}
