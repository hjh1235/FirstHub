using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UpperComputer
{

    public partial class MesLoadParForm : Form
    {
        public static string strPath = AppDomain.CurrentDomain.BaseDirectory + "配方";
        string dgvModeName = "";
        int dgvModeindex = 0;
        public static event Action<string> _CallBack;
        /// <summary>
        /// 操作类
        /// </summary>
        public static MesLoadClass m_doc = new MesLoadClass();
        public MesLoadParForm()
        {
            InitializeComponent();
        }

        private void MesLoadParForm_Load(object sender, EventArgs e)
        {
            GetCsvName();
        }
        public static void CallBack(string inputString)
        {
            _CallBack?.Invoke(inputString);
        }
        private void btn_CreatMode_Click(object sender, EventArgs e)
        {
            if (txt_NewModeName.Text == "")
            {
                MessageBox.Show("新建配方名称不能为空");
                return;
            }
            if (MessageBox.Show("是否以当前选中配方为副本新建?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            string path = strPath + "\\" + txt_NewModeName.Text + ".xml";
            if (File.Exists(path))
            {
                MessageBox.Show("新建配方名称已存在");
                return;
            }
            if (!m_doc.SaveDoc(path))
            {
                MessageBox.Show("新建失败");
            }
            else
            {
                MessageBox.Show("新建成功");
            }
            GetCsvName();
        }

        private void cmb_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            foreach (var item in m_doc.m_dataList)
            {
                if (item.paramName == txt_paramName.Text)
                {
                    MessageBox.Show("已存在添加的名称");
                    return;
                }
            }
            m_doc.m_dataList.Add(new MesLoadData { paramName = txt_paramName.Text, paramCode = txt_paramCode.Text, paramUp = txt_paramUp.Text, paramLower = txt_paramLower.Text, paramUnit = txt_paramUnit.Text, bControl = (cmb_bControl.Text == "是" ? true : false), strDataType = cmb_DataType.Text, FixedValue = txt_FixedValue.Text, strBindCCDDataPos = cmb_CCDPos.Text, bEnable = cmb_Enable.Text == "是" ? true : false });
            ShowView();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var item in m_doc.m_dataList)
            {
                if (i == dgvModeindex)
                {
                    item.paramName = txt_paramName.Text;
                    item.paramCode = txt_paramCode.Text;
                    item.paramUp = txt_paramUp.Text;
                    item.paramLower = txt_paramLower.Text;
                    item.paramUnit = txt_paramUnit.Text;
                    item.bControl = (cmb_bControl.Text == "是" ? true : false);
                    item.strDataType = cmb_DataType.Text;
                    item.FixedValue = txt_FixedValue.Text;
                    item.strBindCCDDataPos = cmb_CCDPos.Text;
                    item.bEnable = (cmb_Enable.Text == "是" ? true : false);
                }
                i++;
            }
            ShowView();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btn_Delete")
            {
                m_doc.m_dataList.RemoveAt(dgvModeindex);
                ShowView();
            }
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Name == "btn_MoveUp")
                {
                    if (dgvModeindex == 0)
                        return;
                    var list = m_doc.m_dataList[dgvModeindex];
                    m_doc.m_dataList.RemoveAt(dgvModeindex);
                    m_doc.m_dataList.Insert(dgvModeindex - 1, list);
                    dgvModeindex -= 1;
                }
                ShowView();
            }
            catch (Exception ex)
            {


            }

        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Name == "btn_MoveDown")
                {
                    if (dgvModeindex == dgv_Mode.Rows.Count - 1)
                        return;
                    var list = m_doc.m_dataList[dgvModeindex];
                    m_doc.m_dataList.RemoveAt(dgvModeindex);
                    m_doc.m_dataList.Insert(dgvModeindex + 1, list);
                    dgvModeindex += 1;

                }

                ShowView();
            }
            catch (Exception ex)
            {


            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string path = strPath + "\\" + cmb_Mode.Text + ".xml";
            if (!m_doc.SaveDoc(path))
            {
                MessageBox.Show("保存失败");
            }
            else
            {
                //CallBack("");
                MessageBox.Show("保存成功");
            }
        }
        public void LoadData()
        {
            string path = strPath + "\\" + cmb_Mode.Text + ".xml";
            m_doc = MesLoadClass.LoadObj(path);
            ShowView();
        }
        public void ShowView()
        {
            dgv_Mode.Rows.Clear();

           
            foreach (var item in m_doc.m_dataList)
            {
               
                dgv_Mode.Rows.Add(new object[] { item.paramName, item.paramCode, item.paramUp, item.paramLower, item.paramUnit, item.bControl == true ? "是" : "否", item.strDataType, item.FixedValue, item.strBindCCDDataPos});

                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgv_Mode.Rows[dgv_Mode.Rows.Count-1].Cells["ck_Enable"];
                checkCell.Value = item.bEnable;
            }
            if (dgv_Mode.Rows.Count - 1 >= dgvModeindex)
                dgv_Mode.CurrentCell = dgv_Mode.Rows[dgvModeindex].Cells[0];

        }

        private void dgv_Mode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvModeindex = dgv_Mode.CurrentCell.RowIndex;
                int i = 0;
                dgvModeName = dgv_Mode.Rows[dgvModeindex].Cells[0].Value.ToString();
                txt_paramName.Text = dgv_Mode.Rows[dgvModeindex].Cells[0].Value.ToString();
                txt_paramCode.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                txt_paramUp.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                txt_paramLower.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                txt_paramUnit.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                cmb_bControl.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                cmb_DataType.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                txt_FixedValue.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                cmb_CCDPos.Text = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString();
                string ss = dgv_Mode.Rows[dgvModeindex].Cells[++i].Value.ToString().ToLower();
                cmb_Enable.Text = (ss=="true"?"是":"否");
            }
            catch (Exception ex)
            {


            }
        }


        public void GetCsvName()
        {
            cmb_Mode.Items.Clear();
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            var filesLeft = Directory.GetFiles(strPath, "*.xml");
            foreach (var file in filesLeft)
            {
                string fileNameExt = file.Substring(file.LastIndexOf("\\") + 1); //获取文件名，不带路径
                string filePath = file.Substring(0, file.LastIndexOf("\\"));//获取文件路径，不带文件名   
                string name = fileNameExt.Substring(0, fileNameExt.LastIndexOf(".")); //获取文件名，不带后缀
                cmb_Mode.Items.Add(name);
            }
            if (cmb_Mode.Items.Count > 0)
                cmb_Mode.SelectedIndex = 0;
        }

        private void btn_DeleteMode_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除该MES配方?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            string path = strPath + "\\" + cmb_Mode.Text + ".xml";
            if (File.Exists(path))
                File.Delete(path);
            GetCsvName();
        }
        private void cmb_bFixedValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_DataType.Text == "默认值")
            {
                cmb_CCDPos.Visible = false;
                lb_Pos.Visible = false;
            }
            else if (cmb_DataType.Text == "视觉采集")
            {
                cmb_CCDPos.Visible = true;
                lb_Pos.Visible = true;
            }
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in m_doc.m_dataList)
                {
                    if (item.paramName == txt_paramName.Text)
                    {
                        MessageBox.Show("已存在添加的名称");
                        return;
                    }
                }
                m_doc.m_dataList.Insert(dgvModeindex, new MesLoadData { paramName = txt_paramName.Text, paramCode = txt_paramCode.Text, paramUp = txt_paramUp.Text, paramLower = txt_paramLower.Text, paramUnit = txt_paramUnit.Text, bControl = (cmb_bControl.Text == "是" ? true : false), strDataType = cmb_DataType.Text, FixedValue = txt_FixedValue.Text, strBindCCDDataPos = cmb_CCDPos.Text, bEnable = cmb_Enable.Text == "是" ? true : false });
                dgvModeindex++;
                ShowView();
            }
            catch (Exception)
            {

               
            }
            
        }
    }
    [XmlInclude(typeof(MesLoadData))]
    public class MesLoadClass
    {
        public List<MesLoadData> m_dataList = new List<MesLoadData>();
        [XmlIgnore]
        public Dictionary<string, MesLoadData> m_dataDictionary = new Dictionary<string, MesLoadData>();
        public static MesLoadClass LoadObj(string name)
        {
            MesLoadClass pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MesLoadClass));
            FileStream fsReader = null;
            try
            {
                //fsReader = File.OpenRead(@".//胶形配方/" + name + ".xml");
                fsReader = File.OpenRead(name);
                pDoc = (MesLoadClass)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                //pDoc.m_dataDictionary = pDoc.m_dataList.ToDictionary(p => p.Name.Replace("*",""));

                //foreach (ProjectDataS item in pDoc.m_dataList)
                //{
                //    item.m_dataDictionary = item.m_dataList.ToDictionary(p => p.strName);
                //}
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new MesLoadClass();
            }
            return pDoc;
        }
        public bool SaveDoc(string name)
        {
            try
            {
                if (!Directory.Exists(@".//配方/"))
                {
                    Directory.CreateDirectory(@".//配方/");
                }

                //FileStream fsWriter = new FileStream(@".//胶形配方/" + name + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                FileStream fsWriter = new FileStream(name, FileMode.Create, FileAccess.Write, FileShare.Read);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(MesLoadClass));
                xmlSerializer.Serialize(fsWriter, this);
                fsWriter.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
    public class MesLoadData
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string paramName { get; set; }
        /// <summary>
        /// 参数代码
        /// </summary>
        public string paramCode { get; set; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string paramUnit { get; set; }
        /// <summary>
        /// 参数上限
        /// </summary>
        public string paramUp { get; set; }
        /// <summary>
        /// 参数下限
        /// </summary>
        public string paramLower { get; set; }
        /// <summary>
        /// 是否管控
        /// </summary>
        public bool bControl { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string strDataType { get; set; }
        /// <summary>
        /// 绑定视觉数据位置
        /// </summary>
        public string strBindCCDDataPos { get; set; }
        /// <summary>
        ///数据
        /// </summary>
        public string FixedValue { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool bEnable { get; set; }
    }
}
