// @author: Ethan
// @date: 2018/01/14

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using System.Reflection;

using HalconDotNet;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace MasterComputer
{
    public partial class FormMain : Form
    {
        // Creat a SerialPort Device
        SerialPort ComDevice = new SerialPort();

        // creat a Video Device
        private static string VideoDevice = null;

        // Camera control variables
        Thread displayVideo;
        private bool TakePhotoFlag = false;
        private bool SetImageSize = true;
        private static string ImageSavePath = null;
        private static string ImageSaveName = null;
        private static string Commodities = "识别出商品：";

        // Save Path
        private static string SaveDatasPath = null;
        private static string AppPath = null;
        private static string LogPath = null;
        private static string PicPath = null;

        // Control variables
        private static uint XB = 0x00000001;
        private static uint XH = 0x00000002;
        private static uint GSQ = 0x00000004;
        private static uint SWW = 0x00000008;
        private static uint YMQ = 0x00000010;
        private static uint YLD = 0x00000020;
        private static uint Task = 0x00000000;

        // Configurations Information
        private static string BaudRate = "BaudRate";
        private static string Parity = "Parity";
        private static string DataBits = "DataBits";
        private static string StopBits = "StopBits";

        // Timer
        private System.Timers.Timer IdentificationTimer = new System.Timers.Timer();
        private bool IdentificationFlag = false;

        // Stack for temporary objects
        HObject[] OTemp = new HObject[20];
        // Local iconic variables
        HObject ho_image, ho_ROI, ho_Image, ho_ImageGauss;
        HObject ho_EdgeAmplitude, ho_Basins, ho_Red, ho_Green, ho_Blue;
        HObject ho_Hue, ho_Saturation, ho_Value, ho_Saturated, ho_HueSaturated;
        HObject ho_connectSaturated, ho_connectSaturatedFill, ho_SelectedRegions_GZ;
        HObject ho_union_tmp, ho_union_tmp_complement = null, ho_CurrentFuse = null;
        HObject ho_CurrentFuseConn = null, ho_CurrentFuseFill = null;
        HObject ho_CurrentFuseSel = null, ho_SWW_ImageReduced, ho_SWW_ImageReduced_Regions;
        HObject ho_SWW_ImageReduced_Regions_conn, ho_SWW_SelectedRegions;
        HObject ho_RegionTransCircle, ho_RegionTransCircle_s, ho_union_ImageReduced;
        HObject ho_GSQ_Regions_Tmp, ho_GSQ_Regions_Tmp_Fill, ho_GSQ_Regions_Tmp_Fill_conn;
        HObject ho_SelectedRegionsGSQ, ho_YMQ_ImageReduced, ho_YMQ_ImageReduced_Regions;
        HObject ho_YMQ_ImageReduced_Regions_Fill, ho_YMQ_ImageReduced_Regions_conn;
        HObject ho_SelectedRegions_YMQ, ho_YLD_ImageReduced, ho_YLD_ImageRegion;
        HObject ho_YLD_ImageRegion_Fill, ho_YLD_ImageRegion_Fill_conn;
        HObject ho_YLD_Select_Region;
        // Local control variables
        HTuple hv_HueStr = null, hv_HueID = null, hv_HueRanges = null;
        HTuple hv_WH = new HTuple(), hv_ID = null, hv_X = null;
        HTuple hv_AcqHandle = null, hv_Min = null, hv_Max = null;
        HTuple hv_Range = null, hv_FuseArea = null, hv_Row1 = null;
        HTuple hv_Column1 = null, hv_num = null, hv_Fuse = null;
        HTuple hv_i = new HTuple();
        HTuple hv_Width = null, hv_Height = null;

        // Menus
        ToolStripMenuItem LastBaudRateMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem LastParityMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem LastDataBitsMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem LastStopBitsMenuItem = new ToolStripMenuItem();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 显示版本信息
            labelVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
            // 显示状态栏时间
            toolStripStatusLabelTime.Text = System.DateTime.Now.ToString();

            // 获取APP启动路径
            AppPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            LogPath = AppPath + "Log";
            PicPath = AppPath + "Pictures";

            // 获取可用串口列表
            string[] ports = SerialPort.GetPortNames();
            // 获取可用视频设备
            FilterInfoCollection VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (VideoDevices.Count != 0)
            {
                VideoDevice = "[0] " + VideoDevices[0].Name.ToString();
            }

            // 打印串口列表
            foreach (string port in ports)
            {
                toolStripComboBoxComNum.Items.Add(port);
            }
            if (toolStripComboBoxComNum.Items.Count > 0)
            {
                toolStripComboBoxComNum.SelectedIndex = 0;
            }
            else
            {
                UpdateStatusMsg("无可用串口设备！");
            }

            // 初始化菜单
            InitializeToolStripMenuItems();

            // 加载用户配置文件
            LoadConfigFile();

            // 连接视频输入设备
            if (VideoDevice != null)
            {
                StartVideo();
            }
            else
            {
                UpdateStatusMsg("不存在视频输入设备！");
            }
            // 显示相册中最后一张图片（如果存在）
            ShowImage();

            // 委托数据接收
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Receive);

            // 设置定时器
            IdentificationTimer.Enabled = true;
            IdentificationTimer.Interval = 1000;
            IdentificationTimer.AutoReset = true;
            IdentificationTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerRun);
        }

        private void FormMain_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("是否退出程序？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        WriteConfigFile();
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusMsg(ex.Message.ToString());
                        UpdateStatusMsg("配置文件写入失败");
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (ComDevice.IsOpen == true)
                    {
                        Send((uint)0x80); // 通知下位机停止工作
                    }
                    if (OpenCloseCameraToolStripMenuItem.Tag.ToString() == "1")
                    {
                        StopVideo();
                    }
                    SaveDataToolStripMenuItem.PerformClick(); // 保存日志
                }
            }
        }

        #region Methods
        /// <summary>
        /// 初始化菜单开始状态
        /// </summary>
        private void InitializeToolStripMenuItems()
        {
            toolStripMenuItemBPS9600.Checked = true;
            toolStripMenuItemBPS9600.Enabled = false;
            LastBaudRateMenuItem = toolStripMenuItemBPS9600;
            PRTnoneToolStripMenuItem.Checked = true;
            PRTnoneToolStripMenuItem.Enabled = false;
            LastParityMenuItem = PRTnoneToolStripMenuItem;
            toolStripMenuItemDTB8.Checked = true;
            toolStripMenuItemDTB8.Enabled = false;
            LastDataBitsMenuItem = toolStripMenuItemDTB8;
            toolStripMenuItemSTP1.Checked = true;
            toolStripMenuItemSTP1.Enabled = false;
            LastStopBitsMenuItem = toolStripMenuItemSTP1;
        }

        /// <summary>
        /// 写入用户配置文件
        /// </summary>
        private void WriteConfigFile()
        {
            SetValue(BaudRate, LastBaudRateMenuItem.Text.ToString());
            SetValue(Parity, LastParityMenuItem.Tag.ToString());
            SetValue(DataBits, LastDataBitsMenuItem.Text.ToString());
            SetValue(StopBits, LastStopBitsMenuItem.Text.ToString());
        }
        /// <summary>
        /// 加载用户配置文件
        /// </summary>
        private void LoadConfigFile()
        {
            string ReadConfig = null;

            ReadConfig = GetValue(BaudRate);
            if (ReadConfig == "115200")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS115200.Checked = true;
                toolStripMenuItemBPS115200.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS115200;
            }
            else if (ReadConfig == "57600")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS57600.Checked = true;
                toolStripMenuItemBPS57600.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS57600;
            }
            else if (ReadConfig == "38400")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS38400.Checked = true;
                toolStripMenuItemBPS38400.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS38400;
            }
            else if (ReadConfig == "19200")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS19200.Checked = true;
                toolStripMenuItemBPS19200.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS19200;
            }
            else if (ReadConfig == "9600")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS9600.Checked = true;
                toolStripMenuItemBPS9600.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS9600;
            }
            else if (ReadConfig == "4800")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS4800.Checked = true;
                toolStripMenuItemBPS4800.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS4800;
            }
            else if (ReadConfig == "1200")
            {
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                toolStripMenuItemBPS1200.Checked = true;
                toolStripMenuItemBPS1200.Enabled = false;
                LastBaudRateMenuItem = toolStripMenuItemBPS1200;
            }

            ReadConfig = GetValue(Parity);
            if (ReadConfig == "0")
            {
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                PRTnoneToolStripMenuItem.Checked = true;
                PRTnoneToolStripMenuItem.Enabled = false;
                LastParityMenuItem = PRTnoneToolStripMenuItem;
            }
            else if (ReadConfig == "1")
            {
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                PRToddToolStripMenuItem.Checked = true;
                PRToddToolStripMenuItem.Enabled = false;
                LastParityMenuItem = PRToddToolStripMenuItem;
            }
            else if (ReadConfig == "2")
            {
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                PRTevenToolStripMenuItem.Checked = true;
                PRTevenToolStripMenuItem.Enabled = false;
                LastParityMenuItem = PRTevenToolStripMenuItem;
            }
            else if (ReadConfig == "3")
            {
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                PRTmarkToolStripMenuItem.Checked = true;
                PRTmarkToolStripMenuItem.Enabled = false;
                LastParityMenuItem = PRTmarkToolStripMenuItem;
            }
            else if (ReadConfig == "4")
            {
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                PRTspaceToolStripMenuItem.Checked = true;
                PRTspaceToolStripMenuItem.Enabled = false;
                LastParityMenuItem = PRTspaceToolStripMenuItem;
            }

            ReadConfig = GetValue(DataBits);
            if (ReadConfig == "8")
            {
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                toolStripMenuItemDTB8.Checked = true;
                toolStripMenuItemDTB8.Enabled = false;
                LastDataBitsMenuItem = toolStripMenuItemDTB8;
            }
            else if (ReadConfig == "7")
            {
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                toolStripMenuItemDTB7.Checked = true;
                toolStripMenuItemDTB7.Enabled = false;
                LastDataBitsMenuItem = toolStripMenuItemDTB7;
            }
            else if (ReadConfig == "6")
            {
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                toolStripMenuItemDTB6.Checked = true;
                toolStripMenuItemDTB6.Enabled = false;
                LastDataBitsMenuItem = toolStripMenuItemDTB6;
            }

            ReadConfig = GetValue(StopBits);
            if (ReadConfig == "1")
            {
                LastStopBitsMenuItem.Checked = false;
                LastStopBitsMenuItem.Enabled = true;
                toolStripMenuItemSTP1.Checked = true;
                toolStripMenuItemSTP1.Enabled = false;
                LastStopBitsMenuItem = toolStripMenuItemSTP1;
            }
            else if (ReadConfig == "2")
            {
                LastStopBitsMenuItem.Checked = false;
                LastStopBitsMenuItem.Enabled = true;
                toolStripMenuItemSTP2.Checked = true;
                toolStripMenuItemSTP2.Enabled = false;
                LastStopBitsMenuItem = toolStripMenuItemSTP2;
            }
        }
        /// <summary>
        /// 写入键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetValue(string key, string value)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            try
            {
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch(Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
            }
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetValue(string key)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                return config.AppSettings.Settings[key].Value;
            return null;
        }

        /// <summary>
        /// 商品特征识别
        /// </summary>
        private void Action()
        {
            // 定义商品信息
            string CommodityInf = "识别出商品：";
            uint NowTask = 0x00000000;
            uint NowHave = 0x00000000;

            // 设置任务
            if (XBToolStripMenuItem.Checked == true)
            {
                NowTask |= XB << 8;
            }
            if (XHToolStripMenuItem.Checked == true)
            {
                NowTask |= XH << 8;
            }
            if (GSQToolStripMenuItem.Checked == true)
            {
                NowTask |= GSQ << 8;
            }
            if (SWWToolStripMenuItem.Checked == true)
            {
                NowTask |= SWW << 8;
            }
            if (YMQToolStripMenuItem.Checked == true)
            {
                NowTask |= YMQ << 8;
            }
            if (YLDToolStripMenuItem.Checked == true)
            {
                NowTask |= YLD << 8;
            }
            /* 其他任务可继续添加 */

            // 颜色识别参考color_fuses.hdev: classify fuses by color
            // step: set up 方块和网球 properties and hue ranges
            hv_HueStr = new HTuple();
            hv_HueStr[0] = "R";
            hv_HueStr[1] = "B";
            hv_HueStr[2] = "Y";
            hv_HueStr[3] = "G";
            hv_HueStr[4] = "W";
            hv_HueID = new HTuple();
            hv_HueID[0] = 0;
            hv_HueID[1] = 6;
            hv_HueID[2] = 3;
            hv_HueID[3] = 9;
            hv_HueID[4] = 11;
            // HueRanges:  Red 0-10...色调
            hv_HueRanges = new HTuple();
            hv_HueRanges[0] = 0;
            hv_HueRanges[1] = 12;
            hv_HueRanges[2] = 125;
            hv_HueRanges[3] = 162;
            hv_HueRanges[4] = 22;
            hv_HueRanges[5] = 40;
            hv_HueRanges[6] = 85;
            hv_HueRanges[7] = 128;
            hv_HueRanges[8] = 42;
            hv_HueRanges[9] = 65;

            hv_ID = new HTuple();
            hv_X = new HTuple();

            // Image Acquisition 01: Code generated by Image Acquisition 01
            ho_image.Dispose();
            HOperatorSet.GrabImageAsync(out ho_image, hv_AcqHandle, -1);

            // Image Acquisition 01: Do something
            // step: acquire image
            ho_ROI.Dispose();
            HOperatorSet.GenRectangle1(out ho_ROI, 232.6, 124.3, 481, 986.167);
            // 并不减少图像的尺寸，只是减少图像的定义域
            ho_Image.Dispose();
            HOperatorSet.ReduceDomain(ho_image, ho_ROI, out ho_Image);
            // 加了一段高斯滤波的程序
            ho_ImageGauss.Dispose();
            HOperatorSet.GaussFilter(ho_image, out ho_ImageGauss, 5);

            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ReduceDomain(ho_ImageGauss, ho_ROI, out ExpTmpOutVar_0);
                ho_ImageGauss.Dispose();
                ho_ImageGauss = ExpTmpOutVar_0;
            }

            ho_Image.Dispose();
            ho_Image = ho_ImageGauss.CopyObj(1, -1);
            ho_EdgeAmplitude.Dispose();
            HOperatorSet.SobelAmp(ho_ImageGauss, out ho_EdgeAmplitude, "sum_abs", 3);
            ho_Basins.Dispose();
            HOperatorSet.WatershedsThreshold(ho_EdgeAmplitude, out ho_Basins, 14);
            // step: extract saturated hues
            ho_Red.Dispose(); ho_Green.Dispose(); ho_Blue.Dispose();
            HOperatorSet.Decompose3(ho_Image, out ho_Red, out ho_Green, out ho_Blue);
            ho_Hue.Dispose(); ho_Saturation.Dispose(); ho_Value.Dispose();
            HOperatorSet.TransFromRgb(ho_Red, ho_Green, ho_Blue, out ho_Hue, out ho_Saturation,
                out ho_Value, "hsv");
            ho_Saturated.Dispose();
            HOperatorSet.Threshold(ho_Saturation, out ho_Saturated, 42, 255);
            ho_HueSaturated.Dispose();
            HOperatorSet.ReduceDomain(ho_Hue, ho_Saturated, out ho_HueSaturated);

            #region 图像识别区

            #region 灌装
            // ***************************Start Identification***************************
            // 罐装(GZ)
            // 要通过色调区分红罐与绿罐
            // 求鲜艳对象的连通域
            ho_connectSaturated.Dispose();
            HOperatorSet.Connection(ho_Saturated, out ho_connectSaturated);
            ho_connectSaturatedFill.Dispose();
            HOperatorSet.FillUp(ho_connectSaturated, out ho_connectSaturatedFill);
            ho_SelectedRegions_GZ.Dispose();
            HOperatorSet.SelectShape(ho_connectSaturatedFill, out ho_SelectedRegions_GZ,
                (new HTuple("area")).TupleConcat("height"), "and", (new HTuple(7580.34)).TupleConcat(
                100), (new HTuple(19338.4)).TupleConcat(200));
            // union_tmp 是识别了的物品的region，它贯穿始终
            // union_tmp_complement 是其补集
            ho_union_tmp.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions_GZ, out ho_union_tmp);
            // percent参数不是很明确
            HOperatorSet.MinMaxGray(ho_SelectedRegions_GZ, ho_Saturation, 0.9, out hv_Min,
                out hv_Max, out hv_Range);
            HOperatorSet.AreaCenter(ho_SelectedRegions_GZ, out hv_FuseArea, out hv_Row1,
                out hv_Column1);
            if ((int)(new HTuple((new HTuple(hv_Min.TupleLength())).TupleEqual(2))) != 0)
            {
                if ((int)(new HTuple(((hv_Min.TupleSelect(0))).TupleLess(hv_Min.TupleSelect(1)))) != 0)
                {
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(0),
                    //    hv_Column1.TupleSelect(0));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "XH");
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(1),
                    //    hv_Column1.TupleSelect(1));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "雪碧");
                    CommodityInf += "雪花 雪碧 ";
                    NowHave |= (XH | XB);
                    hv_ID = hv_ID.TupleConcat(7);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(0));
                    hv_ID = hv_ID.TupleConcat(10);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(1));
                }
                else
                {
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(1),
                    //    hv_Column1.TupleSelect(1));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "XH");
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(0),
                    //    hv_Column1.TupleSelect(0));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "XB");
                    CommodityInf += "雪花 雪碧 ";
                    NowHave |= (XH | XB);
                    hv_ID = hv_ID.TupleConcat(7);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(1));
                    hv_ID = hv_ID.TupleConcat(10);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(0));
                }
            }
            else if ((int)(new HTuple((new HTuple(hv_Min.TupleLength())).TupleEqual(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Min.TupleSelect(0))).TupleGreater(15))) != 0)
                {
                    hv_ID = hv_ID.TupleConcat(10);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(0));
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(0),
                    //    hv_Column1.TupleSelect(0));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "XB");
                    CommodityInf += "雪碧 ";
                    NowHave |= XB;
                }
                else
                {
                    hv_ID = hv_ID.TupleConcat(7);
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(0));
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(0),
                    //    hv_Column1.TupleSelect(0));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, "XH");
                    CommodityInf += "雪花 ";
                    NowHave |= XH;
                }
            }
            #endregion

            #region 爽歪歪
            HOperatorSet.CountObj(ho_union_tmp, out hv_num);
            if ((int)(new HTuple(hv_num.TupleNotEqual(0))) != 0)
            {
                // 将罐装区域去除再进行颜色识别
                ho_union_tmp_complement.Dispose();
                HOperatorSet.Complement(ho_union_tmp, out ho_union_tmp_complement);
            }
            else
            {
                ho_union_tmp_complement.Dispose();
                ho_union_tmp_complement = ho_Saturated.CopyObj(1, -1);
            }

            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ReduceDomain(ho_HueSaturated, ho_union_tmp_complement, out ExpTmpOutVar_0
                    );
                ho_HueSaturated.Dispose();
                ho_HueSaturated = ExpTmpOutVar_0;
            }

            // 此处 union_tmp 是方块、网球和罐的域（如果存在）
            // union_tmp := SelectedRegion_GZ
            // 探测颜色
            for (hv_Fuse = 0; (int)hv_Fuse <= (int)((new HTuple(hv_HueID.TupleLength())) - 1); hv_Fuse = (int)hv_Fuse + 1)
            {
                //step: classify specific fuse
                ho_CurrentFuse.Dispose();
                HOperatorSet.Threshold(ho_HueSaturated, out ho_CurrentFuse, hv_HueRanges.TupleSelect(
                    hv_Fuse * 2), hv_HueRanges.TupleSelect((hv_Fuse * 2) + 1));

                ho_CurrentFuseConn.Dispose();
                HOperatorSet.Connection(ho_CurrentFuse, out ho_CurrentFuseConn);
                ho_CurrentFuseFill.Dispose();
                HOperatorSet.FillUp(ho_CurrentFuseConn, out ho_CurrentFuseFill);
                ho_CurrentFuseSel.Dispose();
                HOperatorSet.SelectShape(ho_CurrentFuseFill, out ho_CurrentFuseSel, (new HTuple("area")).TupleConcat(
                    "height"), "and", (new HTuple(3600)).TupleConcat(54), (new HTuple(8500)).TupleConcat(115.7));

                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_union_tmp, ho_CurrentFuseSel, out ExpTmpOutVar_0);
                    ho_union_tmp.Dispose();
                    ho_union_tmp = ExpTmpOutVar_0;
                }

                HOperatorSet.AreaCenter(ho_CurrentFuseSel, out hv_FuseArea, out hv_Row1, out hv_Column1);
                HOperatorSet.SetColor(hWindowControl.HalconWindow, "magenta");
                for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_FuseArea.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                {
                    //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(hv_i),
                    //    hv_Column1.TupleSelect(hv_i));
                    //HOperatorSet.WriteString(hWindowControl.HalconWindow, ((hv_HueStr.TupleSelect(
                    //    hv_Fuse)) + " ") + (hv_HueID.TupleSelect(hv_Fuse)));
                    hv_ID = hv_ID.TupleConcat(hv_HueID.TupleSelect(hv_Fuse));
                    hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(hv_i));
                }
            }
            
            // 爽歪歪
            HOperatorSet.CountObj(ho_union_tmp, out hv_num);
            if ((int)(new HTuple(hv_num.TupleNotEqual(0))) != 0)
            {
                ho_union_tmp_complement.Dispose();
                HOperatorSet.Complement(ho_union_tmp, out ho_union_tmp_complement);
            }
            else
            {
                ho_union_tmp_complement.Dispose();
                ho_union_tmp_complement = ho_HueSaturated.CopyObj(1, -1);
            }
            ho_SWW_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_HueSaturated, ho_union_tmp_complement, out ho_SWW_ImageReduced);
            ho_SWW_ImageReduced_Regions.Dispose();
            HOperatorSet.Threshold(ho_SWW_ImageReduced, out ho_SWW_ImageReduced_Regions, 0, 97);
            ho_SWW_ImageReduced_Regions_conn.Dispose();
            HOperatorSet.Connection(ho_SWW_ImageReduced_Regions, out ho_SWW_ImageReduced_Regions_conn);
            ho_SWW_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_SWW_ImageReduced_Regions_conn, out ho_SWW_SelectedRegions,
                "area", "and", 2600, 5000);

            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Union2(ho_union_tmp, ho_SWW_SelectedRegions, out ExpTmpOutVar_0);
                ho_union_tmp.Dispose();
                ho_union_tmp = ExpTmpOutVar_0;
            }

            ho_RegionTransCircle.Dispose();
            HOperatorSet.ShapeTrans(ho_SWW_SelectedRegions, out ho_RegionTransCircle, "outer_circle");
            ho_RegionTransCircle_s.Dispose();
            HOperatorSet.SelectShape(ho_RegionTransCircle, out ho_RegionTransCircle_s, "area",
                "and", 6551.4, 8289.72);
            HOperatorSet.AreaCenter(ho_RegionTransCircle_s, out hv_FuseArea, out hv_Row1,
                out hv_Column1);
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "magenta");
            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_FuseArea.TupleLength())) - 1); hv_i = (int)hv_i + 1)
            {
                //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(hv_i),
                //    hv_Column1.TupleSelect(hv_i));
                //HOperatorSet.WriteString(hWindowControl.HalconWindow, "SWW");
                CommodityInf += "爽歪歪 ";
                NowHave |= SWW;
                hv_ID = hv_ID.TupleConcat(4);
                hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(hv_i));
            }
            #endregion

            #region 钢丝球
            // 钢丝球
            HOperatorSet.CountObj(ho_union_tmp, out hv_num);
            if ((int)(new HTuple(hv_num.TupleNotEqual(0))) != 0)
            {
                ho_union_tmp_complement.Dispose();
                HOperatorSet.Complement(ho_union_tmp, out ho_union_tmp_complement);
            }
            else
            {
                ho_union_tmp_complement.Dispose();
                ho_union_tmp_complement = ho_Value.CopyObj(1, -1);
            }
            ho_union_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Value, ho_union_tmp_complement, out ho_union_ImageReduced);
            ho_GSQ_Regions_Tmp.Dispose();
            HOperatorSet.Threshold(ho_union_ImageReduced, out ho_GSQ_Regions_Tmp, 1, 75);
            ho_GSQ_Regions_Tmp_Fill.Dispose();
            HOperatorSet.FillUp(ho_GSQ_Regions_Tmp, out ho_GSQ_Regions_Tmp_Fill);
            ho_GSQ_Regions_Tmp_Fill_conn.Dispose();
            HOperatorSet.Connection(ho_GSQ_Regions_Tmp_Fill, out ho_GSQ_Regions_Tmp_Fill_conn);
            ho_SelectedRegionsGSQ.Dispose();
            HOperatorSet.SelectShape(ho_GSQ_Regions_Tmp_Fill_conn, out ho_SelectedRegionsGSQ,
                "area", "and", 3400, 7990);

            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Union2(ho_union_tmp, ho_SelectedRegionsGSQ, out ExpTmpOutVar_0);
                ho_union_tmp.Dispose();
                ho_union_tmp = ExpTmpOutVar_0;
            }

            HOperatorSet.AreaCenter(ho_SelectedRegionsGSQ, out hv_FuseArea, out hv_Row1, out hv_Column1);
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "magenta");
            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_FuseArea.TupleLength())) - 1); hv_i = (int)hv_i + 1)
            {
                //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(hv_i),
                //    hv_Column1.TupleSelect(hv_i));
                //HOperatorSet.WriteString(hWindowControl.HalconWindow, "GSQ");
                CommodityInf += "钢丝球 ";
                NowHave |= GSQ;
                hv_ID = hv_ID.TupleConcat(5);
                hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(hv_i));
            }
            #endregion

            #region 羽毛球
            // 羽毛球
            HOperatorSet.CountObj(ho_union_tmp, out hv_num);
            if ((int)(new HTuple(hv_num.TupleNotEqual(0))) != 0)
            {
                ho_union_tmp_complement.Dispose();
                HOperatorSet.Complement(ho_union_tmp, out ho_union_tmp_complement);
            }
            else
            {
                ho_union_tmp_complement.Dispose();
                ho_union_tmp_complement = ho_Value.CopyObj(1, -1);
            }
            ho_YMQ_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Value, ho_union_tmp_complement, out ho_YMQ_ImageReduced);
            ho_YMQ_ImageReduced_Regions.Dispose();
            HOperatorSet.Threshold(ho_YMQ_ImageReduced, out ho_YMQ_ImageReduced_Regions, 0, 48);
            ho_YMQ_ImageReduced_Regions_Fill.Dispose();
            HOperatorSet.FillUp(ho_YMQ_ImageReduced_Regions, out ho_YMQ_ImageReduced_Regions_Fill);
            ho_YMQ_ImageReduced_Regions_conn.Dispose();
            HOperatorSet.Connection(ho_YMQ_ImageReduced_Regions_Fill, out ho_YMQ_ImageReduced_Regions_conn);
            ho_SelectedRegions_YMQ.Dispose();
            HOperatorSet.SelectShape(ho_YMQ_ImageReduced_Regions_conn, out ho_SelectedRegions_YMQ,
                "area", "and", 153.54, 200.01);
            HOperatorSet.AreaCenter(ho_SelectedRegions_YMQ, out hv_FuseArea, out hv_Row1, out hv_Column1);
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "magenta");
            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_FuseArea.TupleLength())) - 1); hv_i = (int)hv_i + 1)
            {
                //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(hv_i),
                //    hv_Column1.TupleSelect(hv_i));
                //HOperatorSet.WriteString(hWindowControl.HalconWindow, "YMQ");
                CommodityInf += "羽毛球 ";
                NowHave |= YMQ;
                hv_ID = hv_ID.TupleConcat(2);
                hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(hv_i));
            }
            #endregion

            #region 养乐多
            // 养乐多
            HOperatorSet.CountObj(ho_union_tmp, out hv_num);
            if ((int)(new HTuple(hv_num.TupleNotEqual(0))) != 0)
            {
                ho_union_tmp_complement.Dispose();
                HOperatorSet.Complement(ho_union_tmp, out ho_union_tmp_complement);
            }
            else
            {
                ho_union_tmp_complement.Dispose();
                ho_union_tmp_complement = ho_Value.CopyObj(1, -1);
            }
            ho_YLD_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Value, ho_union_tmp_complement, out ho_YLD_ImageReduced);
            ho_YLD_ImageRegion.Dispose();
            HOperatorSet.Threshold(ho_YLD_ImageReduced, out ho_YLD_ImageRegion, 189, 255);
            ho_YLD_ImageRegion_Fill.Dispose();
            HOperatorSet.FillUp(ho_YLD_ImageRegion, out ho_YLD_ImageRegion_Fill);
            ho_YLD_ImageRegion_Fill_conn.Dispose();
            HOperatorSet.Connection(ho_YLD_ImageRegion_Fill, out ho_YLD_ImageRegion_Fill_conn);
            ho_YLD_Select_Region.Dispose();
            HOperatorSet.SelectShape(ho_YLD_ImageRegion_Fill_conn, out ho_YLD_Select_Region,
                (new HTuple("area")).TupleConcat("row1"), "and", (new HTuple(966.36)).TupleConcat(
                393.46), (new HTuple(2000)).TupleConcat(466.36));
            HOperatorSet.AreaCenter(ho_YLD_Select_Region, out hv_FuseArea, out hv_Row1, out hv_Column1);
            HOperatorSet.SetColor(hWindowControl.HalconWindow, "magenta");
            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_FuseArea.TupleLength())) - 1); hv_i = (int)hv_i + 1)
            {
                //HOperatorSet.SetTposition(hWindowControl.HalconWindow, hv_Row1.TupleSelect(hv_i),
                //    hv_Column1.TupleSelect(hv_i));
                //HOperatorSet.WriteString(hWindowControl.HalconWindow, "YLD");
                CommodityInf += "养乐多 ";
                NowHave |= YLD;
                hv_ID = hv_ID.TupleConcat(1);
                hv_X = hv_X.TupleConcat(hv_Column1.TupleSelect(hv_i));
            }
            // ***************************End of Identification***************************
            #endregion

            #region 其他

            #endregion

            #endregion

            // 显示商品信息
            if (Commodities != CommodityInf)
            {
                UpdateStatusMsg(CommodityInf);
                Commodities = CommodityInf;
            }
            if ((NowTask | NowHave) != Task)
            {
                Task = (NowTask | NowHave);
                if (ComDevice.IsOpen == true)
                {
                    Send(Task);
                }
            }

            // Dispose Resource
            ho_image.Dispose();
            ho_ROI.Dispose();
            ho_Image.Dispose();
            ho_ImageGauss.Dispose();
            ho_EdgeAmplitude.Dispose();
            ho_Basins.Dispose();
            ho_Red.Dispose();
            ho_Green.Dispose();
            ho_Blue.Dispose();
            ho_Hue.Dispose();
            ho_Saturation.Dispose();
            ho_Value.Dispose();
            ho_Saturated.Dispose();
            ho_HueSaturated.Dispose();
            ho_connectSaturated.Dispose();
            ho_connectSaturatedFill.Dispose();
            ho_SelectedRegions_GZ.Dispose();
            ho_union_tmp.Dispose();
            ho_union_tmp_complement.Dispose();
            ho_CurrentFuse.Dispose();
            ho_CurrentFuseConn.Dispose();
            ho_CurrentFuseFill.Dispose();
            ho_CurrentFuseSel.Dispose();
            ho_SWW_ImageReduced.Dispose();
            ho_SWW_ImageReduced_Regions.Dispose();
            ho_SWW_ImageReduced_Regions_conn.Dispose();
            ho_SWW_SelectedRegions.Dispose();
            ho_RegionTransCircle.Dispose();
            ho_RegionTransCircle_s.Dispose();
            ho_union_ImageReduced.Dispose();
            ho_GSQ_Regions_Tmp.Dispose();
            ho_GSQ_Regions_Tmp_Fill.Dispose();
            ho_GSQ_Regions_Tmp_Fill_conn.Dispose();
            ho_SelectedRegionsGSQ.Dispose();
            ho_YMQ_ImageReduced.Dispose();
            ho_YMQ_ImageReduced_Regions.Dispose();
            ho_YMQ_ImageReduced_Regions_Fill.Dispose();
            ho_YMQ_ImageReduced_Regions_conn.Dispose();
            ho_SelectedRegions_YMQ.Dispose();
            ho_YLD_ImageReduced.Dispose();
            ho_YLD_ImageRegion.Dispose();
            ho_YLD_ImageRegion_Fill.Dispose();
            ho_YLD_ImageRegion_Fill_conn.Dispose();
            ho_YLD_Select_Region.Dispose();
        }

        /// <summary>
        /// 显示实时视频
        /// </summary>
        private void ShowVideo()
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_image);
            HOperatorSet.GenEmptyObj(out ho_ROI);
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageGauss);
            HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
            HOperatorSet.GenEmptyObj(out ho_Basins);
            HOperatorSet.GenEmptyObj(out ho_Red);
            HOperatorSet.GenEmptyObj(out ho_Green);
            HOperatorSet.GenEmptyObj(out ho_Blue);
            HOperatorSet.GenEmptyObj(out ho_Hue);
            HOperatorSet.GenEmptyObj(out ho_Saturation);
            HOperatorSet.GenEmptyObj(out ho_Value);
            HOperatorSet.GenEmptyObj(out ho_Saturated);
            HOperatorSet.GenEmptyObj(out ho_HueSaturated);
            HOperatorSet.GenEmptyObj(out ho_connectSaturated);
            HOperatorSet.GenEmptyObj(out ho_connectSaturatedFill);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions_GZ);
            HOperatorSet.GenEmptyObj(out ho_union_tmp);
            HOperatorSet.GenEmptyObj(out ho_union_tmp_complement);
            HOperatorSet.GenEmptyObj(out ho_CurrentFuse);
            HOperatorSet.GenEmptyObj(out ho_CurrentFuseConn);
            HOperatorSet.GenEmptyObj(out ho_CurrentFuseFill);
            HOperatorSet.GenEmptyObj(out ho_CurrentFuseSel);
            HOperatorSet.GenEmptyObj(out ho_SWW_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_SWW_ImageReduced_Regions);
            HOperatorSet.GenEmptyObj(out ho_SWW_ImageReduced_Regions_conn);
            HOperatorSet.GenEmptyObj(out ho_SWW_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionTransCircle);
            HOperatorSet.GenEmptyObj(out ho_RegionTransCircle_s);
            HOperatorSet.GenEmptyObj(out ho_union_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_GSQ_Regions_Tmp);
            HOperatorSet.GenEmptyObj(out ho_GSQ_Regions_Tmp_Fill);
            HOperatorSet.GenEmptyObj(out ho_GSQ_Regions_Tmp_Fill_conn);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsGSQ);
            HOperatorSet.GenEmptyObj(out ho_YMQ_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_YMQ_ImageReduced_Regions);
            HOperatorSet.GenEmptyObj(out ho_YMQ_ImageReduced_Regions_Fill);
            HOperatorSet.GenEmptyObj(out ho_YMQ_ImageReduced_Regions_conn);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions_YMQ);
            HOperatorSet.GenEmptyObj(out ho_YLD_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_YLD_ImageRegion);
            HOperatorSet.GenEmptyObj(out ho_YLD_ImageRegion_Fill);
            HOperatorSet.GenEmptyObj(out ho_YLD_ImageRegion_Fill_conn);
            HOperatorSet.GenEmptyObj(out ho_YLD_Select_Region);

            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            while (true)
            {
                try
                {
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                }
                catch (ThreadAbortException) // 捕获进程结束时的异常
                {
                    return;
                }
                catch (Exception ex) // 捕获其他异常，如同步超时
                {
                    HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
                    HOperatorSet.CloseFramegrabber(hv_AcqHandle);
                    OpenCloseCameraToolStripMenuItem.Tag = "0";
                    OpenCloseCameraToolStripMenuItem.Checked = false;
                    UpdateStatusMsg(ex.Message.ToString());
                    UpdateStatusMsg("摄像头已关闭");
                    displayVideo.Abort();
                    return;
                }
                if (SetImageSize == true)
                {
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.SetPart(hWindowControl.HalconWindow, 0, 0, hv_Height - 1, hv_Width - 1);
                    SetImageSize = false;
                }
                HOperatorSet.DispObj(ho_Image, hWindowControl.HalconWindow);
                //ho_Image.Dispose();
                // Image Acquisition 01: Do something
                // 特征识别
                if (IdentificationFlag == true)
                {
                    try
                    {
                        Action();
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusMsg(ex.Message.ToString());
                    }
                    IdentificationFlag = false;
                }
                // 拍照并保存
                if (TakePhotoFlag == true)
                {
                    ho_Image.Dispose();
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                    try
                    {
                        HOperatorSet.WriteImage(ho_Image, "png", 0, ImageSavePath);
                    }
                    catch (ThreadAbortException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        UpdateStatusMsg(ex.Message.ToString());
                    }
                    TakePhotoFlag = false;
                    try
                    {
                        btnGallery.BackgroundImage = Image.FromFile(ImageSavePath);
                    }
                    catch(Exception ex)
                    {
                        UpdateStatusMsg(ex.Message.ToString());
                    }
                    UpdateStatusMsg("图片已保存");
                }
            }
        }

        /// <summary>
        /// 初始化视频进程
        /// </summary>
        private void StartVideo()
        {
            // 开启摄像头
            if (VideoDevice == null)
            {
                FilterInfoCollection VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (VideoDevices.Count != 0)
                {
                    VideoDevice = "[0] " + VideoDevices[0].Name.ToString();
                }
                else
                {
                    UpdateStatusMsg("不存在视频输入设备！");
                    return;
                }
            }
            try
            {
                // Image Acquisition 01: Code generated by Image Acquisition 01
                HOperatorSet.OpenFramegrabber("DirectShow", 1, 1, 0, 0, 0, 0, "default", 8, "rgb",
                    -1, "false", "default", VideoDevice, 0, -1, out hv_AcqHandle);
            }
            catch (Exception ex)
            {
                OpenCloseCameraToolStripMenuItem.Tag = "0";
                OpenCloseCameraToolStripMenuItem.Checked = false;
                UpdateStatusMsg(ex.Message.ToString());
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            displayVideo = new Thread(ShowVideo);
            displayVideo.Start();
            OpenCloseCameraToolStripMenuItem.Tag = "1";
            OpenCloseCameraToolStripMenuItem.Checked = true;
            UpdateStatusMsg("已启用摄像头：" + VideoDevice);
        }
        /// <summary>
        /// 关闭摄像头并结束视频进程
        /// </summary>
        private void StopVideo()
        {
            // 关闭摄像头
            try
            {
                displayVideo.Abort();
            }
            catch (Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
                Thread.ResetAbort();
                return;
            }
            try
            {
                HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
                HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            }
            catch(Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
            }
            OpenCloseCameraToolStripMenuItem.Tag = "0";
            OpenCloseCameraToolStripMenuItem.Checked = false;
            UpdateStatusMsg("摄像头已关闭");
        }

        /// <summary>
        /// 连接指定串口COMx
        /// </summary>
        private void SerialPortConnect()
        {
            if (btnConnect.Tag.ToString() == "0")
            {
                ComDevice.PortName = toolStripComboBoxComNum.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(LastBaudRateMenuItem.Text.ToString());
                ComDevice.Parity = (Parity)Convert.ToInt32(LastParityMenuItem.Tag.ToString());
                ComDevice.DataBits = Convert.ToInt32(LastDataBitsMenuItem.Text.ToString());
                ComDevice.StopBits = (StopBits)Convert.ToInt32(LastStopBitsMenuItem.Text.ToString());
                try
                {
                    ComDevice.Open();
                }
                catch (Exception ex)
                {
                    UpdateStatusMsg(ex.Message.ToString());
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnConnect.Tag = "1";
                btnConnect.BackgroundImage = Properties.Resources.usb_on;
                UpdateStatusMsg("已连接串口：" + toolStripComboBoxComNum.SelectedItem.ToString());
            }
            else
            {
                try
                {
                    ComDevice.Close();
                }
                catch (Exception ex)
                {
                    UpdateStatusMsg(ex.Message.ToString());
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnConnect.Tag = "0";
                btnConnect.BackgroundImage = Properties.Resources.usb_off;
                UpdateStatusMsg("串口已关闭");
            }
        }
        /// <summary>
        /// 接收串口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Receive(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead]; //返回命令包
            ComDevice.Read(ReDatas, 0, ReDatas.Length); //读取数据
            UpdateStatusMsg(System.Text.Encoding.Default.GetString(ReDatas));
        }
        /// <summary>
        /// 向串口发送数据
        /// </summary>
        /// <param name="Datas"></param>
        private void Send(string Datas)
        {
            if (ComDevice.IsOpen == true)
            {
                byte[] SendBytes = null;
                string SendData = Datas;
                /*
                //16进制发送
                if(SendData.Length % 2 ==1)
                {
                    //奇数个字符
                    SendData = SendData.Remove(SendData.Length - 1, 1);
                }
                List<string> SendDataList = new List<string>();
                for (int i = 0; i < SendData.Length; i = i + 2)
                {
                    SendDataList.Add(SendData.Substring(i, 2));
                }
                SendBytes = new byte[SendDataList.Count];
                for (int j = 0; j < SendBytes.Length; j++)
                {
                    SendBytes[j] = (byte)(Convert.ToInt32(SendDataList[j], 16));
                }
                */
                SendData += "\r\n"; //发送新行
                SendBytes = System.Text.Encoding.Default.GetBytes(SendData);
                ComDevice.Write(SendBytes, 0, SendBytes.Length); //发送数据
            }
            else
            {
                UpdateStatusMsg("请连接！");
            }
        }
        private void Send(uint Datas)
        {
            if (ComDevice.IsOpen == true)
            {
                byte[] SendBytes = null;
                string SendData = Datas.ToString();
                SendData += "\r\n"; //发送新行
                SendBytes = System.Text.Encoding.Default.GetBytes(SendData);
                ComDevice.Write(SendBytes, 0, SendBytes.Length); //发送数据
            }
            else
            {
                UpdateStatusMsg("请连接！");
            }
        }
        private void Send(float Datas)
        {

        }
        private void Send(int Datas)
        {

        }

        /// <summary>
        /// 将拍摄的图片显示在相册框中
        /// </summary>
        private void ShowImage()
        {
            if (Directory.Exists(PicPath) == false)
            {
                Directory.CreateDirectory(PicPath);
            }
            try
            {
                string[] FilesList = Directory.GetFiles(PicPath, "*.png");
                if (FilesList.Length != 0)
                {
                    FilesList[FilesList.Length - 1] = FilesList[FilesList.Length - 1].Replace("\\", "/");
                    btnGallery.BackgroundImage = Image.FromFile(FilesList[FilesList.Length - 1]);
                }
                else
                {
                    UpdateStatusMsg("相册为空");
                }
            }
            catch(Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
            }
        }

        // 更新状态消息区
        public delegate void UpdateString(object NewData);
        public void UpdateStatusMsg(object NewData)
        {
            if (this.InvokeRequired)
            {
                UpdateString UpdataInvoke = new UpdateString(UpdateStatusMsg);
                this.Invoke(UpdataInvoke, new object[] { NewData });
            }
            else
            {
                textBoxStatusMsg.AppendText("[" + System.DateTime.Now.ToLongTimeString() + "] " + NewData.ToString() + "\r\n");
                textBoxStatusMsg.SelectionStart = textBoxStatusMsg.Text.Length - 1;
                textBoxStatusMsg.ScrollToCaret();
            }
        }
        // 定时器开启识别模式
        private void TimerRun(object sender, EventArgs e)
        {
            toolStripStatusLabelTime.Text = System.DateTime.Now.ToString();
            IdentificationFlag = true;
        }

        #endregion

        #region Mouse Click Events
        // 保存数据
        private void SaveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDataToolStripMenuItem.Tag = "0";
            if (Directory.Exists(LogPath) == false)
            {
                Directory.CreateDirectory(LogPath);
            }
            SaveDatasPath = LogPath + "\\Log.txt";
            try
            {
                StreamWriter SaveDatas = new StreamWriter(SaveDatasPath, true, Encoding.Default);
                SaveDatas.Write(System.DateTime.Now.ToShortDateString() + "\r\n" + textBoxStatusMsg.Text);
                SaveDatas.Flush();
                SaveDatas.Close();
            }
            catch(Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
                UpdateStatusMsg("数据保存失败");
                SaveDataToolStripMenuItem.Tag = "1";
                return;
            }
            UpdateStatusMsg("数据已保存");
            SaveDataToolStripMenuItem.Tag = "1";
        }
        // 退出
        private void QuitAPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WriteConfigFile();
            }
            catch (Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
                UpdateStatusMsg("配置文件写入失败");
            }
            if (ComDevice.IsOpen == true)
            {
                Send((uint)0x80); // 通知下位机停止工作
            }
            if (OpenCloseCameraToolStripMenuItem.Tag.ToString() == "1")
            {
                StopVideo();
            }
            SaveDataToolStripMenuItem.Tag = "0";
            SaveDataToolStripMenuItem.PerformClick(); // 保存日志
            while (SaveDataToolStripMenuItem.Tag.ToString() == "0") ;
            try
            {
                System.Environment.Exit(0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 计算器
        private void CalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "calc.exe"
            };
            System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(Info);
        }
        // 记事本
        private void NotePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "notepad.exe"
            };
            System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(Info);
        }

        // 设置波特率为115200
        private void toolStripMenuItemBPS115200_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为57600
        private void toolStripMenuItemBPS57600_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为38400
        private void toolStripMenuItemBPS38400_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为19200
        private void toolStripMenuItemBPS19200_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为9600
        private void toolStripMenuItemBPS9600_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为4800
        private void toolStripMenuItemBPS4800_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为2400
        private void toolStripMenuItemBPS2400_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置波特率为1200
        private void toolStripMenuItemBPS1200_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastBaudRateMenuItem.Checked = false;
                LastBaudRateMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastBaudRateMenuItem = NowToolStripMenuItem;

                ComDevice.BaudRate = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置校验位为None
        private void PRTnoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastParityMenuItem = NowToolStripMenuItem;

                ComDevice.Parity = (Parity)Convert.ToInt32(NowToolStripMenuItem.Tag.ToString());
            }
        }
        // 设置校验位为Odd
        private void PRToddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastParityMenuItem = NowToolStripMenuItem;

                ComDevice.Parity = (Parity)Convert.ToInt32(NowToolStripMenuItem.Tag.ToString());
            }
        }
        // 设置校验位为Even
        private void PRTevenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastParityMenuItem = NowToolStripMenuItem;

                ComDevice.Parity = (Parity)Convert.ToInt32(NowToolStripMenuItem.Tag.ToString());
            }
        }
        // 设置校验位为Mark
        private void PRTmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastParityMenuItem = NowToolStripMenuItem;

                ComDevice.Parity = (Parity)Convert.ToInt32(NowToolStripMenuItem.Tag.ToString());
            }
        }
        // 设置校验位为Space
        private void PRTspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastParityMenuItem.Checked = false;
                LastParityMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastParityMenuItem = NowToolStripMenuItem;

                ComDevice.DataBits = Convert.ToInt32(NowToolStripMenuItem.Tag.ToString());
            }
        }
        // 设置数据位为8位
        private void toolStripMenuItemDTB8_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastDataBitsMenuItem = NowToolStripMenuItem;

                ComDevice.DataBits = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置数据位为7位
        private void toolStripMenuItemDTB7_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastDataBitsMenuItem = NowToolStripMenuItem;

                ComDevice.DataBits = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置数据位为6位
        private void toolStripMenuItemDTB6_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastDataBitsMenuItem.Checked = false;
                LastDataBitsMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastDataBitsMenuItem = NowToolStripMenuItem;

                ComDevice.DataBits = Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置停止位为1位
        private void toolStripMenuItemSTP1_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastStopBitsMenuItem.Checked = false;
                LastStopBitsMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastStopBitsMenuItem = NowToolStripMenuItem;

                ComDevice.StopBits = (StopBits)Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 设置停止位为2位
        private void toolStripMenuItemSTP2_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem NowToolStripMenuItem = (ToolStripMenuItem)sender;
                LastStopBitsMenuItem.Checked = false;
                LastStopBitsMenuItem.Enabled = true;
                NowToolStripMenuItem.Checked = true;
                NowToolStripMenuItem.Enabled = false;
                LastStopBitsMenuItem = NowToolStripMenuItem;

                ComDevice.StopBits = (StopBits)Convert.ToInt32(NowToolStripMenuItem.Text.ToString());
            }
        }
        // 更改摄像头开关状态
        private void OpenCloseCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenCloseCameraToolStripMenuItem.Tag.ToString() == "1")
            {
                StopVideo();
            }
            else if (OpenCloseCameraToolStripMenuItem.Tag.ToString() == "0")
            {
                StartVideo();
            }
        }

        // 关于
        private void AboutMeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Menus.AboutMe().ShowDialog();
        }

        // 设备搜索
        private void DeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            toolStripComboBoxComNum.Items.Clear();
            foreach (string port in ports)
            {
                toolStripComboBoxComNum.Items.Add(port);
            }
            if (toolStripComboBoxComNum.Items.Count > 0)
            {
                toolStripComboBoxComNum.SelectedIndex = 0;
            }
            else
            {
                UpdateStatusMsg("无可用串口设备！");
            }
        }

        // 连接串口设备
        private void btnCom_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxComNum.Items.Count == 0)
            {
                UpdateStatusMsg("无可用串口设备！");
                return;
            }
            SerialPortConnect();
        }
        // 拍照
        private void btnTakePhoto_Click(object sender, EventArgs e)
        {
            if (OpenCloseCameraToolStripMenuItem.Tag.ToString() == "0")
            {
                MessageBox.Show("请先打开摄像头！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // 设置图片保存位置及文件名，格式为png
            if (Directory.Exists(PicPath) == false)
            {
                Directory.CreateDirectory(PicPath);
            }
            ImageSaveName = System.DateTime.Now.ToString();
            ImageSaveName = ImageSaveName.Replace("/", "");
            ImageSaveName = ImageSaveName.Replace(" ", "");
            ImageSaveName = ImageSaveName.Replace(":", "");
            ImageSavePath = PicPath.Replace("\\", "/") + "/" + ImageSaveName + ".png";

            TakePhotoFlag = true;
        }
        // 打开相册文件夹
        private void btnGallery_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(PicPath) == false)
            {
                Directory.CreateDirectory(PicPath);
            }
            try
            {
                System.Diagnostics.Process.Start("explorer", PicPath);
            }
            catch (Exception ex)
            {
                UpdateStatusMsg(ex.Message.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        // 清空状态消息区
        private void btnCleanMsg_Click(object sender, EventArgs e)
        {
            SaveDataToolStripMenuItem.Tag = "0";
            SaveDataToolStripMenuItem.PerformClick();
            while (SaveDataToolStripMenuItem.Tag.ToString() == "0") ;
            textBoxStatusMsg.Text = "";
        }

        #endregion
    }
}
