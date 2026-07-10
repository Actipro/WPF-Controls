namespace ActiproSoftware.ProductSamples.DataGridSamples.Demo.LicenseDashboard;

/// <summary>
/// Provides a converter that gets the <c>LicenseState</c> based on an expiration date.
/// </summary>
public sealed class ExpirationDateToLicenseStateConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if ((value is not null) && (value is DateTime || value is DateTime?)) {
			var dateTime = (DateTime)value;
			if ((dateTime - DateTime.Now).TotalDays <= 0)
				return LicenseState.Expired;
			else if ((dateTime - DateTime.Now).TotalDays <= 60)
				return LicenseState.ExpiringSoon;
		}
		return LicenseState.Valid;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotSupportedException();

}
