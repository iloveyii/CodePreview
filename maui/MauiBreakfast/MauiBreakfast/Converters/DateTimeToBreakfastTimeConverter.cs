using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;


namespace MauiBreakfast.Converters
{
	public class DateTimeToBreakfastTimeConverter : BaseConverter<object, string>
	{
		public DateTimeToBreakfastTimeConverter()
		{
		}

        public override string DefaultConvertReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override object DefaultConvertBackReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override object ConvertBackTo(string value, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override string ConvertFrom(object value, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;

            return $"{dateTime.Hour}:{dateTime.Minute}";
            //return TimeOnly.FromDateTime(dateTime).ToLongTimeString();
        }
    }
}

