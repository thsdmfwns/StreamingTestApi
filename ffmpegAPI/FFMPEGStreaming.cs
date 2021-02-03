using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg; 

namespace ffmpegAPI
{
    public class FFMPEGStreaming
    {
        public int roomid { get; private set; }
        public string filename { get; private set; }
        public CancellationTokenSource CancelToken { get; private set; }
        public string StreamingCommand { get; private set; }
        public bool IsStreaming { get; private set; } = false;

        public FFMPEGStreaming()
        {
            roomid = -1;
            filename = string.Empty;
        }

        public async Task SetStreaming(FFMPEG f)
        {
            roomid = f.RoomId;
            filename = f.FileUrl;
        }

        public async Task Streaming()
        {
            try
            {
                IsStreaming = true;
                var con = FFmpeg.Conversions.New();
                CancelToken = new CancellationTokenSource();

                //StreamingCommand = @$"-re -i C:\samplevideo/{filename} -map 0 -c:v copy -c:a copy -f flv rtmp://pickvideocommunication.icu:1935/{roomid}/stream";

                //테스트용 코드
                StreamingCommand = @$"-re -i C:\samplevideo/{filename} -map 0 -c:v copy -c:a copy -f flv rtmp://pickvideocommunication.icu:1935/test/stream";

                await con.Start(StreamingCommand, CancelToken.Token);
            }
            catch (System.Exception e)
            {

                throw e;
            }

            IsStreaming = false;
        }
    }
}
