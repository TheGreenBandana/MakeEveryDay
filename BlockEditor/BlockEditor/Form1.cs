using System;

namespace BlockEditor
{
    public partial class Form1 : Form
    {
        private List<Block> blocks;

        private TextBox[] textBoxes;

        public Form1()
        {
            InitializeComponent();
            textBoxes = new TextBox[]
            {
                textBox_healthChange,
                textBox_happyChange,
                textBox_educationChange,
                textBox_wealthChange,
                textBox_healthMin,
                textBox_healthMax,
                textBox_happyMin,
                textBox_happyMax,
                textBox_educationMin,
                textBox_educationMax,
                textBox_wealthMin,
                textBox_wealthMax
            };
            blocks = new List<Block>();
            label_width.Text = $"Value: {trackBar_width.Value}";
        }

        public void LoadFromFile(string file)
        {

        }

        public void SaveToFile(string file)
        {

        }

        /// <summary>
        /// Update the number visual for the width slider.
        /// </summary>
        private void trackBar_width_ValueChanged(object sender, EventArgs e)
        {
            label_width.Text = $"Value: {trackBar_width.Value}";
        }

        /// <summary>
        /// Prompt the user to change the color of the block.
        /// </summary>
        private void button_changeColor_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            pictureBox_colorPreview.BackColor = colorDialog.Color;
        }

        /// <summary>
        /// Ensures that the value put into the text box is a number
        /// </summary>
        private void EnsureValidNumber(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!int.TryParse(textBox.Text, out int temp))
            {
                textBox.Text = "";
            }
        }

        /// <summary>
        /// Attempt to create a new block.
        /// </summary>
        private void button_makeBlock_Click(object sender, EventArgs e)
        {
            Block block = GetBlockFromValues();
            if (block is not null)
            {
                blocks.Add(block);
                UpdateList();
            }
        }

        /// <summary>
        /// Creates a block from values on right side of the screen.
        /// </summary>
        /// <returns>The block if possible, or null if inputs aren't valid.</returns>
        private Block GetBlockFromValues()
        {
            List<string> errors = new List<string>();
            List<int> data = new List<int>();
            // Check for valid name
            if (textBox_name.Text.Length < 1)
                errors.Add("Name cannot be blank.");
            // Check for valid stats
            foreach (TextBox textBox in textBoxes)
                if (int.TryParse(textBox.Text, out int result))
                    data.Add(result);
                else
                    errors.Add(textBox.Tag + " is invalid.");
            // If errors are present, show error
            if (errors.Count > 0)
            {
                string errorDisplay = "";
                foreach (string error in errors)
                    errorDisplay += error + "\n";
                MessageBox.Show(errorDisplay, "Error creating new block!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            // Otherwise, add a new block to the local list
            else
            {
                return new(textBox_name.Text, trackBar_width.Value, pictureBox_colorPreview.BackColor,
                    data[0], data[1], data[2], data[3],
                    new CustomRange(data[4], data[5]),
                    new CustomRange(data[6], data[7]),
                    new CustomRange(data[8], data[9]),
                    new CustomRange(data[10], data[11]));
            }
        }

        /// <summary>
        /// Update the list on the left side of the screen.
        /// </summary>
        private void UpdateList()
        {
            listBox_blockList.BeginUpdate();

            listBox_blockList.Items.Clear();
            foreach (Block block in blocks)
                listBox_blockList.Items.Add(block.Name);

            listBox_blockList.EndUpdate();
        }

        /// <summary>
        /// Deletes the selected block, if it exists.
        /// </summary>
        private void button_deleteBlock_Click(object sender, EventArgs e)
        {
            if (listBox_blockList.SelectedItem is not null)
            {
                blocks.RemoveAt(listBox_blockList.SelectedIndex);
                UpdateList();
            }
            else
                MessageBox.Show("No block is selected.", "Error deleting block!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Copy the selected block's value to the right side of the screen.
        /// </summary>
        private void button_editBlock_Click(object sender, EventArgs e)
        {
            if (listBox_blockList.SelectedItem is not null)
            {
                Block block = blocks[listBox_blockList.SelectedIndex];
                textBox_name.Text = block.Name;
                trackBar_width.Value = block.Width;
                pictureBox_colorPreview.BackColor = block.Color;
                textBox_healthChange.Text = block.HealthMod.ToString();
                textBox_educationChange.Text = block.EduMod.ToString();
                textBox_happyChange.Text = block.HappyMod.ToString();
                textBox_wealthChange.Text = block.WealthMod.ToString();
                textBox_healthMin.Text = block.HealthRange.Min.ToString();
                textBox_healthMax.Text = block.HealthRange.Max.ToString();
                textBox_educationMin.Text = block.EduRange.Min.ToString();
                textBox_educationMax.Text = block.EduRange.Max.ToString();
                textBox_happyMin.Text = block.HappyRange.Min.ToString();
                textBox_happyMax.Text = block.HappyRange.Max.ToString();
                textBox_wealthMin.Text = block.WealthRange.Min.ToString();
                textBox_wealthMax.Text = block.WealthRange.Max.ToString();
            }
            else
                MessageBox.Show("No block is selected.", "Error copying block values!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Overwrite the selected block with a block containing the values from the right side of the screen.
        /// </summary>
        private void button_overwriteBlock_Click(object sender, EventArgs e)
        {
            Block block = GetBlockFromValues();
            if (block is not null)
            {
                blocks.Insert(listBox_blockList.SelectedIndex, block);
                blocks.RemoveAt(listBox_blockList.SelectedIndex + 1);
                UpdateList();
            }
        }
    }
}
