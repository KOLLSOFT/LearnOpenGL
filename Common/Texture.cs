// using System.Drawing.Imaging;

using System.Collections.Generic;
using OpenTK.Graphics.ES30;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using GL = OpenTK.Graphics.ES30.GL;
using PixelFormat = OpenTK.Graphics.ES30.PixelFormat;
using PixelType = OpenTK.Graphics.ES30.PixelType;
using Rectangle = System.Drawing.Rectangle;
using TextureMagFilter = OpenTK.Graphics.ES30.TextureMagFilter;
using TextureMinFilter = OpenTK.Graphics.ES30.TextureMinFilter;
using TextureParameterName = OpenTK.Graphics.ES30.TextureParameterName;
using TextureTarget = OpenTK.Graphics.ES30.TextureTarget;
using TextureTarget2d = OpenTK.Graphics.ES30.TextureTarget2d;
using TextureUnit = OpenTK.Graphics.ES30.TextureUnit;
using TextureWrapMode = OpenTK.Graphics.ES30.TextureWrapMode;

namespace Common
{
    // A helper class, much like Shader, meant to simplify loading textures.
    public class Texture
    {
        public readonly int Handle;

        public static Texture LoadFromFile(string path)
        {
            // Generate handle
            int handle = GL.GenTexture();

            // Bind the handle
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            using (var image = Image.Load<Rgba32>(path))
            {
                image.Mutate(x => x.Flip(FlipMode.Vertical));
                var pixels = new List<byte>(4 * image.Width * image.Height);
                for (int y = 0; y < image.Height; y++) {
                    var row = image.GetPixelRowSpan(y);
                    for (int x = 0; x < image.Width; x++) {
                        pixels.Add(row[x].R);
                        pixels.Add(row[x].G);
                        pixels.Add(row[x].B);
                        pixels.Add(row[x].A);
                    }
                }
                
                GL.TexImage2D(TextureTarget2d.Texture2D,
                    0,
                    TextureComponentCount.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    pixels.ToArray());
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            GL.GenerateMipmap(TextureTarget.Texture2D);

            return new Texture(handle);
        }

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }
        
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
