using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Transactions.Data;
using Transactions.Models;
using Transactions.Repositories;

namespace Transactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        private readonly IAdvertismentRepository advertismentRepository;
        public AdvertismentController(IAdvertismentRepository advertismentRepository)
        {
            this.advertismentRepository = advertismentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdvertisments()
        {
            var advertisments = await advertismentRepository.Get();

            return Ok(advertisments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdvertisment(int id)
        {
            var advert = await advertismentRepository.Get(id);
            if (advert is null)
                return BadRequest("advert not found");

            return Ok(advert);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvertisment(Advertisment advert)
        {
            try
            {
                await advertismentRepository.Create(advert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(await advertismentRepository.Get());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdvertisment(Advertisment advert)
        {
            try
            {
                await advertismentRepository.Update(advert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(await advertismentRepository.Get());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdvertisment(int id)
        {
            try
            {
                await advertismentRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(await advertismentRepository.Get());
        }
    }
}
