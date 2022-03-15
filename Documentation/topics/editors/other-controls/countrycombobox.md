---
title: "CountryComboBox"
page-title: "CountryComboBox - Other Editors Controls"
order: 5
---
# CountryComboBox

The [CountryComboBox](xref:@ActiproUIRoot.Controls.Editors.CountryComboBox) can be used to select a [Country](xref:@ActiproUIRoot.Controls.Editors.Country) from a list of countries in the world.  The country code can then be used with LINQ and [CountryCurrencyMapping](xref:@ActiproUIRoot.Controls.Editors.CountryCurrencyMapping) to query the [Currency](xref:@ActiproUIRoot.Controls.Editors.Currency) that is used in that [Country](xref:@ActiproUIRoot.Controls.Editors.Country).

![Screenshot](../images/countrycombobox-closed.png)

## Overview

The [CountryComboBox](xref:@ActiproUIRoot.Controls.Editors.CountryComboBox) extends the native `ComboBox` and displays a list of countries as defined by the ISO.  A country is represented by an instance of [Country](xref:@ActiproUIRoot.Controls.Editors.Country), and the list of countries is defined by the [Country](xref:@ActiproUIRoot.Controls.Editors.Country).[Countries](xref:@ActiproUIRoot.Controls.Editors.Country.Countries) collection

## List of Countries

The list of countries can be customized by updating the [Country](xref:@ActiproUIRoot.Controls.Editors.Country).[Countries](xref:@ActiproUIRoot.Controls.Editors.Country.Countries) collection.  Items can be added or removed as needed directly from this list.  In addition, a custom list can be assigned to the `ItemsSource` property.

This example code shows how the default full list of countries can be filtered down to several European countries via LINQ:

```csharp
var countries = new string[] { "PT", "ES", "GB", "FR", "DE" };
countryComboBox.ItemSource = from c in Country.Countries where countries.Contains(c.Code) select c;
```

## Selection

The selected [Country](xref:@ActiproUIRoot.Controls.Editors.Country) is accessible through the `SelectedItem` property.  In addition, the unique ISO code associated with the country can be accessed through the `SelectedValue` property.

## Country/Currency Mapping

The [CountryCurrencyMapping](xref:@ActiproUIRoot.Controls.Editors.CountryCurrencyMapping) class contains [Mappings](xref:@ActiproUIRoot.Controls.Editors.CountryCurrencyMapping.Mappings) between [Country](xref:@ActiproUIRoot.Controls.Editors.Country).[Countries](xref:@ActiproUIRoot.Controls.Editors.Country.Countries) and [Currency](xref:@ActiproUIRoot.Controls.Editors.Currency).[Currencies](xref:@ActiproUIRoot.Controls.Editors.Currency.Currencies).  This is useful for seeing which countries use which currencies, and which currencies are used by which countries.

It is possible to use LINQ with [CountryCurrencyMapping](xref:@ActiproUIRoot.Controls.Editors.CountryCurrencyMapping).[Mappings](xref:@ActiproUIRoot.Controls.Editors.CountryCurrencyMapping.Mappings), [Country](xref:@ActiproUIRoot.Controls.Editors.Country).[Countries](xref:@ActiproUIRoot.Controls.Editors.Country.Countries) and [Currency](xref:@ActiproUIRoot.Controls.Editors.Currency).[Currencies](xref:@ActiproUIRoot.Controls.Editors.Currency.Currencies).  This shows the currencies for the United States.

```csharp
var results =
	from mapping in CountryCurrencyMapping.Mappings
	join currency in Currency.Currencies on mapping.CurrencyCode equals currency.Code
	where mapping.CountryCode == "US"
	select currency;
```
