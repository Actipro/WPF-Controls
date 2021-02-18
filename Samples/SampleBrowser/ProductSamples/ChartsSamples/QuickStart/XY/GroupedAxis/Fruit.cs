using System;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.GroupedAxis {

	/// <summary>
	/// Represents a fruit, used for sample data.
	/// </summary>
	public class Fruit {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="Fruit"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="color">The color.</param>
		/// <param name="calories">The calories.</param>
		public Fruit(string name, string color, double calories){
			Name = name;
			Color = color;
			Calories = calories;
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public string Color { get; set; }

		/// <summary>
		/// Gets or sets the calories.
		/// </summary>
		/// <value>The calories.</value>
		public double Calories { get; set; }

		#endregion PUBLIC PROCEDURES
	}
}
