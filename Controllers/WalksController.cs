using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domin_Model;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IwalkRepository walkRepository;

        public WalksController(IMapper mapper, IwalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        
        //create walk
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequstDTO addWalkRequstDTO)
        {
            //Map DTO to Domin Model
            var WalkDominModel = mapper.Map<Walk>(addWalkRequstDTO);

            await walkRepository.CreateAsync(WalkDominModel);

            //map domin model to dto

            return Ok(mapper.Map<WalkDTO>(WalkDominModel));

        }
        // GET Walk
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDominModel = await walkRepository.GetAllAsync();

            //map domin model to dto
            return Ok(mapper.Map<List<WalkDTO>>(walksDominModel));
        }

        // GET Walk by ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDominModel = await walkRepository.GetByIdAsync(id);
            if (walkDominModel == null)
            {
                return NotFound();
            }
            //map domin model to dto
            return Ok(mapper.Map<WalkDTO>(walkDominModel));
        }

        //Update Walk  by ID
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Map DTO to Domin Model
            var walkDominModel = mapper.Map<Walk>(updateWalkRequestDTO);
            var updatedWalk = await walkRepository.UpdateAsync(id, walkDominModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            //map domin model to dto
            return Ok(mapper.Map<WalkDTO>(updatedWalk));    
        }

        //Delete Walk by ID
        [HttpDelete("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var DeletedWalkDomin = await walkRepository.GetByIdAsync(id);
            if (DeletedWalkDomin == null)
            {
                return NotFound();
            }
            //map domin model to dto
            await walkRepository.DeleteAsync(id);
            return Ok(mapper.Map<WalkDTO>(DeletedWalkDomin));
        }

    }
}
