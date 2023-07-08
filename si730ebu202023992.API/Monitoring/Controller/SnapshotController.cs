using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using si730ebu202023992.API.Monitoring.Dto.Request;
using si730ebu202023992.Domain.Monitoring.Interface;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.API.Monitoring.Controller
{
    [Route("api/v1")]
    [ApiController]
    public class SnapshotController : ControllerBase
    {
        private IMapper _mapper;
        private ISnapshotDomain _snapshotDomain;
        
        public SnapshotController(ISnapshotDomain snapshotDomain, IMapper mapper)
        {
            _mapper = mapper;
            _snapshotDomain = snapshotDomain;
        }

        [HttpGet("products/{productId}/snapshots", Name = "Get Snapshot By Id")]
        public async Task<List<Snapshot>> GetAllSnapshotByProductId([FromRoute] int productId)
        {
            return await _snapshotDomain.GetAllSnapshotsAsync(productId);
        }

        [HttpPost("products/{productId}/snapshots")]
        public async Task<IActionResult> PostSnapshotAsync([FromRoute] int productId, [FromBody] SnapshotRequest snapshotRequest)
        {
            if (ModelState.IsValid)
            {
                var snapshot = _mapper.Map<SnapshotRequest, Snapshot>(snapshotRequest);
                var result = await _snapshotDomain.SaveAsync(productId, snapshot);
                return result ? Created("products/{productId}/snapshots", snapshot) : StatusCode(500);
            }
            return StatusCode(400);
        }
    }
}
