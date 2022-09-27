using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPG.TypeEditors;

public class IntegerTypeEditor : TextBox
{
	private int _step = 1;

	private static RoutedCommand _UpCommand;

	private static RoutedCommand _DownCommand;

	public static RoutedCommand UpCommand => _UpCommand;

	public static RoutedCommand DownCommand => _DownCommand;

	static IntegerTypeEditor()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(IntegerTypeEditor), 
			new FrameworkPropertyMetadata(typeof(IntegerTypeEditor)));
	}

	public IntegerTypeEditor()
	{
		InitializeCommands();
	}

	private static void InitializeCommands()
	{
		_UpCommand = new RoutedCommand("UpCommand", typeof(IntegerTypeEditor));
		CommandManager.RegisterClassCommandBinding(typeof(IntegerTypeEditor), new CommandBinding(_UpCommand, OnUpCommand));
		CommandManager.RegisterClassInputBinding(typeof(IntegerTypeEditor), new InputBinding(_UpCommand, new KeyGesture(Key.Up)));

		_DownCommand = new RoutedCommand("DownCommand", typeof(IntegerTypeEditor));
		CommandManager.RegisterClassCommandBinding(typeof(IntegerTypeEditor), new CommandBinding(_DownCommand, OnDownCommand));
		CommandManager.RegisterClassInputBinding(typeof(IntegerTypeEditor), new InputBinding(_DownCommand, new KeyGesture(Key.Down)));
	}

	private static void OnUpCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender.GetType() == typeof(IntegerTypeEditor))
		{
			IntegerTypeEditor _myIntegerTypeEditor = (IntegerTypeEditor)sender;
			_myIntegerTypeEditor.CountUp();
		}
	}

	private static void OnDownCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender.GetType() == typeof(IntegerTypeEditor))
		{
			IntegerTypeEditor _myIntegerTypeEditor = (IntegerTypeEditor)sender;
			_myIntegerTypeEditor.CountDown();
		}
	}

	protected void CountUp()
	{
		base.Text = (int.Parse(base.Text) + _step).ToString();
	}

	protected void CountDown()
	{
		base.Text = (int.Parse(base.Text) - _step).ToString();
	}
}
