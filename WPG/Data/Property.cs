using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WPG.Data;

public class Property : Item, IDisposable
{
    private class ByCategoryThenByNameComparer : IComparer<Property>
    {
        public int Compare(Property x, Property y)
        {
            if (x == null || y == null)
                return 0;

            if (x == y)
                return 0;

            int val = x.Category.CompareTo(y.Category);
            if (val == 0)
                return x.Name.CompareTo(y.Name);

            return val;
        }
    }

    private class ByNameComparer : IComparer<Property>
    {
        public int Compare(Property x, Property y)
        {
            if (x == null || y == null)
                return 0;

            if (x == y)
                return 0;

            return x.Name.CompareTo(y.Name);
        }
    }

    protected object _instance;

    protected PropertyDescriptor _property;

    public static readonly IComparer<Property> CompareByCategoryThenByName = new ByCategoryThenByNameComparer();

    public static readonly IComparer<Property> CompareByName = new ByNameComparer();

    public object Value
    {
        get => _property.GetValue(_instance);
        set
        {
            object currentValue = _property.GetValue(_instance);

            if (value != null && value.Equals(currentValue))
                return;

            Type propertyType = _property.PropertyType;
            if (propertyType == typeof(object) || (value == null && propertyType.IsClass) || (value != null && propertyType.IsAssignableFrom(value.GetType())))
            {
                _property.SetValue(_instance, value);
                return;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(_property.PropertyType);
            try
            {
                object convertedValue = converter.ConvertFrom(value);
                _property.SetValue(_instance, convertedValue);
            }
            catch (Exception)
            {
            }
        }
    }

    public string Name => _property.DisplayName ?? _property.Name;

    public string Description => _property.Description;

    public bool IsWriteable => !IsReadOnly;

    public bool IsReadOnly => _property.IsReadOnly;

    public Type PropertyType => _property.PropertyType;

    public string Category => _property.Category;

    public Property(object instance, PropertyDescriptor property)
    {
        if (instance is ICustomTypeDescriptor)
        {
            _instance = ((ICustomTypeDescriptor)instance).GetPropertyOwner(property);
        }
        else
        {
            _instance = instance;
        }

        _property = property;
        _property.AddValueChanged(_instance, instance_PropertyChanged);
    }

    private void instance_PropertyChanged(object sender, EventArgs e)
    {
        NotifyPropertyChanged("Value");
    }

    protected override void Dispose(bool disposing)
    {
        if (!Disposed)
        {
            if (disposing)
            {
                _property.RemoveValueChanged(_instance, instance_PropertyChanged);
            }

            base.Dispose(disposing);
        }
    }
}
