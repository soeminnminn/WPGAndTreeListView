using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WPG.Data;

internal class ExpandableProperty : Property
{
	private PropertyCollection _propertyCollection;

	private bool _automaticlyExpandObjects;

	private string _filter;

	public ObservableCollection<Item> Items
	{
		get
		{
			if (_propertyCollection == null)
			{
				_propertyCollection = new PropertyCollection(_property.GetValue(_instance), noCategory: true, _automaticlyExpandObjects, _filter);
			}
			return _propertyCollection.Items;
		}
	}

	public ExpandableProperty(object instance, PropertyDescriptor property, bool automaticlyExpandObjects, string filter)
		: base(instance, property)
	{
		_automaticlyExpandObjects = automaticlyExpandObjects;
		_filter = filter;
	}
}
