﻿using ModelLayer.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EffizienzControls {

	public class GanttElement : Control {

		#region properties
		public Goal Goal {
			get { return (Goal)GetValue(GoalProperty); }
			set { SetValue(GoalProperty, value); }
		}
		public static readonly DependencyProperty GoalProperty =
			DependencyProperty.Register(nameof(Goal), typeof(Goal), typeof(GanttElement), new FrameworkPropertyMetadata( new Goal(), FrameworkPropertyMetadataOptions.AffectsRender ));
		public DateTime MainStart {
			get { return (DateTime)GetValue(MainStartProperty); }
			set { SetValue(MainStartProperty, value); }
		}
		public static readonly DependencyProperty MainStartProperty =
			DependencyProperty.Register(nameof(MainStart), typeof(DateTime), typeof(GanttElement), new PropertyMetadata(DateTime.Today.AddDays(-1)));
		public DateTime MainEnd {
			get { return (DateTime)GetValue(MainEndProperty); }
			set { SetValue(MainEndProperty, value); }
		}
		public static readonly DependencyProperty MainEndProperty =
			DependencyProperty.Register(nameof(MainEnd), typeof(DateTime), typeof(GanttElement), new PropertyMetadata( DateTime.Today.AddDays(10)));
		public double TimeLineWidth {
			get { return (double)GetValue(TimeLineWidthProperty); }
			set { SetValue(TimeLineWidthProperty, value); }
		}
		public static readonly DependencyProperty TimeLineWidthProperty =
			DependencyProperty.Register(nameof(TimeLineWidth), typeof(double), typeof(GanttElement), new PropertyMetadata(0.0));
		public double TextWidth {
			get { return (double)GetValue(TextWidthProperty); }
			set { SetValue(TextWidthProperty, value); }
		}
		public static readonly DependencyProperty TextWidthProperty =
			DependencyProperty.Register(nameof(TextWidth), typeof(double), typeof(GanttElement), new PropertyMetadata(0.0));
		public Style BorderStyle {
			get { return (Style)GetValue(BorderStyleProperty); }
			set { SetValue(BorderStyleProperty, value); }
		}
		public static readonly DependencyProperty BorderStyleProperty =
			DependencyProperty.Register(nameof(BorderStyle), typeof(Style), typeof(GanttElement), new PropertyMetadata( new Style(typeof(Border)) ));
		#endregion

		#region initializer
		static GanttElement() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(GanttElement), new FrameworkPropertyMetadata(typeof(GanttElement)));
		}
		#endregion
	}
}