using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.MonthCalendarIntro;

/// <summary>
/// Selects a day item template.
/// </summary>
public class CustomDayItemTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use as the default.
	/// </summary>
	public DataTemplate? DefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for marked items.
	/// </summary>
	public DataTemplate? MarkedTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
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
		return (date.Day == 20)
			? MarkedTemplate
			: DefaultTemplate;
	}

}
