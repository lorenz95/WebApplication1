using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetListPerson")]
        public IEnumerable<Person> GetListPerson()
        {
            return DbOperation.ReadData();
        }

        [HttpPost(Name = "InsertPersonInList")]
        public void InsertPersonInList([FromBody] Person person)
        {
            DbOperation.AddData(person.FiscalCode, person.Name, person.Surname);
        }
    }
}
