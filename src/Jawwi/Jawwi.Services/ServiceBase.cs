using Jawwi.DB;
using Jawwi.Models;
using System;
using System.Linq;

namespace Jawwi.Services
{
    public class ServiceBase
    {
        protected readonly DataContext _dataContext;


        protected ServiceBase(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
