namespace RunningEventTracker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtParticipant = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataGridViewData = new System.Windows.Forms.DataGridView();
            this.tabPageRating = new System.Windows.Forms.TabPage();
            this.dataGridViewRating = new System.Windows.Forms.DataGridView();
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).BeginInit();
            this.tabPageRating.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRating)).BeginInit();
            this.SuspendLayout();
            
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageData);
            this.tabControl.Controls.Add(this.tabPageRating);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 561);
            this.tabControl.TabIndex = 0;
            
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.btnStop);
            this.tabPageMain.Controls.Add(this.btnRecord);
            this.tabPageMain.Controls.Add(this.btnStart);
            this.tabPageMain.Controls.Add(this.txtParticipant);
            this.tabPageMain.Controls.Add(this.label3);
            this.tabPageMain.Controls.Add(this.lblTimer);
            this.tabPageMain.Controls.Add(this.txtMinutes);
            this.tabPageMain.Controls.Add(this.label2);
            this.tabPageMain.Controls.Add(this.label1);
            this.tabPageMain.Location = new System.Drawing.Point(4, 29);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(776, 528);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Главная";
            this.tabPageMain.UseVisualStyleBackColor = true;
            
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(
                (int)(((byte)(192))), 
                (int)(((byte)(80))), 
                (int)(((byte)(77)))
            );
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(370, 180);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 40);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.FromArgb(
                (int)(((byte)(79))), 
                (int)(((byte)(129))), 
                (int)(((byte)(189)))
            );
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRecord.ForeColor = System.Drawing.Color.White;
            this.btnRecord.Location = new System.Drawing.Point(210, 180);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(140, 40);
            this.btnRecord.TabIndex = 7;
            this.btnRecord.Text = "Зафиксировать круг";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(
                (int)(((byte)(146))), 
                (int)(((byte)(208))), 
                (int)(((byte)(80)))
            );
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(70, 180);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 40);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Начать отсчет";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            
            // 
            // txtParticipant
            // 
            this.txtParticipant.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtParticipant.Location = new System.Drawing.Point(230, 120);
            this.txtParticipant.Name = "txtParticipant";
            this.txtParticipant.Size = new System.Drawing.Size(100, 32);
            this.txtParticipant.TabIndex = 5;
            
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label3.Location = new System.Drawing.Point(70, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Номер участника:";
            
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.FromArgb(
                (int)(((byte)(0))), 
                (int)(((byte)(0))), 
                (int)(((byte)(192)))
            );
            this.lblTimer.Location = new System.Drawing.Point(230, 60);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(147, 45);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "00:00:00";
            
            // 
            // txtMinutes
            // 
            this.txtMinutes.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtMinutes.Location = new System.Drawing.Point(230, 20);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(100, 32);
            this.txtMinutes.TabIndex = 2;
            this.txtMinutes.Text = "5";
            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label2.Location = new System.Drawing.Point(70, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Оставшееся время:";
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label1.Location = new System.Drawing.Point(70, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Обратный отсчет:";
            
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataGridViewData);
            this.tabPageData.Location = new System.Drawing.Point(4, 29);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(776, 528);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Text = "Данные";
            this.tabPageData.UseVisualStyleBackColor = true;
            
            // 
            // dataGridViewData
            // 
            this.dataGridViewData.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewData.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewData.Name = "dataGridViewData";
            this.dataGridViewData.Size = new System.Drawing.Size(770, 522);
            this.dataGridViewData.TabIndex = 0;
            
            // 
            // tabPageRating
            // 
            this.tabPageRating.Controls.Add(this.dataGridViewRating);
            this.tabPageRating.Location = new System.Drawing.Point(4, 29);
            this.tabPageRating.Name = "tabPageRating";
            this.tabPageRating.Size = new System.Drawing.Size(776, 528);
            this.tabPageRating.TabIndex = 2;
            this.tabPageRating.Text = "Рейтинг";
            this.tabPageRating.UseVisualStyleBackColor = true;
            
            // 
            // dataGridViewRating
            // 
            this.dataGridViewRating.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewRating.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRating.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewRating.Name = "dataGridViewRating";
            this.dataGridViewRating.Size = new System.Drawing.Size(776, 528);
            this.dataGridViewRating.TabIndex = 0;
            
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Система учета забегов";
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.tabPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).EndInit();
            this.tabPageRating.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRating)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.TabPage tabPageRating;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtParticipant;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dataGridViewData;
        private System.Windows.Forms.DataGridView dataGridViewRating;
    }
}