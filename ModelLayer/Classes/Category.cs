﻿using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Planning;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Category : ObservableObject, IUnique, IStatus, IParent<Goal> {

		#region fields

		private string? title;
		private string? description;

		private bool isParent;

		private Color color;
		private EnumState status;

		#endregion

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID {
			get;
		}
		[XmlAttribute("Title")]
		public string Title {
			get {
				return title ??= "New_Category!";
			}
			set {
				title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		[XmlAttribute("Description")]
		public string Description {
			get {
				return description ??= "This is a new standardCategory!";
			}
			set {
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		// IParent
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent {
			get {
				return isParent;
			}
			set {
				isParent = value;
				OnPropertyChanged(nameof(IsParent));
			}
		}

		// WeekPlan
		public ObservableCollection<(DayOfWeek Day, DayTime Time)> WorkTimes { get; set; }
		public event EventHandler<NotifyCollectionChangedEventArgs>? WeekPlanChanged;

		// IStatus
		[XmlAttribute("Status")]
		public EnumState State {
			get {
				return status;
			}
			set {
				status = value;
				OnPropertyChanged(nameof(State));
			}
		}

		// IColorfull
		[XmlElement]
		public Color Color {
			get {
				return color;
			}
			set {
				color = value;
				OnPropertyChanged(nameof(Color));
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Category must have a Titel and a Color!
		/// </summary>
		public Category() {
			this.ID = Guid.NewGuid();
			this.State = EnumState.ToDo;

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;

			// initialize WeekPlan
			this.WorkTimes = new ObservableCollection<(DayOfWeek Day, DayTime Time)>();
			this.WorkTimes.CollectionChanged += WorkTimes_CollectionChanged;
		}

		public Category( string _Titel, Color _Farbe, string _Description = "New Category", EnumState _Status = EnumState.ToDo ) : this() {
			this.Title = _Titel;
			this.Description = _Description;
			this.Color = _Farbe;
			this.State = _Status;
		}

		~Category() { }

		#endregion

		#region Methods

		private void WorkTimes_CollectionChanged( object sender, NotifyCollectionChangedEventArgs e ) => WeekPlanChanged?.Invoke(this, e);

		protected virtual void CheckIfChildrenEmpty( object sender, NotifyCollectionChangedEventArgs e ) {
			if( Children.Count <= 0 ) {
				this.IsParent = false;
				return;
			}
			this.IsParent = true;
		}

		public Goal? GetChild( Guid ID ) {
			Goal? placeholder;
			foreach( Goal child in this.Children ) {
				placeholder = child.GetChild(ID);
				if( placeholder is { ID: Guid phID } && ID == phID ) {
					return placeholder;
				}
			}
			return null;
		}
		public void AddChild( Goal _Child ) {
			_Child.ParentID = this.ID;
			_Child.Color = this.Color;

			this.Children.Add(_Child);
		}
		public void AddChildren( Collection<Goal> _Children ) {
			foreach( Goal child in _Children ) {
				AddChild(child);
			}
		}

		#endregion

	}
}
