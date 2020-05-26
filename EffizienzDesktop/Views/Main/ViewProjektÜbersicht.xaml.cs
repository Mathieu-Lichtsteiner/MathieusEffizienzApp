﻿using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Effizienz.Views {

	public partial class ViewProjektÜbersicht : UserControl, IParsable {

		public ViewProjektÜbersicht() {
			InitializeComponent();
			ObservableCollection<Classes.MenuItem> ProjektListe = new ObservableCollection<Classes.MenuItem>();
			foreach( Projekt item in ListContainer.ProjektListe ) {
				ProjektListe.Add(new Classes.MenuItem(item));
			}
			this.TreeView_Projekte.ItemsSource = ProjektListe;
		}

		~ViewProjektÜbersicht() { }

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
