using Microsoft.AspNetCore.Mvc;
using Openings.Data;
using Openings.Data.Paging;
using Openings.Models;

namespace Openings.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OpeningsController : ControllerBase
{
    private readonly ILogger<OpeningsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OpeningsController(
        ILogger<OpeningsController> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IPaginate<Opening> Get()
    {
        return _unitOfWork.GetRepository<Opening>().GetList();
    }
}