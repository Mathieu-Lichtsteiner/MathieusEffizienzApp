﻿using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {
	[ValueConversion( typeof( double[] ), typeof( double ) )]
	public class DoublesToSize : MarkedupMultiValueConverter<DoublesToSize> {
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
			=> (double)values[0] * (double)values[1];

	}
}
