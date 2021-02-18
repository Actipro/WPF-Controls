using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Provides an abstract base class for a random data generator, to be used with various samples.
	/// Normally, data would come from sources such as database instead.
	/// </summary>
	/// <typeparam name="TOptions">The options type.</typeparam>
	/// <typeparam name="TData">The data type.</typeparam>
	[ContentProperty("Options")]
	public abstract class DataGeneratorBase<TOptions, TData> where TOptions: class {

		private bool												allowNegativeNumbers;
		private int													dataSetCount			= 1;
		private DeferrableObservableCollection<ICollection<TData>>	dataSets = new DeferrableObservableCollection<ICollection<TData>>();
		private TOptions											options;

		private static Random random = new Random(Environment.TickCount);
		
		///////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		///////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DataGeneratorBase</c> class.
		/// </summary>
		public DataGeneratorBase() {
			this.RegenerateDataSets();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether to allow negative numbers.
		/// </summary>
		/// <value>
		/// <c>true</c> if negative numbers are allowed; otherwise, <c>false</c>.
		/// </value>
		public bool AllowNegativeNumbers {
			get {
				return allowNegativeNumbers;
			}
			set {
				if (allowNegativeNumbers == value)
					return;

				allowNegativeNumbers = value;

				this.RegenerateDataSets();
			}
		}

		/// <summary>
		/// Gets the data sets that can be bound to one or more series.
		/// </summary>
		/// <value>The data sets that can be bound to one or more series.</value>
		public DeferrableObservableCollection<ICollection<TData>> DataSets {
			get {
				return dataSets;
			}
		}

		/// <summary>
		/// Gets or sets the data set count.
		/// </summary>
		/// <value>The data set count.</value>
		public int DataSetCount {
			get {
				return dataSetCount;
			}
			set {
				if (dataSetCount == value)
					return;

				dataSetCount = Math.Max(1, value);

				this.RegenerateDataSets();
			}
		}

		/// <summary>
		/// Generates a single data set based on the current options.
		/// </summary>
		/// <returns>The collection of generated data.</returns>
		protected abstract ICollection<TData> Generate();
		
		/// <summary>
		/// Gets or sets the options to use.
		/// </summary>
		/// <value>The options to use.</value>
		public TOptions Options {
			get {
				return options;
			}
			set {
				if (options == value)
					return;

				options = value;

				this.RegenerateDataSets();
			}
		}

		/// <summary>
		/// Gets the random number generator to use.
		/// </summary>
		/// <value>The random number generator to use.</value>
		protected Random Random { 
			get {
				return random;
			}
		}
		
		/// <summary>
		/// Regenerates all data sets.
		/// </summary>
		public void RegenerateDataSets() {
			dataSets.BeginUpdate();
			try {
				dataSets.Clear();
				for (int index = 0; index < dataSetCount; index++) {
					dataSets.Add(this.Generate());
				}
			}
			finally {
				dataSets.EndUpdate();
			}
		}

	}

}
