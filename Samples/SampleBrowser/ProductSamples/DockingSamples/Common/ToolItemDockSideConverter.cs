using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

//
// NOTE: This converter and the related ToolItemDockSide enum can be used in scenarios where you don't wish for your models to directly
//   reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
//   is nothing wrong with directly referencing Side in your VM class to avoid having to use this abstraction layer
//

/// <summary>
/// Represents a value converter that can convert a <see cref="ToolItemDockSide"/> to a <see cref="Side"/>.
/// </summary>
public sealed class ToolItemDockSideConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var side = (ToolItemDockSide)value!;
		return side switch {
			ToolItemDockSide.Left => Side.Left,
			ToolItemDockSide.Top => Side.Top,
			ToolItemDockSide.Right => Side.Right,
			ToolItemDockSide.Bottom or _ => Side.Bottom
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var side = (Side)value!;
		return side switch {
			Side.Left => ToolItemDockSide.Left,
			Side.Top => ToolItemDockSide.Top,
			Side.Right => ToolItemDockSide.Right,
			Side.Bottom or _ => ToolItemDockSide.Bottom
		};
	}

}
