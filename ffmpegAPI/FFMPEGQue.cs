using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ffmpegAPI
{
    public class FFMPEGQue
    {
        #region 싱글톤
        //private 생성자 
        private FFMPEGQue() {}
        //private static 인스턴스 객체
        private static readonly Lazy<FFMPEGQue> _instance = new Lazy<FFMPEGQue>(() => new FFMPEGQue());
        //public static 의 객체반환 함수
        public static FFMPEGQue Instance { get { return _instance.Value; } }
        #endregion

        // key = RoomId
        public Dictionary<int, Queue<FFMPEG>> Ques { get; private set; } = new Dictionary<int, Queue<FFMPEG>> { };
        public Dictionary<int, FFMPEGStreaming> Streaming { get; private set; } = new Dictionary<int, FFMPEGStreaming> { };

        public async Task AddQue(int roomId)
        {
            Ques.Add(roomId, new Queue<FFMPEG> { });
            Streaming.Add(roomId, new FFMPEGStreaming());
        }

        public async Task AddData(FFMPEG f)
        {
            Ques[f.RoomId].Enqueue(f);
        }

        public FFMPEG deque(int roomId)
        {
            FFMPEG f;
            if (Ques[roomId].TryDequeue(out f))
            {
                return f;
            }
            return null;
        }

        public bool IsStreaming(int roomId)
        {
            var s = Streaming[roomId];
            return s.IsStreaming;
        }

        public async Task DoStreaming(int roomId, FFMPEG info)
        {
            await Streaming[roomId].SetStreaming(info);
            var s = Streaming[roomId];
            s.Streaming();
        }
    }
}
