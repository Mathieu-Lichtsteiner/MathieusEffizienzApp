﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace DataLayer {

	public class XMLCollectionHandler<T> : IRepository<ObservableCollection<T>, T>, IErrorHandler {

		#region private fields

		private string _FilePath;
		private string _FileName;

		#endregion

		#region public Events

		/// <summary>
		/// Handles Exceptions: <see cref="FileNotFoundException"/>
		/// </summary>
		public event ErrorEventHandler? ErrorOccured;

		#endregion

		#region constructor

		public XMLCollectionHandler( string fileName, string filePath ) {

			_FileName = fileName;
			_FilePath = filePath;

			if( _FileName.EndsWith( ".xml" ) is false )
				_FileName = string.Concat( fileName, ".xml" );
			if( _FilePath.EndsWith( "/" ) is false )
				_FilePath = string.Concat( filePath, '/' );
		}

		#endregion

		#region methods

		protected void OnErrorOccured( Exception e ) => ErrorOccured?.Invoke( this, new ErrorEventArgs( e ) );

		#endregion

		#region save

		public void SaveData( ObservableCollection<T> Collection ) {
			try {
				using( var fileStream = new FileStream( _FilePath + _FileName, FileMode.Create ) ) {
					var Serializer = new XmlSerializer( typeof( ObservableCollection<T> ) );
					Serializer.Serialize( fileStream, Collection );
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured( e );
			}
		}

		#endregion

		#region load

		public ObservableCollection<T> LoadData() {
			var LoadingList = new ObservableCollection<T>();
			try {
				using( var fileStream = new FileStream( _FilePath + _FileName, FileMode.Open ) ) {
					var Serializer = new XmlSerializer( typeof( ObservableCollection<T> ) );
					LoadingList = Serializer.Deserialize( fileStream ) as ObservableCollection<T> ?? new ObservableCollection<T>();
				}
			}
			catch( FileNotFoundException e ) {
				OnErrorOccured( e );
			}
			return LoadingList;
		}

		#endregion

	}
}
