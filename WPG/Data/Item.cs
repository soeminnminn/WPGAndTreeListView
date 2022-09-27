using System;
using System.ComponentModel;

namespace WPG.Data;

public abstract class Item : INotifyPropertyChanged, IDisposable
{
	private bool _disposed;

	protected bool Disposed => _disposed;

	public event PropertyChangedEventHandler PropertyChanged;

	protected void NotifyPropertyChanged(string property)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!Disposed)
		{
			_disposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	~Item()
	{
		Dispose(disposing: false);
	}
}
