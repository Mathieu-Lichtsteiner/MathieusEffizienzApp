﻿namespace EffizienzControls.Extensions {
	public static class ColorExtensions {

		public static System.Windows.Media.Color ToMediaColor( this System.Drawing.Color color )
			=> System.Windows.Media.Color.FromArgb( color.A, color.R, color.G, color.B );
		public static System.Drawing.Color ToDrawingColor( this System.Windows.Media.Color color )
			=> System.Drawing.Color.FromArgb( color.A, color.R, color.G, color.B );

	}
}
