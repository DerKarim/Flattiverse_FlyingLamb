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

        float drawX,drawY;
        //   int radarScreenMinDimension = Math.Min(radarScreen.Width, radarScreen.Height);
        //   float displayFactor = radarScreenMinDimension / 1000f;

        public int radarScreenMinDimension
        {
            get
            {
                radarScreenMinDimension = Math.Min(radarScreen.Width, radarScreen.Height);
                return radarScreenMinDimension;
            }
            set
            {
                radarScreenMinDimension = value;
            }
            
        }

        public float displayFactor
        {
            get
            {
                displayFactor = radarScreenMinDimension / 1000f;
                return displayFactor;
            }
            set
            {

            }
        }




        //Konstruktor
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
            controller.Enter("DOM II", "Communication Magenta");
            controller.NewMessageEvent += NewMessage;
            radarScreen.Focus(); //Stellt den Focus am anfang
            //Funktionen die direkt am Anfang starten sollen
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

                foreach (FlattiverseMessage message in messages) {
                    lines.Add(message.ToString());
                }
                textBoxMessages.Lines = lines.ToArray();
                textBoxMessages.SelectionStart = textBoxMessages.Text.Length;
                textBoxMessages.ScrollToCaret();
            }
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
            float displayFactor = radarScreenMinDimension / 1000f;

            //Mittelpunkt radarScreen
            float centerX = radarScreen.Width / 2;
            float centerY = radarScreen.Height / 2;

            float shipRadius = controller.ShipRadius * displayFactor;

            //Raunschiff einzeichnen
            Graphics g = e.Graphics;
            g.DrawEllipse(Pens.White, centerX - shipRadius, centerY - shipRadius, shipRadius * 2, shipRadius * 2); //Schiff

            Pen nicerStift = new Pen(Brushes.WhiteSmoke);
            nicerStift.DashPattern = new float[] { 8F, 8F };

            g.DrawEllipse(nicerStift, centerX - controller.ShootLimit, centerY - controller.ShootLimit ,  controller.ShootLimit*2,  controller.ShootLimit*2); //ShootLimit Radius

            //TODO: LERNEN WIE MAN ELLIPSE MIT FARBEN FÜLLEN KANN


            Console.WriteLine(units.Count);
            //Gescannte Objekte einzeichnen
            foreach (Unit u in units)
            {
                //Position des Units bestimmen
                float uX = centerX + u.Position.X * displayFactor;
                float uY = centerY + u.Position.Y * displayFactor;
                float uR = u.Radius * displayFactor;

                //Objekte beschriften
                String uName = u.Name.ToString();
                String uKind = u.Kind.ToString();
                
                Font defaultFont = SystemFonts.DefaultFont;

                SizeF size = g.MeasureString(uName, SystemFonts.DefaultFont);
                PointF pointUname = new PointF(uX - uR, uY - uR);
                PointF pointUkind = new PointF(uX - uR, (uY - uR)+40);
                PointF pointUmissiontargetSeq = new PointF(uX - uR, (uY - uR) + 60);

                if (size.Width < uR * 2) { 
                    g.DrawString(uName, defaultFont, Brushes.White, uX-size.Width/2,uY-size.Height/2);
                    g.DrawString(uKind, defaultFont, Brushes.White, pointUkind);
                }
                else { 
                    g.DrawString(uName, defaultFont, Brushes.White, pointUname);
                }
                //Unterschiedliche Farben je nach UnitTyp 
                switch (u.Kind)
                {
                    case UnitKind.Sun:
                        {
                            g.DrawEllipse(Pens.DarkOrange, uX - uR, uY - uR, uR * 2, uR * 2);

                            foreach (Corona corona in ((Sun)u).Coronas)
                            {
                                g.DrawEllipse(Pens.YellowGreen, uX - corona.Radius * displayFactor, uY - corona.Radius * displayFactor, corona.Radius * 2 * displayFactor, corona.Radius * 2 * displayFactor);
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

                            switch (u.Team.Name)
                            {
                                //DOM I
                                case "Orange":
                                    g.DrawEllipse(Pens.DarkOrange, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;
                                case "Blue":
                                    g.DrawEllipse(Pens.DarkBlue, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;

                                //DOM II
                                case "Communication Magenta":
                                    g.DrawEllipse(Pens.DarkMagenta, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;
                                case "Poisonous Green":
                                    g.DrawEllipse(Pens.DarkGreen, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;
                                case "Signalling Orange":
                                    g.DrawEllipse(Pens.DarkOrange, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;

                                //STF I
                                //case "Green":
                                    


                                default:
                                    g.DrawEllipse(Pens.Red, uX - uR, uY - uR, uR * 2, uR * 2);
                                    break;
                            }


                            
                            break;
                        }

                    case UnitKind.Buoy:
                        { 
                            g.DrawEllipse(Pens.Red, uX - uR, uY - uR, uR * 2, uR * 2);


                           textBox_debug.Clear();
                           textBox_debug.Text = "Message der Boye:" + ((Buoy)u).Message; 
                           


                            break;
                        }

                    case UnitKind.MissionTarget:
                        {
                                 MissionTarget tar = ((MissionTarget)u);
                           
                                Pen nicerStift2 = new Pen(Brushes.GreenYellow);
                                nicerStift2.DashPattern = new float[] { 3F, 3F };

                                g.DrawEllipse(nicerStift2, uX - uR, uY - uR, tar.DominationRadius * 2, tar.DominationRadius * 2);
                            

                            String uMissiontargetSeq = ((MissionTarget)u).SequenceNumber.ToString();
                           

                            g.DrawString(uMissiontargetSeq, defaultFont, Brushes.White, pointUmissiontargetSeq);
                            break;
                        }

                    //Alle anderen Units die nicht definiert worden sind.
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
          
            //ProgressBar Textanzeige
            label_liveEnergy.Text = String.Format("Energy: {0}/{1}", controller.ShipEnergyLive.ToString(), controller.ShipEnergyMax.ToString());

            //Verfügbare Schüsse
            textBox_shots.Text = controller.ShotsAvailable.ToString();

            //Linie Zeichen //TOFIX: LINIE MUSS GEFIXT WERDEN 
            //   controller.VecDir.Length = controller.ShootLimit;
          //  Vector vt = Vector.FromXY(drawX,drawY);
          //  Vector vt2 = new Vector(drawX,drawY);
          //  vt2.Length = controller.ShootLimit;
          //  vt.Length = controller.ShootLimit;
          //  g.DrawLine(Pens.White, centerX,centerY,  vt2.X, vt2.Y);
            
        }
        //Radarschirm mitteilen, dass er sich neu zeichnen soll.
        private void RadarScreenResizedHandler(object sender, EventArgs e)
        {
            radarScreen.Refresh();

            
        }
        //RadarScreen fokusieren bei klick
        private void radarScreen_Click(object sender, EventArgs e)
        {
            radarScreen.Focus();
        }

        //EventHander für den Tastendruck
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
                case Keys.Escape:
                    controller.Disconnect();
                    break;
                case Keys.K:
                    controller.MyShip.Kill();
                    break;
            }
        }

      //  //TODO: EnergieAnzeige seperat im code integrieren
      //  public void energyProgressbar()
      //  {
      //      //ProgressBar
      //      progressBar.Minimum = 0;
      //      progressBar.Maximum = (int)controller.ShipEnergyMax;
      //      progressBar.Value = (int)controller.ShipEnergyLive;
      //
      //      //ProgressBar Textanzeige
      //      label_liveEnergy.Text = String.Format("Energy: {0}/{1}", controller.ShipEnergyLive.ToString(), controller.ShipEnergyMax.ToString());
      //  }


        //Text absenden (chat)
        private void button_send_Click(object sender, EventArgs e)
        {
            
            String message = textBox_chat.Text;

            if((string)comboBox_playerlist.SelectedItem == "Universe" )
            {
                controller.UniverseChat(message);
            } else if (comboBox_playerlist.SelectedItem.ToString() == "Team")
            {
                
            } else if (comboBox_playerlist.SelectedItem.ToString() != "-------------")
            {
                controller.PlayerChat(comboBox_playerlist.SelectedItem.ToString(), message);
            }

            textBox_chat.Clear();

        }
        //Chat soll auch bei enter abgeschickt werden.
        private void textBox_chat_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:

                    String message = textBox_chat.Text;

                    if ((string)comboBox_playerlist.SelectedItem == "Universe")
                    {
                        controller.UniverseChat(message);
                    }
                    else if (comboBox_playerlist.SelectedItem.ToString() == "Team")
                    {

                    }
                    else if (comboBox_playerlist.SelectedItem.ToString() != "-------------")
                    {
                        controller.PlayerChat(comboBox_playerlist.SelectedItem.ToString(), message);
                    }

                    textBox_chat.Clear();
                    break;
            }
        }

        //Updatet Playerliste beim Klick
        private void comboBox_playerlist_Click(object sender, EventArgs e)
        {
            comboBox_playerlist.Items.Clear();
            comboBox_playerlist.Items.Add("Universe");
            comboBox_playerlist.Items.Add("Team");
            comboBox_playerlist.Items.Add("-------------");

            foreach (Player p in controller.PlayerInUniverse)
            {
                comboBox_playerlist.Items.Add(p.Name.ToString());
            }
        }

        //ClickHandler fürs Schießen
        private void RadarScreenClickHandler(object sender, MouseEventArgs e)
        {
            float centerX = radarScreen.Width / 2;
            float centerY = radarScreen.Height / 2;

            int radarScreenMinDimension = Math.Min(radarScreen.Width, radarScreen.Height);
            float displayFactor = radarScreenMinDimension / 1000f;
            
            float x = (e.X - centerX) /2;
            float y = (e.Y - centerY) /2;

            if (controller.ShotsAvailable >= 1)
            controller.ShootAt(x, y);

            textBox_debug.Clear();
            textBox_debug.Text = "x: " + (x).ToString() + " , " + "y: " + (y).ToString();

        }

       //Rein und Raus zoomen
       private void MouseWheelHandler (object sender, MouseEventArgs e)
        {
            int Delta = e.Delta;
            
        }
       





        //Wenn die View geschlossen wird
        private void FormClosingEventHandler(object sender, FormClosingEventArgs e)
        {
            controller.Disconnect();
        }

      

        private void radarScreen_MouseMove(object sender, MouseEventArgs e)
        {
            float centerX = radarScreen.Width / 2;
            float centerY = radarScreen.Height / 2;

            int radarScreenMinDimension = Math.Min(radarScreen.Width, radarScreen.Height);
            float displayFactor = radarScreenMinDimension / 1000f;

            float x = (e.X - centerX) / 2;
            float y = (e.Y - centerY) / 2;

            drawX = x ;
            drawY = y;

            controller.drawX = e.X;
            controller.drawY = e.Y;

        }
    }
}
