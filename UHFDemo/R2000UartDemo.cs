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
    public partial class R2000UartDemo : Form
    {
        private Form1 cambio = new Form1();
        private Reader.ReaderMethod reader;
        private ReaderSetting m_curSetting = new ReaderSetting();
        private InventoryBuffer m_curInventoryBuffer = new InventoryBuffer();
        private OperateTagBuffer m_curOperateTagBuffer = new OperateTagBuffer();
        private OperateTagISO18000Buffer m_curOperateTagISO18000Buffer = new OperateTagISO18000Buffer();
        public OpenFileDialog examinar = new OpenFileDialog();
        private string id_salida = null;
        private string id_salida_salida = null;

        // Use List para almacenar información de etiquetas en tiempo real
        private List<RealTimeTagData> RealTimeTagDataList = new List<RealTimeTagData>();
        private List<RealTimeTagData> datoList = new List<RealTimeTagData>();
        // Si se muestran datos de monitoreo en serie
        private bool m_bDisplayLog = false;
        // Inventario en tiempo real
        private int  m_nTotal = 0;
        // Grabar parámetros rápidos de antena de sondeo
        private Byte  [] m_btAryData = new byte [10];
        // Registre el número total de encuestas rápidas
        cn_usuario objProd = new cn_usuario();
        cn_usuario objetoCN = new cn_usuario();
        datos_u ver = new datos_u();
        
        private string idP = null;
        private bool Editar = false;
        public R2000UartDemo()
        {
            InitializeComponent();
        }

      
        private void R2000UartDemo_Load(object sender, EventArgs e)
        {
            
            salidas.Visible = false;
            lista.Visible = false;
            // Inicializa el acceso a la instancia del lector
            reader = new Reader.ReaderMethod();
            
            // función de devolución de llamada
            reader.AnalyCallback = AnalyData;
            reader.ReceiveCallback = ReceiveData;
            reader.SendCallback = SendData;
            ListarSalidas(dataGridView1);

            // Establecer la validez del elemento de interfaz


            SetFormEnable(false);

            // Inicializa la configuración predeterminada del lector de conexión
            textBox1.Text = String.Format("{0:G}", DateTime.Now);
            ListarCategorias();
            ListarProveedor();
            ListarProductos(dataestudiantes);
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = true;
            button9.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;

            Instances();
        }
        private void ListarSalidas(DataGridView data)
        {

            SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
            conectar.Open();
            SqlCommand comando = new SqlCommand("select id_RFID AS id, nombre_p,categoria.nombre_categ,fecha_registro,descripcion,foto from salida_producto INNER JOIN categoria ON salida_producto.id_categoria = categoria.id_categoria", conectar);
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
            data.Columns[4].Width = 185;
            data.Columns[5].Width = 110;
            conectar.Close();
        }
        private void ListarCategorias()
        {
            cn_usuario objProd = new cn_usuario();
           comboBox1.DataSource = objProd.MostrarCategoria();
            comboBox1.DisplayMember = "nombre_categ";
            comboBox1.ValueMember = "id_categoria";
        }
        private void ListarProveedor()
        {
            cn_usuario objProd = new cn_usuario();
            comboBox2.DataSource = objProd.MostrarProveedor();
            comboBox2.DisplayMember = "nom_proveedor";
            comboBox2.ValueMember = "id_proveedor";
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
        }
        private void ListarListas(DataGridView data)
        {

            SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
            conectar.Open();
            SqlCommand comando = new SqlCommand("select id_RFID AS id, nombre_p,categoria.nombre_categ,proveedor.nom_proveedor,fecha_registro,descripcion,foto from Llamarlista INNER JOIN categoria ON Llamarlista.id_categoria = categoria.id_categoria INNER JOIN proveedor ON Llamarlista.id_proveedor = proveedor.id_proveedor", conectar);
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
        }
        private void LimpiarFormulario()
        {
           textBox4.Clear();
            textBox8.Clear();
            
            richTextBox1.Clear();
            picfoto3.Image.Dispose();
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
       
        private static R2000UartDemo instance = null;
         
          public static R2000UartDemo Instances()
          {
             
                  if (instance == null || instance.IsDisposed)
                  {

                      instance = new R2000UartDemo();
                  }
                  return instance;
              
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
            MessageBox.Show("lector desconectado");
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
                    textBox4.Text= RealTimeTagDataList[i].strEpc;
                    

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
            button3.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button13.Enabled = true;
            button16.Enabled = true;
            button17.Enabled = true;
            button18.Enabled = true;
            button19.Enabled = true;


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
            button3.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;

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
                    textBox4.Text = RealTimeTagDataList[i].strEpc;


                }
            }
            else if (nReturnValue == 0)
            {
                MessageBox.Show("La tiqueta no se encuentra ");
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
        private void button9_Click(object sender, EventArgs e)
        {
           

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void CmbComPort1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbBaudrate1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Borrar_mensaje_error()
        {
            errorProvider1.SetError(textBox4, "");
            errorProvider1.SetError(textBox8, "");
            errorProvider1.SetError(richTextBox1, "");
            errorProvider1.SetError(comboBox1, "");

        }
        private bool validar_campos()
        {
            bool ok = true;
            if (textBox4.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox4, "ingrese el id RFID");
            }
            if (textBox8.Text == "")
            {
                ok = false;
                errorProvider1.SetError(textBox8, "ingrese el nombre del producto");
            }
            if (comboBox1.Text == "")
            {
                ok = false;
                errorProvider1.SetError(comboBox1, "ingrese una categoria");
            }
           
            if (richTextBox1.Text == "")
            {
                ok = false;
                errorProvider1.SetError(richTextBox1, "ingrese una descripcion");
            }

            return ok;
        }
        private void Button6_Click(object sender, EventArgs e)
        {

            //INSERTAR
           Borrar_mensaje_error();
            if (validar_campos())
            {
                if (Editar == false)
                {
                   
                    objetoCN.InsertarProducto(textBox4.Text, textBox8.Text, Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue),Convert.ToDateTime( textBox1.Text), richTextBox1.Text, picfoto3);
                        
                            MessageBox.Show("se inserto correctamente");
                            ListarProductos(dataestudiantes);
                            LimpiarFormulario();
                            
                      
                  
                }
                //EDITAR
                if (Editar == true)
                {

                   
                        objetoCN.EditarP(textBox4.Text, textBox8.Text, Convert.ToInt32(comboBox1.SelectedValue), Convert.ToInt32(comboBox2.SelectedValue), Convert.ToDateTime(textBox1.Text), richTextBox1.Text,picfoto3);
                        MessageBox.Show("se edito correctamente");
                        ListarProductos(dataestudiantes);
                        LimpiarFormulario();
                     
                        Editar = false;

                }
            }
            //ListarProductos(dataestudiantes);
            //LimpiarFormulario();
        }

       

        private void Button8_Click(object sender, EventArgs e)
        {
            if (dataestudiantes.SelectedRows.Count > 0)
            {
                Editar = true;
            
                    SqlConnection miconexion = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                    miconexion.Open();
                    SqlCommand cmd = new SqlCommand("select foto from producto where id_RFID= '"+ idP +"'", miconexion);
                    idP = dataestudiantes.CurrentRow.Cells["id"].Value.ToString();
                    cmd.Parameters.AddWithValue("@idRFD", idP);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    //Representa un set de comandos que es utilizado para llenar un DataSet
                    SqlDataAdapter dp = new SqlDataAdapter(cmd);
                    //Representa un caché (un espacio) en memoria de los datos.
                    DataSet ds = new DataSet("producto");

                    //Arreglo de byte en donde se almacenara la foto en bytes
                    byte[] MyData = new byte[0];


                    //Llenamosel DataSet con la tabla. ESTUDIANTE es nombre de la tabla
                    dp.Fill(ds, "producto");

                    //Si dni existe ejecutara la consulta
                    if (ds.Tables["producto"].Rows.Count > 0)
                    {
                        //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
                        DataRow myRow = ds.Tables["producto"].Rows[0];

                        //Se almacena el campo foto de la tabla en el arreglo de bytes
                        MyData = (byte[])myRow["foto"];
                        //Se inicializa un flujo en memoria del arreglo de bytes
                        MemoryStream stream = new MemoryStream(MyData);
                        //En el picture box se muestra la imagen que esta almacenada en el flujo en memoria 
                        //el cual contiene el arreglo de bytes
                        picfoto3.Image = Image.FromStream(stream);
                        textBox4.Text = dataestudiantes.CurrentRow.Cells["id"].Value.ToString();
                        textBox8.Text = dataestudiantes.CurrentRow.Cells["nombre_p"].Value.ToString();
                        comboBox1.Text = dataestudiantes.CurrentRow.Cells["nombre_categ"].Value.ToString();
                        comboBox2.Text = dataestudiantes.CurrentRow.Cells["nom_proveedor"].Value.ToString();
                        richTextBox1.Text = dataestudiantes.CurrentRow.Cells["descripcion"].Value.ToString();

                    }
               
             

            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (dataestudiantes.SelectedRows.Count > 0)
            {
                idP = dataestudiantes.CurrentRow.Cells["id"].Value.ToString();
                objetoCN.EliminarP(idP);
                MessageBox.Show("Eliminado correctamente");
                ListarProductos(dataestudiantes);
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Buscador_TextChanged(object sender, EventArgs e)
        {
            var aux = new metodo();
            aux.filtrarP(dataestudiantes, this.buscador.Text.Trim());
        }

        private void Button9_Click_1(object sender, EventArgs e)
        {
            examinar.Filter = "image files|*.jpg;*.png;*.gif;*.ico;.*;";
                DialogResult dres1 = examinar.ShowDialog();
                if (dres1 == DialogResult.Abort)
                    return;
                if (dres1 == DialogResult.Cancel)
                    return;
                
                picfoto3.Image = Image.FromFile(examinar.FileName);
          
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (this.dataestudiantes.Columns[e.ColumnIndex].Name)
            {
                case "foto":
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = Image.FromFile(e.Value.ToString());
                        }
                        catch (System.IO.FileNotFoundException exc)
                        {

                            e.Value = null;
                        }
                    }
                    break;
            }
        }

        private void Picfoto3_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("{0:G}", DateTime.Now);
        }

        private void Button10_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void Btnproductos_Click(object sender, EventArgs e)
        {
      
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Button13_Click(object sender, EventArgs e)
        {
           
        }

        private void Button12_Click(object sender, EventArgs e)
        {
           
           


        }

        private void Button14_Click(object sender, EventArgs e)
        {
            
        }

        private void Dataestudiantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button17_Click(object sender, EventArgs e)
        {
            
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button10_Click_1(object sender, EventArgs e)
        {
            
            
        }

        private void Button10_Click_2(object sender, EventArgs e)
        {
            salidas.Visible = true;
            lista.Visible = false;
        }

        private void Button12_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void Button11_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();

            try
            {

                SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                conectar.Open();
                id_salida = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                string consultar = ("select *from salida_producto where id_RFID='" + id_salida + "'");

                if (consultar == id_salida)
                {
                    MessageBox.Show("el producto ya salio");
                }
                else
                {
                    for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                    {
                        id_salida = dataGridView3.Rows[i].Cells[0].Value.ToString();
                        objetoCN.InsertarSalidas(id_salida);
                        ListarSalidas(dataGridView1);
                        objetoCN.EliminarP(id_salida);
                        ListarProductos(dataestudiantes);
                    }
                    MessageBox.Show("se inserto correctamente");
                    MessageBox.Show("Eliminado correctamente");
                    dataGridView3.Rows.Clear();

                   

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("no se pudo insertar los datos por: " + ex);
            }

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            var aux = new metodo();
            aux.filtrarSP(dataGridView1, this.buscador.Text.Trim());
        }

        private void Button13_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idP = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                objetoCN.EliminarSalidaP(idP);
                MessageBox.Show("Eliminado correctamente");
                ListarSalidas(dataGridView1);
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            int nReturnValue = 0;
            string tagInfo = "";
            nReturnValue = realTimeInventory(255, 255, 5);  //公用读写器地址，快速盘存模式，超时控制5秒
            for (int i = 0; i < RealTimeTagDataList.Count; i++)
            {
                tagInfo = RealTimeTagDataList[i].strEpc;
            }
            bool exist = dataGridView3.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToString(row.Cells[0].Value) == tagInfo);
            if (!exist)
            {

                dataGridView3.Rows.Add(tagInfo);

            }
            
            
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            lista.Visible = true;
            salidas.Visible = false;
            
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            salidas.Visible = false;
            lista.Visible = false;
        }

        private void Button17_Click_1(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            timer2.Start();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            timer2.Stop();

            try
            {

                SqlConnection conectar = new SqlConnection(@"Data Source=DESKTOP-MKDN1DT;Initial Catalog=proy_rfid;Integrated Security=True");
                conectar.Open();
                id_salida = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                string consultar = ("select *from salida_producto where id_RFID='" + id_salida + "'");

                if (consultar == id_salida)
                {
                    MessageBox.Show("el producto ya salio");
                }
                else
                {
                    for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                    {
                        id_salida = dataGridView4.Rows[i].Cells[0].Value.ToString();
                        objetoCN.InsertarLista(id_salida);
                        ListarListas(dataGridView5);
                        objetoCN.EliminarP(id_salida);
                    }
                    MessageBox.Show("Se llamo lista correctamente");
                   // MessageBox.Show("Eliminado correctamente");

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("no se pudo insertar los datos por: " + ex);
            }
        }
        
        private void Timer2_Tick(object sender, EventArgs e)
        {
            int nReturnValue = 0;
            string tagInfo = "";
            nReturnValue = realTimeInventory(255, 255, 5);  //公用读写器地址，快速盘存模式，超时控制5秒
            for (int i = 0; i < RealTimeTagDataList.Count; i++)
            {
                tagInfo = RealTimeTagDataList[i].strEpc;
            }
            bool exist = dataGridView4.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToString(row.Cells[0].Value) == tagInfo);
            if (!exist)
            {

                dataGridView4.Rows.Add(tagInfo);

            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
            {
                id_salida = dataGridView4.Rows[i].Cells[0].Value.ToString();
                objetoCN.InsertarProductoLista(id_salida);
                objetoCN.EliminarL(id_salida);
                ListarListas(dataGridView5);

            }
            dataGridView4.Rows.Clear();
           

        }

                private void Button18_Click(object sender, EventArgs e)
        {
            ReporteLista report = new ReporteLista();
            report.Show();
        }
    }
}
