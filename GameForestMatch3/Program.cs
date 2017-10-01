using System;

namespace GameForestMatch3
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Action _closeAction;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Match3Game())
            {
                _closeAction = game.Exit;
                game.Run();
            }
        }

        public static void Close()
        {
            _closeAction?.Invoke();
        }
    }
#endif
}
