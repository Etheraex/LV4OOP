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
            Controller gameController;
            Model gameModel;
            bool start = false;
            switch (dr)
            {
                case DialogResult.OK:
                    gameModel = new StandardModel();
                    gameController = new StandardController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.No:
                    gameModel = new FrenchModel();
                    gameController = new StandardController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.Retry:
                    gameModel = new StandardModel();
                    gameController = new TexasHoldemController(gameView, gameModel);
                    start = true;
                    break;
                case DialogResult.Yes:
                    gameModel = new FrenchModel();
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
