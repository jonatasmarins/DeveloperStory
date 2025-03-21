using DeveloperStore.Domain.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.API.Controllers
{
    public class Controller : ControllerBase
    {
        public Controller()
        {
        }

        protected new IActionResult Response(IResultResponse resultResponse)
        {
            return Ok(resultResponse);
        }

        protected new IActionResult Response<T>(IResultResponse<T> resultResponse)
        {
            return Ok(resultResponse);
        }
    }
}
