using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase, ICharacterRepository
    {
        //Page Index(Job description)
        //GET character/index
        [HttpGet]
        [Route("index")]
        public ContentResult Index()
        {
            return new ContentResult
            {         
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><head><meta charset='utf - 8' />    <meta http-equiv='X - UA - Compatible' content='IE = edge'><title>Capco Evaluation Test - Rafael S. Oliveira</title>"+
                "<meta name='viewport' content='width = device - width, initial - scale = 1'><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css' integrity='sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO' crossorigin='anonymous'>"+                
                "</head><body bgcolor='#f2ebd9'><div align='center'><h1>API Rafael Souza Oliveira</h1>" +
                "<h2>Evaluation Test for Capco Job interview</h2></div>"+
                "<divstyle='margin-right:45px;'><strong>Description</strong></br>This is a .NetCore API that consumes the https://swapi.co/ (The star wars API) and shows some informations obtained from it. There are 4 methods of type GET on the 'on development' API.</br>" +
                "- Index();</br>- GetCharacterAsync();</br>- GetCharactersList();</br>- GetHumanCharacters();</br></br>" +
                "The index page was designed to make easier the calling of the functions described above. The function GetCharactersAsync() returns the character id, the character name, the year of birth if availabe and the names of the movies this character has participated. The function GetCharactersListByName() returns all the characters ordered by name and shows between the character name, the number of films has participated. The function GetHumanCharacter() returns all human characters, their heights and calculate the mean between all the human character heights."+
                "Running The API: Download the code using the ˜clone or download button˜ on the right top corner of the page , open the .sln file on visual studio, execute the solution. it might open the job description page (localhost:port/api/character/index)."+
                "</br></br><strong>Accessing theresources:</strong></br></br>" +
                "<strong>=> Get One character =></strong> localhost:port/api/character/{characterId} =></strong> the id is in the range 1-86.</br>" +
                "<strong>=> Get The list of characters and its respective number of films made =></strong> localhost:port/api/character/order</br>" +
                "<strong>=> Get only the human characters, ther height and the mean of the human heights =></strong> localhost:port/api/character/humans => Note that this one is the slowest link to load.</div>"
                + " </br></body></br></br></br><div class='footer' align='center' >@Copyright Rafael Souza Oliveira</div></html>"
            };
        }
        //Get characeter by ID
        // GET character/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterAsync(int id)
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetCharacterAsync(id); ;
        }
        //Get Character Ordered by name
        [Route("order")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string, string>>> GetCharactersList()
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetCharactersListByName();
        }
        [Route("humans")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string, string>>> GetHumanCharacters()
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetHumanCharacter();
        }
    }
}
