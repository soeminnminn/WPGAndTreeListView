using System;
using System.Windows;
using System.Windows.Controls;
using WPG.Data;

namespace WPG;

public class PropertyTemplateSelector : DataTemplateSelector
{
	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		if (!(item is Property property))
		{
			throw new ArgumentException("item must be of type Property");
		}

		if (!(container is FrameworkElement element))
		{
			return base.SelectTemplate(property.Value, container);
		}

		return FindDataTemplate(property, element);
	}

	private DataTemplate FindDataTemplate(Property property, FrameworkElement element)
	{
		Type propertyType = property.PropertyType;
		DataTemplate template = TryFindDataTemplate(element, propertyType);
		while (template == null && propertyType.BaseType != null)
		{
			propertyType = propertyType.BaseType;
			template = TryFindDataTemplate(element, propertyType);
		}

		if (template == null)
		{
			template = TryFindDataTemplate(element, "default");
		}
		return template;
	}

	private static DataTemplate TryFindDataTemplate(FrameworkElement element, object dataTemplateKey)
	{
		object dataTemplate = element.TryFindResource(dataTemplateKey);

		if (dataTemplate == null)
		{
			dataTemplateKey = new ComponentResourceKey(typeof(PropertyGrid), dataTemplateKey);
			dataTemplate = element.TryFindResource(dataTemplateKey);
		}
		
		return dataTemplate as DataTemplate;
	}
}
