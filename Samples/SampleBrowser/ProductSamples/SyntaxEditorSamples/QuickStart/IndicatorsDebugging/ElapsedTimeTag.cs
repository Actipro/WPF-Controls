using ActiproSoftware.Text.Tagging;
using System;
using System.Windows;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {

	/// <summary>
	/// Provides an <see cref="IIntraTextSpacerTag"/> implementation that reserves intra-text space for a note.
	/// </summary>
	public class ElapsedTimeTag : IIntraTextSpacerTag {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="timeSpan">The elapse time.</param>
		public ElapsedTimeTag(TimeSpan timeSpan) {
			this.TimeSpan = timeSpan;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the spacer baseline.
		/// </summary>
		/// <value>The spacer baseline.</value>
		public double Baseline => 0.0;
		
		/// <summary>
		/// Gets whether the spacer appears before the tagged range.
		/// </summary>
		/// <value>
		/// <c>true</c> if the spacer appears before the tagged range; otherwise, <c>false</c>.
		/// </value>
		public bool IsSpacerBefore => false;

		/// <summary>
		/// Gets an object that can be used to uniquely identify the spacer, whose valus is the tag itself since the tag is persisted in a collection while active.
		/// </summary>
		/// <value>An object that can be used to uniquely identify the spacer.</value>
		public object Key => this;

		/// <summary>
		/// Gets or sets the spacer size.
		/// </summary>
		/// <value>The spacer size.</value>
		public Size Size { get; set; }
	
		/// <summary>
		/// Gets the elapsed time.
		/// </summary>
		/// <value>The elapsed time.</value>
		public TimeSpan TimeSpan { get; }
		
		/// <summary>
		/// Gets the text to display.
		/// </summary>
		/// <value>The text to display.</value>
		public string TimeSpanText {
			get {
				return $"≤ {this.TimeSpan.TotalMilliseconds.ToString("N0")}ms elapsed";
			}
		}

	}
	
}
