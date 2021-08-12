using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.IO.Compression;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Net;
using System.Net.Http;
using GitHub.ReleaseDownloader;


namespace Jig_Replicator
{

    static class Program
	{
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        public static Form3 frm3;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateFileW(
             [MarshalAs(UnmanagedType.LPWStr)] string filename,
             [MarshalAs(UnmanagedType.U4)] FileAccess access,
             [MarshalAs(UnmanagedType.U4)] FileShare share,
             IntPtr securityAttributes,
             [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
             [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
             IntPtr templateFile);

        [DllImport("kernel32.dll")]
        static extern bool WriteFile(
            IntPtr hFile, 
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite, 
            out uint lpNumberOfBytesWritten,
            [In] ref System.Threading.NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [STAThread]

        static void Main()
		{
            if (System.IO.Directory.Exists(Application.StartupPath + @"\jigkicktemp"))
                System.IO.Directory.Delete(Application.StartupPath + @"\jigkicktemp", true);
            
            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            frm3 = new Form3();

            Application.Run(frm3);

        }
        public static void DiskWorker(char driveLetter, string MSIDPath, bool isQuickF, byte[] specialOptions, bool cheat)
        {
            try
            {
                if (MSIDPath == "ddc9")
                {
                    String FlashPath = "nothing_at_all_yet";
                    using (ManagementObject mo = new ManagementObject(@"Win32_LogicalDisk='" + driveLetter + @":" + "'"))
                    {
                        foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                        {
                            foreach (ManagementBaseObject c in b.GetRelated("Win32_Diskdrive"))
                            {
                                FlashPath = (string)c["Name"];
                            }

                        }
                    }
                    Debug.WriteLine(FlashPath);
                    IntPtr DrivePointer = new IntPtr();
                    DrivePointer = CreateFileW
                    (
                        FlashPath,
                        FileAccess.ReadWrite,
                        FileShare.ReadWrite,
                        IntPtr.Zero,                      // no security
                        FileMode.Open,               // always/existing
                        FileAttributes.Normal,     // normal file
                        IntPtr.Zero);                     // no attr. template
                    if (!File.Exists(Application.StartupPath + @"\TM.zip"))
                    {
                        frm3.ChangeStatus("Downloading DDCv9 latest release from Balika's GitHub");
                        // create settings object
                        HttpClient httpClient = new HttpClient();
                        string author = "balika011";
                        string repo = "DC-M33";
                        bool includePreRelease = true;
                        string downloadDirPath = Application.StartupPath;
                        IReleaseDownloaderSettings settings = new ReleaseDownloaderSettings(httpClient, author, repo, includePreRelease, downloadDirPath);

                        // create downloader
                        IReleaseDownloader downloader = new ReleaseDownloader(settings);

                        // download latest github release
                        downloader.DownloadLatestRelease();

                        // clean up
                        downloader.DeInit();
                        httpClient.Dispose();
                    }
                    frm3.ChangeStatus("Partitioning");
                    DiskPart(driveLetter, isQuickF, cheat);
                    frm3.ChangeStatus("Extracting DDCv9 to target drive");
                    ZipFile.ExtractToDirectory(Application.StartupPath + @"\TM.zip", driveLetter + @":\");
                    frm3.ChangeStatus("Installing IPL");
                    byte[] IPL = File.ReadAllBytes(driveLetter + @":\TM\msipl.bin");
                    uint WrittenBytes = 0;
                    System.Threading.NativeOverlapped Overlap = new System.Threading.NativeOverlapped();
                    Overlap.OffsetLow = 0x00002000;
                    WriteFile
                        (
                        DrivePointer,
                        IPL,
                        Convert.ToUInt32(IPL.Length),
                        out WrittenBytes,
                        ref Overlap
                        );
                    Debug.WriteLine(WrittenBytes + " IPL bytes written");
                    CloseHandle(DrivePointer);
                    frm3.ChangeStatus("Done!");
                }
                else if (MSIDPath == "psp")
                {
                    if (File.Exists(Application.StartupPath + @"\" + "pandoraSP.zip"))
                    {
                        // PSPack install code
                        String FlashPath = "nothing_at_all_yet";
                        using (ManagementObject mo = new ManagementObject(@"Win32_LogicalDisk='" + driveLetter + @":" + "'"))
                        {
                            foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                            {
                                foreach (ManagementBaseObject c in b.GetRelated("Win32_Diskdrive"))
                                {
                                    FlashPath = (string)c["Name"];
                                }

                            }
                        }
                        Debug.WriteLine(FlashPath);
                        IntPtr DrivePointer = new IntPtr();
                        DrivePointer = CreateFileW
                        (
                            FlashPath,
                            FileAccess.ReadWrite,
                            FileShare.ReadWrite,
                            IntPtr.Zero,                      // no security
                            FileMode.Open,               // always/existing
                            FileAttributes.Normal,     // normal file
                            IntPtr.Zero);                     // no attr. template
                        frm3.ChangeStatus("Partitioning");
                        DiskPart(driveLetter, isQuickF, cheat);
                        frm3.ChangeStatus("Unzipping Pandora Service Pack files to temp directory");
                        if (System.IO.Directory.Exists(Application.StartupPath + @"\pspacktemp"))
                            System.IO.Directory.Delete(Application.StartupPath + @"\pspacktemp", true);
                        String pspzippath = Application.StartupPath + @"\" + "pandoraSP.zip";
                        ZipFile.ExtractToDirectory(pspzippath, (Application.StartupPath + @"\pspacktemp"));
                        StreamWriter sw = File.AppendText(Application.StartupPath + @"\pspacktemp\TM\config.txt");
                        if (specialOptions[0] == 0)
                        {
                            frm3.ChangeStatus("Deleting unselected 6.61 from temp before copying to MS");
                            Directory.Delete(Application.StartupPath + @"\pspacktemp\TM\661", true);
                        }
                        else sw.WriteLine("UP = \"/TM/661/ipl.bin\";");
                        if (specialOptions[1] == 0)
                        {
                            frm3.ChangeStatus("Deleting unselected 6.60 from temp before copying to MS");
                            Directory.Delete(Application.StartupPath + @"\pspacktemp\TM\660", true);
                        }
                        else sw.WriteLine("TRIANGLE = \"/TM/660/ipl.bin\";");
                        if (specialOptions[2] == 0)
                        {
                            frm3.ChangeStatus("Deleting unselected DCv8 from temp before copying to MS");
                            Directory.Delete(Application.StartupPath + @"\pspacktemp\TM\550", true);
                        }
                        else sw.WriteLine("LEFT = \"/TM/550/ipl.bin\";");
                        if (specialOptions[3] == 0)
                        {
                            frm3.ChangeStatus("Deleting unselected 5.50 GEN from temp before copying to MS");
                            Directory.Delete(Application.StartupPath + @"\pspacktemp\TM\DC8", true);
                        }
                        else sw.WriteLine("RIGHT = \"/TM/DC8/ipl.bin\";");
                        if (specialOptions[4] == 0)
                        {
                            frm3.ChangeStatus("Deleting unselected ELF Menu from temp before copying to MS");
                            Directory.Delete(Application.StartupPath + @"\pspacktemp\ELF", true);
                        }
                        else sw.WriteLine("DOWN = \"/TM/pandora.bin\";");
                        sw.Close();
                        string[] folderentries = Directory.GetDirectories(Application.StartupPath + @"\pspacktemp");
                        frm3.ChangeStatus("Copying Pandora Service Pack files to Memory Stick");
                        foreach (string entry in folderentries)
                        {
                            string DirName = new DirectoryInfo(entry).Name;
                            frm3.ChangeStatus("Copying " + DirName + " folder to MS");
                            if (DirName == "TM") frm3.ChangeStatus("Copying " + DirName + " folder to MS, this may take a while...");
                            DirectoryExtensions.DirectoryCopy(Application.StartupPath + @"\pspacktemp\" + DirName, driveLetter + @":\" + DirName, true);
                        }
                        frm3.ChangeStatus("Installing IPL");
                        byte[] IPL = File.ReadAllBytes(Application.StartupPath + @"\pspacktemp\tmc.bin");
                        uint WrittenBytes = 0;
                        System.Threading.NativeOverlapped Overlap = new System.Threading.NativeOverlapped();
                        Overlap.OffsetLow = 0x00002000;
                        WriteFile
                            (
                            DrivePointer,
                            IPL,
                            Convert.ToUInt32(IPL.Length),
                            out WrittenBytes,
                            ref Overlap
                            );
                        Debug.WriteLine(WrittenBytes + " IPL bytes written");
                        CloseHandle(DrivePointer);
                        System.IO.Directory.SetCurrentDirectory(Application.StartupPath);
                        if (System.IO.Directory.Exists(Application.StartupPath + @"\pspacktemp"))
                            System.IO.Directory.Delete(Application.StartupPath + @"\pspacktemp", true);
                        frm3.ChangeStatus("Done!");
                        frm3.ChangeStatus("Done! Please press [?] for instructions.");

                    }
                    else
                    {
                        frm3.ChangeStatus("Done!");
                        frm3.ChangeStatus("Missing pandoraSP.zip.");
                    }
                }
                else
                {
                    if (File.Exists(Application.StartupPath + @"\" + "jigkick620.zip"))
                    {
                        String FlashPath = "nothing_at_all_yet";
                        using (ManagementObject mo = new ManagementObject(@"Win32_LogicalDisk='" + driveLetter + @":" + "'"))
                        {
                            foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                            {
                                foreach (ManagementBaseObject c in b.GetRelated("Win32_Diskdrive"))
                                {
                                    FlashPath = (string)c["Name"];
                                }

                            }
                        }
                        Debug.WriteLine(FlashPath);
                        IntPtr DrivePointer = new IntPtr();
                        DrivePointer = CreateFileW
                        (
                            FlashPath,
                            FileAccess.ReadWrite,
                            FileShare.ReadWrite,
                            IntPtr.Zero,                      // no security
                            FileMode.Open,               // always/existing
                            FileAttributes.Normal,     // normal file
                            IntPtr.Zero);                     // no attr. template
                        frm3.ChangeStatus("Partitioning");
                        DiskPart(driveLetter, isQuickF, cheat);
                        if (System.IO.Directory.Exists(Application.StartupPath + @"\jigkicktemp"))
                            System.IO.Directory.Delete(Application.StartupPath + @"\jigkicktemp", true);
                        String zippath = Application.StartupPath + @"\" + "jigkick620.zip";
                        ZipFile.ExtractToDirectory(zippath, (Application.StartupPath + @"\jigkicktemp"));
                        if (MSIDPath == "sd_msid")
                        {
                            if (specialOptions[0] == 1) MSIDPath = Application.StartupPath + @"\jigkicktemp\sdmsid.bin";
                            if (specialOptions[0] == 2) MSIDPath = Application.StartupPath + @"\jigkicktemp\Gold.bin";
                            if (specialOptions[0] == 3) MSIDPath = Application.StartupPath + @"\jigkicktemp\Black.bin";
                        }
                        System.IO.File.Copy(MSIDPath, Application.StartupPath + @"\jigkicktemp\dec\msid.bin");
                        frm3.ChangeStatus("Re-encrypting JigKick PRX files");
                        System.IO.Directory.SetCurrentDirectory(Application.StartupPath + @"\jigkicktemp");

                        var process2 = new Process();
                        process2.StartInfo.FileName = "decrypt_sp.exe";
                        process2.StartInfo.UseShellExecute = false;
                        process2.StartInfo.CreateNoWindow = true;
                        process2.StartInfo.RedirectStandardInput = true;
                        process2.StartInfo.RedirectStandardOutput = true;
                        process2.StartInfo.Arguments = "-e";
                        process2.Start();
                        process2.WaitForExit();
                        string output2 = process2.StandardOutput.ReadToEnd();
                        Debug.WriteLine(output2);
                        frm3.ChangeStatus("Copying JigKick files to Memory Stick");
                        DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\jigkicktemp\enc");
                        DirectoryExtensions.RenameTo(di, "PRX");
                        Directory.Delete(Application.StartupPath + @"\jigkicktemp\dec", true);
                        string[] folderentries = Directory.GetDirectories(Application.StartupPath + @"\jigkicktemp");
                        foreach (string entry in folderentries)
                        {
                            string DirName = new DirectoryInfo(entry).Name;
                            Debug.WriteLine(DirName);
                            DirectoryExtensions.DirectoryCopy(Application.StartupPath + @"\jigkicktemp\" + DirName, driveLetter + @":\" + DirName, true);
                        }
                        File.Copy(Application.StartupPath + @"\jigkicktemp\pspbtcnf.txt", driveLetter + @":\pspbtcnf.txt");
                        frm3.ChangeStatus("Installing IPL");

                        Debug.WriteLine(DrivePointer);
                        byte[] IPL = File.ReadAllBytes(Application.StartupPath + @"\jigkicktemp\ipl.bin");
                        uint WrittenBytes = 0;
                        System.Threading.NativeOverlapped Overlap = new System.Threading.NativeOverlapped();
                        Overlap.OffsetLow = 0x00002000;
                        WriteFile
                            (
                            DrivePointer,
                            IPL,
                            Convert.ToUInt32(IPL.Length),
                            out WrittenBytes,
                            ref Overlap
                            );
                        Debug.WriteLine(WrittenBytes + " IPL bytes written");
                        CloseHandle(DrivePointer);
                        frm3.ChangeStatus("Done!");
                        if (WrittenBytes == 0) frm3.ChangeStatus("Error writing IPL!");
                    }
                    else
                    {
                        frm3.ChangeStatus("Done!");
                        frm3.ChangeStatus("Missing jigkick620.zip.");
                    }
                }
                System.IO.Directory.SetCurrentDirectory(Application.StartupPath);
                if (System.IO.Directory.Exists(Application.StartupPath + @"\jigkicktemp"))
                    System.IO.Directory.Delete(Application.StartupPath + @"\jigkicktemp", true);
            
            }
            catch (Exception e)
			{
                frm3.ChangeStatus("Done!");
                MessageBox.Show(e.ToString(), "e x c e p t i o n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void DiskPart(char DrvLetter, bool isQuickF, bool cheat)
        {

            // execute DiskPart programatically
            Process process = new Process();
            frm3.ChangeStatus("Partitioning");
            process.StartInfo.FileName = "diskpart.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.StandardInput.WriteLine("select volume " + DrvLetter);
            process.StandardInput.WriteLine("select disk");
            process.StandardInput.WriteLine("clean");
            process.StandardInput.WriteLine("create partition primary offset 1000");
            process.StandardInput.WriteLine("select partition 1");
            process.StandardInput.WriteLine("active");

            if (cheat)
            {
                MessageBox.Show("You chose to use Ridgecrop's GUI fat32format instead of DiskPart to format your drive." +
                    "\nMedia Creator will download the tool and run it for you." +
                    "\n\nA letter will be assigned to target partition (should be " + DrvLetter + @":\)." + 
                    "\n\nYou'll have to press CANCEL (or just ignore) if Windows prompts you to format the drive by its own means and format the volume manually with the tool instead.", "FAT32 cheat activated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                process.StandardInput.WriteLine("assign letter=" + DrvLetter);
                process.StandardInput.WriteLine("exit");
                string output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine(output);
                
                process.WaitForExit();

                if (!File.Exists(Application.StartupPath + @"\guiformat.exe"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("http://ridgecrop.co.uk/guiformat.exe", Application.StartupPath + @"\guiformat.exe");
                    }
                }
                Process formatter = new Process();
                formatter.StartInfo.FileName = Application.StartupPath + @"\guiformat.exe";
                formatter.Start();
                formatter.WaitForExit();
            }
            else
            {
                if (isQuickF) process.StandardInput.WriteLine("format fs=fat32 quick");
                else process.StandardInput.WriteLine("format fs=fat32");
                process.StandardInput.WriteLine("assign letter=" + DrvLetter);
                process.StandardInput.WriteLine("exit");
                string output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine(output);
                process.WaitForExit();
            }

        }

    }
    public static class DirectoryExtensions
    {
        public static void RenameTo(this DirectoryInfo di, string name)
        {
            if (di == null)
            {
                throw new ArgumentNullException("di", "Directory info to rename cannot be null");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("New name cannot be null or blank", "name");
            }

            di.MoveTo(Path.Combine(di.Parent.FullName, name));

            return; //done
        }
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
