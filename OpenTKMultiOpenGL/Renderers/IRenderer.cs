using System;

namespace OpenTKMultiOpenGL.Renderers
{
  public interface IRenderer
  {
    Action<TimeSpan> Render { get; }
  }
}