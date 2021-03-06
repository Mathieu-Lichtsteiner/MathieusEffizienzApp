﻿using System;
using System.Threading.Tasks;

namespace LogicLayer.Commands {
	public class RelayCommandAsync : IRelayCommand {

		#region private fields
		private bool _IsExecuting;
		private readonly Func<object?, Task> _Execute;
		private readonly Predicate<object?>? _CanExecute;
		private readonly Action<Exception>? _OnException;
		#endregion

		#region public properties
		public bool IsExecuting {
			get => _IsExecuting;
			private set {
				_IsExecuting = value;
				CanExecuteChanged?.Invoke( this, EventArgs.Empty );
			}
		}
		#endregion

		#region public events
		public event EventHandler? CanExecuteChanged;
		public void RaiseCanExecuteChanged( object? sender, EventArgs? e = null )
			=> CanExecuteChanged?.Invoke( sender, e ?? EventArgs.Empty );
		#endregion

		#region constructor
		public RelayCommandAsync( Func<object?, Task> execute, Predicate<object?>? canExecute = null, Action<Exception>? onException = null ) {
			if( execute is null )
				throw new NullReferenceException( "Execute has to be set!" );
			_Execute = execute;
			_CanExecute = canExecute;
			_OnException = onException;
		}
		#endregion

		#region public methods
		public async Task ExecuteAsync( object? parameter )
			=> await _Execute( parameter );
		public bool CanExecute( object? parameter )
			=> IsExecuting is false && (_CanExecute is null || _CanExecute( parameter ));
		public async void Execute( object? parameter ) {
			IsExecuting = true;
			try {
				await ExecuteAsync( parameter );
			}
			catch( Exception ex ) {
				_OnException?.Invoke( ex );
			}
			IsExecuting = false;
		}
		#endregion

	}
}