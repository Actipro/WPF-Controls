using System;
using System.Collections.Generic;
using System.Windows;

namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.FileCopyDialog {
	/// <summary>
	/// Represents meta-data for a file copy operation.
	/// </summary>
	public class FileCopyData : DependencyObject {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="CopiedFileSize"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="CopiedFileSize"/> dependency property.</value>
		public static readonly DependencyProperty CopiedFileSizeProperty = DependencyProperty.Register("CopiedFileSize",
			typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(0.0));

		/// <summary>
		/// Identifies the <see cref="RemainingFileCount"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="RemainingFileCount"/> dependency property.</value>
		public static readonly DependencyProperty RemainingFileCountProperty = DependencyProperty.Register("RemainingFileCount",
			typeof(int), typeof(FileCopyData), new FrameworkPropertyMetadata(0));

		/// <summary>
		/// Identifies the <see cref="RemainingFileSize"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="RemainingFileSize"/> dependency property.</value>
		public static readonly DependencyProperty RemainingFileSizeProperty = DependencyProperty.Register("RemainingFileSize",
			typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(0.0));

		/// <summary>
		/// Identifies the <see cref="Speed"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Speed"/> dependency property.</value>
		public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed",
			typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(0.0));

		/// <summary>
		/// Identifies the <see cref="TimeRemaining"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="TimeRemaining"/> dependency property.</value>
		public static readonly DependencyProperty TimeRemainingProperty = DependencyProperty.Register("TimeRemaining",
			typeof(TimeSpan), typeof(FileCopyData), new FrameworkPropertyMetadata(TimeSpan.Zero));

		/// <summary>
		/// Identifies the <see cref="TotalFileCount"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="TotalFileCount"/> dependency property.</value>
		public static readonly DependencyProperty TotalFileCountProperty = DependencyProperty.Register("TotalFileCount",
			typeof(int), typeof(FileCopyData), new FrameworkPropertyMetadata(0));

		/// <summary>
		/// Identifies the <see cref="TotalFileSize"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="TotalFileSize"/> dependency property.</value>
		public static readonly DependencyProperty TotalFileSizeProperty = DependencyProperty.Register("TotalFileSize",
			typeof(double), typeof(FileCopyData), new FrameworkPropertyMetadata(1.0));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the size, in gigabytes, of the files already copied.
		/// </summary>
		public double CopiedFileSize {
			get {
				return (double)GetValue(FileCopyData.CopiedFileSizeProperty);
			}
			set {
				SetValue(FileCopyData.CopiedFileSizeProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the number of files still waiting to be copied. This is a dependency property.
		/// </summary>
		public int RemainingFileCount {
			get {
				return (int)GetValue(FileCopyData.RemainingFileCountProperty);
			}
			set {
				SetValue(FileCopyData.RemainingFileCountProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the size, in gigabytes, of the files still waiting to be copied. This is a dependency property.
		/// </summary>
		public double RemainingFileSize {
			get {
				return (double)GetValue(FileCopyData.RemainingFileSizeProperty);
			}
			set {
				SetValue(FileCopyData.RemainingFileSizeProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the speed of the file copy operation in megabytes per second. This is a dependency property.
		/// </summary>
		public double Speed {
			get {
				return (double)GetValue(FileCopyData.SpeedProperty);
			}
			set {
				SetValue(FileCopyData.SpeedProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TimeSpan"/> remaining. This is a dependency property.
		/// </summary>
		public TimeSpan TimeRemaining {
			get {
				return (TimeSpan)GetValue(FileCopyData.TimeRemainingProperty);
			}
			set {
				SetValue(FileCopyData.TimeRemainingProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the total number of files being copied. This is a dependency property.
		/// </summary>
		public int TotalFileCount {
			get {
				return (int)GetValue(FileCopyData.TotalFileCountProperty);
			}
			set {
				SetValue(FileCopyData.TotalFileCountProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the total size, in gigabytes, of the files being copied. This is a dependency property.
		/// </summary>
		public double TotalFileSize {
			get {
				return (double)GetValue(FileCopyData.TotalFileSizeProperty);
			}
			set {
				SetValue(FileCopyData.TotalFileSizeProperty, value);
			}
		}
	}
}
