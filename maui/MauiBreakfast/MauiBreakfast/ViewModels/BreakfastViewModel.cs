using System;
using MauiBreakfast.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiBreakfast.ViewModels
{
	public partial class BreakfastViewModel : ObservableObject
	{
		[ObservableProperty]
		List<Breakfast> breakfasts;

        [ObservableProperty]
        bool isRefreshing;

		public BreakfastViewModel()
		{
			LoadBreakfasts();
		}

        [RelayCommand]
        public void LoadBreakfasts()
        {
            try
            {
                Breakfasts = new()
                {
                    new Breakfast()
                    {
                        Name = "Vegan Sunshine",
                        Description = "Vegan everything! Join us for a healthy breakfast..",
                        StartDateTime = DateTime.UtcNow.AddDays(5),
                        EndDateTime = DateTime.UtcNow.AddDays(10).AddHours(12),
                        Image = new Uri("https://rimages.softhem.net/image/150/300?1"),
                        Savory = new List<string> {
                            "Oatmeal",
                            "Avocado Toast",
                            "Omelette",
                            "Salad"
                        },
                        Sweet = new List<string> {
                            "Cookie"
                        }
                    },
                    new Breakfast()
                    {
                        Name = "Breakfast tiffany",
                        Description = "Tiffany everything! Join us for a healthy breakfast..",
                        StartDateTime = DateTime.UtcNow,
                        EndDateTime = DateTime.UtcNow.AddHours(2),
                        Image = new Uri("https://rimages.softhem.net/image/150/300?2"),
                        Savory = new List<string> {
                            "Avocado Toast",
                            "Omelette",
                            "Tomato"
                        },
                        Sweet = new List<string> {
                            "Dates", "Donuts"
                        }
                    }
                };
            } finally
            {
                IsRefreshing = false;
            }
        }
    }

}

