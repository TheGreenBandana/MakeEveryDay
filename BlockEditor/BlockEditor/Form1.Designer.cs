namespace BlockEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            button_load = new Button();
            saveFileDialog = new SaveFileDialog();
            openFileDialog = new OpenFileDialog();
            groupBox1 = new GroupBox();
            button_save = new Button();
            listBox_blockList = new ListBox();
            groupBox2 = new GroupBox();
            button_overwriteBlock = new Button();
            button_editBlock = new Button();
            button_makeBlock = new Button();
            button_deleteBlock = new Button();
            groupBox3 = new GroupBox();
            groupBox6 = new GroupBox();
            label13 = new Label();
            textBox_wealthMax = new TextBox();
            label14 = new Label();
            textBox_happyMax = new TextBox();
            label15 = new Label();
            textBox_wealthMin = new TextBox();
            label16 = new Label();
            textBox_happyMin = new TextBox();
            label9 = new Label();
            textBox_educationMax = new TextBox();
            label10 = new Label();
            textBox_healthMax = new TextBox();
            label11 = new Label();
            textBox_educationMin = new TextBox();
            label12 = new Label();
            textBox_healthMin = new TextBox();
            groupBox5 = new GroupBox();
            label7 = new Label();
            textBox_wealthChange = new TextBox();
            label8 = new Label();
            textBox_happyChange = new TextBox();
            label6 = new Label();
            textBox_educationChange = new TextBox();
            label5 = new Label();
            textBox_healthChange = new TextBox();
            groupBox4 = new GroupBox();
            label_width = new Label();
            label4 = new Label();
            label3 = new Label();
            pictureBox_colorPreview = new PictureBox();
            label1 = new Label();
            button_changeColor = new Button();
            trackBar_width = new TrackBar();
            textBox_name = new TextBox();
            label2 = new Label();
            colorDialog = new ColorDialog();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_colorPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_width).BeginInit();
            SuspendLayout();
            // 
            // button_load
            // 
            button_load.Location = new Point(125, 22);
            button_load.Name = "button_load";
            button_load.Size = new Size(113, 50);
            button_load.TabIndex = 0;
            button_load.Text = "Load File";
            button_load.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button_save);
            groupBox1.Controls.Add(button_load);
            groupBox1.Location = new Point(12, 450);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(248, 87);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Files";
            // 
            // button_save
            // 
            button_save.Location = new Point(6, 22);
            button_save.Name = "button_save";
            button_save.Size = new Size(113, 50);
            button_save.TabIndex = 1;
            button_save.Text = "Save File";
            button_save.UseVisualStyleBackColor = true;
            // 
            // listBox_blockList
            // 
            listBox_blockList.FormattingEnabled = true;
            listBox_blockList.ItemHeight = 15;
            listBox_blockList.Location = new Point(6, 138);
            listBox_blockList.Name = "listBox_blockList";
            listBox_blockList.ScrollAlwaysVisible = true;
            listBox_blockList.Size = new Size(232, 274);
            listBox_blockList.TabIndex = 4;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button_overwriteBlock);
            groupBox2.Controls.Add(button_editBlock);
            groupBox2.Controls.Add(button_makeBlock);
            groupBox2.Controls.Add(listBox_blockList);
            groupBox2.Controls.Add(button_deleteBlock);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(248, 432);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Blocks";
            // 
            // button_overwriteBlock
            // 
            button_overwriteBlock.Location = new Point(125, 78);
            button_overwriteBlock.Name = "button_overwriteBlock";
            button_overwriteBlock.Size = new Size(113, 50);
            button_overwriteBlock.TabIndex = 7;
            button_overwriteBlock.Text = "Overwrite Selected Block";
            button_overwriteBlock.UseVisualStyleBackColor = true;
            button_overwriteBlock.Click += button_overwriteBlock_Click;
            // 
            // button_editBlock
            // 
            button_editBlock.Location = new Point(6, 76);
            button_editBlock.Name = "button_editBlock";
            button_editBlock.Size = new Size(113, 50);
            button_editBlock.TabIndex = 6;
            button_editBlock.Text = "Copy Selected Block Values";
            button_editBlock.UseVisualStyleBackColor = true;
            button_editBlock.Click += button_editBlock_Click;
            // 
            // button_makeBlock
            // 
            button_makeBlock.Location = new Point(6, 22);
            button_makeBlock.Name = "button_makeBlock";
            button_makeBlock.Size = new Size(113, 50);
            button_makeBlock.TabIndex = 3;
            button_makeBlock.Text = "Make Block From Current Values";
            button_makeBlock.UseVisualStyleBackColor = true;
            button_makeBlock.Click += button_makeBlock_Click;
            // 
            // button_deleteBlock
            // 
            button_deleteBlock.Location = new Point(125, 22);
            button_deleteBlock.Name = "button_deleteBlock";
            button_deleteBlock.Size = new Size(113, 50);
            button_deleteBlock.TabIndex = 2;
            button_deleteBlock.Text = "Delete Selected Block";
            button_deleteBlock.UseVisualStyleBackColor = true;
            button_deleteBlock.Click += button_deleteBlock_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(groupBox6);
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(groupBox4);
            groupBox3.Location = new Point(266, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(534, 525);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Block Values";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(textBox_wealthMax);
            groupBox6.Controls.Add(label14);
            groupBox6.Controls.Add(textBox_happyMax);
            groupBox6.Controls.Add(label15);
            groupBox6.Controls.Add(textBox_wealthMin);
            groupBox6.Controls.Add(label16);
            groupBox6.Controls.Add(textBox_happyMin);
            groupBox6.Controls.Add(label9);
            groupBox6.Controls.Add(textBox_educationMax);
            groupBox6.Controls.Add(label10);
            groupBox6.Controls.Add(textBox_healthMax);
            groupBox6.Controls.Add(label11);
            groupBox6.Controls.Add(textBox_educationMin);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(textBox_healthMin);
            groupBox6.Location = new Point(10, 267);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(506, 240);
            groupBox6.TabIndex = 22;
            groupBox6.TabStop = false;
            groupBox6.Text = "Stat Spawn Prerequisites";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(253, 197);
            label13.Name = "label13";
            label13.Size = new Size(69, 15);
            label13.TabIndex = 29;
            label13.Text = "Wealth Max";
            // 
            // textBox_wealthMax
            // 
            textBox_wealthMax.Location = new Point(324, 193);
            textBox_wealthMax.Name = "textBox_wealthMax";
            textBox_wealthMax.PlaceholderText = "Enter maximum wealth...";
            textBox_wealthMax.Size = new Size(169, 23);
            textBox_wealthMax.TabIndex = 28;
            textBox_wealthMax.Tag = "Wealth maximum spawn value";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(254, 145);
            label14.Name = "label14";
            label14.Size = new Size(67, 15);
            label14.TabIndex = 27;
            label14.Text = "Happy Max";
            // 
            // textBox_happyMax
            // 
            textBox_happyMax.Location = new Point(324, 141);
            textBox_happyMax.Name = "textBox_happyMax";
            textBox_happyMax.PlaceholderText = "Enter maximum happiness...";
            textBox_happyMax.Size = new Size(169, 23);
            textBox_happyMax.TabIndex = 26;
            textBox_happyMax.Tag = "Happiness maximum spawn value";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(10, 197);
            label15.Name = "label15";
            label15.Size = new Size(68, 15);
            label15.TabIndex = 25;
            label15.Text = "Wealth Min";
            // 
            // textBox_wealthMin
            // 
            textBox_wealthMin.Location = new Point(79, 193);
            textBox_wealthMin.Name = "textBox_wealthMin";
            textBox_wealthMin.PlaceholderText = "Enter minimum wealth...";
            textBox_wealthMin.Size = new Size(169, 23);
            textBox_wealthMin.TabIndex = 24;
            textBox_wealthMin.Tag = "Wealth minimum spawn value";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(10, 145);
            label16.Name = "label16";
            label16.Size = new Size(66, 15);
            label16.TabIndex = 23;
            label16.Text = "Happy Min";
            // 
            // textBox_happyMin
            // 
            textBox_happyMin.Location = new Point(79, 141);
            textBox_happyMin.Name = "textBox_happyMin";
            textBox_happyMin.PlaceholderText = "Enter minimum happiness...";
            textBox_happyMin.Size = new Size(169, 23);
            textBox_happyMin.TabIndex = 22;
            textBox_happyMin.Tag = "Happiness minimum spawn value";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(261, 90);
            label9.Name = "label9";
            label9.Size = new Size(52, 15);
            label9.TabIndex = 21;
            label9.Text = "Edu Max";
            // 
            // textBox_educationMax
            // 
            textBox_educationMax.Location = new Point(324, 86);
            textBox_educationMax.Name = "textBox_educationMax";
            textBox_educationMax.PlaceholderText = "Enter maximum education...";
            textBox_educationMax.Size = new Size(169, 23);
            textBox_educationMax.TabIndex = 20;
            textBox_educationMax.Tag = "Education maximum spawn value";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(254, 38);
            label10.Name = "label10";
            label10.Size = new Size(67, 15);
            label10.TabIndex = 19;
            label10.Text = "Health Max";
            // 
            // textBox_healthMax
            // 
            textBox_healthMax.Location = new Point(324, 34);
            textBox_healthMax.Name = "textBox_healthMax";
            textBox_healthMax.PlaceholderText = "Enter maximum health...";
            textBox_healthMax.Size = new Size(169, 23);
            textBox_healthMax.TabIndex = 18;
            textBox_healthMax.Tag = "Health maximum spawn value";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(18, 90);
            label11.Name = "label11";
            label11.Size = new Size(51, 15);
            label11.TabIndex = 17;
            label11.Text = "Edu Min";
            // 
            // textBox_educationMin
            // 
            textBox_educationMin.Location = new Point(79, 86);
            textBox_educationMin.Name = "textBox_educationMin";
            textBox_educationMin.PlaceholderText = "Enter minimum education...";
            textBox_educationMin.Size = new Size(169, 23);
            textBox_educationMin.TabIndex = 16;
            textBox_educationMin.Tag = "Education minimum spawn value";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(10, 38);
            label12.Name = "label12";
            label12.Size = new Size(66, 15);
            label12.TabIndex = 15;
            label12.Text = "Health Min";
            // 
            // textBox_healthMin
            // 
            textBox_healthMin.Location = new Point(79, 34);
            textBox_healthMin.Name = "textBox_healthMin";
            textBox_healthMin.PlaceholderText = "Enter minimum health...";
            textBox_healthMin.Size = new Size(169, 23);
            textBox_healthMin.TabIndex = 14;
            textBox_healthMin.Tag = "Health minimum spawn value";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(textBox_wealthChange);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(textBox_happyChange);
            groupBox5.Controls.Add(label6);
            groupBox5.Controls.Add(textBox_educationChange);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(textBox_healthChange);
            groupBox5.Location = new Point(10, 160);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(506, 101);
            groupBox5.TabIndex = 13;
            groupBox5.TabStop = false;
            groupBox5.Text = "Stat Changes";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(267, 66);
            label7.Name = "label7";
            label7.Size = new Size(44, 15);
            label7.TabIndex = 21;
            label7.Text = "Wealth";
            // 
            // textBox_wealthChange
            // 
            textBox_wealthChange.Location = new Point(324, 63);
            textBox_wealthChange.Name = "textBox_wealthChange";
            textBox_wealthChange.PlaceholderText = "Enter change in wealth...";
            textBox_wealthChange.Size = new Size(169, 23);
            textBox_wealthChange.TabIndex = 20;
            textBox_wealthChange.Tag = "Wealth change";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(267, 25);
            label8.Name = "label8";
            label8.Size = new Size(42, 15);
            label8.TabIndex = 19;
            label8.Text = "Happy";
            // 
            // textBox_happyChange
            // 
            textBox_happyChange.Location = new Point(324, 22);
            textBox_happyChange.Name = "textBox_happyChange";
            textBox_happyChange.PlaceholderText = "Enter change in happiness...";
            textBox_happyChange.Size = new Size(169, 23);
            textBox_happyChange.TabIndex = 18;
            textBox_happyChange.Tag = "Happiness change";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 66);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 17;
            label6.Text = "Education";
            // 
            // textBox_educationChange
            // 
            textBox_educationChange.Location = new Point(79, 63);
            textBox_educationChange.Name = "textBox_educationChange";
            textBox_educationChange.PlaceholderText = "Enter change in education...";
            textBox_educationChange.Size = new Size(169, 23);
            textBox_educationChange.TabIndex = 16;
            textBox_educationChange.Tag = "Education change";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 25);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 15;
            label5.Text = "Health";
            // 
            // textBox_healthChange
            // 
            textBox_healthChange.Location = new Point(79, 22);
            textBox_healthChange.Name = "textBox_healthChange";
            textBox_healthChange.PlaceholderText = "Enter change in health...";
            textBox_healthChange.Size = new Size(169, 23);
            textBox_healthChange.TabIndex = 14;
            textBox_healthChange.Tag = "Health change";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label_width);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(pictureBox_colorPreview);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(button_changeColor);
            groupBox4.Controls.Add(trackBar_width);
            groupBox4.Controls.Add(textBox_name);
            groupBox4.Controls.Add(label2);
            groupBox4.Location = new Point(10, 21);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(506, 133);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Visuals";
            // 
            // label_width
            // 
            label_width.AutoSize = true;
            label_width.Location = new Point(424, 92);
            label_width.Name = "label_width";
            label_width.Size = new Size(62, 15);
            label_width.TabIndex = 14;
            label_width.Text = "Value: ###";
            label_width.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(382, 103);
            label4.Name = "label4";
            label4.Size = new Size(25, 15);
            label4.TabIndex = 13;
            label4.Text = "100";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(57, 103);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 12;
            label3.Text = "10";
            // 
            // pictureBox_colorPreview
            // 
            pictureBox_colorPreview.BackColor = Color.Red;
            pictureBox_colorPreview.Location = new Point(444, 18);
            pictureBox_colorPreview.Name = "pictureBox_colorPreview";
            pictureBox_colorPreview.Size = new Size(50, 50);
            pictureBox_colorPreview.TabIndex = 7;
            pictureBox_colorPreview.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 31);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 9;
            label1.Text = "Name";
            // 
            // button_changeColor
            // 
            button_changeColor.Location = new Point(315, 18);
            button_changeColor.Name = "button_changeColor";
            button_changeColor.Size = new Size(123, 50);
            button_changeColor.TabIndex = 5;
            button_changeColor.Text = "Change Color";
            button_changeColor.UseVisualStyleBackColor = true;
            button_changeColor.Click += button_changeColor_Click;
            // 
            // trackBar_width
            // 
            trackBar_width.AutoSize = false;
            trackBar_width.Cursor = Cursors.SizeWE;
            trackBar_width.Location = new Point(57, 73);
            trackBar_width.Maximum = 100;
            trackBar_width.Minimum = 10;
            trackBar_width.Name = "trackBar_width";
            trackBar_width.Size = new Size(350, 45);
            trackBar_width.SmallChange = 5;
            trackBar_width.TabIndex = 5;
            trackBar_width.TickFrequency = 5;
            trackBar_width.Value = 55;
            trackBar_width.ValueChanged += trackBar_width_ValueChanged;
            // 
            // textBox_name
            // 
            textBox_name.Location = new Point(57, 28);
            textBox_name.Name = "textBox_name";
            textBox_name.PlaceholderText = "Enter name...";
            textBox_name.Size = new Size(229, 23);
            textBox_name.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 73);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 11;
            label2.Text = "Width";
            // 
            // colorDialog
            // 
            colorDialog.AnyColor = true;
            colorDialog.Color = Color.Red;
            colorDialog.SolidColorOnly = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 543);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_colorPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_width).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button_load;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
        private GroupBox groupBox1;
        private Button button_save;
        private ListBox listBox_blockList;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button button_makeBlock;
        private Button button_deleteBlock;
        private Button button_changeColor;
        private ColorDialog colorDialog;
        private PictureBox pictureBox_colorPreview;
        private TextBox textBox_name;
        private Label label1;
        private Label label2;
        private TrackBar trackBar_width;
        private GroupBox groupBox4;
        private Label label4;
        private Label label3;
        private GroupBox groupBox5;
        private Label label5;
        private TextBox textBox_healthChange;
        private Label label6;
        private TextBox textBox_educationChange;
        private Label label7;
        private TextBox textBox_wealthChange;
        private Label label8;
        private TextBox textBox_happyChange;
        private GroupBox groupBox6;
        private Label label13;
        private TextBox textBox_wealthMax;
        private Label label14;
        private TextBox textBox_happyMax;
        private Label label15;
        private TextBox textBox_wealthMin;
        private Label label16;
        private TextBox textBox_happyMin;
        private Label label9;
        private TextBox textBox_educationMax;
        private Label label10;
        private TextBox textBox_healthMax;
        private Label label11;
        private TextBox textBox_educationMin;
        private Label label12;
        private TextBox textBox_healthMin;
        private Label label_width;
        private Button button_overwriteBlock;
        private Button button_editBlock;
    }
}
