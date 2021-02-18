using System;
using System.Linq;
using System.Windows.Input;
using ActiproSoftware.ProductSamples.GridsSamples.Common;

#if WINRT
using Windows.UI.Xaml.Media.Imaging;
using ActiproSoftware.UI.Xaml.Input;
#else
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Input;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Provides a tree node model implementation for a toolbox control.
	/// </summary>
	public class ControlTreeNodeModel : ToolboxTreeNodeModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlTreeNodeModel"/> class.
		/// </summary>
		/// <param name="data">The control data to be represented by the model.</param>
		public ControlTreeNodeModel(ControlData data) {
			this.Data = data ?? throw new ArgumentNullException(nameof(data));
			this.Name = GetControlNameOnly(data.FullName);
			this.ImageSource = new BitmapImage(new Uri($"/Images/Icons/Toolbox{Category}{Name}16.png", UriKind.Relative));

			this.ToggleFavoriteCommand = new DelegateCommand<object>(p => {
				this.IsFavorite = !this.IsFavorite;
			});
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the data used by the model.
		/// </summary>
		private ControlData Data { get; }

		/// <summary>
		/// Gets only the name of the control from the full name.
		/// </summary>
		/// <param name="fullName">The full name of the control.</param>
		/// <returns>The name of the control.</returns>
		private static string GetControlNameOnly(string fullName) {
			// Full name includes the namespace, so the last part of the full name is the control
			return fullName.Split('.').Last();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the category for the control.
		/// </summary>
		public string Category {
			get {
				return Data.Category;
			}
		}

		/// <summary>
		/// Gets the text to be assigned to a <see cref="System.Windows.IDataObject"/> for the <see cref="System.Windows.DataFormats.Text"/> format.
		/// </summary>
		public override string DataObjectText {
			get {
				// Use the full control name as the default text for drag operations
				return FullName;
			}
		}

		/// <summary>
		/// Gets the default value to be assigned to <see cref="TreeNodeModel.IsDraggable"/>.
		/// </summary>
		protected override bool DefaultIsDraggable {
			get {
				// Allow controls to be dragged
				return true;
			}
		}

		/// <summary>
		/// Gets the full name of the control.
		/// </summary>
		public string FullName {
			get {
				return Data.FullName;
			}
		}

		/// <summary>
		/// Gets or sets whether the control is a favorite.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control is a favorite; otherwise, <c>false</c>.
		/// </value>
		public bool IsFavorite {
			get {
				return ControlDataRepository.Instance.IsFavorite(Data);
			}
			set {
				if (this.IsFavorite == value)
					return;

				if (value)
					ControlDataRepository.Instance.AddFavorite(Data);
				else
					ControlDataRepository.Instance.RemoveFavorite(Data);

				this.NotifyPropertyChanged(nameof(IsFavorite));
			}
		}

		/// <summary>
		/// Gets the <see cref="ICommand"/> that can be used to toggle if the control is a favorite.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that can be used to toggle if the control is a favorite.
		/// </value>
		public ICommand ToggleFavoriteCommand { get; }

	}

}
