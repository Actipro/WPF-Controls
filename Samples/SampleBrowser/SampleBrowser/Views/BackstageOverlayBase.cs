using ActiproSoftware.Windows.Controls.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides a base class for a Backstage overlay.
	/// </summary>
	public partial class BackstageOverlayBase : UserControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>BackstageOverlayBase</c> class.
		/// </summary>
		public BackstageOverlayBase() {
			this.DataContextChanged += this.OnDataContextChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the data context changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			var oldViewModel = e.OldValue as ApplicationViewModel;
			var newViewModel = e.NewValue as ApplicationViewModel;

			if (oldViewModel != null)
				oldViewModel.PropertyChanged -= this.OnViewModelPropertyChanged;

			if (newViewModel != null)
				newViewModel.PropertyChanged += this.OnViewModelPropertyChanged;
		}

		/// <summary>
		/// Occurs when a property changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> containing data related to this event.</param>
		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case nameof(ApplicationViewModel.IsBackstageOpen):
					if (this.ViewModel.IsBackstageOpen) {
						var scrollViewer = this.ScrollViewer;
						scrollViewer?.ScrollToTop(TimeSpan.Zero);
					}
					break;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the primary scroll viewer.
		/// </summary>
		/// <value>The primary scroll viewer.</value>
		public virtual InertiaScrollViewer ScrollViewer => null;

		/// <summary>
		/// Gets the view-model for this view.
		/// </summary>
		/// <value>The view-model for this view.</value>
		public ApplicationViewModel ViewModel => (ApplicationViewModel)this.DataContext;
		
	}

}
