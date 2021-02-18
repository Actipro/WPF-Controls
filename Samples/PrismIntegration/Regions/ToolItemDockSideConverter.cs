using System;
using System.Globalization;
using System.Windows.Data;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;

namespace ActiproSoftware.Windows.PrismIntegration.Regions {
	
	//
	// NOTE: This converter and the related ToolItemDockSide enum can be used in scenarios where you don't wish for your models to directly 
	//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
	//       is nothing wrong with directly referencing Side in your VM class to avoid having to use this abstraction layer
	//

	/// <summary>
	/// Represents a value converter that can convert a <see cref="ToolItemDockSide"/> to a <see cref="Side"/>.
	/// </summary>
	public sealed class ToolItemDockSideConverter : IValueConverter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var dockSide = (ToolItemDockSide)value;
			switch (dockSide) {
				case ToolItemDockSide.Left:
					return Side.Left;
				case ToolItemDockSide.Top:
					return Side.Top;
				case ToolItemDockSide.Right:
					return Side.Right;
				default:
					return Side.Bottom;
			}
		}

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding target to the binding source.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			var dockSide = (Side)value;
			switch (dockSide) {
				case Side.Left:
					return ToolItemDockSide.Left;
				case Side.Top:
					return ToolItemDockSide.Top;
				case Side.Right:
					return ToolItemDockSide.Right;
				default:
					return ToolItemDockSide.Bottom;
			}
		}

	}

}