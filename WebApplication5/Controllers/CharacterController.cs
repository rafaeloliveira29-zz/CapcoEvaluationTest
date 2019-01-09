using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class CharacterController : ControllerBase, ICharacterRepository
    {
        //Page Index
        //GET character/index
        [HttpGet]
        [Route("index")]
        public ContentResult Index()
        {
            return new ContentResult
            {
                /*
                 * HTML da pagina index. 
                 * 1) Funcionando OK
                 * 2) Funcionando Ok
                 * 3)Funcionando porém com ressalvas a serem melhoradas
                 * - Não está sendo validado se o usuario digitará letras ao invés de numeros, 
                 * - Nao está sendo validado se o usuário nao digitará nada,
                 * - So é considerado o caso onde o usuário digita um numero de 1 a 86
                 */
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body bgcolor='#f2ebd9'><div align='center'><h1>API Rafael Souza Oliveira</h1>" +
                "<h2>Evaluation Test for Capco Job interview</h2></div>"+
                "<a>1) Bring a single Character(Choose a number between 1 and 86), type it in the next window!</a></br/>"
                + "<input id = 'charId'/>"
                + "<input id='clickMe' type='button' value='clickme' onclick='doFunction();'/>" +
                "<script>" +
                "document.getElementById('clickMe').onclick = function () { var id = document.getElementById('charId').value;" +
                " window.location ='https://localhost:44325/api/character/'+id ; };</script>"+
                "</br></br><a href='https://localhost:44325/api/character/order'>2) Bring All Characters Ordered By Name and Maybe by number of films they made</a></br></br>"+
                "<a href='https://localhost:44325/api/character/humans'>3) Bring all humans and its height mean(Be Patient...This one is Very Slow :D )</a></br></br>"
                + " </br></body></br></br></br><div class='footer' align='center' >@Copyright Rafael Souza Oliveira</div></html>"
            };
        }

    
    
    



        //Get Personagem por ID
        // GET character/id
        [HttpGet("{id}")]
        [EnableCors("AllowAll")]
        public async Task<ActionResult<Character>> GetCharacterAsync(int id)
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetCharacterAsync(id); ;
        }

        //Get Character Ordered by name
        [Route("order")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string, string>>> GetCharactersList()
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetCharactersListByName();
        }

        [Route("humans")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string, string>>> GetHumanCharacters()
        {
            CharacterRepository result = new CharacterRepository();
            return await result.GetHumanCharacter();

        }

    }
}
