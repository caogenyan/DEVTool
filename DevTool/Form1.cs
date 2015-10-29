using HostsManager;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevTool
{
    public partial class Form1 : Form
    {
        private DataTable m_DomainTable;
        private HostHelperJsonManager hostManager = new HostHelperJsonManager(new HostHelperJson(), new FileLogger());
        private FileLogger log = new FileLogger();

        private const string ENABLE = "Enable";
        private const string DISABLE = "Disable";
        private const string DELETE = "Delete";
        private int _rowIndex = -1;
        private int firstdisplayrowindex = 0;

        int sortColumn = -1;
        SortOrder sortDirection = SortOrder.None;

        ContextMenuStrip menu = new ContextMenuStrip();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                hostManager.FormatHost();
                this.dgvDomain.AutoGenerateColumns = false;
                AddComboBoxColumn();
                BindData();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message + "\nRight click the shortcut and select Run as Administrator.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddComboBoxColumn()
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.Resizable = DataGridViewTriState.False;
            column.ReadOnly = false;
            column.Width = 55;
            column.Name = "SiteType";
            column.DataSource = GetSiteTypeDataSource();
            column.ValueMember = "SiteTypeValue";
            column.DisplayMember = "SiteTypeStr";
            column.DataPropertyName = "SiteType";
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            this.dgvDomain.Columns.Insert(0, column);
        }
        private void BindData()
        {
            if (dgvDomain.Rows.Count > 0)
            {
                firstdisplayrowindex = this.dgvDomain.FirstDisplayedScrollingRowIndex;
            }

            if (dgvDomain.SortedColumn == null)
            {
                sortColumn = 0;
                sortDirection = SortOrder.Ascending;
            }
            else
            {
                sortColumn = dgvDomain.SortedColumn.Index;
                sortDirection = dgvDomain.SortOrder;
            }

            SetHostDomainDataTable();
            this.bdsDomain.DataSource = m_DomainTable;

            #region recover scroll view
            if (firstdisplayrowindex != 0 && firstdisplayrowindex < dgvDomain.Rows.Count)
            {
                this.dgvDomain.FirstDisplayedScrollingRowIndex = firstdisplayrowindex;
            }
            #endregion
            #region recover sort
            if (sortColumn != -1)
            {
                switch (sortDirection)
                {
                    case SortOrder.Ascending:
                        dgvDomain.Sort(dgvDomain.Columns[sortColumn], ListSortDirection.Ascending);
                        break;
                    case SortOrder.Descending:
                        dgvDomain.Sort(dgvDomain.Columns[sortColumn], ListSortDirection.Descending);
                        break;
                    case SortOrder.None:
                        break;
                }
            }
            #endregion
            //#region set line number
            //for (int count = 0; (count <= (dgvDomain.Rows.Count - 2)); count++)
            //{
            //    dgvDomain.Rows[count].HeaderCell.Value = string.Format((count + 1).ToString(), "0");
            //}
            //#endregion
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.ContextMenuStrip = menu;
            if (this.dgvDomain.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;

                //if subscribe the event SelectionIndexChanged,it weill fire multi-times when span row clicking
                comboBox.SelectionChangeCommitted -= SiteTypeSelectionCommitted;
                comboBox.SelectionChangeCommitted += SiteTypeSelectionCommitted;
            }
        }

        private void SiteTypeSelectionCommitted(object sender, EventArgs e)
        {
            HostDNS host = GetCurrentBoundHost(this.dgvDomain.CurrentCell.RowIndex);
            var sendingCB = sender as DataGridViewComboBoxEditingControl;
            var siteTypeStr = (string)sendingCB.EditingControlFormattedValue;
            SiteType newSiteType = (SiteType)Enum.Parse(typeof(SiteType), siteTypeStr);

            if (host.Remark.SiteType != newSiteType)//change the sitetype
            {
                hostManager.ChangeSiteType(host, newSiteType);
                BindData();
            }
        }

        private void SetHostDomainDataTable()
        {
            List<HostDNS> domainlist = hostManager.GetDomanList(new XmlRecognizer());

            m_DomainTable = new DataTable();
            m_DomainTable.Columns.Add("SiteType", typeof(Model.SiteType));
            m_DomainTable.Columns.Add("Domain", typeof(string));
            m_DomainTable.Columns.Add("IP", typeof(string));
            m_DomainTable.Columns.Add("Remark", typeof(string));
            m_DomainTable.Columns.Add("QA1", typeof(Image));
            m_DomainTable.Columns.Add("QA2", typeof(Image));
            m_DomainTable.Columns.Add("QA3", typeof(Image));
            m_DomainTable.Columns.Add("LOC", typeof(Image));
            m_DomainTable.Columns.Add("IsDisabled", typeof(bool));

            foreach (var domain in domainlist)
            {
                Image enable = GetEnableImage();
                Image disable = GetDisableImage();
                if (domain.Remark != null)
                {
                    switch (domain.Remark.Target)
                    {
                        case TargetType.QA1:
                            m_DomainTable.Rows.Add(new object[] { domain.Remark.SiteType, domain.DomainName, domain.IP, domain.Remark.Comment, enable, disable, disable, disable, domain.IsDisabled });
                            break;
                        case TargetType.QA2:
                            m_DomainTable.Rows.Add(new object[] { domain.Remark.SiteType, domain.DomainName, domain.IP, domain.Remark.Comment, disable, enable, disable, disable, domain.IsDisabled });
                            break;
                        case TargetType.QA3:
                            m_DomainTable.Rows.Add(new object[] { domain.Remark.SiteType, domain.DomainName, domain.IP, domain.Remark.Comment, disable, disable, enable, disable, domain.IsDisabled });
                            break;
                        case TargetType.LOCAL:
                            m_DomainTable.Rows.Add(new object[] { domain.Remark.SiteType, domain.DomainName, domain.IP, domain.Remark.Comment, disable, disable, disable, enable, domain.IsDisabled });
                            break;
                        default:
                            m_DomainTable.Rows.Add(new object[] { domain.Remark.SiteType, domain.DomainName, domain.IP, domain.Remark.Comment, disable, disable, disable, disable, domain.IsDisabled });
                            break;
                    }
                }
                else
                {
                    m_DomainTable.Rows.Add((new object[] { SiteType.UNK, domain.DomainName, domain.IP, "", disable, disable, disable, disable, domain.IsDisabled }));
                }
            }
        }

        private Bitmap GetEnableImage()
        {
            Bitmap image = Properties.Resources.ENABLE;
            image.Tag = ENABLE;
            return image;
        }

        private Bitmap GetDisableImage()
        {
            Bitmap image = Properties.Resources.DISABLE;
            image.Tag = DISABLE;
            return image;
        }

        private void dgvDomain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == -1)
            {
                if (this.dgvDomain.Rows[e.RowIndex].Cells["Domain"].Value == DBNull.Value)
                    return;

                Rectangle newRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Height,
                    e.CellBounds.Height);

                using (Brush gridBrush = new SolidBrush(this.dgvDomain.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush, 2))
                    {
                        // Erase the cell.
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        //划线
                        Point p1 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top);
                        Point p2 = new Point(e.CellBounds.Left + e.CellBounds.Width, e.CellBounds.Top + e.CellBounds.Height);
                        Point p3 = new Point(e.CellBounds.Left, e.CellBounds.Top + e.CellBounds.Height);
                        Point[] ps = new Point[] { p1, p2, p3 };
                        e.Graphics.DrawLines(gridLinePen, ps);

                        //画图标
                        //e.Graphics.DrawImage(img, newRect);
                        ////画字符串
                        e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.CellStyle.Font, Brushes.Black,
                            e.CellBounds.Left + 3, e.CellBounds.Top + 5, StringFormat.GenericDefault);
                        e.Handled = true;
                    }
                }
            }
        }

        private static DataTable GetSiteTypeDataSource()
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("SiteTypeStr", typeof(string)), new DataColumn("SiteTypeValue", typeof(int)) });
            Array array = Enum.GetValues(typeof(SiteType));
            foreach (var a in array)
            {
                table.Rows.Add(new object[] { a.ToString(), (int)a });
            }
            return table;
        }

        class SiteTypeOjbect
        {
            public string SiteTypeStr { get; set; }
            public string SiteTypeValue { get; set; }
        }

        private void dgvDomain_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int col = e.ColumnIndex;
                if (col >= 4 && col <= 7)
                {
                    TargetType target = (TargetType)(col - 3);
                    HostDNS host = GetCurrentBoundHost(e.RowIndex);
                    hostManager.ChangeTargetType(host, target);
                    BindData();
                }
            }
        }

        private HostDNS GetCurrentBoundHost(int rowindex)
        {
            if (rowindex < 0 || rowindex > dgvDomain.Rows.Count)
            {
                rowindex = dgvDomain.CurrentCell.RowIndex;
            }
            HostDNS dns = new HostDNS();
            var currentcell = dgvDomain.CurrentCellAddress;
            var a = dgvDomain.Rows[rowindex].DataBoundItem as DataRowView;


            dns.DomainName = a["Domain"] as string;
            dns.IP = a["IP"] as string;

            dns.Remark = new HostRemark();
            SiteType type = (SiteType)a["SiteType"];
            dns.Remark.SiteType = type;
            dns.Remark.Comment = a["Remark"] as string;


            if (((Image)a["QA1"]).Tag.ToString() == ENABLE)
            {
                dns.Remark.Target = TargetType.QA1;
            }
            else if (((Image)a["QA2"]).Tag.ToString() == ENABLE)
            {
                dns.Remark.Target = TargetType.QA2;
            }
            else if (((Image)a["QA3"]).Tag.ToString() == ENABLE)
            {
                dns.Remark.Target = TargetType.QA3;
            }
            else if (((Image)a["LOC"]).Tag.ToString() == ENABLE)
            {
                dns.Remark.Target = TargetType.LOCAL;
            }
            else
            {
                dns.Remark.Target = TargetType.DEFAULT;
            }
            dns.IsDisabled = (bool)a["IsDisabled"];
            return dns;
        }

        private void dgvDomain_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > -1)
            {
                string oldvalue = this.dgvDomain[e.ColumnIndex, e.RowIndex].Value.ToString();
                string newvalue = e.FormattedValue.ToString();
                HostDNS host = GetCurrentBoundHost(e.RowIndex);//include old value
                // try
                // {
                if (oldvalue != newvalue)
                {
                    switch (e.ColumnIndex)
                    {
                        case 1://domainName
                            hostManager.ChangeDomainName(host, newvalue);
                            this.BindData();
                            break;
                        case 2://IP
                            hostManager.ChangeIP(host, newvalue);
                            this.BindData();
                            break;
                        case 3://Remark
                            HostRemark newremark = host.Remark;
                            newremark.Comment = newvalue;
                            hostManager.ChangeRemark(host, newremark, true);
                            this.BindData();
                            break;
                    }
                }
                //  }
                //   catch (Exception ex)
                //  {
                //      MessageBox.Show(ex.ToString());
                //  }
            }
        }

        private void dgvDomain_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex > 0 && e.ColumnIndex < 4 && e.RowIndex > -1)
            {
                HostDNS host = GetCurrentBoundHost(e.RowIndex);
                menu.Items.Clear();
                if (host.IsDisabled)
                {
                    menu.Items.Add(ENABLE);
                    menu.Items.Add(DELETE);
                }
                else
                {
                    menu.Items.Add(DISABLE);
                    menu.Items.Add(DELETE);
                }
                _rowIndex = e.RowIndex;
                menu.Show(MousePosition.X, MousePosition.Y);
                menu.ItemClicked += menu_ItemClicked;
            }
        }

        void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //try
            //{
            var item = e.ClickedItem.Text;
            HostDNS host = GetCurrentBoundHost(_rowIndex);
            if (item == DISABLE)
            {
                hostManager.DisableHost(host);
            }
            else if (item == ENABLE)
            {
                hostManager.EnableHost(host);
            }
            else if (item == DELETE)
            {
                hostManager.DeleteHostLine(host, true);
            }
            this.BindData();
            //  }
            //  catch (Exception ex)
            // {
            //     MessageBox.Show(ex.Message);
            // }
        }

        private void dgvDomain_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dgvDomain.Rows.Count; i++)
            {
                DataRowView view = dgvDomain.Rows[i].DataBoundItem as DataRowView;
                if ((bool)view["IsDisabled"])
                {
                    this.dgvDomain.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;
                }
            }
        }
    }
}
