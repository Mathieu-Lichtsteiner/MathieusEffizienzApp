﻿using Effizienz.Classes;
using System.Collections.ObjectModel;
using System.Windows;

namespace Effizienz.Views {

	public class ViewModelDashboard : ViewModelBase {

		#region fields

		#endregion

		#region properties

		public ObservableCollection<Kategorie> Kategorien
			=> ( Application.Current as App ).KategorienListe;

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion
	}
}
