using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace Mp3TagReader.Components.Helpers
{
    public class ImageResizer
    {
        private readonly byte[] _original;
        private readonly int _width = 100;
        private readonly int _height = 100;

        public ImageResizer(byte[] original)
        {
            _original = original;
        }

        public byte[] Process()
        {
            byte[] result = null;
            
            using (Image<Rgba32> image = Image.Load(_original))
            {
            
                IResampler sampler = KnownResamplers.Lanczos3;
                bool compand = true;
                ResizeMode mode = ResizeMode.Stretch;

                // init resize object
                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(_width, _height),
                    Sampler = sampler,
                    Compand = compand,
                    Mode = mode
                };

                // mutate image
                image.Mutate(x => x
                    .Resize(resizeOptions)
                );

                var afterMutations = image.Size();

                //Encode here for quality
                var encoder = new JpegEncoder()
                {
                    Quality = 80 //Use variable to set between 5-30 based on your requirements
                };

                using (MemoryStream stream = new MemoryStream())
                {
                    //This saves to the memoryStream with encoder
                    image.Save(stream, encoder);
                    stream.Position = 0; // The position needs to be reset.

                    // prepare result to byte[]
                    result = stream.ToArray();
                }
            }
            
            return result;
        }
    }
}