﻿namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a ribbon backstage header.
	/// </summary>
	public class RibbonBackstageHeaderSeparatorViewModel : ObservableObjectBase, IHasTag {

		private RibbonBackstageHeaderAlignment headerAlignment = RibbonBackstageHeaderAlignment.Top;
		private bool isVisible = true;
		private object tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonBackstageHeaderSeparatorViewModel()  // Parameterless constructor required for XAML support
			: this(RibbonBackstageHeaderAlignment.Top) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified header alignment.
		/// </summary>
		/// <param name="headerAlignment">A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.</param>
		public RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment headerAlignment) {
			this.headerAlignment = headerAlignment;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// The default value is <see cref="RibbonBackstageHeaderAlignment.Top"/>.
		/// </value>
		public RibbonBackstageHeaderAlignment HeaderAlignment {
			get => headerAlignment;
			set {
				if (headerAlignment != value) {
					headerAlignment = value;
					this.NotifyPropertyChanged(nameof(HeaderAlignment));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => isVisible;
			set {
				if (isVisible != value) {
					isVisible = value;
					this.NotifyPropertyChanged(nameof(IsVisible));
				}
			}
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object Tag {
			get => tag;
			set {
				if (tag != value) {
					tag = value;
					this.NotifyPropertyChanged(nameof(Tag));
				}
			}
		}

	}

}
