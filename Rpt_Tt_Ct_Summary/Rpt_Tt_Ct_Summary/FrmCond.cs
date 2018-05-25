using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using k.libary;

namespace Rpt_Tt_Ct_Summary
{
    public partial class FrmCond : frmReportCondition
    {
        string strconn;
        TextBox txt_sql = new TextBox();
        TextBox txt_bl = new TextBox();

        public FrmCond()
        {
            InitializeComponent();

            strconn = "";
            txt_sql.Text =  "";
            txt_bl.Text = "false";
        }

        public FrmCond(string _strconn,ref TextBox _txt_sql,ref TextBox _txt_bl)
        {
            InitializeComponent();

            strconn = _strconn;
            txt_sql = _txt_sql;
            txt_bl = _txt_bl;
        }

        private void FrmCond_Load(object sender, EventArgs e)
        {
            txt_bl.Text = "false";
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            //base.btnSubmit_Click(sender, e);
            txt_bl.Text = "false";

            string sql = "";

            sql = @"select b.mpcode,b.FULLNAME,sum(tqty) as tqty,sum(cqty) as cqty,sum(tqty - cqty) as qty from (
                    select b.MP_ID,sum(b.QTY * -1) as tqty,0 as cqty
                    from[CMD-FX].dbo.DOC_ST_AJ a
                     left join[CMD-FX]..DOC_ST_AJ_i b on a.docno = b.docno
                    where a.DOCDATE between '" + dtpStart.getDateOnlyForSql() + "' and '" + DtpEnd.getDateOnlyForSql() + @"'
                    group by b.MP_ID
                    union all
                    select b.MP_ID,0 as tqt,sum(b.QTY) as cqtydtpS
                    from[dbBeautyCommSupport]..NT_DOC_CT a
                    left join[dbBeautyCommSupport]..NT_DOC_CT_i b on a.docno = b.docno
                    where a.DOCDATE between '" + dtpStart.getDateOnlyForSql() + "' and '" + DtpEnd.getDateOnlyForSql() + @"'
                    group by b.MP_ID
                    ) as a
                    left join[CMD-FX]..mas_mp b on a.mp_id = b.id
                    group by b.mpcode,b.FULLNAME
                    order by b.MPCODE";


            txt_sql.Text = sql;
            txt_bl.Text = "true";

            this.Close();

        }
    }


    
}
