﻿using LogicLayer.ViewModels;
using ModelLayer.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LogicLayer {

	//TODO I should catch the exceptions, which can be thrown by reflection (In constructor and private Methods)
	public class ValidationViewModel : ViewModelBase, INotifyDataErrorInfo {

		#region private fields
		private readonly Type _VMType;
		private readonly List<Type> _ValidationAttributes = new List<Type>{
				typeof(CompareAttribute),
				typeof(DataTypeAttribute),
				typeof(MaxLengthAttribute),
				typeof(MinLengthAttribute),
				typeof(RangeAttribute),
				typeof(RegularExpressionAttribute),
				typeof(RequiredAttribute),
				typeof(StringLengthAttribute),
				typeof(CustomValidationAttribute)
			};
		private readonly Dictionary<string, List<ValidationAttribute>> _PropertyValidations = new Dictionary<string, List<ValidationAttribute>>();
		private readonly Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
		#endregion

		#region public properties
		public bool NoErrors
			=> !HasErrors;
		public bool HasErrors
			=> _PropertyErrors.Values.Any( errorList => errorList.Count > 0 );
		#endregion

		#region Events
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
		protected void RaiseErrorsChanged( string? propertyName ) {
			ErrorsChanged?.Invoke( this, new DataErrorsChangedEventArgs( propertyName ) );
			Debug.WriteLine( $"Raised ErrorsChanged with the Property {propertyName}" );
		}
		#endregion

		#region constructor
		public ValidationViewModel() {
			_VMType = GetType();
			InitPropertyValidations();
			//Hook into the PropertyChanged Event of validated Properties
			PropertyChanged += ( sender, e ) => {
				if( _PropertyValidations.ContainsKey( e.PropertyName ) )
					CollectToPropertyErrors( e.PropertyName );
			};
			//Initialize all Errors
			Parallel.ForEach( _PropertyValidations.Keys, propertyName => CollectToPropertyErrors( propertyName ) );
		}
		#endregion

		#region public methods
		public IEnumerable? GetErrors( string propertyName )
			=> _PropertyErrors.GetValueOrDefault( propertyName, new List<string>() );
		#endregion

		#region private methods
		private void InitPropertyValidations()
			=> _PropertyValidations.AddRange(
				_VMType.GetProperties( BindingFlags.Public | BindingFlags.Instance ) // All public nonstatic properties
				.Where( prop => _ValidationAttributes.Any( attrType => prop.IsDefined( attrType, false ) ) ) // where atleast one attribute is defined
				.Select( prop => new KeyValuePair<string, List<ValidationAttribute>>(
					 prop.Name,
					 new List<ValidationAttribute>(
						 _ValidationAttributes.Select( attrType => (ValidationAttribute) prop.GetCustomAttribute( attrType, false ) ) // get all Attributes of the specified types in ValidationAttributes
						 .Where( attr => attr is ValidationAttribute ) ) // filter out null values
					 ) )
				.ToList() );
		private void CollectToPropertyErrors( string? propertyName ) {
			if( string.IsNullOrEmpty( propertyName ) )
				return;

			int oldErrorsCount = _PropertyErrors.GetValueOrDefault( propertyName )?.Count ?? 0;
			InitOrClearPropertyErrors( propertyName );

			if( GetValidationErrors( propertyName ) is List<string> errors ) {
				if( errors.Count > 0 )
					_PropertyErrors[propertyName].AddUniqueRange( errors );
				if( errors.Count != oldErrorsCount )
					RaiseErrorsChanged( propertyName );
			}
		}
		private void InitOrClearPropertyErrors( string propertyName ) {
			if( _PropertyErrors.ContainsKey( propertyName ) )
				_PropertyErrors[propertyName].Clear();
			else
				_PropertyErrors.Add( propertyName, new List<string>() );
		}
		private List<string>? GetValidationErrors( string propertyName )
			=> _PropertyValidations.GetValueOrDefault( propertyName ) // get the validationAttributes
				.Select( vAttr => ValidateProperty( propertyName, vAttr ) ) // validate property with 
				.Where( err => string.IsNullOrEmpty( err ) is false ) // filter out null values
			.ToList()
			as List<string>;
		private string? ValidateProperty( string propName, ValidationAttribute validAttribute )
			=> validAttribute.IsValid( GetPropertyValue( propName ) ) ? null : validAttribute.ErrorMessage;
		private object GetPropertyValue( string propName )
			=> _VMType.GetProperty( propName ).GetValue( this );
		#endregion

	}
}
