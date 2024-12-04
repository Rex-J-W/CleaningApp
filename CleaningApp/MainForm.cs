using System.Diagnostics;

namespace CleaningApp
{
    public partial class MainForm : Form
    {
        public string dir = string.Empty;
        public DriveInfo[] drives = [];

        public string[] dirs = [];
        public string[] names = [];
        public long[] dirSizes = [];
        public long[] dirFiles = [];
        public bool[] finished = [];

        public int session_tracker = 0;
        public bool drawn = false;

        public MainForm()
        {
            InitializeComponent();
            GetDrives();
        }

        private void GetDrives()
        {
            drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
                drivesBox.Items.Add(drives[i].Name);

            drivesBox.SelectedIndex = 0;
        }

        private void DrawNew()
        {
            folderView.Items.Clear();

            for (int i = 0; i < dirs.Length; i++)
                folderView.Items.Add(new ListViewItem([names[i], "...", dirFiles[i].ToString()]));

            if (dirs.Length == 0)
                folderView.Items.Add(new ListViewItem(["< Empty >", "N/A", "N/A"]));
        }

        private void DrawUpdates()
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                string sizeString;
                if (finished[i]) sizeString = dirSizes[i].ToPrettySize(2);
                else sizeString = dirSizes[i].ToPrettySize(2) + "...";

                folderView.Items[i].SubItems[1].Text = sizeString;
                folderView.Items[i].SubItems[2].Text = dirFiles[i].ToString();

                if (sizeString.Contains("Gb"))
                    folderView.Items[i].BackColor = Color.LightSalmon;
                else if (dirFiles[i] > 100000)
                    folderView.Items[i].BackColor = Color.PaleTurquoise;
                else if (!sizeString.Contains("..."))
                    folderView.Items[i].BackColor = Color.PaleGreen;
                else
                    folderView.Items[i].BackColor = Color.Yellow;
            }
        }

        private void UpdateFolders(string newDir)
        {
            try
            {
                dirs = Directory.GetDirectories(newDir);
            }
            catch (Exception)
            {
                Debug.WriteLine("Inaccessible");
                return;
            }

            drawn = false;
            session_tracker++;

            dir = newDir;
            dirLabel.Text = dir;
            names = new string[dirs.Length];
            dirSizes = new long[dirs.Length];
            dirFiles = new long[dirs.Length];
            finished = new bool[dirs.Length];

            for (int i = 0; i < dirs.Length; i++)
            {
                names[i] = "< Inaccessible >";
                try
                {
                    names[i] = dirs[i][(dirs[i].LastIndexOf('\\') + 1)..];
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                dirSizes[i] = 0;
                dirFiles[i] = 0;
                finished[i] = false;
                FindSize(i, session_tracker);
            }

            DrawNew();
            drawn = true;
        }

        private async void FindSize(int dirIndex, int session)
        {
            Thread t = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                EnumerationOptions options = new EnumerationOptions
                {
                    IgnoreInaccessible = true,
                    AttributesToSkip = FileAttributes.System | FileAttributes.Temporary,
                    RecurseSubdirectories = true
                };
                string curDir = dirs[dirIndex];

                DirectoryInfo dirInfo = new DirectoryInfo(curDir);
                foreach (FileInfo file in dirInfo.EnumerateFiles("*", options))
                {
                    if (session != session_tracker) return;
                    dirSizes[dirIndex] += file.Length;
                    dirFiles[dirIndex] += 1;
                }
            });

            t.Start();
            while (t.IsAlive) await Task.Delay(1);

            if (session != session_tracker) return;
            finished[dirIndex] = true;
            Debug.WriteLine("Ended " + dirIndex);
            if (drawn) DrawUpdates();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (drawn) DrawUpdates();
        }

        private void folderView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (folderView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in folderView.SelectedItems)
                {
                    if (dirs.Length == 0) break;
                    UpdateFolders(dirs[item.Index]);
                    break;
                }
                folderView.SelectedItems.Clear();
            }
        }

        private void upDirButton_Click(object sender, EventArgs e)
        {
            UpdateFolders(Path.GetDirectoryName(dir));
        }

        private void drivesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFolders(drivesBox.Text);
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", @dir);
        }
    }
}
