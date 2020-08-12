﻿using LogicLayer.Manager;
using System.Windows;

namespace FrontLayer {
	public partial class App : Application {

		#region constructor

		public App() {
		}

		#endregion

		#region methods

		protected override void OnStartup( StartupEventArgs e ) {
			// Set the Theme to the standard-Theme
			Resources.MergedDictionaries[0].Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(ThemeManager.SelectedTheme);

			// Load the saved Categories from The XML-File
			FileManager.LoadCategories();
			base.OnStartup(e);
		}

		#endregion

	}
}
