using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MutantsAPI.Domains;
using MutantsAPI.Dtos;

namespace MutantsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly IGenomeService _genomeService;

        public MutantController(IGenomeService genomeService)
        {
            _genomeService = genomeService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMutant(Dna dna)
        {
            var isMutant = await _genomeService.ProcessDna(dna);
            return isMutant ? StatusCode((int)HttpStatusCode.OK) : StatusCode((int)HttpStatusCode.Forbidden);       
        }
    }
}
