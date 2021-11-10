using AdbCustomClient;
using AdbTool.Enums.Calendar;
using AdbTool.Enums.Contacts;
using AdbTool.Enums.Messages;
using DigitalnaForenzikaAdb.CustomControls;
using DigitalnaForenzikaAdb.Enums;
using EnumsNET;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DigitalnaForenzikaAdb
{
    public partial class Form1 : Form
    {
        private readonly IClient _client;
        TreeView tree = new TreeView();
        List<string> expandedNodes = new List<string>();

        public Form1()
        {
            InitializeComponent();

            _client = new Client();
            AdbServer server = new AdbServer();
           
            //start server just in case
            var result = server.StartServer(@"C:\platform-tools\adb.exe", restartServerIfNewer: false);

            tree.AfterSelect += treeView_Click;

            msgsBtn_Click(null, null);
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

            //default
            msgSmsAllBtn_Click(null, null);
        }

        public void msgSmsAllBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetAllMessagesCommand();

            var columns = Enum.GetValues(typeof(MessageColumnEnum))
               .Cast<MessageColumnEnum>()
               .Select(e => e.AsString(EnumFormat.Description));

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, columns.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }
        }

        public void msgSmsInboxBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetInboxMessagesCommand();

            var columns = Enum.GetValues(typeof(MessageColumnEnum))
              .Cast<MessageColumnEnum>()
              .Select(e => e.AsString(EnumFormat.Description));

            var lsView = new CustomListView(lbPanel.Width, lbPanel.Height, columns.ToList());
            lbPanel.Controls.Add(lsView);

            foreach (var row in result.Rows)
            {
                lsView.Items.Add(new ListViewItem(row.ToArray()));
            }
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
        //    btn3.Click += contactGroupsBtn_Click;
            btnPanel.Controls.Add(btn3);

            var btn4 = new FlatButton("Phones", btnPanel.Width / 4, btnPanel.Height, new Point(btn3.Width + btn2.Width + btn1.Width, 0));
           // btn2.Click += contactPhonesBtn_Click;
            btnPanel.Controls.Add(btn4);

            //default
            contanctsBtn_Click(null, null);
        }

        public void contanctsBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetContactsCommand();

            var columns = Enum.GetValues(typeof(ContactGroupColumnEnum))
                .Cast<ContactGroupColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, columns.ToList());

            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }
        }

        public void contactPeopleBtn_Click(object sender, EventArgs e)
        {
            //to do
        }

        public void contactGroupsBtn_Click(object sender, EventArgs e)
        {
            //to do
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

            //default
            eventsBtn_Click(null, null);

        }

        public void eventsBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetEventsCommand();

            var columns = Enum.GetValues(typeof(EventMessageColumnEnum))
              .Cast<EventMessageColumnEnum>()
              .Select(e => e.AsString(EnumFormat.Description));

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, columns.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }
        }

        public void calendarBtn_Click(object sender, EventArgs e)
        {
            lbPanel.Controls.Clear();
            var result = _client.ExeGetCalendarCommand();

            var columns = Enum.GetValues(typeof(CalendarMessageColumnEnum))
              .Cast<CalendarMessageColumnEnum>()
              .Select(e => e.AsString(EnumFormat.Description));

            var lsBox = new CustomListView(lbPanel.Width, lbPanel.Height, columns.ToList());
            lbPanel.Controls.Add(lsBox);

            foreach (var row in result.Rows)
            {
                lsBox.Items.Add(new ListViewItem(row.ToArray()));
            }
        }

        #endregion
    }
}
