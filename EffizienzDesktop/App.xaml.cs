﻿using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Effizienz {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		private ResourceDictionary themeDark;
		private ResourceDictionary themeBright;
		private string themeDirectory = "/Effizienz;component/Themes/";

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeBright = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeBright.xaml", UriKind.RelativeOrAbsolute) };

			Laden( ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
			Laden(ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
			Laden(ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));

		}

		public void SetTheme( bool _DarkMode ) {
			Resources.MergedDictionaries[0].MergedDictionaries.Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(	_DarkMode ? themeDark : themeBright );
		}

		private void Laden<T>( ObservableCollection<T> _inputListe, string _listenName ) {
			ObservableCollection<T> neueListe;
			XMLHandler.Laden(out neueListe, _listenName);
			foreach( T item in neueListe ) {
				_inputListe.Add(item);
			}
		}
		public void Speichern<T>( ObservableCollection<T> _inputListe, string _listenName ) {
			XMLHandler.Speichern(_inputListe, _listenName);
			MessageBoxDisplayer.ListeGespeichert(_listenName);
		}

	}
}
