//using AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomeActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domin_Model;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NzWalksDbContext dbContext,
            IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            //try
            ////{
            //    throw new Exception("this is a custom execption");
            //    logger.LogInformation("GetAll Region Action Method was Invoked");

                var RegionsDomin = await regionRepository.GetAllAsync();

                //logger.LogInformation($"FInished GetAllRegions Request With Data : {JsonSerializer.Serialize(RegionsDomin)}");

                var regiondto = mapper.Map<List<RegionDTO>>(RegionsDomin);

                return Ok(regiondto);
            }
            //catch (Exception ex) 
            //{
            //    logger.LogError(ex, ex.Message);
            //    throw;
            //}
           
            //var RegionsDto = new List<RegionDTO>();
            //foreach (var RegionDomin in RegionsDomin) {
            //    RegionsDto.Add(new RegionDTO()
            //    {
            //        Id = RegionDomin.Id,
            //        Name = RegionDomin.Name,
            //        Code = RegionDomin.Code,
            //        RegionImageURL = RegionDomin.RegionImageURL,
            //    });
            //}
                
        //}


        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomin = await regionRepository.GetByIdAsync(id);

            if (regionDomin == null)
            {
                return NotFound();
            }
            //map / convert Region Model to Dto
            //var regionDto = new RegionDTO()
            //{
            //    Id = regionDomin.Id,
            //    Name = regionDomin.Name,
            //    Code = regionDomin.Code,
            //    RegionImageURL = regionDomin.RegionImageURL,
                //};
            return Ok( mapper.Map<RegionDTO>(regionDomin));

        }

        // Post to create new region
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //if (ModelState.IsValid)
            //{
                // Map or Convert Dto to Domin Model
                var regionDominModel = mapper.Map<Region>(addRegionRequestDTO);
                //var regionDominModel = new Region
                //{
                //    Code = addRegionRequestDTO.Code,
                //    Name = addRegionRequestDTO.Name,
                //    RegionImageURL = addRegionRequestDTO.RegionImageURL
                //};

                regionDominModel = await regionRepository.CreateAsync(regionDominModel);

                // map domin back to DTO
                var RegionDto = mapper.Map<RegionDTO>(regionDominModel);
                //var RegionDto = new RegionDTO
                //{
                //    Id = regionDominModel.Id,
                //    Name = regionDominModel.Name,
                //    Code = regionDominModel.Code,
                //    RegionImageURL = regionDominModel.RegionImageURL
                //};

                return CreatedAtAction(nameof(GetById), new { id = RegionDto.Id }, RegionDto);

            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
            
        }

        //update region
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var RegionDominModel = mapper.Map<Region>(updateRegionRequestDTO);
            //var RegionDominModel = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageURL= updateRegionRequestDTO.RegionImageURL
            //};

            RegionDominModel = await regionRepository.UpdateAsync(id, RegionDominModel);
            if (RegionDominModel == null)
            {
                return NotFound();
            }


            // convert domin model to dto
            var regionDto = mapper.Map<RegionDTO>(RegionDominModel);
            //var regionDto = new RegionDTO
            //{
            //    Id = RegionDominModel.Id,
            //    Name = RegionDominModel.Name,
            //    Code = RegionDominModel.Code,
            //    RegionImageURL = RegionDominModel.RegionImageURL
            //};
            return Ok(regionDto);
        }

        // Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var RegionDominModel = await regionRepository.DeleteAsync(id);
            if (RegionDominModel == null)
            {
                return NotFound();
            }
                      

            // return deleted region back
            //map domin model to Dto
            var regionDto = mapper.Map<RegionDTO>(RegionDominModel);
            //var regionDto = new RegionDTO
            //{
            //    Id = RegionDominModel.Id,
            //    Name = RegionDominModel.Name,
            //    Code = RegionDominModel.Code,
            //    RegionImageURL = RegionDominModel.RegionImageURL
            //};
            return Ok(regionDto);
        }
    }
}
