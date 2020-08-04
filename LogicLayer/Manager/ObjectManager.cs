﻿using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region properties

		/// <summary>
		/// Die Globale KategorienListe, welche gespeichert wird und alle Kategorien enthalten Sollte.
		/// </summary>
		public static ObservableCollection<Kategorie> KategorienListe { get; set; }

		#endregion

		#region constructor
		static ObjectManager() {

			KategorienListe = new ObservableCollection<Kategorie>();

		}

		#endregion

		#region methods

		public static void GenerateObjects() {
			Kategorie CBKategorie = new Kategorie("CodeBehind-Kategorie", Colors.Magenta);
			Projekt CBProjekt = new Projekt("CodeBehind-Projekt", CBKategorie.ID, DateTime.Today.AddDays(1), DateTime.Today.AddDays(10)){
				Zeit = new TimeSpan(1, 2, 3)
			};
			Aufgabe CBAufgabe = new Aufgabe("CodeBehind-Aufgabe", CBProjekt.ID, DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Zeit = new TimeSpan(3, 12, 20)
			};

			CBProjekt.Aufgaben.Add(CBAufgabe);
			CBKategorie.Projekte.Add(CBProjekt);
			KategorienListe.Add(CBKategorie);
		}

		#endregion
	}
}
