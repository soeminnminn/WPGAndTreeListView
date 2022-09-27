// WPG.PropertyGrid
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WPG.Data;

namespace WPG;

[TemplatePart(Name = "PART_PropertyName", Type = typeof(Label))]
[TemplatePart(Name = "PART_InstanceType", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_InstanceName", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_Thumb", Type = typeof(Thumb))]
[TemplatePart(Name = "PART_PropertyDescription", Type = typeof(TextBlock))]
public class PropertyGrid : Control
{
    #region Dependency Properties
    public static readonly DependencyProperty InstanceProperty = DependencyProperty.Register(
        nameof(Instance), typeof(object), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(null, OnInstanceChanged, CoerceInstance));

    public static DependencyProperty FilterProperty = DependencyProperty.Register(
        nameof(Filter), typeof(string), typeof(PropertyGrid),
        new FrameworkPropertyMetadata("", OnFilterChanged));

    public static DependencyProperty CategorizedProperty = DependencyProperty.Register(
        nameof(Categorized), typeof(bool), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(true, OnCategorizedChanged));

    public static DependencyProperty ShowDescriptionProperty = DependencyProperty.Register(
        nameof(ShowDescription), typeof(bool), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(false, OnShowDescriptionChanged));

    public static readonly DependencyProperty HeadlineProperty = DependencyProperty.Register(
        nameof(Headline), typeof(string), typeof(PropertyGrid),
        new FrameworkPropertyMetadata("Properties", OnHeadlineChanged));

    public static DependencyProperty NameWidthProperty = DependencyProperty.Register(
        nameof(NameWidth), typeof(double), typeof(PropertyGrid),
        new PropertyMetadata(120.0, OnNameWidthChanged));

    public static DependencyProperty AutomaticlyExpandObjectsProperty = DependencyProperty.Register(
        nameof(AutomaticlyExpandObjects), typeof(bool), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(false, OnAutomaticlyExpandObjectsChanged));

    public static readonly DependencyProperty NullInstanceProperty = DependencyProperty.Register(
        nameof(NullInstance), typeof(object), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(null, OnNullInstanceChanged));

    public static readonly DependencyProperty PropertiesProperty = DependencyProperty.Register(
        nameof(Properties), typeof(ObservableCollection<Item>), typeof(PropertyGrid),
        new FrameworkPropertyMetadata(new ObservableCollection<Item>(), OnPropertiesChanged));
    #endregion

    #region Properties
    public object Instance
    {
        get => GetValue(InstanceProperty);
        set { SetValue(InstanceProperty, value); }
    }

    [Description("If you set a filter, only properties Containing this Text will be displayed")]
    public string Filter
    {
        get => (string)GetValue(FilterProperty);
        set { SetValue(FilterProperty, value); }
    }

    [Description("Propertys are Categorized or Alphabetical Sorted")]
    public bool Categorized
    {
        get => (bool)GetValue(CategorizedProperty);
        set { SetValue(CategorizedProperty, value); }
    }

    public bool ShowDescription
    {
        get => (bool)GetValue(ShowDescriptionProperty);
        set { SetValue(ShowDescriptionProperty, value); }
    }

    public string Headline
    {
        get => (string)GetValue(HeadlineProperty);
        set { SetValue(HeadlineProperty, value); }
    }

    public double NameWidth
    {
        get => (double)GetValue(NameWidthProperty);
        set { SetValue(NameWidthProperty, value); }
    }

    public bool AutomaticlyExpandObjects
    {
        get => (bool)GetValue(AutomaticlyExpandObjectsProperty);
        set { SetValue(AutomaticlyExpandObjectsProperty, value); }
    }

    public object NullInstance
    {
        get => GetValue(NullInstanceProperty);
        set { SetValue(NullInstanceProperty, value); }
    }

    public ObservableCollection<Item> Properties
    {
        get => (ObservableCollection<Item>)GetValue(PropertiesProperty);
        set { SetValue(PropertiesProperty, value); }
    }
    #endregion

    #region Constructors
    static PropertyGrid()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
    }

    public PropertyGrid()
    {
        PreviewMouseDown += PropertyGrid_PreviewMouseDown;
    }
    #endregion

    #region Methods
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (Template.FindName("PART_Thumb", this) is Thumb thumb)
        {
            thumb.DragDelta += PART_Thumb_DragDelta;
        }
        Refresh();
    }

    private void PropertyGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        FrameworkElement orginalSource = e.OriginalSource as FrameworkElement;
        if (orginalSource.DataContext != null && orginalSource.DataContext.GetType() == typeof(Property))
        {
            Property selectedProperty = (Property)orginalSource.DataContext;
            if (Template.FindName("Part_PropertyName", this) is TextBlock descriptionField)
            {
                descriptionField.Text = selectedProperty.Name;
            }

            if (Template.FindName("Part_PropertyDescription", this) is TextBlock nameField)
            {
                nameField.Text = selectedProperty.Description;
            }
        }
    }

    private void PART_Thumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        NameWidth = Math.Max(0.0, NameWidth + e.HorizontalChange);
    }

    public void Refresh()
    {
        if (Instance == null)
        {
            Properties = new ObservableCollection<Item>();
            return;
        }

        PropertyCollection properties = new PropertyCollection(Instance, !Categorized, AutomaticlyExpandObjects, Filter.ToLower());
        Properties = properties.Items;
        if (Instance == null || Template == null)
        {
            return;
        }

        if (Template.FindName("PART_InstanceType", this) is TextBlock instanceType)
        {
            string typeName = Instance.GetType().ToString();
            typeName = typeName.Substring(typeName.LastIndexOf('.') + 1);
            instanceType.Text = typeName;
        }

        if (Template.FindName("PART_InstanceName", this) is TextBlock instanceName)
        {
            if (Instance is FrameworkElement)
            {
                instanceName.Text = ((FrameworkElement)Instance).Name;
            }
            else
            {
                instanceName.Text = "";
            }
        }
    }

    private static void OnInstanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnInstanceChanged(e.OldValue, e.NewValue);
    }

    protected virtual void OnInstanceChanged(object oldValue, object newValue)
    {
        Refresh();
    }

    private static object CoerceInstance(DependencyObject d, object value)
    {
        PropertyGrid propertyGrid = d as PropertyGrid;
        if (value == null)
            return propertyGrid.NullInstance;
        return value;
    }

    private static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnFilterChanged((string)e.OldValue, (string)e.NewValue);
    }

    protected virtual void OnFilterChanged(string oldValue, string newValue)
    {
        Refresh();
    }

    private static void OnCategorizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnCategorizedChanged((bool)e.OldValue, (bool)e.NewValue);
    }

    protected virtual void OnCategorizedChanged(bool oldValue, bool newValue)
    {
        Refresh();
    }

    private static void OnShowDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnShowDescriptionChanged((bool)e.OldValue, (bool)e.NewValue);
    }

    protected virtual void OnShowDescriptionChanged(bool oldValue, bool newValue)
    {
    }

    private static void OnHeadlineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnHeadlineChanged((string)e.OldValue, (string)e.NewValue);
    }

    protected virtual void OnHeadlineChanged(string oldValue, string newValue)
    {
    }

    private static void OnNameWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnNameWidthChanged((double)e.OldValue, (double)e.NewValue);
    }

    protected virtual void OnNameWidthChanged(double oldValue, double newValue)
    {
    }

    private static void OnAutomaticlyExpandObjectsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnAutomaticlyExpandObjectsChanged((bool)e.OldValue, (bool)e.NewValue);
    }

    protected virtual void OnAutomaticlyExpandObjectsChanged(bool oldValue, bool newValue)
    {
        Refresh();
    }

    private static void OnNullInstanceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnNullInstanceChanged(e.OldValue, e.NewValue);
    }

    protected virtual void OnNullInstanceChanged(object oldValue, object newValue)
    {
    }

    private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PropertyGrid propertyGrid)
            propertyGrid.OnPropertiesChanged(e.OldValue, e.NewValue);

    }

    protected virtual void OnPropertiesChanged(object oldValue, object newValue)
    {
        ObservableCollection<Item> properties = oldValue as ObservableCollection<Item>;
        foreach (Item item in properties)
        {
            item.Dispose();
        }
    }
    #endregion
}
