﻿using System;
using System.Windows.Data;

namespace FrontLayer.Converters {
	public class ConverterNullToFalse : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			return value != null;
		}

		public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			return null;
		}
	}
}
