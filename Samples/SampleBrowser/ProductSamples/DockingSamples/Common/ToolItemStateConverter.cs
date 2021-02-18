using System;
using System.Globalization;

#if WINRT
using Windows.UI.Xaml.Data;
using ActiproSoftware.UI.Xaml.Controls.Docking;
#else
using System.Windows.Data;
using ActiproSoftware.Windows.Controls.Docking;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {
	
	//
	// NOTE: This converter and the related ToolItemState enum can be used in scenarios where you don't wish for your models to directly 
	//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
	//       is nothing wrong with directly referencing DockingWindowState in your VM class to avoid having to use this abstraction layer
	//

	/// <summary>
	/// Represents a value converter that can convert a <see cref="ToolItemState"/> to a <see cref="DockingWindowState"/>.
	/// </summary>
	public sealed class ToolItemStateConverter : IValueConverter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#if WINRT
		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="language">The language of the conversion.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, string language) {
		#else
		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			var state = (ToolItemState)value;
			switch (state) {
				case ToolItemState.AutoHide:
					return DockingWindowState.AutoHide;
				case ToolItemState.Docked:
					return DockingWindowState.Docked;
				default:
					return DockingWindowState.Document;
			}
		}

		#if WINRT
		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding target to the binding source.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="language">The language of the conversion.</param>
		/// <returns>A converted value.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, string language) {
		#else
		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding target to the binding source.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			var state = (DockingWindowState)value;
			switch (state) {
				case DockingWindowState.AutoHide:
					return ToolItemState.AutoHide;
				case DockingWindowState.Docked:
					return ToolItemState.Docked;
				default:
					return ToolItemState.Document;
			}
		}

	}

}