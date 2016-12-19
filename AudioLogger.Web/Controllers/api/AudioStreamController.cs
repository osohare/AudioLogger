using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AudioLogger.Web.Controllers.api
{
    public class AudioStreamController : ApiController
    {
        // GET: api/AudioStream
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(FlushTostream, "audio/mpeg");
            return response;
        }

        internal async Task FlushTostream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];
                using (var fileStream = File.Open("", FileMode.Open, FileAccess.Read))
                {
                    var lenght = (int)fileStream.Length;
                    var bytesRead = 1;

                    while (lenght > 0 && bytesRead > 0)
                    {
                        bytesRead = await fileStream.ReadAsync(buffer, 0, Math.Min(lenght, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        lenght -= bytesRead;
                    }
                }
            }
            catch (HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }

        // GET: api/AudioStream/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
