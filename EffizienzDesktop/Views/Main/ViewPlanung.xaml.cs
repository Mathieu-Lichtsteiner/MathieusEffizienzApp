﻿using EffizienzNeu.Interfaces;
using System;
using System.Collections.Generic;
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

namespace EffizienzNeu.Views {

	public partial class ViewPlanung : UserControl, IParsable {
		
		public ViewPlanung() {
			InitializeComponent();
		}

		~ViewPlanung() { }

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
