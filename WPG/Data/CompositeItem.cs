using System.Collections.ObjectModel;

namespace WPG.Data;

public abstract class CompositeItem : Item
{
	private readonly ObservableCollection<Item> _items = new ObservableCollection<Item>();

	public ObservableCollection<Item> Items => _items;

	protected override void Dispose(bool disposing)
	{
		if (base.Disposed)
            return;

        if (disposing)
		{
			foreach (Item item in Items)
			{
				item.Dispose();
			}
		}
		base.Dispose(disposing);
	}
}
