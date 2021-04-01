using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using OpenTKMultiOpenGL.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OpenTKMultiOpenGL.Attached
{
  public static class OpenGL
  {
    public static readonly List<GLWpfControl> _controls = new List<GLWpfControl>();


    static OpenGL()
    {
      Observable.Interval(TimeSpan.FromMilliseconds(2500))
        .Select(_ => _controls)
        .Where(x => x.Any())
        .ObserveOn(SynchronizationContext.Current)
        .Subscribe(items =>
        {
          foreach (var item in items)
          {
            item.InvalidateVisual();
          }
        });
    }

    public static bool GetStartOnInitialized(DependencyObject obj)
    {
      return (bool)obj.GetValue(StartOnInitializedProperty);
    }

    public static void SetStartOnInitialized(DependencyObject obj, bool value)
    {
      obj.SetValue(StartOnInitializedProperty, value);
    }

    // Using a DependencyProperty as the backing store for StartOnInitialized.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartOnInitializedProperty =
        DependencyProperty.RegisterAttached("StartOnInitialized", typeof(bool), typeof(OpenGL), new PropertyMetadata(false, OnStartOnInitializedChanged));

    private static void OnStartOnInitializedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is GLWpfControl gLWpfControl))
      {
        throw new NotSupportedException($"{d.GetType()} is not supported. Please use {typeof(GLWpfControl).FullName}");
      }


      // when set to false it renders each control after 500ms
      var continuous = false;

      if (!continuous)
      {
        _controls.Add(gLWpfControl);
      }

      var mainSettings = new GLWpfControlSettings {
        MajorVersion = 4,
        MinorVersion = 5,
        GraphicsProfile = ContextProfile.Compatability,
        GraphicsContextFlags = ContextFlags.Debug, RenderContinuously = continuous
      };

      Action action = () =>
      {
        gLWpfControl.Start(mainSettings);
      };

      if (gLWpfControl.IsInitialized)
      {
        action();
      }
      else 
      {
        gLWpfControl.Initialized += (o, e) =>
        {
          action();
        };
      }
    }



    public static IRenderer GetRenderAction(DependencyObject obj)
    {
      return (IRenderer)obj.GetValue(RenderActionProperty);
    }

    public static void SetRenderAction(DependencyObject obj, IRenderer value)
    {
      obj.SetValue(RenderActionProperty, value);
    }

    // Using a DependencyProperty as the backing store for RenderAction.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RenderActionProperty =
        DependencyProperty.RegisterAttached("RenderAction", typeof(IRenderer), typeof(OpenGL), new PropertyMetadata(null, OnRenderActionChanged));

    private static void OnRenderActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is GLWpfControl gLWpfControl))
      {
        throw new NotSupportedException($"{d.GetType()} is not supported. Please use {typeof(GLWpfControl).FullName}");
      }

      if (e.OldValue is IRenderer oldRenderer)
      {
        gLWpfControl.Render -= oldRenderer.Render;
      }

      if (!(e.NewValue is IRenderer renderer))
      {
        return;
      } 

      gLWpfControl.Render += renderer.Render;

    }
  }
}
