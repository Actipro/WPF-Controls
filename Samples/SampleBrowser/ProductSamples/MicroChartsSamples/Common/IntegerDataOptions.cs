using System;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Stores options for <see cref="IntegerData"/> generation.
	/// </summary>
	public class IntegerDataOptions {

		private int		count				= 12;
		private string	description;
		private int		startValue			= 10;
		private int		stepRange			= 8;
		private double	trendPercentage		= 0.5;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the number of data objects to generate.
		/// </summary>
		/// <value>The number of data objects to generate.</value>
		public int Count { 
			get {
				return count;
			}
			set {
				count = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}

		/// <summary>
		/// Gets the start value.
		/// </summary>
		/// <value>The start value.</value>
		[TypeConverter(typeof(ConvertibleTypeConverter<int>))]
		public int StartValue { 
			get {
				return startValue;
			}
			set {
				startValue = value;
			}
		}

		/// <summary>
		/// Gets the range over which any amount can change from the previous amount.
		/// </summary>
		/// <value>The range over which any amount can change from the previous amount.</value>
		[TypeConverter(typeof(ConvertibleTypeConverter<int>))]
		public int StepRange { 
			get {
				return stepRange;
			}
			set {
				stepRange = value;
			}
		}

		/// <summary>
		/// Gets the sets the step range adjustment so that steps can trend up/down.
		/// </summary>
		/// <value>The step range adjustment so that steps can trend up/down.</value>
		/// <remarks>
		/// <c>0.5</c> means trend evenly.  Low numbers means trend toward higher amounts.
		/// </remarks>
		public double TrendPercentage { 
			get {
				return trendPercentage;
			}
			set {
				trendPercentage = value;
			}
		}

	}

}
