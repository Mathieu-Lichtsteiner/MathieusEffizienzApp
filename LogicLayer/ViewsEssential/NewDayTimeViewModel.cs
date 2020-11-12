﻿using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.Validation;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewDayTimeViewModel : ValidationViewModel {

		#region private fields
		private ICommand? _SaveDayTimeCommand;
		#endregion

		#region public properties
		public IAccountableParent<Category> CategoryList { get; }
		[CustomValidation( typeof( ValidationRules ), nameof( ValidationRules.ValidateCategory ) )]
		public Category? SelectedCategory { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The Day has to be defined!" )]
		public DayOfWeek? DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public string? Warning { get; set; }
		#endregion

		#region public commands
		public ICommand SaveDayTimeCommand => _SaveDayTimeCommand ??=
			 new RelayCommandAsync(
				 parameter => AddToWeekPlan(),
				 parameter => NoErrors,
				 ex => {
					 if( ex is ArgumentException )
						 Warning = $"{ex.Message}";
					 else
						 AlertManager.InputInkorrekt( ex.Message );
				 } );
		#endregion

		#region constructor
		public NewDayTimeViewModel( IAccountableParent<Category> categoryList ) {
			CategoryList = categoryList;
			ErrorsChanged += OnErrorsChanged;
		}
		#endregion

		#region private methods
		private Task AddToWeekPlan()
			=> ObjectManager.WeekPlan.AddItemToDayAsync( DayOfWeek!.Value, new PlanItem( new DoubleTime( StartTime, EndTime ), SelectedCategory! ) );
		private void OnErrorsChanged( object sender, DataErrorsChangedEventArgs e )
			=> (SaveDayTimeCommand as RelayCommandAsync)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
