using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.ColorBrowser;

/// <summary>
/// Stores information about resource data.
/// </summary>
public class NamedColor {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="color">The color.</param>
	protected NamedColor(string name, Color color, bool isSystemColor) {
		Brush = new SolidColorBrush(color);
		Color = color;
		Name = name;
		IsSystemColor = isSystemColor;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The brush.
	/// </summary>
	public Brush Brush { get; }

	/// <summary>
	/// The color.
	/// </summary>
	public Color Color { get; }

	/// <summary>
	/// The color names from <see cref="System.Windows.Media.Colors"/>.
	/// </summary>
	public static IEnumerable<NamedColor> Colors {
		get => typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Select(p => new NamedColor(p.Name, (Color)p.GetValue(obj: null, index: null)!, isSystemColor: false));
	}

	/// <summary>
	/// Indicates whether this instance is system color.
	/// </summary>
	public bool IsSystemColor { get; }

	/// <summary>
	/// The name.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The color names from <see cref="System.Windows.SystemColors"/>.
	/// </summary>
	public static IEnumerable<NamedColor> SystemColors {
		get {
			foreach (var p in typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static)) {
				if (p.PropertyType == typeof(Color)) {
					var name = p.Name;
					if (name.EndsWith("Color"))
						name = name.Substring(0, name.Length - "Color".Length);
					yield return new NamedColor(name, (Color)p.GetValue(obj: null, index: null)!, isSystemColor: true);
				}
			}
		}
	}

}
