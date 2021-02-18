using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.MonthCalendarIntro {

	/// <summary>
	/// Selects a day item template.
	/// </summary>
	public class CustomDayItemTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use as the default.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use as the default.</value>
		public DataTemplate DefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for marked items.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for marked items.</value>
		public DataTemplate MarkedTemplate { get; set; }

		/// <summary>
		/// Selects a data template.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container to examine.</param>
		/// <returns>The <see cref="DataTemplate"/> that was selected.</returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			var date = (DateTime)item;
			var element = (MonthCalendarItem)container;

			// Change the foreground of weekend days
			switch (date.DayOfWeek) {
				case DayOfWeek.Saturday:
				case DayOfWeek.Sunday:
					element.Foreground = Brushes.Blue;
					break;
			}

			// Mark day 20 of each month
			if (date.Day == 20)
				return this.MarkedTemplate;
			else
				return this.DefaultTemplate;
		}

	}

}
