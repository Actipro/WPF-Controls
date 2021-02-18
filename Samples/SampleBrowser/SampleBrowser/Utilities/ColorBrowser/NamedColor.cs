using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace ActiproSoftware.SampleBrowser.Utilities.ColorBrowser {

	/// <summary>
	/// Stores information about resource data.
	/// </summary>
	public class NamedColor {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="NamedColor"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="color">The color.</param>
		protected NamedColor(string name, Color color, bool isSystemColor) {
			this.Brush = new SolidColorBrush(color);
			this.Color = color;
			this.Name = name;
			this.IsSystemColor = isSystemColor;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Brush Brush { get; private set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color { get; private set; }

		/// <summary>
		/// Gets the color names from <see cref="Colors"/>.
		/// </summary>
		public static IEnumerable<NamedColor> Colors {
			get {
				foreach (PropertyInfo p in typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static))
					yield return new NamedColor(p.Name, (Color)p.GetValue(null, null), false);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is system color.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is system color; otherwise, <c>false</c>.
		/// </value>
		public bool IsSystemColor { get; private set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the color names from <see cref="SystemColors"/>.
		/// </summary>
		public static IEnumerable<NamedColor> SystemColors {
			get {
				foreach (PropertyInfo p in typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static)) {
					if (typeof(Color) == p.PropertyType) {
						string name = p.Name;
						if (name.EndsWith("Color"))
							name = name.Substring(0, name.Length - 5);
						yield return new NamedColor(name, (Color)p.GetValue(null, null), true);
					}
				}
			}
		}

	}

}