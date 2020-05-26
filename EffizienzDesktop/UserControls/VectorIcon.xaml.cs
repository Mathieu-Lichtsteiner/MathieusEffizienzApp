﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Effizienz.UserControls {
	public partial class VectorIcon : UserControl {

		public VectorIcon() {
			InitializeComponent();
		}

		public double PathHeight {
			get { return (double)GetValue(PathHeightProperty); }
			set { SetValue(PathHeightProperty, value); }
		}

		public static readonly DependencyProperty PathHeightProperty =
			DependencyProperty.Register(
				name: nameof(PathHeight),
				propertyType: typeof(double),
				ownerType: typeof(VectorIcon),
				typeMetadata: new PropertyMetadata(24d));

		public double PathWidth {
			get { return (double)GetValue(PathWidthProperty); }
			set { SetValue(PathWidthProperty, value); }
		}

		public static readonly DependencyProperty PathWidthProperty =
			DependencyProperty.Register(
				name: nameof(PathWidth),
				propertyType: typeof(double),
				ownerType: typeof(VectorIcon),
				typeMetadata: new PropertyMetadata(24d));

		public Geometry Geometry {
			get { return (Geometry)GetValue(GeometryProperty); }
			set { SetValue(GeometryProperty, value); }
		}
		public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register(
			name: nameof(Geometry),
			propertyType: typeof(Geometry),
			ownerType: typeof(VectorIcon),
			typeMetadata: new PropertyMetadata(null));
	}
}
