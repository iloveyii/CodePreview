namespace MauiBreakfast;
using MauiBreakfast.ViewModels;


public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new BreakfastViewModel();
	}

    void RefreshView_Loaded(System.Object sender, System.EventArgs e)
    {
    }

}


