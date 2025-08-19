namespace WF_Compressor
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Elementos
        private Panel dragDropPanel;
        private Label statusLabel;
        private Panel panel2;
        private Button compressButton;
        private Button selectFileButton;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dragDropPanel = new Panel();
            statusLabel = new Label();
            panel2 = new Panel();
            selectFileButton = new Button();
            compressButton = new Button();
            dragDropPanel.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dragDropPanel
            // 
            dragDropPanel.AccessibleName = "dragDropPanel";
            dragDropPanel.AllowDrop = true;
            dragDropPanel.BorderStyle = BorderStyle.FixedSingle;
            dragDropPanel.Controls.Add(statusLabel);
            dragDropPanel.Controls.Add(panel2);
            dragDropPanel.Dock = DockStyle.Fill;
            dragDropPanel.Location = new Point(0, 0);
            dragDropPanel.Name = "dragDropPanel";
            dragDropPanel.Size = new Size(800, 450);
            dragDropPanel.TabIndex = 0;
            dragDropPanel.DragEnter += dragDropPanel_DragEnter;
            // 
            // statusLabel
            // 
            statusLabel.AccessibleName = "statusLabel";
            statusLabel.AutoSize = true;
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.Font = new Font("Times New Roman", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(584, 36);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "Arrastra un archivo .txt aquí o selecciónalo";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.AccessibleName = "secondaryPanel";
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(selectFileButton);
            panel2.Controls.Add(compressButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 367);
            panel2.Name = "panel2";
            panel2.Size = new Size(798, 81);
            panel2.TabIndex = 0;
            // 
            // selectFileButton
            // 
            selectFileButton.AccessibleName = "selectFileButton";
            selectFileButton.Dock = DockStyle.Bottom;
            selectFileButton.Location = new Point(0, 3);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(796, 40);
            selectFileButton.TabIndex = 1;
            selectFileButton.Text = "Seleccionar Archivo...";
            selectFileButton.UseVisualStyleBackColor = true;
            // 
            // compressButton
            // 
            compressButton.AccessibleName = "compressButton";
            compressButton.Dock = DockStyle.Bottom;
            compressButton.Enabled = false;
            compressButton.Location = new Point(0, 43);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(796, 36);
            compressButton.TabIndex = 0;
            compressButton.Text = "Comprimir";
            compressButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dragDropPanel);
            Name = "Form1";
            Text = "Form1";
            dragDropPanel.ResumeLayout(false);
            dragDropPanel.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

    }
}
