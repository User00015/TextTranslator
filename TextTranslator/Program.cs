using System;

namespace ConsoleApp2
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            var guiForm = new Form1();
            guiForm.ShowDialog();


            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
