using System;

namespace ActiproSoftware.ProductSamples.Charts.Common {

	internal class EnumValueProvider {

		private Type _enumType;

		public EnumValueProvider(Type enumType) {
			_enumType = enumType;
		}

		public Array EnumValues {
			get { return Enum.GetValues(_enumType); }
		}
	}
}
