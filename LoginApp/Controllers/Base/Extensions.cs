namespace LoginApp.Controllers.Base
{
    public static class ControllerExtensions
    {
        private const string _xBorder = "******************************************************************";
        private const int _length = 66;
        private static int _x = (Console.WindowWidth - _length) > 0 ? (Console.WindowWidth - _length) / 2 : 1;
        private static int _y { get => Console.CursorTop; }
        private static void Write(string message)
        {
            Console.Write(message);
        }
        private static void WriteRow(string message, string start = "*         ")
        {

            Console.SetCursorPosition(_x, _y);
            Console.Write(start);
            Write(message);
            Console.Write(String.Join("", start.Reverse()));
            Console.Write("\n");
        }
        public static void StartApp(this WebApplication app)
        {

            app.Run();
        }
    }
}
