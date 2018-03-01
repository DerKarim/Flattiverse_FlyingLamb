namespace FlyingLambV1
{
    partial class View
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxMessages = new System.Windows.Forms.RichTextBox();
            this.radarScreen = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBox_chat = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.label_liveEnergy = new System.Windows.Forms.Label();
            this.comboBox_playerlist = new System.Windows.Forms.ComboBox();
            this.textBox_debug = new System.Windows.Forms.TextBox();
            this.label_debugtitle = new System.Windows.Forms.Label();
            this.textBox_shots = new System.Windows.Forms.TextBox();
            this.label_textshots = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessages.Location = new System.Drawing.Point(12, 446);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textBoxMessages.Size = new System.Drawing.Size(1226, 115);
            this.textBoxMessages.TabIndex = 0;
            this.textBoxMessages.TabStop = false;
            this.textBoxMessages.Text = "";
            this.textBoxMessages.WordWrap = false;
            // 
            // radarScreen
            // 
            this.radarScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radarScreen.AutoSize = true;
            this.radarScreen.BackColor = System.Drawing.Color.Black;
            this.radarScreen.Location = new System.Drawing.Point(12, 12);
            this.radarScreen.Name = "radarScreen";
            this.radarScreen.Size = new System.Drawing.Size(1051, 428);
            this.radarScreen.TabIndex = 1;
            this.radarScreen.Click += new System.EventHandler(this.radarScreen_Click);
            this.radarScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RadarScreenClickHandler);
            this.radarScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.radarScreen_MouseMove);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(1069, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(328, 23);
            this.progressBar.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBox_chat
            // 
            this.textBox_chat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_chat.Location = new System.Drawing.Point(13, 567);
            this.textBox_chat.Name = "textBox_chat";
            this.textBox_chat.Size = new System.Drawing.Size(1225, 20);
            this.textBox_chat.TabIndex = 0;
            this.textBox_chat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_chat_KeyPress);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(1293, 565);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 4;
            this.button_send.Text = "Senden";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // label_liveEnergy
            // 
            this.label_liveEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_liveEnergy.AutoSize = true;
            this.label_liveEnergy.BackColor = System.Drawing.Color.Transparent;
            this.label_liveEnergy.Location = new System.Drawing.Point(1159, 17);
            this.label_liveEnergy.Name = "label_liveEnergy";
            this.label_liveEnergy.Size = new System.Drawing.Size(154, 13);
            this.label_liveEnergy.TabIndex = 5;
            this.label_liveEnergy.Text = "[AktuelleEnergie]/[MaxEnergie]";
            this.label_liveEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_playerlist
            // 
            this.comboBox_playerlist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_playerlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_playerlist.FormattingEnabled = true;
            this.comboBox_playerlist.Location = new System.Drawing.Point(1269, 408);
            this.comboBox_playerlist.Name = "comboBox_playerlist";
            this.comboBox_playerlist.Size = new System.Drawing.Size(128, 21);
            this.comboBox_playerlist.TabIndex = 6;
            this.comboBox_playerlist.Click += new System.EventHandler(this.comboBox_playerlist_Click);
            // 
            // textBox_debug
            // 
            this.textBox_debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_debug.Location = new System.Drawing.Point(1187, 41);
            this.textBox_debug.Multiline = true;
            this.textBox_debug.Name = "textBox_debug";
            this.textBox_debug.ReadOnly = true;
            this.textBox_debug.Size = new System.Drawing.Size(210, 52);
            this.textBox_debug.TabIndex = 7;
            // 
            // label_debugtitle
            // 
            this.label_debugtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_debugtitle.AutoSize = true;
            this.label_debugtitle.Location = new System.Drawing.Point(1124, 44);
            this.label_debugtitle.Name = "label_debugtitle";
            this.label_debugtitle.Size = new System.Drawing.Size(57, 13);
            this.label_debugtitle.TabIndex = 8;
            this.label_debugtitle.Text = "Debugger:";
            // 
            // textBox_shots
            // 
            this.textBox_shots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_shots.Location = new System.Drawing.Point(1175, 408);
            this.textBox_shots.Name = "textBox_shots";
            this.textBox_shots.Size = new System.Drawing.Size(41, 20);
            this.textBox_shots.TabIndex = 9;
            this.textBox_shots.Text = "[Shots]";
            // 
            // label_textshots
            // 
            this.label_textshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_textshots.AutoSize = true;
            this.label_textshots.Location = new System.Drawing.Point(1066, 411);
            this.label_textshots.Name = "label_textshots";
            this.label_textshots.Size = new System.Drawing.Size(106, 13);
            this.label_textshots.TabIndex = 10;
            this.label_textshots.Text = "Verfügbare Schüsse:";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1409, 599);
            this.Controls.Add(this.label_textshots);
            this.Controls.Add(this.textBox_shots);
            this.Controls.Add(this.label_debugtitle);
            this.Controls.Add(this.textBox_debug);
            this.Controls.Add(this.comboBox_playerlist);
            this.Controls.Add(this.label_liveEnergy);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_chat);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.radarScreen);
            this.Controls.Add(this.textBoxMessages);
            this.KeyPreview = true;
            this.Name = "View";
            this.Text = "Fyling Lamb (V1)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingEventHandler);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEventHandler);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxMessages;
        private System.Windows.Forms.Panel radarScreen;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBox_chat;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Label label_liveEnergy;
        private System.Windows.Forms.ComboBox comboBox_playerlist;
        private System.Windows.Forms.Label label_debugtitle;
        private System.Windows.Forms.TextBox textBox_debug;
        private System.Windows.Forms.Label label_textshots;
        private System.Windows.Forms.TextBox textBox_shots;
    }
}

