namespace WPG.Data;

public class PropertyCategory : PropertyCollection
{
	private readonly string _categoryName;

	public string Category => _categoryName;

	public PropertyCategory()
	{
		_categoryName = "Misc";
	}

	public PropertyCategory(string categoryName)
	{
		_categoryName = categoryName;
	}
}
