using System;
using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject : ObservableObjectBase, IDataErrorInfo {

		private int businessLogic1;
		private int businessLogic2;
		private int businessLogic3;
		private int errorReporting1;
		private int errorReporting2;
		private int errorReporting3;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region IDataErrorInfo

		/// <summary>
		/// Gets an error message indicating what is wrong with this object.
		/// </summary>
		/// <value></value>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		string IDataErrorInfo.Error {
			get { return null; }
		}

		/// <summary>
		/// Gets the <see cref="String"/> with the specified column name.
		/// </summary>
		/// <value></value>
		string IDataErrorInfo.this[string columnName] {
			get {
				if ("BusinessLogic3" == columnName && this.businessLogic3 < 0)
					return "Value is not positive";
				return null;
			}
		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the first business logic integer.
		/// </summary>
		/// <value>The first business logic integer.</value>
		[Category("Custom Business Logic")]
		[DefaultValue((int)0)]
		[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and a custom ValidationRule to prevent values <= 0.")]
		public int BusinessLogic1 {
			get {
				return this.businessLogic1;
			}
			set {
				if (value != this.businessLogic1) {
					this.businessLogic1 = value;
					this.NotifyPropertyChanged("BusinessLogic1");
				}
			}
		}

		/// <summary>
		/// Gets or sets the second business logic integer.
		/// </summary>
		/// <value>The second business logic integer.</value>
		[Category("Custom Business Logic")]
		[DefaultValue((int)0)]
		[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and includes data validation in the property setter to prevent values <= 0.")]
		public int BusinessLogic2 {
			get {
				return this.businessLogic2;
			}
			set {
				if (value < 0)
					throw new ArgumentOutOfRangeException("value", "Value is not positive");

				if (value != this.businessLogic2) {
					this.businessLogic2 = value;
					this.NotifyPropertyChanged("BusinessLogic2");
				}
			}
		}

		/// <summary>
		/// Gets or sets the third business logic integer.
		/// </summary>
		/// <value>The third business logic integer.</value>
		[Category("Custom Business Logic")]
		[DefaultValue((int)0)]
		[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and includes data validation using IDataErrorInfo to prevent values <= 0.")]
		public int BusinessLogic3 {
			get {
				return this.businessLogic3;
			}
			set {
				if (value != this.businessLogic3) {
					this.businessLogic3 = value;
					this.NotifyPropertyChanged("BusinessLogic3");
				}
			}
		}

		/// <summary>
		/// Gets or sets the first error reporting integer.
		/// </summary>
		/// <value>The first error reporting integer.</value>
		[Category("Custom Error Reporting")]
		[DefaultValue((int)0)]
		[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline.")]
		public int ErrorReporting1 {
			get {
				return this.errorReporting1;
			}
			set {
				if (value != this.errorReporting1) {
					this.errorReporting1 = value;
					this.NotifyPropertyChanged("ErrorReporting1");
				}
			}
		}

		/// <summary>
		/// Gets or sets the second error reporting integer.
		/// </summary>
		/// <value>The second error reporting integer.</value>
		[Category("Custom Error Reporting")]
		[DefaultValue((int)0)]
		[Description("This integer property uses a custom Validation.ErrorTemplate, which is a pulsing red overlay.")]
		public int ErrorReporting2 {
			get {
				return this.errorReporting2;
			}
			set {
				if (value != this.errorReporting2) {
					this.errorReporting2 = value;
					this.NotifyPropertyChanged("ErrorReporting2");
				}
			}
		}

		/// <summary>
		/// Gets or sets the third error reporting integer.
		/// </summary>
		/// <value>The third error reporting integer.</value>
		[Category("Custom Error Reporting")]
		[DefaultValue((int)0)]
		[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and a custom dialog.")]
		public int ErrorReporting3 {
			get {
				return this.errorReporting3;
			}
			set {
				if (value != this.errorReporting3) {
					this.errorReporting3 = value;
					this.NotifyPropertyChanged("ErrorReporting3");
				}
			}
		}

	}

}
