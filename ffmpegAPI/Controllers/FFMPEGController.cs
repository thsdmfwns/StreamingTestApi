using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffmpegAPI;

namespace ffmpegAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FFMPEGController : ControllerBase
    {
        [HttpGet("{roomId}")]
        public async Task<GetFFMPEG> GetFFMPEGsAsync(int roomId)
        {
            if (!FFMPEGQue.Instance.Ques.ContainsKey(roomId))
            {
                await FFMPEGQue.Instance.AddQue(roomId);
            }
            return new GetFFMPEG(FFMPEGQue.Instance.Ques[roomId]);
        }
        [HttpGet]
        public GetFFMPEGQues GetFFMPEGQues()
        {
            return new GetFFMPEGQues(FFMPEGQue.Instance.Ques);
        }
        [HttpPost]
        public async Task AddFFMPEG(FFMPEG f)
        {
            if (!FFMPEGQue.Instance.Ques.ContainsKey(f.RoomId))
            {
                await FFMPEGQue.Instance.AddQue(f.RoomId);
            }
            FFMPEGQue.Instance.AddData(f);
        }


    }

}
