using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public HeroController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Hero
        [HttpGet]
        // public async Task<ActionResult<IEnumerable<HeroDTO>>> GetHero()
        // {
        //     var heroItems = await _repositoryWrapper.Hero.FindAllAsync();
        //     return Ok(heroItems);
        // }

        public async Task<ActionResult<IEnumerable<HeroDTO>>> GetHero()
        {
            var heroItems = await _repositoryWrapper.Hero.FindAllAsync();
            return heroItems
                .Select(x => HeroToDTO(x))
                .ToList();
        }

        // GET: api/Hero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroDTO>> GetHero(int id)
        {

            var hero = await _repositoryWrapper.Hero.FindByIDAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return HeroToDTO(hero);
        }

        // PUT: api/Hero/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(int id, HeroDTO heroDTO)
        {
            if (id != heroDTO.Id)
            {
                return BadRequest();
            }

            var hero = await _repositoryWrapper.Hero.FindByIDAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            hero.Name = heroDTO.Name;
            hero.Address = heroDTO.Address;

            try
            {
                await _repositoryWrapper.Hero.UpdateAsync(hero);
            }
            catch (DbUpdateConcurrencyException) when (!HeroExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Hero
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeroDTO>> CreateHero(HeroDTO heroDTO)
        {

            var hero = new Hero
            {
                Address = heroDTO.Address,
                Name = heroDTO.Name
            };

            await _repositoryWrapper.Hero.CreateAsync(hero);

            return CreatedAtAction(
                nameof(GetHero),
                new { id = hero.Id },
                HeroToDTO(hero));
        }

        // DELETE: api/Hero/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await _repositoryWrapper.Hero.FindByIDAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Hero.DeleteAsync(hero, true);

            return NoContent();
        }

        // Search

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> SearchHero(string term)
        {
            var empList = await _repositoryWrapper.Hero.SearchHero(term);
            return Ok(empList);
        }

        [HttpPost("searchhero")]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> SearchHeroMultiple(HeroSearchPayload SearchObj)
        {
            var empList = await _repositoryWrapper.Hero.SearchHeroMultiple(SearchObj);
            return Ok(empList);
        }


        private bool HeroExists(int id)
        {
            return _repositoryWrapper.Hero.IsExists(id);
        }

        private static HeroDTO HeroToDTO(Hero hero) =>
            new HeroDTO
            {
                Id = hero.Id,
                Name = hero.Name,
                Address = hero.Address
            };
    }
}
