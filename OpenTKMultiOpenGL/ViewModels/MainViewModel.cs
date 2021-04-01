using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKMultiOpenGL.ViewModels
{

  public class ChartViewModel
  {

  }

  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {

      UpdateElements(Count);
    }

    private int _count = 180;

    public int Count
    {
      get => _count;
      set
      {
        if (Set(ref _count, value))
        {
          UpdateElements(value);
        }
      }
    }

    private void UpdateElements(int value)
    {
      Items = Enumerable.Range(0, value).Select(x => new ChartViewModel() { }).ToList();
    }

    private List<ChartViewModel> _items;

    public List<ChartViewModel> Items
    {
      get => _items;
      set => Set(ref _items, value);
    }


  }
}
