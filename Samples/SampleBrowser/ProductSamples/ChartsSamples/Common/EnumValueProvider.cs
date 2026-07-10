namespace ActiproSoftware.ProductSamples.Charts.Common;

internal class EnumValueProvider(Type enumType) {

	private readonly Type _enumType = enumType;

	public Array EnumValues
		=> Enum.GetValues(_enumType);

}
