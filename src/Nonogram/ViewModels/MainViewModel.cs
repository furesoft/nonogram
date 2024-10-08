using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using NonoGramGen.Model;

namespace NonoGramGen.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Welcome to Avalonia!";
    [ObservableProperty] private Nonogram _nonogram = new();

    public MainViewModel()
    {
       Nonogram = Nonogram.LoadFromStream(GetType().Assembly.GetManifestResourceStream("NonoGramGen.Samples.42.non"));
    }
}