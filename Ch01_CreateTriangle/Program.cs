using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Ch01_CreateTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameWindowSettings = new GameWindowSettings
            {
                IsMultiThreaded = true,
                RenderFrequency = 120f,
                UpdateFrequency = 120f,
            };
            var nativeWindowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(1024, 768),
                Title = "LearnOpenGL_Ch01_CreateTriangle",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new Window(gameWindowSettings, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}