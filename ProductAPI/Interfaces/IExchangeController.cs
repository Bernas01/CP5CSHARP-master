using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiConversion.Interfaces
{
    public interface IExchangeController
    {
        Task<JsonResult> GetExchangeRate();
    }
}