using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestApiCotroller : ControllerBase
{

    public TestApiCotroller()
    {
        //
    }

    [HttpGet("GetInfo", Name = "GetInfo")]
    public string GetInfo()
    {
        return "Todo API Running. Version: 1.0. DateTime: " + DateTime.Now;
    }
}
