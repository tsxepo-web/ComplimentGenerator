using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplimentGeneratorAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ComplimentGeneratorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplimentGeneratorController : ControllerBase
    {
        private readonly ILogger<ComplimentGeneratorController> _logger;
        private readonly IMongoCollection<Compliment> _complimentCollection;

        public ComplimentGeneratorController(
            ILogger<ComplimentGeneratorController> logger,
            IMongoDatabase database)
        {
            _logger = logger;
            _complimentCollection = database.GetCollection<Compliment>("Compliments");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compliment>>> Get()
        {
            var compliments = await _complimentCollection.Find(_ => true).ToListAsync();
            return Ok(compliments);
        }

        [HttpGet("random")]
        public ActionResult<Compliment> GetRandomCompliment()
        {
            var compliment = _complimentCollection.AsQueryable().Sample(1).First();
            return Ok(compliment);
        }

        [HttpPost]
        public async Task<ActionResult<Compliment>> Post(Compliment compliment)
        {
            await _complimentCollection.InsertOneAsync(compliment);
            return CreatedAtAction("Get", new { id = compliment.Id }, compliment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Compliment compliment)
        {
            var retData = await _complimentCollection.FindOneAndUpdateAsync(
                t => t.Id == ObjectId.Parse(id),
                Builders<Compliment>.Update
                    .Set(t => t.Content, compliment.Content));

            if (retData == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _complimentCollection.DeleteOneAsync(t => t.Id == ObjectId.Parse(id));
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}