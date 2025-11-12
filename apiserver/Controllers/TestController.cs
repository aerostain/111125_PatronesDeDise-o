using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shared.Mydata;

namespace apiserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Test(
            Person nperson,
            ILogger<TestController> logger,
            CancellationToken cancellationToken
            )
        {
            try
            {
                if (nperson.Id > 0)
                {
                    await Task.Delay(2000, cancellationToken);
                    var x = new
                    {
                        Cod = nperson.Id,
                        Nombre = nperson.Nap + "_" + DateTime.Now.ToString()
                    };
                    logger.LogInformation("Consulta Api existosa!!!");
                    return Ok(x);
                }
                else
                {
                    throw new ArgumentException("Id debe ser positivo",nameof(nperson));
                }
            }
            catch (TaskCanceledException ex) when (ex.CancellationToken.IsCancellationRequested)
            {
                logger.LogWarning("Se cancelo la petición");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
