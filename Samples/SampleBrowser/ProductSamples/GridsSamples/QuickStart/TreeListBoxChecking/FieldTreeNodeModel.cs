using System;
using System.Globalization;
using System.Windows.Input;
using ActiproSoftware.ProductSamples.GridsSamples.Common;

#if WINRT
using Windows.UI.Xaml.Media.Imaging;
using ActiproSoftware.UI.Xaml.Input;
#else
using System.Windows;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Input;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxChecking {
	
	/// <summary>
	/// Provides a tree node model implementation for tracking optional fields.
	/// </summary>
	public class FieldTreeNodeModel : CheckableTreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FieldTreeNodeModel"/> class.
		/// </summary>
		public FieldTreeNodeModel() {
			var imageUri = new Uri("/Images/Icons/New16.png", UriKind.Relative);
			this.ImageSource = new BitmapImage(imageUri);

			this.IsCheckable = true;

			this.ShowDialogCommand = new DelegateCommand<object>(p => {
				MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "Show custom dialog here for item '{0}'.", this.Name), "Button Clicked");
			});
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that can be used to show a dialog.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that can be used to show a dialog.
		/// </value>
		public ICommand ShowDialogCommand { get; private set; }
		
	}

}
