A code first framework for uCommmerce
=====================================

Introduction
------------

In big projects where you have several environments it is time consuming to navigate inside uCommerce user interface to "click & point" for creating uCommerce definitions (category definitions, product definitions so forth).
This framework will help you making deployment throughout several environment easier as you only have to create your definitions as POCOs and then the framework will making sure that definitions will be created or updated.

Requirements
------------

Umbraco: 7.1.4

uCommerce: 6.1.0.14195

These versions are currently used for development. This is not the same as it doesn't work with older versions. I know  uCommerce API is not changing a lot and I use the low level API access (going directly on database on NHibernate) so it "should" be working with older versions of uCommerce, though I haven't tested more than the development version as this point. 
The same goes for Umbraco version. The only thing I use from Umbraco is ApplicationEventHandler and if this is available in older versions of Umbraco it should work as well (I recall that Umbraco 6 uses that as well).


Usage
-----

This framework create definitions in uCommerce by using attributes on POCO classes.
At this point there is only one attribute (CategoryDefinitionAttribute) for creating "Category Definitions". My recommendation is to take a look at the Models project which will have examples of all available attributes. 
With time there will come a complete wiki and guides.

Examples
--------

**Creating a category definition with a field**

	[CategoryDefinition("Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[DefinitionField("IsPrimaryCategoryAlt", "Number",
			Multilingual = true,
			DisplayOnSite = true,
			RenderInEditor = true
		)]
		public bool IsPrimaryCategory { get; set; }
	}

This is still under development and is NOT ready for usages yet. Feel free to get inspired
==========================================================================================