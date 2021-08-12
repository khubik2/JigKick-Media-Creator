using System;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.Timers;
using iTuner;

namespace Jig_Replicator
{
	
	public partial class Form3 : Form
	{
        System.Timers.Timer timer = new System.Timers.Timer(1000);

        public static Form4 frm4;
        private static Thread serviceThread;
        private char DriveLetter;
        public byte SDAdapterID = 0;
        private bool fat32Cheats = false;
        private byte[] opts = { 0, 0, 0, 0, 0 };
        public enum EventType
        {
            Inserted = 2,
            Removed = 3
        }
        public bool ThreadRunning = false;
        public Form3()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
            timer.Elapsed += DangerousHighlightEvent;
            timer.AutoReset = true;
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");
            button4.Enabled = false;
            label6.Enabled = false;
            label7.Enabled = false;

            watcher.EventArrived += (s, e) =>
            {
                RefreshDisks();
            };

            watcher.Query = query;
            watcher.Start();

            RefreshDisks();


        }

        private void RefreshDisks()
        {
            if (ThreadRunning) return;
            else
            {
                comboBox1.Items.Clear();
                comboBox1.ResetText();
                UsbManager manager = new UsbManager();
                UsbDiskCollection disks = manager.GetAvailableDisks();
                foreach (UsbDisk disk in disks)
                {
                    comboBox1.Items.Add(disk.ToString());
                }
                if (!checkBox1.Checked) label5.Text = "Waiting for PSP Memory Stick mount...";
                if (comboBox1.Items.Count > 0)
                {
                    if (checkBox1.Checked == false)
                    {
                        foreach (string choice in comboBox1.Items)
                        {
                            if (choice.Contains("MS USB Device"))
                            {
                                comboBox1.Text = choice;
                                label5.Text = "PSP Memory Stick mount found!";
                                break;
                            }
                        }
                    }
                    else comboBox1.SelectedIndex = 0;
                }
                else comboBox1.ResetText();
            }
        }
		private void DoStateChanged(UsbStateChangedEventArgs e)
		{

		}

		private void Form3_Load(object sender, EventArgs e)
		{
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton2.Checked)
			{
				textBox1.Enabled = true;
				button1.Enabled = true;
			}
			else
			{
				textBox1.Enabled = false;
				button1.Enabled = false;
			}
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
        }

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			RefreshDisks();
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.Text.Contains("SONY \"PSP\" SS USB Device"))
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
            if (checkBox1.Checked)
            {
                
                comboBox1.Enabled = true;
                label5.Text = "Mount detection overridden...";
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;
                label5.Text = "Waiting for PSP Memory Stick mount...";
                foreach (string choice in comboBox1.Items)
                {
                    if (choice.Contains("SONY \"PSP\" MS USB Device") || choice.Contains ("SONY PSP USB Device"))
                    {
                        comboBox1.Text = choice;
                        label5.Text = "PSP Memory Stick mount found!";
                        break;
                    }
                }
            }
        }
		public void button2_Click(object sender, EventArgs e)
		{

            String workerVar = "0";
            if (radioButton2.Checked && textBox1.Text != "msid.bin file..." && System.IO.File.Exists(textBox1.Text)) workerVar = textBox1.Text;
            else if (radioButton3.Checked) workerVar = "sd_msid";
            else if (radioButton4.Checked) workerVar = "ddc9";
            if (radioButton2.Checked && textBox1.Text == "msid.bin file...") label5.Text = "No msid.bin file specified.";
            else if (!radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked) label5.Text = "No option selected.";
            else if (radioButton3.Checked && SDAdapterID == 0) label5.Text = "No microSD adapter selected.";
            else if (comboBox1.Text == "") label5.Text = "No device selected.";
            else
            {
                DialogResult LETSGOOO = MessageBox.Show("To make your card bootable in PSP's factory service mode, it needs to be formatted.\n" +
                                    "This means you will lose ALL DATA STORED ON IT!\n" +
                                    "This tool is in TESTING and may wreck havoc.\n" +
                                    "Proceed anyway?",
                                        "WARNING!",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);
                if (LETSGOOO == DialogResult.Yes)
                {
                    DriveLetter = comboBox1.Text[0];
                    button2.Enabled = false;

                    if (workerVar == "psp")
					{
                        // Braindead code to fill the array as check say goes here //
                        // index 0 - 661
                        // index 1 - 660
                        // index 2 - 550 gen
                        // index 3 - ddc
                        // index 4 - biohazard :)
                    }
                    else if (workerVar == "sd_msid") opts[0] = SDAdapterID;
                    ThreadRunning = true;
                    serviceThread = new Thread(() => Program.DiskWorker(DriveLetter, workerVar, checkBox2.Checked, opts, fat32Cheats));
                    comboBox1.Enabled = false;
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    checkBox8.Enabled = false;
                    radioButton2.Enabled = false;
                    radioButton3.Enabled = false;
                    label6.Enabled = false;
                    label7.Enabled = false;
                    button4.Enabled = false;
                    textBox1.Enabled = false;
                    button1.Enabled = false;
                    radioButton4.Enabled = false;
                    serviceThread.Start();
                    label5.Text = "*** Begin Installation ***";
                }
            }
            
		}
        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
{
    ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
    foreach (var property in instance.Properties)
    {
        Console.WriteLine(property.Name + " = " + property.Value);
    }
}           
        public void ChangeStatus(string status)
        {
            label5.Text = status;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Enabled)
            {
                if (comboBox1.Text.Contains("SS USB Device"))
                {
                    button2.Enabled = false;
                    ChangeStatus("You can't select PSPgo's internal memory as the target device.");
                }
                else
                {
                    button2.Enabled = true;
                    ChangeStatus("Mount detection overridden...");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "MSID dump|*.bin";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }

        private void label5_TextChanged(object sender, EventArgs e)
        {
            if (label5.Text == "Done!")
            {
                checkBox8.Enabled = true;
        
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                button2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                radioButton4.Enabled = true;
                ThreadRunning = false;
                if (checkBox1.Checked)
                {
                    comboBox1.Enabled = true;
                }

                if (radioButton2.Checked)
                { 
                    textBox1.Enabled = true;
                    button1.Enabled = true;
                }
                if (radioButton3.Checked)
				{
                    button4.Enabled = true;
                    label6.Enabled = true;
                    label7.Enabled = true;
                }
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void button3_Click_1(object sender, EventArgs e)
		{
            MessageBox.Show("Installs selected unbricker tools and Time Machine IPL on your Memory Stick.\nTo boot a unbricker tool, you need to hold down the key next to its name while booting in service mode.", "Pandora Service Pack help", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
        }

        private static void DangerousHighlightEvent(Object source, ElapsedEventArgs e)
        {

        }

		private void checkBox7_Click(object sender, EventArgs e)
		{
        }

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}

		private void button4_Click(object sender, EventArgs e)
		{
            frm4 = new Form4();
            frm4.Show();
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
            if (radioButton3.Checked)
            {
                button4.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
            }
        }
        public void changeSD(string name)
		{
            label7.Text = name;
		}

		private void checkBox8_CheckedChanged(object sender, EventArgs e)
		{
            if (checkBox8.Checked) fat32Cheats = true;
            else fat32Cheats = false;
		}
	}
}
