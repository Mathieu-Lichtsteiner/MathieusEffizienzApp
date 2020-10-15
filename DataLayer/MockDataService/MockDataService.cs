﻿using DataLayer.Interfaces;
using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace DataLayer.MockDataService {
	public class MockDataService : IDataService<ObservableCollection<Category>, Category> {

		public ObservableCollection<Category> LoadData() {
			var placeholder = new ObservableCollection<Category>();
			for( int counter = 0; counter <= 3; counter++ ) {
				Random randomGen = new Random();
				Color randomColor = Color.FromArgb( 255,
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255),
					(byte)randomGen.Next(255));

				Category CodeCat = new Category(new UserText($"Generated-Category{counter}",null,randomColor)){
					Children = {

						new Goal(new UserText($"Generated-Goal{counter}_1", null, randomColor), new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(8))) {
							Children = {
								new Goal( new UserText($"Generated-Goal{counter}_1.1", null, randomColor), new DateSpan(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5))){
									WorkHours = {
										new WorkItem(DateTime.Today.AddDays(randomGen.Next(-10, 11)), TimeSpan.FromHours(randomGen.NextDouble() * 5))
									}
								}

							}
						},
						new Goal(new UserText($"Generated-Goal{counter}_2", null, randomColor), new DateSpan(DateTime.Today, DateTime.Today.AddDays(10)))
					},
					WorkPlan = {
						((DayOfWeek)randomGen.Next(7),
						new DoubleTime((0.0 + counter, 1.0 + counter)))
					}
				};

				placeholder.Add(CodeCat);
			}
			return placeholder;
		}

		public void SaveData( ObservableCollection<Category> Collection ) {
			Debug.WriteLine($"Tried to save the {nameof(Collection)}[{Collection}] in the MockDataService");
		}

	}
}
