using System;
using System.Windows.Media.Imaging;
using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a tool view-model for the sample.
	/// </summary>
	/// <remarks>
	/// This view-model derives from a base class that initializes the <c>ToolWindow</c> from instance properties.
	/// </remarks>
	public class ToolboxToolItemViewModel : ToolItemViewModel {

		public const string SerializationIdText = "ToolboxToolWindow";
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolboxToolItemViewModel"/> class.
		/// </summary>
		public ToolboxToolItemViewModel() {
			this.DefaultDockSide = ToolItemDockSide.Left;
			this.ImageSource = new BitmapImage(new Uri("/Resources/Images/Toolbox16.png", UriKind.Relative));
			this.SerializationId = SerializationIdText;
			this.State = ToolItemState.AutoHide;
			this.Title = "Toolbox";
		}

	}

}
