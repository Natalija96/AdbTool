using AdbCustomClient;
using Commons.DTO;
using DigitalnaForenzikaAdb.CustomControls;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdbTool
{
    public partial class Form1 : Form
    {
        private readonly IClient _client;
        private TreeView tree = new TreeView();
        private List<string> expandedNodes = new List<string>();
        private Result presentedData;

        public Form1()
        {
            InitializeComponent();

            _client = new Client();
            AdbServer server = new AdbServer();
           
            //start server just in case
            var result = server.StartServer(@"C:\platform-tools\adb.exe", restartServerIfNewer: false);

            tree.AfterSelect += treeView_Click;

            phoneInfoBtn_Click(null, null);
        }

        #region Messages

        private void msgsBtn_Click(object sender, EventArgs e)
        {
            //clear controls
            btnPanel.Controls.Clear();
            lbPanel.Controls.Clear();

            headerLabel.Text = "Messages";

            //button setup
            var btn1 = new FlatButton("SMS All", btnPanel.Width / 3, btnPanel.Height, new Point(0, 0));
            btn1.Click += msgSmsAllBtn_Click;
            btnPanel.Controls.Add(btn1);

            var btn2 = new FlatButton("SMS Inbox", btnPanel.Width / 3, btnPanel.Height, new Point(btn1.Width, 0));
            btn2.Click += msgSmsInboxBtn_Click;
            btnPanel.Controls.Add(btn2);

            var btn3 = new FlatButton("MMS", btnPanel.Width / 3, btnPanel.Height, new Point(btn2.Width + btn1.Width, 0));
            btnPanel.Controls.Add(btn3);

            var exportBtn = new FlatButton("Export", exportPanel.Width, exportPanel.Height, new Point(0, 0));
            exportBtn.Click += ExportPresentedData_Click;
            exportPanel.Controls.Add(exportBtn);

            //default
            msgSmsAllBtn_Click(null, null);
        }

        public void msgSmsAllBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();

            var result = _client.ExeGetAllMessagesCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void msgSmsInboxBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetInboxMessagesCommand();

            var lsView = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());
            lbPanel.Controls.Add(lsView);

            foreach (var row in result.Rows)
            {
                lsView.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        #endregion

        #region Contacts
        private void contactsBtn_Click(object sender, EventArgs e)
        {
            //clear controls
            btnPanel.Controls.Clear();
            lbPanel.Controls.Clear();

            headerLabel.Text = "Contacts";

            var btn1 = new FlatButton("Contacts", btnPanel.Width / 4, btnPanel.Height, new Point(0,0));
            btn1.Click += contanctsBtn_Click;
            btnPanel.Controls.Add(btn1);

            var btn2 = new FlatButton("People", btnPanel.Width / 4, btnPanel.Height, new Point(btn1.Width, 0));
         //   btn2.Click += contactPeopleBtn_Click;
            btnPanel.Controls.Add(btn2);

            var btn3 = new FlatButton("Groups", btnPanel.Width / 4, btnPanel.Height, new Point(btn2.Width + btn1.Width, 0));
            btn3.Click += contactGroupsBtn_Click;
            btnPanel.Controls.Add(btn3);

            var btn4 = new FlatButton("Phones", btnPanel.Width / 4, btnPanel.Height, new Point(btn3.Width + btn2.Width + btn1.Width, 0));
           // btn4.Click += contactPhonesBtn_Click;
            btnPanel.Controls.Add(btn4);

            var exportBtn = new FlatButton("Export", exportPanel.Width, exportPanel.Height, new Point(0, 0));
            exportBtn.Click += ExportPresentedData_Click;
            exportPanel.Controls.Add(exportBtn);

            //default
            contanctsBtn_Click(null, null);
        }

        public void contanctsBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetContactsCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());

            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void contactPeopleBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetGroupsCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());

            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void contactGroupsBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetGroupsCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());

            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void contactPhonesBtn_Click(object sender, EventArgs e)
        {
            //to do
        }

        #endregion

        #region File System

        private void fsBtn_Click(object sender, EventArgs e)
        {
            tree.Width = lbPanel.Width;
            tree.Height = lbPanel.Height;
            btnPanel.Controls.Clear();
            lbPanel.Controls.Clear();

            TreeNode node = new TreeNode();

            //root
            node.Text = "";
            tree.Nodes.Add(node);

            //depth = 1
            RecursiveTreee(node, 1);

            tree.ExpandAll();
            lbPanel.Controls.Add(tree);

            headerLabel.Text = "File System";

            //button setup
            var btn1 = new FlatButton("Download", btnPanel.Width / 4, btnPanel.Height, new Point(0, 0));
            btn1.Click += downloadBtn_Click;
            btnPanel.Controls.Add(btn1);
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode == null)
            {
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var path = tree.SelectedNode.FullPath.Replace("\\", "/");
            string selectedPath = null;

            var sdResult = fbd.ShowDialog();
            if (sdResult == DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }

            if (sdResult != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(fbd.SelectedPath))
            {
                return;
            }

            var result = _client.ExePullCommand(path, selectedPath);

            string message = result.Rows.FirstOrDefault().FirstOrDefault().ToString();
            MessageBox.Show(message);
        }

        private void treeView_Click(object sender, EventArgs e)
        {
            var selectedNode = tree.SelectedNode;
            if (!expandedNodes.Contains(selectedNode.FullPath))
            {
                RecursiveTreee(selectedNode, 1);
                tree.ExpandAll();
                lbPanel.Controls.Add(tree);
            }
        }

        public void RecursiveTreee(TreeNode root, int depth)
        {
            if(depth == 0)
            {
                return;
            }         

            var fullpath = root.FullPath.Replace("\\", "/");

            var results =  _client.ExeGetFilesCommand(fullpath);

            if (results.Rows.FirstOrDefault().Any(result => result.Contains("Permission denied")))
            {
                return;
            }

            if (results.Rows.FirstOrDefault().Any(result => result.Contains("Not a directory")))
            {
                return;
            }

            if (results.Rows.FirstOrDefault().Any(result => result.Contains("No such file or directory")))
            {
                return;
            }

            foreach (var result in results.Rows.FirstOrDefault())
            {
                TreeNode node = new TreeNode
                {
                    Text = result,
                    Name = result
                };

                root.Nodes.Add(node);
                RecursiveTreee(node, depth - 1);
            }

            expandedNodes.Add(root.FullPath);         
        }

        #endregion


        #region Calendar/Events

        private void calendarEventsBtn_Click(object sender, EventArgs e)
        {
            //clear controls
            btnPanel.Controls.Clear();
            lbPanel.Controls.Clear();

            headerLabel.Text = "Calendar/Events";

            //button setup
            var btn1 = new FlatButton("Events", btnPanel.Width / 3, btnPanel.Height, new Point(0, 0));
            btn1.Click += eventsBtn_Click;
            btnPanel.Controls.Add(btn1);

            //button setup
            var btn2 = new FlatButton("Calendar", btnPanel.Width / 3, btnPanel.Height, new Point(btn1.Width, 0));
            btn2.Click += calendarBtn_Click;
            btnPanel.Controls.Add(btn2);

            //button setup
            var btn3 = new FlatButton("Attendees", btnPanel.Width / 3, btnPanel.Height, new Point(btn2.Width + btn1.Width, 0));
            btn3.Click += attendeesBtn_Click;
            btnPanel.Controls.Add(btn3);

            var exportBtn = new FlatButton("Export", exportPanel.Width, exportPanel.Height, new Point(0, 0));
            exportBtn.Click += ExportPresentedData_Click;
            exportPanel.Controls.Add(exportBtn);

            //default
            eventsBtn_Click(null, null);

        }

        public void eventsBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetEventsCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void calendarBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetCalendarCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        public void attendeesBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetAttendeesCommand();

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, result.Header.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }

            presentedData = result;
        }

        #endregion

        public void ExportPresentedData_Click(object sender, EventArgs e)
        {
            var csv = new StringBuilder();

            var header = string.Join(",", presentedData.Header);
            csv.AppendLine(header);

            foreach (var row in presentedData.Rows)
            {
                var newRows = row.Select(column => $"\"{column}\"");
                var line = string.Join(",", newRows);
                csv.AppendLine(line);
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string selectedPath = null;
            var sdResult = fbd.ShowDialog();
            if (sdResult == DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }

            if (sdResult != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(fbd.SelectedPath))
            {
                return;
            }

            var timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            File.WriteAllText(selectedPath + "\\data" + timestamp.ToString()+".csv", csv.ToString());

            MessageBox.Show("Export finished.");
        }

        private void phoneInfoBtn_Click(object sender, EventArgs e)
        {
            //clear controls
            btnPanel.Controls.Clear();
            lbPanel.Controls.Clear();
            exportPanel.Controls.Clear();
            var btn1 = new FlatButton("Refresh", btnPanel.Width / 3, btnPanel.Height, new Point(0, 0));
            btn1.Click += refreshBtn_Click;
            btnPanel.Controls.Add(btn1);

            headerLabel.Text = "Phone Info";
            var textLabel = new Label();

            textLabel.AutoSize = true;
            //textLabel.Location = new Point(102, 97);
            textLabel.Name = "phoneValueLabel";
            textLabel.Size = new System.Drawing.Size(0, 24);
            textLabel.TabIndex = 14;

            var valueLabel = new Label();

            valueLabel.AutoSize = true;
            valueLabel.Name = "phoneValueLabel";
            valueLabel.Size = new System.Drawing.Size(0, 24);
            valueLabel.TabIndex = 14;

            textLabel.Location = new Point(0, 0);
            valueLabel.Location = new Point(0, 50);

            var result = _client.ExeGetPhoneInfoCommand();
           
            
            lbPanel.Controls.Add(textLabel);
            lbPanel.Controls.Add(valueLabel);

            if (result == null)
            {
               valueLabel.Text = "Please, connect your phone, and click refresh button";
                textLabel.Text = "Not Connected";
                msgsBtn.Enabled = false;
                contactsBtn.Enabled = false;
                fsBtn.Enabled = false;
                calendarBtn.Enabled = false;
                return;
            }

            valueLabel.Text = result.Header.LastOrDefault();


            textLabel.Text = "Connected";
            msgsBtn.Enabled = true;
            contactsBtn.Enabled = true;
            fsBtn.Enabled = true;
            calendarBtn.Enabled = true;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            phoneInfoBtn_Click(null, null);
        }
    }
}
