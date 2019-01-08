using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        //Page Index
        //GET character/index
        [HttpGet]
        [Route("index")]
        public ContentResult Index()
        {
            
            return new ContentResult
            {

                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body><h1>Hello World</h1>" +
                "<a href='character/1'><img src='StoryImageHandler.ashx ? storyId = 1' /></a></body></html>"
            };
        }

        //Get Personagem por ID
        // GET character/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterAsync(int id)
        {
            Character character = new Character();
            List<string> namesFilm = new List<string>();
            var filmId = string.Empty;
            var http = new HttpClient();
            var httpFilms = new HttpClient();

            var url = ("https://swapi.co/api/people/ "+id);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Character));
            //character.id= Regex.Replace(character.url, "[A-Za-z ]", "").Replace('/', ' ').Replace(':', ' ').Replace('.', ' ').Replace(" ", string.Empty);
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var jo = JObject.Parse(result);
            var valueSet = JsonConvert.DeserializeObject<Character>(Convert.ToString(jo)).films;
            foreach (var item in valueSet.ToList())
            {
                
                var urlFilms = ("https://swapi.co/api/films/" + Regex.Replace(item, "[A-Za-z ]", "").Replace('/', ' ').Replace(':', ' ').Replace('.', ' ').Replace(" ", string.Empty)+"/");
                var responseFilm = await httpFilms.GetAsync(urlFilms);
                var resultFilm = await responseFilm.Content.ReadAsStringAsync();
                var serializerFilms = new DataContractJsonSerializer(typeof(Character));
                var msFilms = new MemoryStream(Encoding.UTF8.GetBytes(resultFilm));
                var joFilms = JObject.Parse(resultFilm);
                var film= (string)joFilms.SelectToken("title");
               namesFilm.Add(film);

            }


            character.id = Regex.Replace((string)jo.SelectToken("url"), "[A-Za-z ]", "").Replace('/', ' ').Replace(':', ' ').Replace('.', ' ').Replace(" ", string.Empty);
            character.name = (string)jo.SelectToken("name");
            character.birth_year = (string)jo.SelectToken("birth_year");
            character.numberOfFilms = jo.SelectToken("films").Count().ToString();
            character.mass = "-";
            character.url = "-";
            character.height = "-";
            character.films = namesFilm;
            return character;
        }

        //Get Character Ordered by name
        [Route("orderbyname")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string,string>>> GetCharactersListByName()
        {
            var http = new HttpClient();
            SortedDictionary<string,string> namesAndFilmsList = new SortedDictionary<string, string>();
            string replaceable = string.Empty;
            Character cha = new Character();
            for (int i = 1; i <= 9; i++)
            {
                var url = ("https://swapi.co/api/people/?page="+i);
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(Character));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (Character)serializer.ReadObject(ms);
                var jo = JObject.Parse(result);
                JArray x = (JArray)jo.SelectToken("results");
                foreach (JToken item in x)
                {
                    cha.name = (string)item.SelectToken("name");
                    cha.numberOfFilms = item.SelectToken("films").Count().ToString();
                    replaceable = Regex.Replace((string)item.SelectToken("url"), "[A-Za-z ]", "").Replace('/', ' ').Replace(':', ' ').Replace('.', ' ').Replace(" ", string.Empty);
                    cha.id = replaceable;
                    namesAndFilmsList.Add("Name: " +cha.name,"Num. Films: " + cha.numberOfFilms);
                    // namesList.Add(cha.id);

                }
            }
          //  namesAndFilmsList.OrderBy(x => x.Value);
            return namesAndFilmsList ;
        }

        [Route("humans")]
        [HttpGet]
        public async Task<ActionResult<SortedDictionary<string, string>>> GetHumanCharacter()
        {
            var meanHeight=0;
            var http = new HttpClient();
            var httpSpecie = new HttpClient();
            string newPeso = "0";
            SortedDictionary<string, string> namesAndHeight = new SortedDictionary<string, string>();
            SortedDictionary<string, string> finalResult = new SortedDictionary<string, string>();
            string specie = string.Empty;
            Character cha = new Character();
            for (int i = 1; i <= 9; i++)
            {
                var url = ("https://swapi.co/api/people/?page=" + i);
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(Character));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (Character)serializer.ReadObject(ms);
                var jo = JObject.Parse(result);
                JArray x = (JArray)jo.SelectToken("results");
                foreach (JToken item in x)
                {
                    cha.name = (string)item.SelectToken("name");
                    cha.height = (string)item.SelectToken("height");
                    cha.species = (string)item.SelectToken("species").ToString();
                    string urlSpecie = cha.species;
                    urlSpecie = Regex.Replace(urlSpecie, @"\r\n?|\n", "<br>").Replace("<br>",string.Empty).Replace("\\", "").Replace("\"", "").Replace(" ", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                    if (urlSpecie == "")
                        break;
                    if (urlSpecie == null)
                        break;
                    var responseSpecie = await http.GetAsync(urlSpecie);
                    var resultSpecie = await responseSpecie.Content.ReadAsStringAsync();
                    var serializerSpecie = new DataContractJsonSerializer(typeof(Character));
                    var msSpecie = new MemoryStream(Encoding.UTF8.GetBytes(resultSpecie));
                    var dataFilms = (Character)serializer.ReadObject(msSpecie);
                    var joSpecie = JObject.Parse(resultSpecie);
                    var species = (string)joSpecie.SelectToken("name");
                    specie = species;
                    if(specie == "Human")
                    {
                        if (cha.height == "unknown")
                        {
                            cha.name = cha.name + "Do Not Considered to mean(Height)";
                            cha.height = newPeso;
                        }
                        else

                        {
                            namesAndHeight.Add("Name: " + cha.name, "Peso: " + cha.height);
                            int peso = Convert.ToInt32(cha.height);
                            meanHeight += peso;
                        }
                        
                    }
                   
                 
                }
               
            }
            namesAndHeight.Add("MEDIA ============> ", meanHeight.ToString());
            //  namesAndFilmsList.OrderBy(x => x.Value);
            return namesAndHeight ;
        }

    }
   
}
