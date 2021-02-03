using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffmpegAPI
{
    public class GetFFMPEG
    {
        public GetFFMPEG(Queue<FFMPEG> fFMPEGs)
        {
            count = fFMPEGs.Count();
            ffmpegs = fFMPEGs.ToArray();
        }

        public int count { get; private set; }
        public IEnumerable<FFMPEG> ffmpegs { get; private set; }
    }
    public class GetFFMPEGQues
    {
        public GetFFMPEGQues(Dictionary<int, Queue<FFMPEG>> streaming)
        {
            count = streaming.Keys.Count();
            roomids = streaming.Keys.ToArray();
        }

        public int count { get; set; }
        public IEnumerable<int> roomids { get; set; }
    }

    public class GetStreamingInfo
    {
        public GetStreamingInfo(int roomId)
        {
            RoomId = FFMPEGQue.Instance.Streaming[roomId].roomid;
            FileName = FFMPEGQue.Instance.Streaming[roomId].filename;
        }

        public int RoomId { get; set; }
        public string FileName { get; set; }
    }

}
