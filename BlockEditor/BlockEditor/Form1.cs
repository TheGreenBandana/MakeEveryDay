using System;

namespace BlockEditor
{
    public partial class Form1 : Form
    {
        // List of current saved boxes
        private List<Block> blocks;

        // Array of text boxes for making data validation easier
        private TextBox[] textBoxes;

        // On form load
        public Form1()
        {
            InitializeComponent();
            // Save text boxes to validation array
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
                textBox_wealthMax,
                textBox_ageMin,
                textBox_ageMax
            };
            // Make empty block list and make width bar value reflect visual bar
            blocks = new List<Block>();
            ResetValues();
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
        /// Attempt to create a new block.
        /// </summary>
        private void button_makeBlock_Click(object sender, EventArgs e)
        {
            blocks.Add(GetBlockFromValues());
            UpdateList();
        }

        /// <summary>
        /// Creates a block from values on right side of the screen.
        /// </summary>
        /// <returns>The block if possible, or null if inputs aren't valid.</returns>
        private Block GetBlockFromValues()
        {
            List<int> data = new List<int>();
            // Check for valid stats and use default values for invalid stats
            for (int i = 0; i < textBoxes.Length; i++)
            {
                if (int.TryParse(textBoxes[i].Text, out int result))
                    data.Add(result);
                else
                    data.Add(i > 3 ? -1 : 0);
            }
            // Return the new block
            string name = textBox_name.Text.Length > 0 ? textBox_name.Text : $"Block {blocks.Count + 1}";
            return new(name, trackBar_width.Value, pictureBox_colorPreview.BackColor,
                data[0], data[1], data[2], data[3],
                new CustomRange(data[4], data[5]),
                new CustomRange(data[6], data[7]),
                new CustomRange(data[8], data[9]),
                new CustomRange(data[10], data[11]),
                new CustomRange(data[12], data[13]));
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
        /// Deletes the selected blocks, if they exists.
        /// </summary>
        private void button_deleteBlock_Click(object sender, EventArgs e)
        {
            if (listBox_blockList.SelectedItems.Count > 0)
            {
                int itemCount = listBox_blockList.SelectedItems.Count;
                for (int i = 0; i < itemCount; i++)
                    blocks.RemoveAt(listBox_blockList.SelectedIndices[0]);
                UpdateList();
            }
            else
                MessageBox.Show("No blocks are selected.", "Error deleting block!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Copy the selected block's value to the right side of the screen.
        /// </summary>
        private void button_editBlock_Click(object sender, EventArgs e)
        {
            // Ensure only 1 block is selected
            if (listBox_blockList.SelectedItems.Count < 1)
            {
                MessageBox.Show("No block is selected.", "Error copying blocks!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (listBox_blockList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Only one block can be selected.", "Error copying blocks!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Change all editor values to match selected block
            else
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
                textBox_ageMin.Text = block.AgeRange.Min.ToString();
                textBox_ageMax.Text = block.AgeRange.Max.ToString();
            }
        }

        /// <summary>
        /// Overwrite the selected blocks with a block containing the values from the right side of the screen.
        /// </summary>
        private void button_overwriteBlock_Click(object sender, EventArgs e)
        {
            Block block = GetBlockFromValues();
            if (listBox_blockList.SelectedItems.Count > 0)
            {
                int itemCount = listBox_blockList.SelectedItems.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    blocks.Insert(listBox_blockList.SelectedIndices[i], block);
                    blocks.RemoveAt(listBox_blockList.SelectedIndices[i] + 1);
                }
                UpdateList();
            }
            else
                MessageBox.Show("No blocks are selected.", "Error overwriting blocks!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Button calls function to reset all values to default.
        /// </summary>
        private void button_reset_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        /// <summary>
        /// Reset all input values to their defaults.
        /// </summary>
        private void ResetValues()
        {
            foreach (TextBox textBox in textBoxes)
                textBox.Text = "";
            textBox_name.Text = "";
            pictureBox_colorPreview.BackColor = Color.Green;
            trackBar_width.Value = (trackBar_width.Maximum + trackBar_width.Minimum) / 2;
        }

        /// <summary>
        /// Save the current local list to an external file
        /// </summary>
        private void SaveFile(object sender, EventArgs e)
        {
            StreamWriter writer = null;
            // Save blocks to the file using the blocks' ToString method
            try
            {
                writer = new("..\\..\\..\\..\\..\\MakeEveryDay\\Content\\gameBlocks.blocks");
                foreach (Block block in blocks)
                    writer.WriteLine(block.ToString());
            }
            catch
            {
                MessageBox.Show("Failed to save data to file.", "Error saving blocks!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    MessageBox.Show("Saved blocks to file!", "Blocks saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Load blocks from a file and choose to replace or add blocks from that file to the current list
        /// </summary>
        private void LoadFile(object sender, EventArgs e)
        {
            StreamReader reader = null;
            List<Block> loadedBlocks = new List<Block>();
            bool success = false;
            try
            {
                reader = new("..\\..\\..\\..\\..\\MakeEveryDay\\Content\\gameBlocks.blocks");
                while (!reader.EndOfStream)
                {
                    string[] blockData = reader.ReadLine().Split('|');
                    // Splitting line into data that fits the block's constructor
                    loadedBlocks.Add(new Block(
                        blockData[0],
                        int.Parse(blockData[1]),
                        Color.FromArgb(int.Parse(blockData[2])),
                        int.Parse(blockData[3]),
                        int.Parse(blockData[4]),
                        int.Parse(blockData[5]),
                        int.Parse(blockData[6]),
                        new CustomRange(int.Parse(blockData[7].Split(',')[0]), int.Parse(blockData[7].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[8].Split(',')[0]), int.Parse(blockData[8].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[9].Split(',')[0]), int.Parse(blockData[9].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[10].Split(',')[0]), int.Parse(blockData[10].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[11].Split(',')[0]), int.Parse(blockData[11].Split(',')[1]))
                    ));
                }
                success = true;
            }
            catch
            {
                MessageBox.Show("Failed to load data from file.", "Error loading blocks!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (success)
                {
                    // If no list existed, just load the blocks into the list
                    if (blocks.Count == 0)
                    {
                        blocks = loadedBlocks;
                        UpdateList();
                        MessageBox.Show("Loaded blocks from file!", "Blocks loaded!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // If a list already existed, check if the user wants to replace the existing list, add to it or cancel loading entirely
                    else
                    {
                        DialogResult userInput = MessageBox.Show("Loaded blocks from file!\nDo you want to add the loaded blocks to the current list?",
                            "Blocks loaded!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (userInput == DialogResult.No)
                        {
                            blocks = loadedBlocks;
                            UpdateList();
                        }
                        else if (userInput == DialogResult.Yes)
                        {
                            foreach (Block block in loadedBlocks)
                                blocks.Add(block);
                            UpdateList();
                        }
                    }
                }
            }
        }
    }
}
