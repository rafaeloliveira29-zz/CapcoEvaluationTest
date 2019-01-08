using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication5.Controllers
{
    public class Character
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public string height { get; set; }
        [JsonIgnore]
        public string mass { get; set; }
        public List<string> films { get; set; }
        public string birth_year { get; set; }
        [JsonIgnore]
        public string url { get; set; }
        public string species { get; set; }
        public string numberOfFilms { get; set; }
      
    }
  
}