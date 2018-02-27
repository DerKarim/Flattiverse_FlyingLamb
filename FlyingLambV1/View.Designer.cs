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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessages.Location = new System.Drawing.Point(724, 200);
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.Size = new System.Drawing.Size(252, 290);
            this.textBoxMessages.TabIndex = 0;
            this.textBoxMessages.Text = "";
            this.textBoxMessages.WordWrap = false;
            this.textBoxMessages.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // radarScreen
            // 
            this.radarScreen.BackColor = System.Drawing.Color.Black;
            this.radarScreen.Location = new System.Drawing.Point(12, 12);
            this.radarScreen.Name = "radarScreen";
            this.radarScreen.Size = new System.Drawing.Size(706, 478);
            this.radarScreen.TabIndex = 1;
            this.radarScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.radarScreen_Paint);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(725, 171);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(251, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Click += new System.EventHandler(this.progressBarEventHandler);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 502);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.radarScreen);
            this.Controls.Add(this.textBoxMessages);
            this.KeyPreview = true;
            this.Name = "View";
            this.Text = "Fyling Lamb (V1)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingEventHandler);
            this.Load += new System.EventHandler(this.View_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEventHandler);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxMessages;
        private System.Windows.Forms.Panel radarScreen;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.ProgressBar progressBar;
    }
}

