using System;
using Gtk;

class MainClass {
	public static void Main (string[] args)
	{
		Application.Init();

		Window w = new Window ("Gtk# Basics");
		Button b = new Button ("Hit me");

		// set up event handling: verbose to illustrate
		// the use of delegates.
		w.DeleteEvent += new DeleteEventHandler (Window_Delete);
		b.Clicked += new EventHandler (Button_Clicked);

		// initialize the GUI
		w.Add (b);
		w.SetDefaultSize (200, 100);
		w.ShowAll ();

		Application.Run();
	}

	static void Window_Delete (object o, DeleteEventArgs args)
	{
		Application.Quit ();
		args.RetVal = true;
	}

	static void Button_Clicked (object o, EventArgs args)
	{
		System.Console.WriteLine ("Hello, World!");
	}
}

