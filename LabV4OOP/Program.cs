using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabV4OOP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PickTypeForm pick = new PickTypeForm();
            DialogResult dr = pick.ShowDialog();

            GameForm gameView = new GameForm();
            IModel gameModel;
            IController gameController;
            bool start = false;
            switch (dr)
            {
                case DialogResult.OK:
                    gameModel = new StandardModel(true);
                    gameController = new StandardController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.No:
                    gameModel = new StandardModel(false);
                    gameController = new StandardController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.Retry:
                    gameModel = new TexasHoldemModel(true);
                    gameController = new TexasHoldemController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.Yes:
                    gameModel = new TexasHoldemModel(false);
                    gameController = new TexasHoldemController(gameView, gameModel);
                    start = true;
                    break;
            }
            if (start)
                Application.Run(gameView);
            else
                Application.Exit();
        }
    }
}
