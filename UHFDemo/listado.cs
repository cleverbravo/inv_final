using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;



namespace UHFDemo
{
    public partial class listado : Form
    {
        
        private Reader.ReaderMethod reader;
        private ReaderSetting m_curSetting = new ReaderSetting();
        private InventoryBuffer m_curInventoryBuffer = new InventoryBuffer();
        private OperateTagBuffer m_curOperateTagBuffer = new OperateTagBuffer();
        private OperateTagISO18000Buffer m_curOperateTagISO18000Buffer = new OperateTagISO18000Buffer();

        
        // Use List para almacenar información de etiquetas en tiempo real
        private List<RealTimeTagData> RealTimeTagDataList = new List<RealTimeTagData>();
        // Si se muestran datos de monitoreo en serie
        private bool m_bDisplayLog = false;
        // Inventario en tiempo real
        private int  m_nTotal = 0;
        // Grabar parámetros rápidos de antena de sondeo
        private Byte  [] m_btAryData = new byte [10];
        // Registre el número total de encuestas rápidas
        cn_usuario objProd = new cn_usuario();
        cn_usuario objetoCN = new cn_usuario();
        private string idP = null;
        private bool Editar = false;
        private string id_salida = null;
        public listado()
        {
            InitializeComponent();
        }
        private void MostrarSalidas()
        {

            cn_usuario objeto = new cn_usuario();
            dataGridView1.DataSource = objeto.ListarSalidas();
        }
        private void listado_Load(object sender, EventArgs e)
        {
           // Inicializa el acceso a la instancia del lector
            reader = new Reader.ReaderMethod();
            
            // función de devolución de llamada
            reader.AnalyCallback = AnalyData;
            reader.ReceiveCallback = ReceiveData;
            reader.SendCallback = SendData;

           
            // Establecer la validez del elemento de interfaz


            SetFormEnable(false);
           

            
            // Inicializa la configuración predeterminada del lector de conexión
            cmbComPort.SelectedIndex = 0;
            cmbBaudrate.SelectedIndex = 1;
            



        }

        private void ListarProductos(DataGridView data)
        {

            SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
            conectar.Open();
            SqlCommand comando = new SqlCommand("select id_RFID AS id, nombre_p,categoria.nombre_categ,proveedor.nom_proveedor,fecha_registro,descripcion,foto from producto INNER JOIN categoria ON producto.id_categoria = categoria.id_categoria INNER JOIN proveedor ON producto.id_proveedor = proveedor.id_proveedor", conectar);
            comando.Connection = conectar;
            comando.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            data.DataSource = dt;
            data.Columns[0].Width = 165;
            data.Columns[1].Width = 160;
            data.Columns[2].Width = 100;
            data.Columns[3].Width = 165;
            data.Columns[4].Width = 85;
            data.Columns[4].Width = 100;
            data.Columns[5].Width = 110;


            conectar.Close();

            // cn_usuario objprod = new cn_usuario();
            //dataGridView1.DataSource = objprod.ListarProducto();



            //            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            //dataGridView1.Columns.Add(col);
        }
        private void ReceiveData(byte[] btAryReceiveData)
        {
            if (m_bDisplayLog)
            {
                string strLog = CCommondMethod.ByteArrayToString(btAryReceiveData, 0, btAryReceiveData.Length);

               
            }            
        }

        private void SendData(byte[] btArySendData)
        {
            if (m_bDisplayLog)
            {
                string strLog = CCommondMethod.ByteArrayToString(btArySendData, 0, btArySendData.Length);

               
            }            
        }

        private void AnalyData(Reader.MessageTran msgTran)
        {
            if (msgTran.PacketType != 0xA0)
            {
                return;
            }
            switch(msgTran.Cmd)
            {
                case 0x81:
                    ProcessReadTag(msgTran);
                    break;
                case 0x89:
                    ProcessInventoryReal(msgTran);
                    break;
                default:
                    break;
            }
        }

        

        private void SetFormEnable(bool bIsEnable)
        {
                      

           
          
        }

        private void btnConnectRs232_Click(object sender, EventArgs e)
        {
            
         // Procesando lector de conexión de puerto serie
            string strException = string.Empty;
            string strComPort = cmbComPort.Text;
            int nBaudrate=Convert.ToInt32(cmbBaudrate.Text);

            int nRet = reader.OpenCom(strComPort, nBaudrate, out strException);
            if (nRet != 0)
            {
                string strLog = "No se pudo conectar con el lector, el motivo del error: " + strException;

              
                return;
            }
            else
            {
                string strLog = "Lector de conexión " + strComPort + "@" + nBaudrate.ToString();
                MessageBox.Show("lector conectado");
              
               
            }
            
            //处理界面元素是否有效
            SetFormEnable(true);

            
            btnConnectRs232.Enabled = false;
            btnDisconnectRs232.Enabled = true;

            //设置按钮字体颜色
            btnConnectRs232.ForeColor = Color.Black;
            btnDisconnectRs232.ForeColor = Color.Indigo;
           
        }

        private void btnDisconnectRs232_Click(object sender, EventArgs e)
        {
            MessageBox.Show("lector conectado");
            //处理串口断开连接读写器
            reader.CloseCom();

            //处理界面元素是否有效
            SetFormEnable(false);
            btnConnectRs232.Enabled = true;
            btnDisconnectRs232.Enabled = false;

            //设置按钮字体颜色
            btnConnectRs232.ForeColor = Color.Indigo;
            btnDisconnectRs232.ForeColor = Color.Black;
           
        }

      

        private delegate void WriteLogUnSafe(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType);
        private void WriteLog(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType)
        {
            if (this.InvokeRequired)
            {
                WriteLogUnSafe InvokeWriteLog = new WriteLogUnSafe(WriteLog);
                this.Invoke(InvokeWriteLog, new object[] { logRichTxt, strLog, nType });
            }
            else
            {
                if (nType == 0)
                {
                    logRichTxt.AppendTextEx(strLog, Color.Indigo);
                }
                else
                {
                    logRichTxt.AppendTextEx(strLog, Color.Red);
                }



                logRichTxt.Select(logRichTxt.TextLength, 0);
                logRichTxt.ScrollToCaret();
            }
        }

        

 
  

        private void ProcessReadTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Leer etiqueta";
            string strErrorCode = string.Empty;
            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Fracaso, razón del fracaso: " + strErrorCode;
                m_curSetting.btRealInventoryFlag = 100; // reader devuelve un mensaje de error

                
            }
            else
            {
                RealTimeTagData tagData = new RealTimeTagData();
                int nLen = msgTran.AryData.Length;
                int nDataLen = Convert.ToInt32(msgTran.AryData[nLen - 3]);
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - nDataLen - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 7 + nEpcLen, nDataLen);

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);


                tagData.strEpc = strEPC;
                tagData.strPc = strPC;
                tagData.strTid = strData;
                tagData.btAntId = byAntId;

                RealTimeTagDataList.Add(tagData);

                int nReaddataCount = msgTran.AryData[0] * 255 + msgTran.AryData[1]; // El número total de datos
                if (RealTimeTagDataList.Count == nReaddataCount)// Recibe todos los datos
                {
                    m_curSetting.btRealInventoryFlag = 1; // reader devuelve un mensaje de error
                }
            }
        }
        private void ProcessInventoryReal(Reader.MessageTran msgTran)
        {
            string strCmd = "";
            strCmd = "Inventario en tiempo real";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1) // Recibir paquete de mensaje de error
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd +"Fracaso, razón del fracaso:" + strErrorCode;
                m_curSetting.btRealInventoryFlag = 100; //读写器返回盘存错误
           

              
            }
            else if (msgTran.AryData.Length == 7) //收到命令结束数据包
            {
                m_curInventoryBuffer.nReadRate = Convert.ToInt32(msgTran.AryData[1]) * 256 + Convert.ToInt32(msgTran.AryData[2]);
                m_curInventoryBuffer.nDataCount = Convert.ToInt32(msgTran.AryData[3]) * 256 * 256 * 256 + Convert.ToInt32(msgTran.AryData[4]) * 256 * 256 + Convert.ToInt32(msgTran.AryData[5]) * 256 + Convert.ToInt32(msgTran.AryData[6]);
                m_curSetting.btRealInventoryFlag = 1; //成功收到盘存命令结束数据包
               
            }
            else //收到实时标签数据信息
            {
                m_nTotal++;
                int nLength = msgTran.AryData.Length;
                int nEpcLength = nLength - 4;
                RealTimeTagData tagData = new RealTimeTagData();

                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, nEpcLength);
                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 2);
                string strRSSI = (msgTran.AryData[nLength - 1] - 129).ToString() + " dBm";
                byte btTemp = msgTran.AryData[0];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                byte btFreq = (byte)(btTemp >> 2);
               
            

                tagData.strEpc = strEPC;
                tagData.strPc = strPC;
                tagData.strRssi = strRSSI;
             
                tagData.btAntId = btAntId;
                
                RealTimeTagDataList.Add(tagData);
        
           }
            
        }

        


      
        //Función de etiqueta de inventario en tiempo real
        // ----------- Parámetros de entrada --------------
        // btReaderId: dirección del lector, 0xff es la dirección pública
        // btRepeat: repite el número de inventario por comando, 0xff es el modo rápido.
        // btTimeOut: control de tiempo de espera, en segundos, si el lector no responde o el comando no se ejecuta dentro de este tiempo, se devuelve el tiempo de espera.
        // ---------------------------------
        // ----------- Parámetros de salida --------------
        // 0: inventario exitoso pero no inventario para etiquetar
        // 1: inventario exitoso e inventario a la etiqueta
        // -1: se produjo un error durante el proceso de inventario
        // -2: Tiempo de espera de inventario
        // ---------------------------------
        // Nota: No actualice la interfaz en esta función y la función que llama, porque el hilo de la interfaz está esperando que esta función regrese.
        private int realTimeInventory(byte btReaderId, byte btRepeat, byte btTimeOut)
        {
            DateTime startTime;
            TimeSpan timeOutControl;

            //这里使用等待数据的方法，数据全部接收完毕后再进行处理
            m_curSetting.btRealInventoryFlag = 0;
            RealTimeTagDataList.Clear();  //清空标签信息表
            reader.InventoryReal(255, 1); // 先发送实时盘存命令，用0xFF公共地址，每条命令重复盘存一次

            startTime = DateTime.Now;

            while (m_curSetting.btRealInventoryFlag == 0) //等待读写器返回数据完成，若超时，返回超时标志
            {
                timeOutControl = DateTime.Now - startTime;
                if (timeOutControl.TotalMilliseconds > btTimeOut * 1000)//超时返回 
                {
                    return -2;
                }
            }

            if (m_curSetting.btRealInventoryFlag == 1) //命令执行成功
            {
                if (RealTimeTagDataList.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            if (m_curSetting.btRealInventoryFlag == 100) //命令执行失败
            {
                return -1;
            }
            return 0;
        }

   
        

        
        


       
       

        private void cmbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void button2_Click_2(object sender, EventArgs e)
        {

            int nReturnValue = 0;
           
            

            nReturnValue = realTimeInventory(255, 255, 5); // Dirección de lector común, modo de inventario rápido, control de tiempo de espera 5 segundos

            if (nReturnValue == 1)
            {
                for (int i = 0; i < RealTimeTagDataList.Count; i++)
                {
                   

                }
            }
            else if (nReturnValue == 0)
            {
                MessageBox.Show("Ejecución exitosa del comando pero sin inventario en la etiqueta");
            }
            else if (nReturnValue == -1)
            {
                MessageBox.Show("Error de inventario");
            }
            else if (nReturnValue == -2)
            {
                MessageBox.Show("Tiempo de espera de inventario");
            }
            else
            {
                return;
            }
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private static listado instance = null;

        public static listado InstanceL()
        {

            if (instance == null)
            {
                instance = new listado();
            }
            return instance;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            // Procesando lector de conexión de puerto serie
            string strException = string.Empty;
            string strComPort = cmbComPort1.Text;
            int nBaudrate = Convert.ToInt32(cmbBaudrate1.Text);

            int nRet = reader.OpenCom(strComPort, nBaudrate, out strException);
            if (nRet != 0)
            {
                string strLog = "No se pudo conectar con el lector, el motivo del error: " + strException;


                return;
            }
            else
            {
              
                MessageBox.Show("LECTOR CONECTADO");


            }

            //处理界面元素是否有效
            SetFormEnable(true);


            button2.Enabled = false;
            button1.Enabled = true;

            //设置按钮字体颜色
            button2.ForeColor = Color.Black;
            button1.ForeColor = Color.Indigo;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LECTOR DESCONECTADO");
            //处理串口断开连接读写器
            reader.CloseCom();

            //处理界面元素是否有效
            SetFormEnable(false);
            button2.Enabled = true;
            button1.Enabled = false;

            //设置按钮字体颜色
            button2.ForeColor = Color.Indigo;
            button1.ForeColor = Color.Black;



        }

        private void button3_Click(object sender, EventArgs e)
        {


            int nReturnValue = 0;



            nReturnValue = realTimeInventory(255, 255, 5); // Dirección de lector común, modo de inventario rápido, control de tiempo de espera 5 segundos

            if (nReturnValue == 1)
            {
                for (int i = 0; i < RealTimeTagDataList.Count; i++)
                {
                   


                }
            }
            else if (nReturnValue == 0)
            {
                MessageBox.Show("Ejecución exitosa del comando pero sin inventario en la etiqueta");
            }
            else if (nReturnValue == -1)
            {
                MessageBox.Show("Error de inventario");
            }
            else if (nReturnValue == -2)
            {
                MessageBox.Show("Tiempo de espera de inventario");
            }
            else
            {
                return;
            }
        }
        DateTime hoy = DateTime.Now;
      

        

        private void CmbComPort1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbBaudrate1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo insertar los datos por: " + ex);
                }
            }
            //EDITAR
            if (Editar == true)
            {

                try
                {
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo editar los datos por: " + ex);
                }
            }
           
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
               
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idP = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                objetoCN.EliminarSalidaP(idP);
                MessageBox.Show("Eliminado correctamente");
                //ListarSalidas(dataGridView1);
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Buscador_TextChanged(object sender, EventArgs e)
        {
            var aux = new metodo();
           // aux.filtrarSP(dataGridView1, this.buscador.Text.Trim());
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            int nReturnValue = 0;
            string tagInfo = "";
           


            nReturnValue = realTimeInventory(255, 255, 5);  //公用读写器地址，快速盘存模式，超时控制5秒
            
            if (nReturnValue == 1)
            {
                for (int i = 0; i < RealTimeTagDataList.Count; i++)
                {
                    tagInfo = RealTimeTagDataList[i].strEpc;
                    dataGridView3.Rows.Add(tagInfo);

                }

            }/**
            else {
                Boolean existe = false;
                tagInfo = RealTimeTagDataList[0].strEpc;
                for (int j = 0; j < RealTimeTagDataList.Count; j++)
                {
                    if (tagInfo.Equals(dataGridView3.Rows[j].Cells[1].Value.ToString()))
                    {
                        existe = true;
                        break;
                    }


                }
                if (existe)
                {
                    MessageBox.Show("el id ya existe");
                }
                else {

                }

            }**/
            


        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
          
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            
            try
            {
            
                SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                conectar.Open();
                id_salida = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                string consultar=("select *from salida_producto where id_RFID='" + id_salida +"'");

                if (consultar == id_salida)
                {
                    MessageBox.Show("el producto ya salio");
                }
                else
                {
                    for (int i = 0; i < dataGridView3.RowCount - 1; i++) { 
                    id_salida = dataGridView3.Rows[i].Cells[0].Value.ToString();
                   // objetoCN.InsertarSalidas(id_salida);
                    //ListarSalidas(dataGridView1);
                  //  objetoCN.EliminarP(id_salida);
                    }
                    MessageBox.Show("se inserto correctamente");
                    MessageBox.Show("Eliminado correctamente");
                    
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("no se pudo insertar los datos por: " + ex);
            }
            

            
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
