using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flattiverse;

namespace FlyingLambV1
{
    public partial class View : Form
    {
        Controller controller;
        List<Corona> coronas = new List<Corona>(); //[K]
        List<Unit> units = new List<Unit>();
       

        public View(Controller controller)
        {
            this.controller = controller;
            InitializeComponent();
            radarScreen.Resize += RadarScreenResizedHandler;
            Shown += View_Shown;

        }

        private void View_Shown(object sender, EventArgs e)
        {
            controller.Connect();
            //controller.ListUniverses();
            controller.Enter("Time Master", "None");
            controller.NewMessageEvent += NewMessage;

            radarScreen.Paint += RadarScreenPaintEventHandler;
            controller.NewScanEvent += NewScan;
        }

        private void NewScan()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(NewScan));
            }
            else
            {
                units = controller.Units;
                radarScreen.Invalidate();
            }
        }

        //Neue Nachrichten Anzeigen
        private void NewMessage()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(NewMessage));
            }
            else
            {
                List<FlattiverseMessage> messages = controller.Messages;
                List<string> lines = new List<string>();
                foreach (FlattiverseMessage message in messages)
                    lines.Add(message.ToString());
                textBoxMessages.Lines = lines.ToArray();
            }

            textBoxMessages.SelectionStart = textBoxMessages.Text.Length; // TODO: Fehler
            textBoxMessages.ScrollToCaret();
            textBoxMessages.Refresh();
        }

        //Paint Ereignisse
        private void RadarScreenPaintEventHandler(object sender, PaintEventArgs e)
        {
            if (!controller.ShipReady)
                return;

            // Compute magnification factor
            int radarScreenMinDimension = Math.Min(radarScreen.Width, radarScreen.Height);
            // Make screen at least 2000x2000 flattiverse miles with Ship at center
            // i.e. minimum screenPixels corresponds to 2000 flattiverse miles
            float displayFactor = radarScreenMinDimension / 2000f;

            float centerX = radarScreen.Width / 2;
            float centerY = radarScreen.Height / 2;



            float shipRadius = controller.ShipRadius * displayFactor;

            //Raunschiff einzeichnen
            Graphics g = e.Graphics;
            g.DrawEllipse(Pens.White,
                centerX - shipRadius, centerY - shipRadius,
                shipRadius * 2, shipRadius * 2);

            //Gescannte Objekte einzeichnen
            foreach (Unit u in units)
            {
                float uX = centerX + u.Position.X * displayFactor;
                float uY = centerY + u.Position.Y * displayFactor;
                float uR = u.Radius * displayFactor;

                //Objekte beschriften
                String uName = u.Name.ToString();
                Font defaultFont = SystemFonts.DefaultFont;

                SizeF size = g.MeasureString(uName, SystemFonts.DefaultFont);
                SolidBrush brush = new SolidBrush(Color.White);
                PointF point = new PointF(uX, uY);

                g.DrawString(uName,defaultFont,brush,point);

                //Unterschiedliche Farben je nach UnitTyp [K]
                
                switch (u.Kind)
                {
                    case UnitKind.Sun:
                        {
                            g.DrawEllipse(Pens.DarkOrange, uX - uR, uY - uR, uR * 2, uR * 2);
                            
                            foreach(Corona corona in ((Sun)u).Coronas)
                            {


                                g.DrawEllipse(Pens.YellowGreen, uX - corona.Radius * displayFactor , uY - corona.Radius * displayFactor, corona.Radius*2*displayFactor,corona.Radius*2*displayFactor );
                            }


                            break;
                            
                        }

                    case UnitKind.Planet:
                        {
                            g.DrawEllipse(Pens.DarkBlue, uX - uR, uY - uR, uR * 2, uR * 2);
                            break;
                        }

                    case UnitKind.Meteoroid:
                        {
                            g.DrawEllipse(Pens.LightBlue, uX - uR, uY - uR, uR * 2, uR * 2);
                            break;
                        }

                    case UnitKind.PlayerShip:
                        {
                            g.DrawEllipse(Pens.Red, uX - uR, uY - uR, uR * 2, uR * 2);
                            break;
                        }

                  

                    default:
                        {
                            g.DrawEllipse(Pens.HotPink, uX - uR, uY - uR, uR * 2, uR * 2);
                            break;
                        }
                }

                  


            }
            
           
            


            //ProgressBar
            progressBar.Minimum = 0;
            progressBar.Maximum = (int)controller.ShipEnergyMax;
            progressBar.Value = (int)controller.ShipEnergyLive;

        }

        //Radarschirm mitteilen, dass er sich neu zeichnen soll.
        private void RadarScreenResizedHandler(object sender, EventArgs e)
        {
            radarScreen.Refresh();
            
        }



        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormClosingEventHandler(object sender, FormClosingEventArgs e)
        {
            controller.Disconnect();
        }




        private void View_Load(object sender, EventArgs e)
        {





        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //textBoxMessages
        }

        private void radarScreen_Paint(object sender, PaintEventArgs e)
        {
            //radarScreen
           // float radius = radarScreen.Height > radarScreen.Width ? radarScreen.Width / 2 : radarScreen.Height / 2; // if(...) ? { .. } : else {..}


        }

        private void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
           
            switch (e.KeyCode)
            {
                case Keys.W:
                    controller.Impulse(0, 1);
                    break;
                case Keys.A:
                    controller.Impulse(-1, 0);
                    break;
                case Keys.S:
                    controller.Impulse(0, -1);
                    break;
                case Keys.D:
                    controller.Impulse(1, 0);
                    break;
            }
        }

        private void progressBarEventHandler(object sender, EventArgs e)
        {

            progressBar.Minimum = 0;
            progressBar.Maximum = (int)controller.ShipEnergyMax;
            progressBar.Value = (int)controller.ShipEnergyLive;

        }






    }
}
