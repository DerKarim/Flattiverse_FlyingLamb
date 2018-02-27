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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessages.Enabled = false;
            this.textBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessages.Location = new System.Drawing.Point(12, 362);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textBoxMessages.Size = new System.Drawing.Size(1020, 115);
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
            this.radarScreen.Size = new System.Drawing.Size(795, 344);
            this.radarScreen.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(813, 12);
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
            this.textBox_chat.Enabled = false;
            this.textBox_chat.Location = new System.Drawing.Point(13, 483);
            this.textBox_chat.Name = "textBox_chat";
            this.textBox_chat.Size = new System.Drawing.Size(1019, 20);
            this.textBox_chat.TabIndex = 3;
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(1054, 483);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 4;
            this.button_send.Text = "Senden";
            this.button_send.UseVisualStyleBackColor = true;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 515);
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
    }
}

