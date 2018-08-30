using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace TangSiToLis
{
    public partial class frmTangSiToLis : Form
    {
        private static string pathDb = "";
        int sucessInsertCount = 0;

        public frmTangSiToLis()
        {
            InitializeComponent();

        }

        public static OleDbConnection myOleDbConn;     // Access 数据库连接对象
        public static OleDbDataAdapter myOleDbAdap;    // 数据库结果连接对象
        public static OleDbCommand myOleDbComm;        // 数据库执行对象
        public bool myDebugFlag = true;              // 调试
        List<int> _selectYbbhList = new List<int>();

        // 数据库连接
        public string myMsSqlConn  = "Server='LipMstDB.kindstar.com.cn';Database=NewLIMS;User=sa;Pwd='Lis9op0(OP)'";
        public string myMsSqlConn1 = @"Data Source=LipMstDB.kindstar.com.cn; Initial Catalog =NewLIMS;User ID =sa; Password =Lis9op0(OP);";
        //====//
        //public string myMsSqlConn = "Server='192.168.5.252';Database=ELIMSDB_LIU;User=sa;Pwd='abc123@'";
        //public string myMsSqlConn1 = @"Data Source =192.168.5.252; Initial Catalog =ELIMSDB_LIU;User ID =sa; Password =abc123@;";

        public string myAccessConn = getStrConn("Interface.mdb");

        // --- Get Access Conn String ---
        public static string getStrConn(string myDbFileName)
        {
            string pathTxt = Application.StartupPath;

            //====//
            //string fileName = "d:\\liscomm\\" + myDbFileName;

            pathDb = ReadValueByKey("DbPath");
            string fileName = pathDb + myDbFileName;

            if (File.Exists(fileName))
            {
                return string.Format("{0}{1}", "Provider=microsoft.jet.oledb.4.0;Data Source=", fileName);
                //return string.Format("{0}{1}", "Provider=microsoft.jet.oledb.12.0;Persist Security Info=False;Data Source=", fileName);

            }
            else
            {
                return "";
            }
        }

        // --- 打开 Access 数据库 ----
        static bool OpenDb(string myStrConn)
        {
            try
            {
                // 捕获数据库打开异常
                myOleDbConn = new OleDbConnection(myStrConn);
                myOleDbConn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static bool CloseDb(OleDbConnection myOleDbConn)
        {
            try
            {
                myOleDbConn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // --- 查询Access数据 ---
        static DataSet sqlSearch(string strMySql, string strTableName)
        {
            try
            {
                myOleDbAdap = new OleDbDataAdapter(strMySql, myOleDbConn);
                if (!string.IsNullOrEmpty(strTableName) && !string.IsNullOrEmpty(strMySql))
                {
                    var dt = new DataSet();
                    myOleDbAdap.Fill(dt, strTableName);
                    myOleDbAdap.Dispose();
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // --- insert Access --- 
        static bool sqlInsert(string strMyConn, string strMySql)
        {
            try
            {
                if (!string.IsNullOrEmpty(strMySql))
                {
                    myOleDbComm = new OleDbCommand(strMySql, myOleDbConn);
                    myOleDbComm.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // --- 写Access数据 ---
        public void myWriteAccess(string mySql)
        {

            if (sqlInsert(myAccessConn, mySql))
            {
                // write log
            }
            else
            {
                // write log
            }
            System.Threading.Thread.Sleep(100);
        }

        // 查询 LIS 数据

        public DataTable myMsSqlQuery(string mySqlData,string myID,string myInOutFlag, List<int> ybbh)
        {
            var ybbh1 = "";
            var i = 0;
            // ReSharper disable once InconsistentNaming
            foreach (var _ybbh in ybbh)
            {
                if (i < ybbh.Count - 1)
                {
                    ybbh1 += _ybbh + ",";
                }
                else
                {
                    ybbh1 += _ybbh;
                }
                i++;
            }
            string mySql = "";
            if ( myInOutFlag=="INPUT" && ybbh.Count>0)
            {

                mySql = @"SELECT DISTINCT a.Barcode Rep_No,a.SampleNumber Pat_Zip,a.TestTime Rep_Date,b.PatientName Pat_Name,PatientAgeDisplay Pat_Birthday
                        FROM TT.Test_SampleInfo a
                        INNER JOIN TT.Test_BaseInfo b ON a.BaseInfoID=b.ID
                        INNER JOIN TT.Test_SampleResult c ON a.ID=c.SampleID
                        WHERE  c.ApplyCode = 'CZ00152' AND a.TestTime =  '" + mySqlData +
                        "' AND a.SampleNumber in (" + ybbh1 + ") ";
            }
            else
            {
                if (myID!="" && ybbh.Count > 0)
                {
                    //mySql = "select convert(char(10),l.cdrq,120) as 检测日期,l.ybid 条码,l.ybbh 样本编号,convert(char(19),l.cyrq,120) 采样日期,l.byh 医院,l.brxm 姓名,convert(char(19),f.csrq,120)  出生日期,z.xmdh 项目,ltrim(z.xmcdz) 检测值 from lis_ybxx l left join f_k_ybxx f on l.ybid = f.ybid left join lis_xmcdz z on l.yqdh = z.yqdh and l.cdrq = z.cdrq and l.ybbh = z.ybbh where l.ybid='" + myID.TrimEnd() + "'";
                    mySql = @"SELECT DISTINCT a.Barcode Rep_No,a.SampleNumber Pat_Zip,a.TestTime Rep_Date,b.PatientName Pat_Name,PatientAgeDisplay Pat_Birthday
                        FROM TT.Test_SampleInfo a
                        INNER JOIN TT.Test_BaseInfo b ON a.BaseInfoID=b.ID
                        INNER JOIN TT.Test_SampleResult c ON a.ID=c.SampleID
                        WHERE  c.ApplyCode = 'CZ00152' AND a.TestTime =  '" + mySqlData +
                        "' AND a.SampleNumber in (" + ybbh1 + ") ";
                
                }
                // 只有“测定日期”的查询
                else
                {
                    //mySql = "select convert(char(10),l.cdrq,120) as 检测日期,l.ybid 条码,l.ybbh 样本编号,convert(char(19),l.cyrq,120) 采样日期,l.byh 医院,l.brxm 姓名,convert(char(19),f.csrq,120)  出生日期,z.xmdh 项目,ltrim(z.xmcdz) 检测值 from lis_ybxx l left join f_k_ybxx f on l.ybid = f.ybid left join lis_xmcdz z on l.yqdh = z.yqdh and l.cdrq = z.cdrq and l.ybbh = z.ybbh where convert(char(10),l.cdrq,121) = '" + mySqlData + "' and l.jymd='唐氏筛查'";
                    mySql = @"SELECT DISTINCT a.Barcode Rep_No,a.SampleNumber Pat_Zip,a.TestTime Rep_Date,b.PatientName Pat_Name,PatientAgeDisplay Pat_Birthday
                        FROM TT.Test_SampleInfo a
                        INNER JOIN TT.Test_BaseInfo b ON a.BaseInfoID=b.ID
                        INNER JOIN TT.Test_SampleResult c ON a.ID=c.SampleID
                        WHERE  (c.ApplyCode = 'CZ00152'OR c.ApplyCode ='CZ00160') 
                        AND a.TestTime =  '" + mySqlData + "'";

                }
            }

            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = myMsSqlConn;
            myConn.Open();

            SqlCommand myCmd = new SqlCommand();
            myCmd.Connection = myConn;
            myCmd.CommandText = mySql;
            myCmd.CommandType = CommandType.Text;

            DataSet myDataSet = new DataSet();

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCmd);
            myAdapter.Fill(myDataSet);
            DataTable myTable = myDataSet.Tables[0];

            myConn.Close();

            return myTable;
        
        }


        // MSSQL Write To Access
        public Int32 myMsSqlToAccess(string myfSql)
        {
            string myReadAccessTable = "InputTable";
            string myLiscyrq, myLiscdrq, myLisybid, myLisybbh, myLisbyh, myLisbrxm, myLiscsrq, myLisWeight, myLisLastMenstrual, myLisBPD, myLisjsrq, myLisSjys;
            string myStrSql = "";
            int myInsertCount = 0;

            // --- Read MSSQL Table Recode --- 
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = myMsSqlConn;
            myConn.Open();

            SqlCommand myCmd = new SqlCommand();
            myCmd.Connection = myConn;
            myCmd.CommandText = myfSql;
            myCmd.CommandType = CommandType.Text;

            DataSet myDataSet = new DataSet();

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCmd);
            myAdapter.Fill(myDataSet);
            
            myConn.Close();

            DataTable myTable = myDataSet.Tables[0];
            DataRowCollection myRows = myTable.Rows;

            // --- Write Access ---

            if (!OpenDb(myAccessConn))
            {
                return 0;
            }

            for (int i = 0; i < myRows.Count; i++)
            {
                Application.DoEvents();

                DataRow myRow = myRows[i];

                if (myRow["采样日期"].ToString()=="")
                {
                    myLiscyrq = "";
                }
                else
                {
                    myLiscyrq = myRow["采样日期"].ToString().Substring(0, 10);
                }

                if (myRow["检测日期"].ToString() == "")
                {
                    myLiscdrq = "";
                }
                else
                {
                    myLiscdrq = myRow["检测日期"].ToString().Substring(0, 10);
                }

                if (myRow["接收日期"].ToString() == "")
                {
                    myLisjsrq = "";
                }
                else
                {
                    myLisjsrq = myRow["接收日期"].ToString().Substring(0, 10);
                }
                
                myLisybid = myRow["条码"].ToString();
                myLisybbh = myRow["样本编号"].ToString();
                myLisbyh = myRow["医院"].ToString();
                myLisbrxm = myRow["姓名"].ToString();
                myLisWeight = myRow["体重"].ToString();
                myLisBPD = myRow["双顶径"].ToString();
                myLisSjys = myRow["送检医生"].ToString();

                if (myRow["出生日期"].ToString()=="")
                {
                    myLiscsrq = "";
                }
                else
                {
                    myLiscsrq = myRow["出生日期"].ToString().Substring(0, 10);  // 出生日期
                }

                if (myRow["末次月经"].ToString()=="")
                {
                    myLisLastMenstrual = "";
                }
                else
                {
                    myLisLastMenstrual = myRow["末次月经"].ToString().Substring(0, 10);
                }

                if (myRow["体重"].ToString() == "")
                {
                    myLisWeight = "0";
                }
                else
                {
                    myLisWeight = myRow["体重"].ToString();
                }

                if (myRow["双顶径"].ToString() == "")
                {
                    myLisBPD = " ";
                }
                else
                {
                    myLisBPD = myRow["双顶径"].ToString().Substring(0, 10);
                }


                myStrSql = string.Format("SELECT * FROM {0} WHERE  format(检测日期,'yyyy-mm-dd')='{1}' and Rep_No='{2}'", myReadAccessTable, myLiscdrq, myLisybid);


                // --- if inputtable have inserted, no insert ---
                DataSet myAcceccDs = sqlSearch(myStrSql, myReadAccessTable);

                if (myAcceccDs != null)
                {
                    if (myAcceccDs.Tables[0].Rows.Count != 0)
                    {
                        Log("Info",myLisybid+" "+ myLisbrxm+" 信息已经存在，无需导入！");
                    }
                    else
                    {
                        // --- insert into inputtable ---
                        myStrSql = string.Format("INSERT INTO InputTable (Pat_Zip,Rep_SampleDate,Rep_Date,Rep_No,送检单位,Pat_Name,Pat_Birthday,检测日期,Pat_Weight,Pat_Methor_Date, Rep_Methor) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','LMP(末次月经)') ",
                                    myLisybbh, myLiscyrq, myLisjsrq, myLisybid, myLisbyh, myLisbrxm, myLiscsrq, myLiscdrq, myLisWeight, myLisLastMenstrual, myLisSjys );
                        myWriteAccess(myStrSql);
                        myInsertCount++;
                    }
                }
                else
                {
                    return 0;
                }
            }   // === for ===

            if (!CloseDb(myOleDbConn))
            {
                return 0;
            }

            return myInsertCount;
        }

        // --- 导入Lis数据到唐氏数据库 ---
        public void inputLisToTang(List<int> ybbh)
        {
            var ybbh1 = "";
            var i = 0;
            string mySql = "";
            // ReSharper disable once InconsistentNaming
            foreach (var _ybbh in ybbh)
            {
                if (i < ybbh.Count - 1)
                {
                    ybbh1 += _ybbh + ",";
                }
                else
                {
                    ybbh1 += _ybbh;
                }
                i++;
            }
            if (myAccessConn.Count() > 2)
            {
                //string mySql = "select convert(char(10),l.cdrq,120) cdrq,l.ybid,l.ybbh,convert(char(19),l.cyrq,120) cyrq,l.byh,l.brxm,convert(char(10),f.csrq,120) csrq,z.xmdh,ltrim(z.xmcdz) xmcdz from lis_ybxx l left join f_k_ybxx f on l.ybid = f.ybid left join lis_xmcdz z on l.yqdh = z.yqdh and l.cdrq = z.cdrq and l.ybbh = z.ybbh where convert(char(10),l.cdrq,121) = '" + this.dtpInputData.Text.TrimEnd() + "' and l.jymd='唐氏筛查' and z.xmdh = 'AFP'";
                if (!string.IsNullOrEmpty(ybbh1))
                {
                    mySql = @"SELECT DISTINCT convert(char(10),
                                I.TestTime,120) as 检测日期,
                                I.Barcode as 条码,
                                I.SampleNumber as 样本编号,
                                convert(char(10),
                                x.SampleCollectionTime, 120) as 采样日期,
                                x.HospitalName as 医院,
                                x.PatientName as 姓名,
                                convert(char(10),
                                x.AgeBirthDay, 120) as 出生日期,
                                S.InstrumentChannelCode as 项目,
                                R.TestResult as 检测值, 
                                x.Weight as 体重,
                                convert(char(10),
                                x.LastMenstrual, 120) as 末次月经,
                                x.BPD as 双顶径, 
                                convert(char(10),
                                I.SampleReceivedate, 120) as 接收日期, 
                                x.DoctorName as 送检医生
                                From TT.Test_SampleInfo I
                                INNER JOIN TT.Test_BaseInfo x ON i.BaseInfoID = x.ID
                                LEFT JOIN TT.Test_SampleResult R on I.ID = R.SampleID
                                LEFT JOIN TE.BAS_ApplyItemSub S on S.SubItemCode = R.SubItemCode
                                Where convert(char(10), I.TestTime, 120)= '"+ dtpInputData.Text + "'" +
                                "AND(r.ApplyCode = 'CZ00152' OR r.ApplyCode = 'CZ00160')"+
                                "AND S.InstrumentChannelCode = '21-ST'"+
                                "AND ISNUMERIC(I.SampleNumber) = 1"+
                                "AND Convert(bigint, I.SampleNumber) in(" + ybbh1 + ")";
                }
                else
                {
                    mySql = @"SELECT DISTINCT convert(char(10),
                                I.TestTime,120) as 检测日期,
                                I.Barcode as 条码,
                                I.SampleNumber as 样本编号,
                                convert(char(10),
                                x.SampleCollectionTime, 120) as 采样日期,
                                x.HospitalName as 医院,
                                x.PatientName as 姓名,
                                convert(char(10),
                                x.AgeBirthDay, 120) as 出生日期,
                                S.InstrumentChannelCode as 项目,
                                R.TestResult as 检测值, 
                                x.Weight as 体重,
                                convert(char(10),
                                x.LastMenstrual, 120) as 末次月经,
                                x.BPD as 双顶径, 
                                convert(char(10),
                                I.SampleReceivedate, 120) as 接收日期, 
                                x.DoctorName as 送检医生
                                From TT.Test_SampleInfo I
                                INNER JOIN TT.Test_BaseInfo x ON i.BaseInfoID = x.ID
                                LEFT JOIN TT.Test_SampleResult R on I.ID = R.SampleID
                                LEFT JOIN TE.BAS_ApplyItemSub S on S.SubItemCode = R.SubItemCode
                                Where convert(char(10), I.TestTime, 120)= '"+ dtpInputData.Text+ "'"+
                                "AND(r.ApplyCode = 'CZ00152' OR r.ApplyCode = 'CZ00160')"+
                                "AND S.InstrumentChannelCode = '21-ST'"+
                                "AND ISNUMERIC(I.SampleNumber) = 1";
                }

                //mySql += " and Convert(int,I.SerialNumber) >=" + Convert.ToInt16(this.tboxBeginSerialNumber.Text);
                //mySql += " and Convert(int,I.SerialNumber) <=" + Convert.ToInt16(this.tboxEndSerialNumber.Text);

                Int32 myInsertNum = myMsSqlToAccess(mySql);
                lblMessageLis.Text = this.dtpInputData.Text + " 导入唐氏数据：" + "  " + myInsertNum + " 条";
            }
            else
            {
                lblMessageLis.Text = "达瑞数据库不存在，导入唐氏数据退出！";
            }
        }


        // 取得 excel 数据
        public DataTable xsldata()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "请选择要导入的excel表格！！";
            openFileDialog1.ShowDialog();

            string filename = openFileDialog1.FileName;
            tbxInputFile.Text = filename;
            string extendedName = Path.GetExtension(filename);

            if (tbxInputFile.Text == "")
            {
                lblMessage.Text = "请选择文件";
                return null;
            }
            else
            {
                if (extendedName != ".xls" && extendedName != ".xlsx")
                {
                    lblMessage.Text = "上传的文件格式不正确";
                    return null;
                }
            }

            try
            {
                //HDR=Yes，这代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。系统默认的是YES
                string connstr2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                string connstr2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                
                OleDbConnection conn;

                if (extendedName == ".xls")
                {
                    conn = new OleDbConnection(connstr2003);
                }
                else
                {
                    conn = new OleDbConnection(connstr2007);
                }

                conn.Open();

                string sql = "select * from [Sheet1$]";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                DataTable dt = new DataTable();
                OleDbDataReader sdr = cmd.ExecuteReader();

                dt.Load(sdr);
                sdr.Close();
                conn.Close();

                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        private void btnInputFile_Click_1(object sender, EventArgs e)
        {
            string cdrq = DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd");
            //string cdrq = "2015-12-10";

            DataTable dt = xsldata();
            this.dgvDaan.DataSource = dt;
            int updatecount = 0;    //记录更新信息条数
            if (dt != null)
            {
                int countnumber = dt.Rows.Count;
            }

            var sqlcon = new SqlConnection(myMsSqlConn1);  //链接数据库
            var oledbComm = new SqlCommand();
            oledbComm.CommandTimeout = 600;

            try
            {
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                string name = tbxOperator.Text;

                if (name != "")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string lisBarCode0 = dt.Rows[i][0].ToString();
                        string lisSampleNo1 = dt.Rows[i][1].ToString();
                        string lisPatientName3 = dt.Rows[i][3].ToString();
                        string lisBirthDay4 = dt.Rows[i][4].ToString();             // 回写
                        string lisPatientWeight5 = dt.Rows[i][5].ToString();        // 回写
                        string lisSampleCollectionTime6 = dt.Rows[i][6].ToString(); // 回写
                        string lisSampleColletionAge7 = dt.Rows[i][7].ToString();   // 回写
                        string lisGestationCalculateMethod8 = dt.Rows[i][8].ToString();
                        string lisLMPDate9 = dt.Rows[i][9].ToString();              // 回写
                        string lisGestationWeek11 = dt.Rows[i][11].ToString();
                        string lisGestationDays12 = dt.Rows[i][12].ToString();
                        string lisGestationWeekNT14 = dt.Rows[i][14].ToString();
                        string lisGestationDaysNT15 = dt.Rows[i][15].ToString();
                        string lisAgeRisk31 = dt.Rows[i][31].ToString();
                        string lisRiskValue_21_32 = dt.Rows[i][32].ToString();
                        string lisRiskValue_18_33 = dt.Rows[i][33].ToString();
                        string lisNTD_Risk34 = dt.Rows[i][34].ToString();

                        string lisFBHCGResult17 = dt.Rows[i][17].ToString();        // 质控
                        string lisFBHCGCorrentionMOM22= dt.Rows[i][22].ToString();
                        string lisFBHCGMOM27= dt.Rows[i][27].ToString();

                        string lisAFPResult18 = dt.Rows[i][18].ToString();          // 质控
                        string lisAFPCorrectionMOM23 = dt.Rows[i][23].ToString();
                        string lisAFPMOM28 = dt.Rows[i][28].ToString();

                        string lisSendTestHospital47 = dt.Rows[i][47].ToString();
                        string lisSendTestDoctor48 = dt.Rows[i][48].ToString();
                        string lisTestTime49 = dt.Rows[i][49].ToString();

                        string lisAFPResult16 = dt.Rows[i][16].ToString();          // 早孕测试
                        string lisAFPCorrectionMOM21 = dt.Rows[i][21].ToString();
                        string lisAFPMOM26 = dt.Rows[i][26].ToString(); 

                        writeMssqlDbNew(sqlcon,
                                        lisBarCode0,
                                        lisSampleNo1,
                                        lisPatientName3,
                                        lisBirthDay4,
                                        lisPatientWeight5,
                                        lisSampleCollectionTime6,
                                        lisSampleColletionAge7,
                                        lisGestationCalculateMethod8,
                                        lisLMPDate9,
                                        lisGestationWeek11,
                                        lisGestationDays12,
                                        lisGestationWeekNT14,
                                        lisGestationDaysNT15,
                                        lisAgeRisk31,
                                        lisRiskValue_21_32,
                                        lisRiskValue_18_33,
                                        lisNTD_Risk34,
                                        lisFBHCGResult17,
                                        lisFBHCGCorrentionMOM22,
                                        lisFBHCGMOM27,
                                        lisAFPResult18,
                                        lisAFPCorrectionMOM23,
                                        lisAFPMOM28,
                                        lisSendTestHospital47,
                                        lisSendTestDoctor48,
                                        lisTestTime49,
                                        lisAFPResult16,
                                        lisAFPCorrectionMOM21,
                                        lisAFPMOM26);

                        Application.DoEvents();
                    }
                    lblMessage.Text = "Excel有: " + dt.Rows.Count +" 条数据!  成功更新 "+ sucessInsertCount +" 条记录！"; //+ Convert.ToString(updatecount);
                }
                else
                {
                    lblMessage.Text = "操作人员必须填写！此次操作未导入数据到Lis系统。";
                }
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //lblMessage.Text = updatecount + "  " + ex.ToString();
            }
        }

        // 导入项目测定值
        public void writeMssqlDbNew(SqlConnection myConn,
            string BarCode,
            string SampleNo,
            string PatientName,
            string BirthDay,
            string PatientWeight,
            string SampleCollectionTime,
            string SampleColletionAge,
            string GestationCalculateMethod,
            string LMPDate,
            string GestationWeek,
            string GestationDays,
            string GestationWeekNT,
            string GestationDaysNT,
            string AgeRisk,
            string RiskValue_21,
            string RiskValue_18,
            string NTD_Risk,
            string FBHCGResult,
            string FBHCGCorrentionMOM,
            string FBHCGMOM,
            string AFPResult,
            string AFPCorrectionMOM,
            string AFPMOM,
            string SendTestHospital,
            string SendTestDoctor,
            string TestTime,
            string PAPP_Aresult,
            string PAPP_ACorrectionMOM,
            string PAPP_AMOM
        )
        {
            var mySampleinfoId="";
            var mySql = @"SELECT DISTINCT I.Status,I.Barcode,x.PatientAgeDisplay,x.AgeBirthDay,x.Weight,
                    x.LastMenstrual,x.SampleCollectionTime,S.InstrumentChannelCode,I.Id
                    FROM TT.Test_SampleInfo I
                    INNER JOIN TT.Test_BaseInfo x ON x.ID = I.BaseInfoID
                    LEFT JOIN TT.Test_SampleResult R on I.ID = R.SampleID
                    LEFT JOIN TE.BAS_ApplyItemSub S on s.SubItemCode = R.SubItemCode
                    WHERE I.Barcode = '" + BarCode+"'"+
                    " AND(r.ApplyCode = 'CZ00139' OR r.ApplyCode = 'CZ00152' OR r.ApplyCode = 'CZ00160')" +
                    " AND I.TestTime='"+ TestTime + "'";

            DataTable myTable1 = readMssqlTable(mySql, myConn);
            Application.DoEvents();

            // 判断是否有项目
            if (myTable1.Rows.Count > 0)  // 查询到记录，且不是质控
            {
                //if (SampleNo.Length > 2)
                //{
                //    if (SampleNo.Substring(0, 2) != "92" && SampleNo.Substring(0, 2) != "82")
                //    {
                        string myState, myAge, myAgeBirthDay, myWeight, myLastMenstrual, mySampleCollectionTime, myInstrumentChannelCode;
                        mySampleinfoId = myTable1.Rows[0][8].ToString();//sampleinfoId
                        myState = myTable1.Rows[0][0].ToString();      // BarCode
                        myAge = myTable1.Rows[0][2].ToString();
                        myInstrumentChannelCode = myTable1.Rows[0][7].ToString();

                        if (myStringIsNumber(myTable1.Rows[0][4].ToString()))  // myWeight is number
                        {
                            myWeight = myTable1.Rows[0][4].ToString();
                        }
                        else
                        {
                            myWeight = "";
                        }

                        if (myStringIsNumber(myTable1.Rows[0][3].ToString()))
                        {
                            myAgeBirthDay = DateTime.Parse(myTable1.Rows[0][3].ToString()).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            myAgeBirthDay = "/";
                        }

                        if (myTable1.Rows[0][5] == null)         // myLastMenstrual is null
                        {
                            if (myStringIsDate(myTable1.Rows[0][5].ToString().Substring(0, 10)))
                            {
                                myLastMenstrual = DateTime.Parse(myTable1.Rows[0][5].ToString()).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                myLastMenstrual = "/";
                            }
                        }
                        else
                        {
                            myLastMenstrual = "/";
                        }

                        mySampleCollectionTime = "/";
                        try
                        {
                            if (myStringIsDate(myTable1.Rows[0][6].ToString().Substring(0, 10)))
                            {
                                mySampleCollectionTime = DateTime.Parse(myTable1.Rows[0][6].ToString()).ToString("yyyy-MM-dd");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(BarCode + "  LIS数据库“样本采集时间”错误!\n\r" + ex.Message.ToString());
                        }


                        // 判断是否审核
                        if (int.Parse(myState) > 9)
                        {
                            Log("Info","2.Ok:" + BarCode + "   " + PatientName + "   已审核！" + "\r\n");
                        }
                        else
                        {
                            // 判断测定值是否相同
                            // 删除age小数位
                            myAge = Regex.Replace(myAge, @"\.\d+$", string.Empty);
                            string SampleColletionAge1 = Regex.Replace(SampleColletionAge, @"\.\d+$", string.Empty);

                            if (myAge == SampleColletionAge1 && myAgeBirthDay == BirthDay && myWeight == PatientWeight && myLastMenstrual == LMPDate && mySampleCollectionTime == SampleCollectionTime)
                            //if ( myAgeBirthDay == BirthDay && myWeight == PatientWeight && myLastMenstrual == LMPDate && mySampleCollectionTime == SampleCollectionTime)
                            {
                                Log("Info","2.Ok:" + BarCode + "   " + PatientName + "   基础信息相同不需要更新！" + "\r\n");
                            }
                            else
                            {
                                ////  更新数据库
                                //mySql = "Update test.SampleInfo set Age = '" + SampleColletionAge1 + "', AgeBirthDay = '" + BirthDay + "',Weight = '" + PatientWeight + "',LastMenstrual = '" + LMPDate + "',SampleCollectionTime = '" + SampleCollectionTime + "'";
                                //mySql += ",AgeMonth = '' ,AgeDayHour = ''";
                                //mySql += " From test.SampleInfo where Barcode = '" + BarCode + "' AND  ( ApplyItemCodes='CZ00139' OR ApplyItemCodes='CZ00152' OR ApplyItemCodes='CZ00160')";
                                mySql =
                                    @"Update a set PatientAge = '" + Convert.ToInt32(SampleColletionAge1) * 10000 + "', AgeBirthDay = '" + BirthDay + "', Weight = '" + PatientWeight + "', LastMenstrual = '" + LMPDate + "', SampleCollectionTime = '" + SampleCollectionTime + "'" +
                                        " FROM TT.Test_BaseInfo a" +
                                        " INNER JOIN TT.Test_SampleInfo b ON b.BaseInfoID = a.ID" +
                                        " INNER JOIN TT.Test_SampleResult c ON b.ID = c.SampleID" +
                                        " WHERE b.Barcode = '" + BarCode + "'" +
                                        " AND(c.ApplyCode = 'CZ00139'" +
                                        " OR c.ApplyCode = 'CZ00152'" +
                                        " OR c.ApplyCode = 'CZ00160')";
                                writeMssqlTable(mySql, myConn);
                                //-----------------
                                Log("Info","3.Update:" + mySql + "\r\n");

                                /// 插入日志
                                var remark = "更新：" + myAge + ":" + myAgeBirthDay + ":" + myWeight + ":" + myLastMenstrual + ":" + mySampleCollectionTime;
                                InsertTrack(BarCode, "DR6606", TestTime, SampleNo, "程序导入", remark, mySampleinfoId);
                                //mySql = "insert into TT.QCL_SampleTrack(TrackType,OperateObject,Barcode,OperateTime,OperateType,InstrumentCode,TestTime,SampleNo,Operator,Remarks)";
                                //mySql += " values('标本','标本信息','" + BarCode + "',getdate(),'仪器导入','" + "DR6606" + "','" + TestTime + "','" + SampleNo + "','程序导入','更新：" + myAge + ":" + myAgeBirthDay + ":" + myWeight + ":" + myLastMenstrual + ":" + mySampleCollectionTime + "')";
                                //writeMssqlTable(mySql, myConn);
                            }
                        }
                //    }
                //}

            // 插入结构化结果
            //if (SampleNo.Substring(0, 2) != "92" && SampleNo.Substring(0, 2) != "82")
            //{
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050813-3A20-4322-8F65-6DE48CBB86DE", "1.6", PatientName, 1);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050813-3B00-4F09-BF9E-23B440BA86F9", "1.6", SampleNo, 2);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050813-3B0C-42EC-8FA1-5330407CB818", "1.6", TestTime, 3);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050813-3B16-43DC-AF5E-99737EFDB92C", "1.6", SendTestDoctor, 4);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050813-3B20-4D68-A3E5-4458ED51EEF0", "1.6", BarCode, 5);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0018-42D7-A937-2BE51A297086", "1.6", SendTestHospital, 6);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0022-47BB-A072-E894C3DDEFA9", "1.6", BirthDay, 7);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0338-493B-8BC8-EBF46FCF84DD", "1.6", SampleCollectionTime, 8);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-040B-458B-AB81-87AF251F90D5", "1.6", SampleColletionAge, 9);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0416-4DB0-AF3C-D5475E4279D2", "1.6", GestationWeek, 10);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0421-4A09-A026-14B5FC7D260B", "1.6", GestationDays, 11);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-042D-4380-8706-E70446F00128", "1.6", GestationCalculateMethod, 12);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0436-4E35-AEDD-8E65323D46CB", "1.6", PatientWeight, 13);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0503-4EAA-9305-61DAE9C75861", "1.6", RiskValue_21, 14);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-050D-452C-956F-BE99E646BE71", "1.6", RiskValue_18, 15);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0516-4350-B5FA-16EB25F1898E", "1.6", AgeRisk, 16);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0521-46C8-A5EA-8F92B8EB0B66", "1.6", NTD_Risk, 17);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-052C-4F97-B8FF-5F137A7D97E6", "1.6", AFPResult, 18);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0537-47C1-8359-1287771915D2", "1.6", AFPCorrectionMOM, 19);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0607-402F-84DE-81AFF22292BD", "1.6", AFPMOM, 20);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0611-4F34-8401-56C3530A1E16", "1.6", FBHCGResult, 21);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-061D-454D-BACC-10CD5A18658B", "1.6", FBHCGCorrentionMOM, 22);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0626-4791-8A63-859134B88C57", "1.6", FBHCGMOM, 23);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0639-4838-94DF-6E8E05ACC152", "1.6", PAPP_Aresult, 24);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-062F-4CFD-AAD0-167A25F011C9", "1.6", PAPP_ACorrectionMOM, 25);
                InsertStructResult(myConn, mySampleinfoId, "E2050814-1216-4323-BC30-64CA05BFFCBF",
                    "E2050814-0706-4C36-B009-1CAF79BE1934", "1.6", PAPP_AMOM, 26);

                //-----------------
                Log("Info","3.Insert:" + mySql + "\r\n");

                /// 插入日志
                var remark1 = "插入结果 21三体风险值：" + RiskValue_21+ "~ 18三体风险值:"+ RiskValue_18+ "~AFPCorrectionMOM："+ AFPCorrectionMOM+ "~DR_PAPP-A MOM:"+ PAPP_AMOM+ "~DR_PAPP-A结果:"+ PAPP_Aresult;
                InsertTrack(BarCode, "DR6606", TestTime, SampleNo, "程序导入", remark1, mySampleinfoId);
                //mySql = "insert into TT.QCL_SampleTrack(TrackType,OperateObject,Barcode,OperateTime,OperateType,InstrumentCode,TestTime,SampleNo,Operator,Remarks)";
                //mySql += " values('标本','标本信息','" + BarCode + "',getdate(),'仪器导入','" + "DR6606" + "','" + TestTime + "','" + SampleNo + "','程序导入','" + AFPResult + ":" + FBHCGResult + ":" + RiskValue_21 + ":" + RiskValue_18 + ":" + NTD_Risk + ":" + PAPP_Aresult + ":" + PAPP_ACorrectionMOM + ":" + PAPP_AMOM + ":" + " TSSCExportResult')";
                //writeMssqlTable(mySql, myConn);
                //}

                // 质控数据处理 92001
                //if (SampleNo.Substring(0, 2) == "92" || SampleNo.Substring(0, 2) == "82")
                //{
                //    Application.DoEvents();

                ////zzh  指控先不管，待指控完成后插入 begin
                //insertQCRecord(myConn, TestTime, "DR6606", SampleNo, "HCG", FBHCGResult);
                //insertQCRecord(myConn, TestTime, "DR6606", SampleNo, "AFP", AFPResult);


                /// 插入日志
                //mySql = "insert into TT.QCL_SampleTrack(TrackType,OperateObject,Barcode,OperateTime,OperateType,InstrumentCode,TestTime,SampleNo,Operator,Remarks)";
                //mySql += " values('标本','标本信息','" + BarCode + "',getdate(),'仪器导入','" + "DR6606" + "','" + TestTime + "','" + SampleNo + "','程序导入','" + AFPResult + ":" + FBHCGResult + ": 质控数据')";
                //writeMssqlTable(mySql, myConn);
                ////zzh  指控先不管，待指控完成后插入 end
                //    Application.DoEvents();

                //}
                // 质控数据 82xxx 92xxx 
                sucessInsertCount++;

            }
        }

        public void InsertTrack(string barcode, string InstrumentCode, string testTime, string SampleNo, string Operator,
            string remark, string sampleId)
        {
            var sqlcon = new SqlConnection(myMsSqlConn1); //链接数据库
            var oledbComm = new SqlCommand();
            oledbComm.CommandTimeout = 600;
            var mysql = "";
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                mysql = @"INSERT INTO [TT].[QCL_SampleTrack]
                            ( TrackType ,
                              OperateObject ,
                              Barcode ,
                              OperateTime ,
                              OperateType ,
                              InstrumentCode ,
                              TestTime ,
                              SampleNo ,                              
                              Operator ,
                              Remarks ,
                              SampleID
                            )
                    VALUES  ( '标本' , 
                              '标本信息'," +
                            "'" + barcode + "'," +
                            "GETDATE()," +
                            "'仪器导入'," +
                            "'" + InstrumentCode + "'," +
                            "'" + testTime + "'," +
                            "'" + SampleNo + "'," +
                           "'" + Operator + "'," +
                            "'" + remark + "'," +
                            "'" + sampleId + "')";
                writeMssqlTable(mysql, sqlcon);

                sqlcon.Close();
            }
            catch (Exception ex)
            {
                Log("Err", "9插入跟踪记录失败：" + mysql);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 插入结构化结果
        /// </summary>
        /// <param name="myConn"></param>
        /// <param name="sampleInfoId"></param>
        /// <param name="mainElementId">元素ID</param>
        /// <param name="formUiId">UIid</param>
        /// <param name="activityId"></param>
        /// <param name="result">结果</param>
        /// <param name="sort"></param>
        public void InsertStructResult(SqlConnection myConn, string sampleInfoId, string formUiId, string mainElementId,
            string activityId, string result, int sort)
        {
            string sqlstr = "";
            string recordId = "";
            try
            {
                sqlstr = "SELECT TOP 1 ID FROM TT.WF_RuningRecord WHERE SampleInfoID = '" + sampleInfoId + "' AND customuiid = '" + formUiId + "' ORDER BY CreatedDate DESC";
                var mytable = readMssqlTable(sqlstr, myConn);
                if (mytable.Rows.Count > 0)
                {
                    recordId = mytable.Rows[0][0].ToString();
                }
                if (!string.IsNullOrWhiteSpace(recordId))
                {
                    if (IsExist(sampleInfoId, mainElementId,recordId))
                    {
                        sqlstr = @"UPDATE a SET Result='" + result + "' ,RecordId='" + recordId + "' FROM TT.Test_StructResult a " +
                                 " WHERE SampleinfoId= '" + sampleInfoId + "'" +
                                 " AND MainElementId ='" + mainElementId + "'" +
                                " AND RecordId='" + recordId + "'" +
                                " AND FormUIId='" + formUiId + "'";
                    }
                    else
                    {
                        sqlstr = @"INSERT INTO TT.Test_StructResult
                            ( Id ,
                              SampleinfoId ,
                              RecordId,
                              MainElementId ,
                              FormUIId ,
                              ActivityId ,
                              ActivityCacheId ,
                              Result ,
                              SubType ,
                              Sort ,
                              CreatedDate
                            )
                            VALUES  ( NEWID() , '" +
                                 sampleInfoId + "','" +
                                 recordId + "'," +
                                 "cast('" + mainElementId + "' as varchar(36))," +
                                 "cast('" + formUiId + "' as varchar(36)),'" +
                                 activityId + "'," +
                                 "'0','" +
                                 result + "'," +
                                 "'0'," +
                                 sort + "," +
                                 "GETDATE())";
                    }
                    writeMssqlTable(sqlstr, myConn);
                    Log("Info", "3.1 插入结构化信息:" + sqlstr + "\r\n");

                }
            }
            catch
            {
                Log("Err", "3.2 结构化信息插入失败："+sqlstr);

            }
        }
        public string GetRecordId(SqlConnection myConn, string sampleinfoId, string formUiId)
        {
            var sqlstr = "";
            var recordId = "";
            try
            {
                sqlstr = "SELECT TOP 1 ID FROM TT.WF_RuningRecord WHERE SampleInfoID = '" + sampleinfoId + "' AND customuiid = '" + formUiId + "' AND State = 1 ORDER BY CreatedDate DESC";
                var mytable = readMssqlTable(sqlstr, myConn);
                recordId = mytable.Rows[0][0].ToString();
            }
            catch
            {
                Log("Err","3.3查找节点记录Id:"+sqlstr);
            }
            return recordId;
        }

        /// <summary>
        /// 判断结构化数据是否存在
        /// </summary>
        /// <param name="sampelinfoId"></param>
        /// <param name="elementId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        private bool IsExist(string sampelinfoId,string elementId,string recordId)
        {
            var isExist = false;
            var sqlcon = new SqlConnection(myMsSqlConn1);  //链接数据库
            var oledbComm = new SqlCommand {CommandTimeout = 600};
            var mysql = "";
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                mysql = @"SELECT Result FROM TT.Test_StructResult a 
                            WHERE SampleinfoId= '"+sampelinfoId+"'"+
                            " AND MainElementId ='"+elementId+"'" +
                            " AND RecordID = '" + recordId + "'";
                var mytable = readMssqlTable(mysql, sqlcon);
                if (mytable.Rows.Count>0)
                {
                    isExist = true;
                }

                sqlcon.Close();
            }
            catch (Exception ex)
            {
                Log("Err", "3.5判断结构化数据是否存在失败！" + mysql);
                MessageBox.Show(ex.Message);
            }
            return isExist;
        }

        //插入质控数据
        private void InsertQcRecord(SqlConnection myConn, string cdrq, string yqdh, string ybbh, string xmdh, string xmcdz)
        {
            var myCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var mySql = "";
            var mySubItemCode = "";
            Int32 myID = 0;

            // 查询专业组
            //mySql = "select top 1 labGroupCode from common.TestItemSub where InstrumentCode = '" + yqdh + "'  and InstrumentChannelCode='" + xmdh + "'";
            mySql = @"SELECT TOP 1 b.GroupCode FROM TT.Test_SampleInfo a
    INNER JOIN TT.Test_SampleResult b ON a.ID = b.SampleID
    WHERE SampleNumber = '"+ybbh+"' AND a.TestTime = '"+cdrq+"' AND b.InstrumentChannelCode = '"+xmdh+"'";
            //if (myDebugFlag)
            //{
            //    commWriteLog("5.QC:" + mySql + "\r\n");
            //}

            var myQCtable = readMssqlTable(mySql, myConn);

            var labGroupCode = myQCtable.Rows[0][0].ToString();

            // 查询是已有相同条件主质控记录
            //mySql = "Select ID From Test.SampleInfoQuality Where convert(char(10),TestDate,120) = '" + cdrq + "'";
            //mySql += " and SampleNo ='" + ybbh + "' and InstrumentCode ='" + yqdh + "'";

            mySql = "SELECT ID FROM TT.Test_SampleInfoQuality WHERE TestDate='"+cdrq+"' " +
                    "AND SampleNo='"+ybbh+"' AND InstrumentCode='"+yqdh+"'";

            //mySql += " and SampleNo ='" + ybbh + "' and InstrumentCode ='" + yqdh + "' and State=1";

            //if (myDebugFlag)
            //{
            //    commWriteLog("5.QC:" + mySql + "\r\n");
            //}

            myQCtable.Clear();
            myQCtable = readMssqlTable(mySql, myConn);

            // 无相同条件的主记录
            if (myQCtable == null || myQCtable.Rows.Count < 1)
            {
                // 插入主质控记录
                //mySql = "Insert into Test.SampleInfoQuality(SampleNo,CreateDate,TestDate,InstrumentCode,GroupCode,State) ";
                //mySql += " Values('" + ybbh + "','" + myCreateTime + "','" + cdrq + "','" + yqdh + "','" + labGroupCode + "',0 )";

                mySql = @"INSERT INTO TT.Test_SampleInfoQuality
            (SampleNo,
              CreateDate,
              TestDate,
              Creator,
              InstrumentCode,
              GroupCode,
              State
            )
    VALUES('"+ybbh+"',"+
              " GETDATE(),"+
              "'"+cdrq+"',"+
              "'" + yqdh+"',"+
              "''," +
              "'"+labGroupCode+"',"+
              "0"+
            ")";
                if (myDebugFlag)
                {
                    Log("Info","5.QC:" + mySql + "\r\n");
                }

                writeMssqlTable(mySql, myConn);
                System.Threading.Thread.Sleep(50);

                // 取新记录ID
                mySql = "SELECT @@IDENTITY";

                //mySql += " and SampleNo ='" + ybbh + "' and InstrumentCode ='" + yqdh + "' and State=1";
                //if (myDebugFlag)
                //{
                //    commWriteLog("5.QC:" + mySql + "\r\n");
                //}

                // 查询并修改 state
                DataTable myQCtable1 = readMssqlTable(mySql, myConn);
                myID = Convert.ToInt32(myQCtable1.Rows[0][0]);      // ID
                myQCtable1.Clear();
            }
            // 有相同条件主记录，取ID
            else
            {
                myID = Convert.ToInt32(myQCtable.Rows[0][0]);       // ID
            }

            // 取项目代号
            mySql = "SELECT TOP 1 SubItemCode FROM TE.BAS_ApplyItemSub WHERE InstrumentCode='"+yqdh+"' AND InstrumentChannelCode='"+xmdh+"'";

            //if (myDebugFlag)
            //{
            //    commWriteLog("5.QC:" + mySql + "\r\n");
            //}
            myQCtable = readMssqlTable(mySql, myConn);

            if (myQCtable != null && myQCtable.Rows.Count > 0)
            {
                mySubItemCode = myQCtable.Rows[0][0].ToString();      // SubItemCode
            }
            else
            {
                mySubItemCode = "Error";
            }


            // 查询是已有相同条件从质控表记录
            mySql =
                "SELECT ID FROM TT.Test_SampleResultQuality WHERE ResultTime='"+cdrq+"' " +
                "AND SampleNo='"+ybbh+"' AND InstrumentCode='"+yqdh+"' " +
                "AND InstrumentChannelCode='"+xmdh+"'";
            //mySql = "Select ID From Test.SampleResultQuality Where convert(char(10),ResultTime,120) = '" + cdrq + "'";
            //mySql += " and SampleNo ='" + ybbh + "' and InstrumentCode ='" + yqdh + "' and InstrumentChannelCode = '" + xmdh + "'";

            //if (myDebugFlag)
            //{
            //    commWriteLog("5.QC:" + mySql + "\r\n");
            //}

            myQCtable.Clear();
            myQCtable = readMssqlTable(mySql, myConn);

            // 无相同条件的主记录
            if (myQCtable == null || myQCtable.Rows.Count < 1)
            {
                // 插入从质控表
                mySql = @"INSERT INTO TT.Test_SampleResultQuality
                    (SampleQCID,
                      SampleNo,
                      SubItemCode,
                      SubItemName,
                      TestResult,
                      HLFlag,
                      ReferenceValue,
                      Unit,
                      UnitName,
                      ResultTime,
                      GroupCode,
                      InstrumentChannelCode,
                      InstrumentCode,
                      CreateDate,
                      PatchNumber
                    )
            VALUES('"+ myID + "',"+
                      "'"+ybbh+"'," +
                      " '"+mySubItemCode+"'," +
                      " '"+ mySubItemCode + "'," +
                      "'" + xmcdz + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "'" + mySubItemCode + "'," +
                      "GETDATE(),"+
                      "'" + mySubItemCode + "'" +
                    ")";
                //mySql = "Insert into Test.SampleResultQuality(SampleQCID,SampleNo,CreateDate,ResultTime,SubItemCode,InstrumentCode,InstrumentChannelCode,TestResult,GroupCode) ";
                //mySql += " Values(" + myID + ",'" + ybbh + "','" + myCreateTime + "','" + cdrq + "','" + mySubItemCode + "','" + yqdh + "','" + xmdh + "','" + xmcdz + "','" + labGroupCode + "' )";

                if (myDebugFlag)
                {
                    Log("Info","5.QC:" + mySql + "\r\n");
                }
                writeMssqlTable(mySql, myConn);
                System.Threading.Thread.Sleep(50);

                //if (myDebugFlag)
                //{
                //    commWriteLog("QC: " + yqdh + "   " + cdrq + "   " + ybbh + "   " + xmdh + "   " + xmcdz + "   记录！" + "\r\n");
                //}
            }
            // 有相同条件的更新
            {
                mySql = "Update Test.SampleResultQuality Set TestResult = '" + xmcdz + "',CreateDate='" + myCreateTime + "',ResultTime='" + cdrq + "' Where ";
                mySql += "convert(char(10),ResultTime,120) = '" + cdrq + "' and SampleNo ='" + ybbh + "' and InstrumentCode ='" + yqdh + "' and InstrumentChannelCode = '" + xmdh + "'";
                
                if (myDebugFlag)
                {
                    Log("Info","5.QC:" + mySql + "\r\n");
                }
                writeMssqlTable(mySql, myConn);
                System.Threading.Thread.Sleep(50);
            }
        }

        // 读取数据
        public DataTable readMssqlTable(string myStrSQL, SqlConnection myConn)
        {
            SqlCommand myCmd1 = new SqlCommand();
            myCmd1.Connection = myConn;
            myCmd1.CommandText = myStrSQL;
            myCmd1.CommandType = CommandType.Text;

            DataSet myDataSet1 = new DataSet();
            myDataSet1.Clear();

            SqlDataAdapter myAdapter1 = new SqlDataAdapter(myCmd1);
            myAdapter1.Fill(myDataSet1);
            DataTable myTable1 = myDataSet1.Tables[0];

            return myTable1;
        }

        public void writeMssqlTable(string myStrSQL, SqlConnection myConn)
        {
            SqlCommand myCmd1 = new SqlCommand
            {
                Connection = myConn,
                CommandText = myStrSQL
            };
            myCmd1.ExecuteNonQuery();
        }

        public void Log(string path,string myStrLog)
        {
            var mySysDate = DateTime.Now.ToString("yyyy-MM-dd");
            var pathTxt = Application.StartupPath + @"\Logs\"+ path;
            var fileName = pathTxt + @"\" + mySysDate + ".log";
            if (!Directory.Exists(pathTxt))
                Directory.CreateDirectory(pathTxt);
            if (!File.Exists(fileName))
            {
                var fs1 = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                var sw = new StreamWriter(fs1);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +" "+ myStrLog);
                sw.Close();
                fs1.Close();
            }
            else
            {
                StreamWriter sr = File.AppendText(fileName);
                sr.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + myStrLog);
                sr.Close();
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImputDaan_Click(object sender, EventArgs e)
        {
            lblMessageLis.Text = "LIS导入唐氏数据...... ";

            CalucateBatchNumber(tboxBeginSerialNumber.Text.Trim());
            inputLisToTang(_selectYbbhList);
        }

        private void btnClose_1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQueryLis_Click(object sender, EventArgs e)
        {
            DataTable myTable;
            CalucateBatchNumber(tboxBeginSerialNumber.Text.Trim());
            myTable = myMsSqlQuery(this.dtpInputData.Text.TrimEnd(),"","INPUT", _selectYbbhList);

            this.dgvLIS.DataSource = myTable;
            lblMessageLis.Text = this.dtpInputData.Text + " LIS可导入唐氏数据：" + " " + myTable.Rows.Count + " 条";

        }

        private void btnQueryLisLog_Click(object sender, EventArgs e)
        {
            DataTable myTable;
            CalucateBatchNumber(tboxBeginSerialNumber.Text.Trim());
            myTable = myMsSqlQuery(this.dtpQueryLis.Text.TrimEnd(), this.tbxYBID.Text.TrimEnd(), "", _selectYbbhList);

            this.dgvQueryData.DataSource = myTable;

        }

        private void dtpInputData_ValueChanged(object sender, EventArgs e)
        {

        }


        private bool myStringIsNumber(string myString)
        {
            myString = myString.Trim();
            string myPatttern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            return Regex.IsMatch(myString, myPatttern);
        }

        private bool myStringIsDate(string myString)
        {
            myString = myString.Trim();
            string myPatttern = @"^\d{4}-\d{2}-\d{2}$";

            return Regex.IsMatch(myString, myPatttern);
        }
        private bool myStringIsDateTime1(string myString)
        {
            myString = myString.Trim();
            string myPatttern = @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ";

            return Regex.IsMatch(myString, myPatttern);
        }

         
         /// <summary>
         /// 处理上传基本信息到仪器的样本号
         /// </summary>
         /// <param name="sampleNos"></param>
        private void CalucateBatchNumber(string sampleNos)
        {
            _selectYbbhList = new List<int>();
            var list = sampleNos.Split(',');
            foreach (var s in list)
            {
                int i;
                if (int.TryParse(s, out i))
                {
                    if (!_selectYbbhList.Contains(i))
                        _selectYbbhList.Add(i);
                }
                else if (s.Contains("-"))
                {
                    int min, max;
                    var rang = s.Split('-');
                    if (rang.Length != 2 || !int.TryParse(rang[0], out min) || !int.TryParse(rang[1], out max))
                        continue;
                    for (var j = min; j <= max; j++)
                    {
                        if (!_selectYbbhList.Contains(j))
                            _selectYbbhList.Add(j);
                    }
                }
            }
        }

        private void lblBeginSerialNumber_Click(object sender, EventArgs e)
        {

        }

        private static string fileName = GetFileName();
        public static string ReadValueByKey(string key)
        {
            string result = string.Empty;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                result = xmlElement.GetAttribute("value");
            }
            return result;
        }
        private static string GetFileName()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }
    }
}
