﻿using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Planning;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Category : ObservableObject, IUnique, IStatus, IParent<Goal> {

		#region fields

		private string? _Title;
		private string? _Description;

		private bool _IsParent;

		private string _ColorHex;
		private EnumState _State;

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
				return _Title ??= "New_Category!";
			}
			set {
				_Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		[XmlAttribute("Description")]
		public string Description {
			get {
				return _Description ??= "This is a new standardCategory!";
			}
			set {
				_Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		// IParent
		[XmlArray("Children")]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent {
			get {
				return _IsParent;
			}
			set {
				_IsParent = value;
				OnPropertyChanged(nameof(IsParent));
			}
		}

		// WeekPlan
		[XmlArray("WorkTimes")]
		public ObservableCollection<(DayOfWeek Day, DayTime Time)> WorkTimes { get; set; }
		public event EventHandler<NotifyCollectionChangedEventArgs>? WeekPlanChanged;

		// IStatus
		[XmlAttribute("Status")]
		public EnumState State {
			get {
				return _State;
			}
			set {
				_State = value;
				OnPropertyChanged(nameof(State));
			}
		}

		// IColorfull
		[XmlAttribute("Color")]
		public string ColorHex {
			get {
				return _ColorHex;
			}
			set {
				_ColorHex = value;
				OnPropertyChanged(nameof(ColorHex));
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

		public Category( string title, string colorHex, string description = "New Category", EnumState state = EnumState.ToDo ) : this() {
			this.Title = title;
			this.Description = description;
			this.ColorHex = colorHex;
			this.State = state;
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
			_Child.ColorHex = this.ColorHex;

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
