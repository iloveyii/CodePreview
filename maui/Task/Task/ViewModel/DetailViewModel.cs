using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace Task.ViewModel 
{
	[QueryProperty("Text", "Text")]
	public partial class DetailViewModel : ObservableObject
    {
		[ObservableProperty]
		string text;

		[RelayCommand]
		void GoBack()
		{
			Shell.Current.GoToAsync("..", true);
		}
	}
}

