using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffmpegAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        [HttpGet("{roomId}")]
        public async Task<GetStreamingInfo> GetInfo(int roomId)
        {
            if (!FFMPEGQue.Instance.Streaming.ContainsKey(roomId))
            {
                await FFMPEGQue.Instance.AddQue(roomId);
            }
            return new GetStreamingInfo(roomId);
        }
        [HttpGet("isstreaming/{roomId}")]
        public async Task<bool> IsStreaming(int roomId)
        {
            if (!FFMPEGQue.Instance.Streaming.ContainsKey(roomId))
            {
                await FFMPEGQue.Instance.AddQue(roomId);
            }
            return FFMPEGQue.Instance.IsStreaming(roomId);
        }
        [HttpPost("{roomId}")]
        public async Task<IActionResult> Streaming (int roomId)
        {
            if (FFMPEGQue.Instance.IsStreaming(roomId))
            {
                return BadRequest();
            }

            var info = FFMPEGQue.Instance.deque(roomId);
            if (info == null)
            {
                return NotFound();
            }

            FFMPEGQue.Instance.DoStreaming(info.RoomId, info);
            return Ok();
        }
    }
}
