using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using k.libary;

using System.Data.SqlClient;



namespace Rpt_Tt_Ct_Summary
{
    public partial class FrmMain : frmReport
    {
        string Strconn;
        TextBox txt_sql = new TextBox();
        TextBox txt_bl = new TextBox();


        public FrmMain()
        {
            InitializeComponent();

            Strconn = "data source=bcrjc.dyndns.info,1801; initial catalog=CMD-FX;Integrated Security=false;User id=sa;Password=0000";
        }

        public FrmMain(string _strconn)
        {
            InitializeComponent();
            Strconn = _strconn;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {


            btnSearch.Visible = false;
            btnRefreshData.Visible = false;
            btnAddData.Visible = false;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnImportCSV.Visible = false;
            btnImportData.Visible = false;
            btnImportTextTab.Visible = false;
            btnNewData.Visible = false;
            btnPrint.Visible = false;
            btnRefreshDataAll.Visible = false;
            btnRefreshDataClose.Visible = false;
            btnRefreshDataOpen.Visible = false;
            btnSaveData.Visible = false;
            btnSubmit.Visible = false;
            btnSyncData.Visible = false;
            toolStripButton1.Visible = false;
            toolStripSeparator2.Visible = false;
            toolStripSeparator4.Visible = false;


        }

        public override void btnCondition_Click(object sender, EventArgs e)
        {
            //base.btnCondition_Click(sender, e);
            try
            {
                FrmCond frm = new FrmCond(Strconn, ref txt_sql, ref txt_bl);
                frm.ShowDialog();

                if (txt_bl.Text.ToLower() == "true")
                {
                    string sql = txt_sql.Text;

                    DataSet ds = new DataSet();
                    using (cWaitIndicator cw = new cWaitIndicator())
                    {
                        ds = cData.getDataSetWithSqlCommand(Strconn, sql, 1000000, true);

                        lsvReport.addDataWithDataset(ds, true, true,false,3);

                        lsvReport.Items.Clear();
                        ListViewItem lvitem = new ListViewItem();
                                             
                        for(int i = 0; i<= ds.Tables[0].Rows.Count -1;i++)
                        {
                            lvitem =  lsvReport.Items.Add((i+1).ToString());

                            int idx = lsvReport.Items.IndexOf(lvitem);

                            lsvReport.Items[idx].SubItems.Add(ds.Tables[0].Rows[i]["mpcode"].ToString());
                            lsvReport.Items[idx].SubItems.Add(ds.Tables[0].Rows[i]["fullname"].ToString());
                            lsvReport.Items[idx].SubItems.Add(Convert.ToDecimal(ds.Tables[0].Rows[i]["tqty"]).ToString("#,##0"));
                            lsvReport.Items[idx].SubItems.Add(Convert.ToDecimal(ds.Tables[0].Rows[i]["cqty"]).ToString("#,##0"));
                            lsvReport.Items[idx].SubItems.Add(Convert.ToDecimal( ds.Tables[0].Rows[i]["qty"]).ToString("#,##0"));
                        }

                    }

                    MessageBox.Show("ดึงข้อมูลสำเร็จ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด\n" + ex.Message);
            }
        }

      
    }
}
