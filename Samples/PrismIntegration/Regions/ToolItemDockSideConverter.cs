using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using System.Globalization;

namespace ActiproSoftware.Windows.PrismIntegration.Regions;

//
// NOTE: This converter and the related ToolItemDockSide enum can be used in scenarios where you don't wish for your models to directly 
//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
//       is nothing wrong with directly referencing Side in your VM class to avoid having to use this abstraction layer
//

/// <summary>
/// Represents a value converter that can convert a <see cref="ToolItemDockSide"/> to a <see cref="Side"/>.
/// </summary>
public sealed class ToolItemDockSideConverter : IValueConverter {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static object? Convert(object? value, Type targetType) {
		if (targetType == typeof(Side)) {
			if (value is Side typedValue)
				return typedValue;
			else if (value is ToolItemDockSide otherSide) {
				return otherSide switch {
					ToolItemDockSide.Left => Side.Left,
					ToolItemDockSide.Top => Side.Top,
					ToolItemDockSide.Right => Side.Right,
					ToolItemDockSide.Bottom or _ => Side.Bottom
				};
			}
		}
		else if (targetType == typeof(ToolItemDockSide)) {
			if (value is ToolItemDockSide typedValue)
				return typedValue;
			else if (value is Side otherSide) {
				return otherSide switch {
					Side.Left => ToolItemDockSide.Left,
					Side.Top => ToolItemDockSide.Top,
					Side.Right => ToolItemDockSide.Right,
					Side.Bottom or _ => ToolItemDockSide.Bottom
				};
			}
		}
		return null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> Convert(value, targetType);

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> Convert(value, targetType);

}
