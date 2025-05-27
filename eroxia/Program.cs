namespace eroxia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tui = new Tui(new BusinessLogic(new DBStorage()));
            tui.Start();
        }
    }
}
