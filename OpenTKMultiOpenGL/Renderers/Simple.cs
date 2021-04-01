using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKMultiOpenGL.Renderers
{
  public class Simple: IRenderer
  {
    public static System.Diagnostics.Stopwatch _stopwatch = System.Diagnostics.Stopwatch.StartNew();
    public Action<TimeSpan> Render => DoRender;

    private void DoRender(TimeSpan obj)
    {
      RenderShared();
    }

    public static void Ready()
    {
      Console.WriteLine("GlWpfControl is now ready");
      GL.Enable(EnableCap.Blend);
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.ScissorTest);
    }

    public static void RenderShared()
    {
      var hue = (float)_stopwatch.Elapsed.TotalSeconds * 0.15f % 1;
      var c = Color4.FromHsv(new Vector4(hue, 0.75f, 0.75f, 1));
      GL.ClearColor(c);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.LoadIdentity();
      GL.Begin(PrimitiveType.Triangles);

      GL.Color4(Color4.Red);
      GL.Vertex2(0.0f, 0.5f);

      GL.Color4(Color4.Green);
      GL.Vertex2(0.58f, -0.5f);

      GL.Color4(Color4.Blue);
      GL.Vertex2(-0.58f, -0.5f);

      GL.End();
      GL.Finish();
    }
  }
}
