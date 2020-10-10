﻿using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DateTimesToWidth : MarkedupMultiValueConverter<DateTimesToWidth> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			//DateTime projektStart = (DateTime)values[0];
			//DateTime projektEnde = (DateTime)values[1];
			//DateTime datum = (DateTime)values[2];
			//double gesamtLänge = (double)values[3];
			//double faktor = 0.0;
			#endregion

			#region conversion
			// setzt den faktor zu einem Bruch der gesamtlänge 
			// Datum - StartDatum = Reiner Tagesunterschied a
			// EndDatum - StartDatum = Reiner Tagesunterschied b

			//double start = datum.Date.Subtract(projektStart.Date).TotalDays;
			//double end = projektEnde.Date.Subtract(projektStart.Date).TotalDays;
			//if( end > 0 )
			//	faktor = start / end; 
			#endregion

			//return ( faktor * gesamtLänge );
			return null;
		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}

}