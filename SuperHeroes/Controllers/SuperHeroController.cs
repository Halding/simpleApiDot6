using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;
        
        private static  List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero { Id = 1, Name = "Dj john", FirstName = "John", Lastname = "UNKNOWN", PLace = "Denmark" },
            new SuperHero { Id = 2, Name = "Dj john", FirstName = "John", Lastname = "UNKNOWN", PLace = "Denmark" }

        };

        public SuperHeroController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            
            return Ok(heroes);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        { 
            
            var hero = heroes.Find(h => h.Id == id);
            
            if(hero == null) 
            {
                return BadRequest("Hero was not found"); 
            }

            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero request)
        {
            var hero = heroes.Find(h => h.Id == request.Id);

            if (hero == null)
            {
                return BadRequest("Hero was not found");
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.Lastname = request.Lastname;
            hero.PLace = request.PLace;

            return Ok(hero);

        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        { 
            
            var hero = heroes.Find(h => h.Id == id);
            
            if(hero == null) 
            {
                return BadRequest("Hero was not found"); 
            }
            heroes.Remove(hero);
            
            return Ok(heroes);
        }

        [HttpGet("test")]
        public async Task<string> GetPuzzle()
        {
            var URL = "https://lichess.org/api/puzzle/daily";
            var httpClient = _factory.CreateClient();
            var response = await httpClient.GetAsync(URL);
            return await response.Content.ReadAsStringAsync();
        }


    }
}
