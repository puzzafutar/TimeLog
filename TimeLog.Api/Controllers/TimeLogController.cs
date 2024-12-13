using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeLog.Service.Response;
using TimeLog.Service.Service.Interface;

namespace TimeLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogService _timeLogService;
        private readonly IMapper _mapper;

        public TimeLogController(ITimeLogService timeLogService, IMapper mapper)
        {
            _timeLogService = timeLogService;
            _mapper = mapper;
        }

        [HttpGet("GetTimeLogs")]
        [ProducesResponseType(typeof(List<TimeLogDto>), StatusCodes.Status200OK)]
        public async Task<List<TimeLogDto>> GetTimeLogsAsync(DateTime? fromDate, DateTime? toDate, int currentPage, int pageSize)
        {
            var result = await _timeLogService.GetTimeLogsAsync(fromDate, toDate, currentPage, pageSize);
            return _mapper.Map<List<TimeLogDto>>(result);
        }

        [HttpPost("StartStopTimer")]
        [ProducesResponseType(typeof(List<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartStopTimerAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest("stopIndex must be greater then -1");
            }

            if (id > 0)
            {
                bool isExsts = await _timeLogService.HasActiveTimer(id);
                if (isExsts)
                {
                    await _timeLogService.StartStopTimerAsync(id);
                    return Ok();
                }
                else
                {
                    return BadRequest("Nincs ilyen timer!");
                }
            }
            else
            {
                await _timeLogService.StartStopTimerAsync(id);
                return Ok();
            }
        }

        [HttpPut("UpdateTimer")]
        [ProducesResponseType(typeof(List<TimeLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTimerAsync(int id, [FromBody] string description)
        {
            if (id < 0)
            {
                return BadRequest("stopIndex must be greater then -1");
            }

            bool isExsts = await _timeLogService.HasActiveTimer(id);
            if (isExsts)
            {
                await _timeLogService.UpdateTimerAsync(id, description);
                return Ok();
            }
            else
            {
                return BadRequest("Nincs ilyen timer!");
            }

        }

        [HttpPut("DeleteTimer")]
        [ProducesResponseType(typeof(List<TimeLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTimerAsync(int id)
        {
            bool isExsts = await _timeLogService.HasTimer(id);
            if (isExsts)
            {
                await _timeLogService.DeleteTimerAsync(id);
                return Ok();
            }
            else
            {
                return BadRequest("Nincs ilyen timer!");
            }
        }
    }
}
