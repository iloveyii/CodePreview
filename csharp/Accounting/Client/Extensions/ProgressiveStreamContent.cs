using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;


namespace Accounting.Client.Extensions
{
    public class ProgressiveStreamContent : StreamContent
    {
        private readonly Stream _fileStream;
        // Max amount of bytes to send per packed
        private readonly int _maxBuffer = 1024 * 4;

        public ProgressiveStreamContent(Stream stream, int maxBuffer, Action<long, double> onProgress) : base(stream)
        {
            _fileStream = stream;
            _maxBuffer = maxBuffer;
            OnProgress += onProgress;
        }

        ///<summary>
        /// Event that we can subscribe to which will be triggered everytime there is a part of the file uploaded
        /// It passes the totoal uploaded bytes and percent as well
        ///</summary>
        public event Action<long, double> OnProgress;

        // Override the SerializeToStreamAsync method which provides us witht the stream that we can write our chunks into it
        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            // Define the array of bytes[] with the max length to be pushed per time
            var buffer = new byte[_maxBuffer];
            var totalLength = _fileStream.Length;
            // To hold the amout of bytes uploaded
            long uploaded = 0;

            // Create a while loop that we will break when all bytes are uploaded to server
            while (true)
            {
                // We read a chunk of bytes and write it to the stream of HttpContent
                try
                {
                    var length = await _fileStream.ReadAsync(buffer, 0, _maxBuffer);
                    // If no bytes left to read break
                    if (length <= 0)
                    {
                        break;
                    }
                    // Increment uploaded by bytes being uploaded
                    uploaded += length;
                    // Calculate the percentage
                    var percentage = Convert.ToDouble(uploaded * 100 / _fileStream.Length);
                    // Write the bytes to the HttpContent stream
                    await stream.WriteAsync(buffer);
                    // Fire the event of onProgress to notify the client about progress so far
                    OnProgress?.Invoke(uploaded, percentage);
                    // Add a delay to see it
                    await Task.Delay(300);

                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                }
            }
        }

    }
}
