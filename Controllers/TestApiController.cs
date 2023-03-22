using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestApiCotroller : BaseController<TestApiCotroller>
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
