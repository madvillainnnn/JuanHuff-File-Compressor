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
        private Button decompressButton;
        private ProgressBar progressBar;
        private Label statsLabel;

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
            statsLabel = new Label();
            progressBar = new ProgressBar();
            statusLabel = new Label();
            panel2 = new Panel();
            decompressButton = new Button();
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
            dragDropPanel.Controls.Add(statsLabel);
            dragDropPanel.Controls.Add(progressBar);
            dragDropPanel.Controls.Add(statusLabel);
            dragDropPanel.Controls.Add(panel2);
            dragDropPanel.Dock = DockStyle.Fill;
            dragDropPanel.Location = new Point(0, 0);
            dragDropPanel.Name = "dragDropPanel";
            dragDropPanel.Size = new Size(800, 450);
            dragDropPanel.TabIndex = 0;
            dragDropPanel.DragEnter += dragDropPanel_DragEnter;
            dragDropPanel.DragLeave += dragDropPanel_DragLeave;
            dragDropPanel.Paint += dragDropPanel_Paint;
            // 
            // statsLabel
            // 
            statsLabel.AccessibleName = "statsLabel";
            statsLabel.AutoSize = true;
            statsLabel.Font = new Font("Times New Roman", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            statsLabel.Location = new Point(-1, 54);
            statsLabel.Name = "statsLabel";
            statsLabel.Size = new Size(0, 36);
            statsLabel.TabIndex = 3;
            // 
            // progressBar
            // 
            progressBar.AccessibleName = "progressBar";
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 290);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(800, 34);
            progressBar.TabIndex = 2;
            progressBar.Visible = false;
            progressBar.Click += progressBar1_Click;
            // 
            // statusLabel
            // 
            statusLabel.AccessibleName = "statusLabel";
            statusLabel.AutoSize = true;
            statusLabel.BackColor = Color.Transparent;
            statusLabel.Font = new Font("Times New Roman", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(12, 9);
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
            panel2.Controls.Add(decompressButton);
            panel2.Controls.Add(selectFileButton);
            panel2.Controls.Add(compressButton);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 324);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 126);
            panel2.TabIndex = 0;
            // 
            // decompressButton
            // 
            decompressButton.AccessibleName = "decompressButton";
            decompressButton.Dock = DockStyle.Bottom;
            decompressButton.Enabled = false;
            decompressButton.Location = new Point(0, 40);
            decompressButton.Name = "decompressButton";
            decompressButton.Size = new Size(798, 42);
            decompressButton.TabIndex = 2;
            decompressButton.Text = "Descomprimir";
            decompressButton.UseVisualStyleBackColor = true;
            decompressButton.MouseEnter += decompressButton_MouseEnter;
            decompressButton.MouseLeave += decompressButton_MouseLeave;
            // 
            // selectFileButton
            // 
            selectFileButton.AccessibleName = "selectFileButton";
            selectFileButton.Dock = DockStyle.Top;
            selectFileButton.Location = new Point(0, 0);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(798, 40);
            selectFileButton.TabIndex = 1;
            selectFileButton.Text = "Seleccionar Archivo...";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.MouseEnter += selectFileButton_MouseEnter;
            selectFileButton.MouseLeave += selectFileButton_MouseLeave;
            // 
            // compressButton
            // 
            compressButton.AccessibleName = "compressButton";
            compressButton.Dock = DockStyle.Bottom;
            compressButton.Enabled = false;
            compressButton.Location = new Point(0, 82);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(798, 42);
            compressButton.TabIndex = 0;
            compressButton.Text = "Comprimir";
            compressButton.UseVisualStyleBackColor = true;
            compressButton.MouseEnter += compressButton_MouseEnter;
            compressButton.MouseLeave += compressButton_MouseLeave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dragDropPanel);
            Name = "Form1";
            Text = "JuanHuff File Compressor";
            dragDropPanel.ResumeLayout(false);
            dragDropPanel.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

    }
}
