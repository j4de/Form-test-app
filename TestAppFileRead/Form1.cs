using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestAppFileRead
{
    public partial class Form1 : Form
    {
        //Procmon data
        private int PM_TimeOfDay = 1;
        private int PM_ProcessName = 3;
        private int PM_lengthFromSmallArray = 4;
        private int PM_PID = 5;
        private int PM_Operation = 7;
        private int PM_Path = 9;
        private int PM_FileName = 10;
        private int PM_Detail = 13;
        private int PM_Length = 14;

        //DataGridView data
        private int DG_Name = 1;
        private int DG_PID = 2;
        private int DG_FileName = 3;
        private int DG_Operation = 4;
        private int DG_Length = 5;
        private int DG_Path = 6;

        private List<string> operationList = new List<string>();
        
        //To parse the Length value from the column => details
        public static Regex regexLength = new Regex(@"Length:\s*(?<getLengthNum>\d+(,\d+)*)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        //public enum OperationType {
        //                All_Operations,
        //                CancelRemoveDevice,
        //                CancelStopDevice,
        //                CloseFile,
        //                CreateFile,
        //                CreateFileMapping,
        //                CreateMailSlot,
        //                CreatePipe,
        //                DebugOutputProfiling,
        //                DeviceChange,
        //                DeviceIoControl,
        //                DeviceUsageNotification,
        //                Eject,
        //                FileSystemControl,
        //                FilterResourceRequirements,
        //                FlushBuffersFile,
        //                InternalDeviceIoControl,
        //                LoadImage,
        //                LockFile,
        //                NotifyChangeDirectory,
        //                Power,
        //                ProcessCreate,
        //                ProcessExit,
        //                ProcessProfiling,
        //                ProcessStart,
        //                ProcessStatistics,
        //                QueryAllInformationFile,
        //                QueryAttributeCacheInformation,
        //                QueryAttributeInformationVolume,
        //                QueryAttributeTag,
        //                QueryAttributeTagFile,
        //                QueryBasicInformationFile,
        //                QueryBusInformation,
        //                QueryCapabilities,
        //                QueryCompressionInformationFile,
        //                QueryControlInformationVolume,
        //                QueryDesiredStorageClassInformation,
        //                QueryDeviceInformationVolume,
        //                QueryDeviceRelations,
        //                QueryDeviceText,
        //                QueryDirectory,
        //                QueryEAFile,
        //                QueryEaInformationFile,
        //                QueryEndOfFile,
        //                QueryFileInternalInformationFile,
        //                QueryFileQuota,
        //                QueryFullSizeInformationVolume,
        //                QueryHardLinkFullIdInformation,
        //                QueryId,
        //                QueryIdBothDirectory,
        //                QueryIdExtdBothDirectoryInformation,
        //                QueryIdExtdDirectoryInformation,
        //                QueryIdGlobalTxDirectoryInformation,
        //                QueryIdInformation,
        //                QueryInformationVolume,
        //                QueryInterface,
        //                QueryIoPiorityHint,
        //                QueryIsRemoteDeviceInformation,
        //                QueryLabelInformationVolume,
        //                QueryLegacyBusInformation,
        //                QueryLinkInformationBypassAccessCheck,
        //                QueryLinks,
        //                QueryMemoryPartitionInformation,
        //                QueryMoveClusterInformationFile,
        //                QueryNameInformationFile,
        //                QueryNetworkOpenInformationFile,
        //                QueryNetworkPhysicalNameInformationFile,
        //                QueryNormalizedNameInformationFile,
        //                QueryNumaNodeInformation,
        //                QueryObjectIdInformationVolume,
        //                QueryOpen,
        //                QueryPnpDeviceState,
        //                QueryPositionInformationFile,
        //                QueryRemoteProtocolInformation,
        //                QueryRemoveDevice,
        //                QueryRenameInformationBypassAccessCheck,
        //                QueryResourceRequirements,
        //                QueryResources,
        //                QuerySecurityFile,
        //                QueryShortNameInformationFile,
        //                QuerySizeInformationVolume,
        //                QueryStandardInformationFile,
        //                QueryStandardLinkInformation,
        //                QueryStatInformation,
        //                QueryStopDevice,
        //                QueryStreamInformationFile,
        //                QueryValidDataLength,
        //                QueryVolumeNameInformation,
        //                ReadConfig,
        //                ReadFile,
        //                RegCloseKey,
        //                RegCreateKey,
        //                RegDeleteKey,
        //                RegDeleteValue,
        //                RegEnumKey,
        //                RegEnumValue,
        //                RegFlushKey,
        //                RegLoadKey,
        //                RegOpenKey,
        //                RegQueryKey,
        //                RegQueryKeySecurity,
        //                RegQueryMultipleValueKey,
        //                RegQueryValue,
        //                RegRenameKey,
        //                RegSetInfoKey,
        //                RegSetKeySecurity,
        //                RegSetValue,
        //                RegUnloadKey,
        //                RemoveDevice,
        //                SetAllocationInformationFile,
        //                SetBasicInformationFile,
        //                SetDispositionInformationEx,
        //                SetDispositionInformationFile,
        //                SetEAFile,
        //                SetEndOfFileInformationFile,
        //                SetFileQuota,
        //                SetFileStreamInformation,
        //                SetLinkInformationFile,
        //                SetLock,
        //                SetPipeInformation,
        //                SetPositionInformationFile,
        //                SetRenameInformationEx,
        //                SetRenameInformationExBypassAccessCheck,
        //                SetRenameInformationFile,
        //                SetReplaceCompletionInformation,
        //                SetSecurityFile,
        //                SetShortNameInformation,
        //                SetValidDataLengthInformationFile,
        //                SetVolumeInformation,
        //                Shutdown,
        //                StartDevice,
        //                StopDevice,
        //                SurpriseRemoval,
        //                SystemStatistics,
        //                SystemControl,
        //                TCPAccept,
        //                TCPConnect,
        //                TCPDisconnect,
        //                TCPOther,
        //                TCPReceive,
        //                TCPReconnect,
        //                TCPRetransmit,
        //                TCPSend,
        //                TCPTCPCopy,
        //                TCPUnknown,
        //                ThreadCreate,
        //                ThreadExit,
        //                ThreadProfile,
        //                ThreadProfiling,
        //                UDPAccept,
        //                UDPConnect,
        //                UDPDisconnect,
        //                UDPOther,
        //                UDPReceive,
        //                UDPReconnect,
        //                UDPRetransmit,
        //                UDPSend,
        //                UDPTCPCopy,
        //                UDPUnknown,
        //                UnlockFileAll,
        //                UnlockFileByKey,
        //                UnlockFileSingle,
        //                VolumeDismount,
        //                VolumeMount,
        //                WriteConfig,
        //                WriteFile
        //}
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            //OperationComboBox.DataSource = Enum.GetNames(typeof(OperationType));
            //OperationComboBox.SelectedIndex = 0;

        }
       
        //Get the file path selected.
        //Filter setup for CSV files in Form1.designer.cs
        private void GetFileButton(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = (openFileDialog1.FileName);
            }

        }


        //Only setup to Read in the data from a Procmon CSV file
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time");
            dataTable.Columns.Add("Process Name");
            dataTable.Columns.Add("PID");
            dataTable.Columns.Add("File Name");
            dataTable.Columns.Add("Operation");
            dataTable.Columns.Add("Length");
            dataTable.Columns.Add("Path");

            string filePath = textBoxFilePath.Text;

            //try
            //{
                StreamReader streamReader = new StreamReader(filePath);
                string[] totalData = new string[File.ReadAllLines(filePath).Length];

                string megabyteOrKilobyte;
                long fileSize;
                long stringSize;
                long progress = 0;


                FindFileSize(filePath, out megabyteOrKilobyte, out fileSize);
                //progress bar
                loadingForm load = new loadingForm(megabyteOrKilobyte, dataGridView1, this, fileSize, progress);
                load.Show();
                //Reset the data grid
                dataGridView1.DataSource = null;

                if (fileSize != 0)
                {
                    string line = streamReader.ReadLine();
                    totalData = line.Split('"');
                    while (!streamReader.EndOfStream)
                    {

                        totalData = ParseProcmonData(streamReader);
                        //get the offset value

                        stringSize = line.Length;
                        progress += stringSize;
                        Update();
                        load.setprogress(progress);
                        if (load.iscanceled())
                        {
                            load.Close();
                            break;
                        }
                        Application.DoEvents();

                        //to deal with arrays of different sizes
                        if (totalData.Length == 15)
                        {
                            dataTable.Rows.Add(
                                        totalData[PM_TimeOfDay],
                                        totalData[PM_ProcessName],
                                        totalData[PM_PID],
                                        totalData[PM_FileName],
                                        totalData[PM_Operation],
                                        totalData[PM_Length],
                                        totalData[PM_Path]
                                        );
                        }
                        if (totalData.Length == 14)
                        {
                            dataTable.Rows.Add(
                                        totalData[PM_TimeOfDay],
                                        totalData[PM_ProcessName],
                                        totalData[PM_PID],
                                        totalData[PM_FileName],
                                        totalData[PM_Operation],
                                        totalData[PM_lengthFromSmallArray],
                                        totalData[PM_Path]
                                      );
                        }

                    }

                    //If file load is not cancelled
                    ProcessFileData(dataTable, load);

                }
        
            //}//end try
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //}

}

      
        private void ProcessFileData(DataTable dataTable, loadingForm load)
        {
            if (!load.iscanceled())
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dataTable;
                FindLengthForEachProcess();
                load.Close();
            }
        }

        private string[] ParseProcmonData(StreamReader streamReader)
        {
            
            string defaultLengthValue = "0";
            string defaultPath = "noFilePathFound";
            string[] totalData = streamReader.ReadLine().Split('"');
            if (totalData.Length == 14)
            {

                foreach (var item in totalData)
                {

                    try
                    {
                        totalData[PM_Operation] = totalData[PM_Operation].Replace(" ", "");

                        if (totalData[PM_FileName] == "")
                        {
                            totalData[PM_FileName] = defaultPath;
                        }
                        else
                            totalData[PM_FileName] = Path.GetFileName(totalData[PM_Path]).ToString();

                        //get value of length and remove any comma's
                        var match = regexLength.Match(totalData[PM_Detail]);
                        if (match.Success)
                        {
                            totalData[PM_lengthFromSmallArray] = match.Groups["getLengthNum"].Value;
                            totalData[PM_lengthFromSmallArray] = totalData[PM_Length].Replace(",", "");
                        }
                        else
                        {
                            totalData[PM_lengthFromSmallArray] = defaultLengthValue;
                        }
                    }

                    catch (IncorrectFormatException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
      
            }
            else
                foreach (var item in totalData)
                {

                    try
                    {

                        
                        if (totalData.Length > 12)
                        {
                            totalData[PM_Operation] = totalData[PM_Operation].Replace(" ", "");
                            if (totalData[PM_FileName] == "")
                            {
                                totalData[PM_FileName] = defaultPath;
                            }
                            else
                                totalData[PM_FileName] = Path.GetFileName(totalData[PM_Path]).ToString();

                            //get value of length and remove any comma's
                            var match = regexLength.Match(totalData[PM_Detail]);
                            if (match.Success)
                            {
                                totalData[PM_Length] = match.Groups["getLengthNum"].Value;
                                totalData[PM_Length] = totalData[PM_Length].Replace(",", "");
                            }
                            else
                            {
                                totalData[PM_Length] = defaultLengthValue;
                            }
                        }
                      
                       
                    }

                    catch (IncorrectFormatException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            return totalData;
        }

        private void SaveToCSV(DataGridView DGVfiles)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = ""
            };

            int counter = 0;

            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                Deleteifexists(filename);
                int columnCount = DGVfiles.ColumnCount;
                string columnNames = "";

                SaveOutputTotalFiles(DGVfiles, filename, ref counter, columnCount, ref columnNames);
            }
        }

        private static void SaveOutputTotalFiles(DataGridView DGV, string filename, ref int counter, int columnCount, ref string columnNames)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {

                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DGV.Columns[i].Name.ToString() + ",";
                }
                sw.WriteLine(columnNames);
                for (int i = 1; (i - 1) < DGV.RowCount; i++)
                {
                    string rowdata = "";
                    for (int j = 0; j < columnCount; j++)
                    {

                        rowdata += DGV.Rows[i - 1].Cells[j].Value.ToString() + ",";
                        counter++;
                    }
                    sw.WriteLine(rowdata);
                    //break out of loop to avoid null object reference
                    if (i == DGV.RowCount - 1)
                        break;
                }
            }
            
        }

        private static void Deleteifexists(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                }
            }
        }


        //Convert file size for user information
        private static void FindFileSize(string filePath, out string megabyteOrKilobyte, out long fileSize)
        {
            //Get the size of the file and append Gigabyte, magabyte or kilobyte
            megabyteOrKilobyte = "";
            FileInfo f = new FileInfo(filePath);
            fileSize = f.Length;
            //if (fileSize > 1073741824)
            //{
            //    fileSize = fileSize / 1073741824;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Gigabytes";
            //}
            //else if (fileSize > 1048576)
            //{
            //    fileSize = fileSize / 1048576;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Megabytes";
            //}
            //else if (fileSize > 1024)
            //{
            //    fileSize = fileSize / 1024;
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + " Kilobytes";
            //}
            //else
            //{
            //    fileSize.ToString();
            //    megabyteOrKilobyte = fileSize + "bytes";
            //}
        }

        //Calculate the size/length of each process
        private void FindLengthForEachProcess()
        {
            var ProcessFileList = new List<ProcessData>();
            var ProcessIDList = new List<ProcessData>();
            
            int[] topLengths = new int[10];
            int[] topIDLengths = new int[10];
            string[] topFileNames = new string[10];
            string[] topProcessID = new string[10];
            
            int processLength = 0;
            string processName = "";
            string processFileName = "";
            string processPath = "";
            string processPID = "";
            string processOperation = "";
            bool processFound = false;
            int loopCounter = 0;
            string processKeyString = "";
            //Var for bar chart
            string processKeyID = "";
            bool processIDFound = false;

            //find the rows with the matching process key and then add the length for
            //each of these rows together and select the top ten values 
            //to populate the bar chart
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    processFileName = row.Cells[DG_FileName].Value.ToString();
                    processName = row.Cells[DG_Name].Value.ToString();
                    processOperation = row.Cells[DG_Operation].Value.ToString();
                    processPID = row.Cells[DG_PID].Value.ToString();
                    processPath = row.Cells[DG_Path].Value.ToString();
                    int iPL = 0;
                    int.TryParse(row.Cells[DG_Length].Value.ToString(), out iPL);
                    processLength = iPL;

                    

                    processKeyString = processName + "|" + processPID + "|" + processPath;
                    processKeyID = processName + "|" + processPID;

                    AddNewItemToList(ProcessFileList, processLength, processName, processFileName, processPath, processPID, processOperation, processFound, processKeyString);
                    //If the process processKey is found in the list
                    //append the length value
                    processFound = AppendLength(ProcessFileList, processLength, processKeyString);

                    AddNewItemToList(ProcessIDList, processLength, processName, processFileName, processPath, processPID, processOperation, processIDFound, processKeyID);

                    processIDFound = AppendLength(ProcessIDList, processLength, processKeyID);

                    ComboBoxListItems(operationList, processOperation);


                    loopCounter++;
                    if (loopCounter == dataGridView1.RowCount - 1)
                        break;

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }

           

            SortedFileList(ProcessFileList);

            SortedProcessList(ProcessIDList);

            FilterOnCombobox(ProcessFileList, ProcessIDList, topLengths, topIDLengths, topFileNames, topProcessID);

            PopulateChart(topLengths, topIDLengths, topFileNames, topProcessID);

            totalProcessesLabel.Text = ProcessFileList.Count().ToString();


        }

        private void ComboBoxListItems(List<string> operationList, string processOperation)
        {
            
            bool alreadyExists = operationList.Any(x => x.Contains(processOperation));
            if (alreadyExists == false)
            {
                operationList.Add(processOperation);
                OperationComboBox.Items.Add(processOperation);
            }
 
        }

        //add a new process to the list
        private static void AddNewItemToList(List<ProcessData> ProcessFileList, int processLength, string processName, string processFileName, string processPath, string processPID, string processOperation, bool processFound, string keyString)
        {
            if (processFound == false)
            {
                ProcessFileList.Add(new ProcessData
                {
                    ProcessLength = processLength,
                    ProcessName = processName,
                    Operation = processOperation,
                    ProcessPID = processPID,
                    ProcessPath = processPath,
                    ProcessKey = keyString,
                    ProcessFileName = processFileName
                });
            }
            
        }

        //Append the length value
        private static bool AppendLength(List<ProcessData> fileList, int length, string keyString)
        {
            bool processFound = false;
            foreach (var item in fileList)
            {
                if (item.ProcessKey == keyString)
                {

                    item.ProcessLength += length;
                    processFound = true;
                    break;
                }


            }

            return processFound;
        }

        private void FilterOnCombobox(List<ProcessData> ProcessFileList, List<ProcessData> ProcessIDList, int[] topLengths, int[] topIDLengths, string[] topFileNames, string[] topProcessID)
        {


            if (OperationComboBox.SelectedItem == null)
            {
                int i = operationList.FindIndex(x => x.StartsWith("WriteFile"));
                if (operationList.Contains("WriteFile"))
                {
                    OperationComboBox.SelectedIndex = i;
                }
                else
                {
                    OperationComboBox.SelectedIndex = 0;
                }
            }
            string operationName = OperationComboBox.SelectedItem.ToString();
            OperationComboBox_SelectedIndexChanged(ProcessFileList, ProcessIDList);
            GetDataForCharts(ProcessFileList, ProcessIDList, topLengths, topIDLengths, topFileNames, topProcessID);

        }

        private void SortedFileList(List<ProcessData> ProcessFileList)
        {
            List<ProcessData> SortedFileList = ProcessFileList.OrderByDescending(o => o.ProcessLength).ToList();

            DataTable filteredTable = new DataTable();
            filteredTable.Columns.Add("Process Name");
            filteredTable.Columns.Add("PID");
            filteredTable.Columns.Add("File Name");
            filteredTable.Columns.Add("Operation");
            filteredTable.Columns.Add("Length");
            filteredTable.Columns.Add("Path");

            for (int i = 0; i < SortedFileList.Count; i++)
            {
                filteredTable.Rows.Add(
                                SortedFileList[i].ProcessName,
                                SortedFileList[i].ProcessPID,
                                SortedFileList[i].ProcessFileName,
                                SortedFileList[i].Operation,
                                SortedFileList[i].ProcessLength,
                                SortedFileList[i].ProcessPath
                              );
            }
            dataGridView1.DataSource = filteredTable;

        }
        private void SortedProcessList(List<ProcessData> ProcessIDList)
        {
           

            List<ProcessData> SortedProcessList = ProcessIDList.OrderByDescending(o => o.ProcessLength).ToList();

            DataTable filteredTableProcesses = new DataTable();
            filteredTableProcesses.Columns.Add("Process Name");
            filteredTableProcesses.Columns.Add("PID");
            filteredTableProcesses.Columns.Add("File Name");
            filteredTableProcesses.Columns.Add("Operation");
            filteredTableProcesses.Columns.Add("Length");
            filteredTableProcesses.Columns.Add("Path");

            for (int i = 0; i < SortedProcessList.Count; i++)
            {
                filteredTableProcesses.Rows.Add(
                                SortedProcessList[i].ProcessName,
                                SortedProcessList[i].ProcessPID,
                                SortedProcessList[i].ProcessFileName,
                                SortedProcessList[i].Operation,
                                SortedProcessList[i].ProcessLength,
                                SortedProcessList[i].ProcessPath
                              );
            }
            dataGridViewProcesses.DataSource = filteredTableProcesses;
        }

        //Add the top ten files and lengths
        private void GetDataForCharts(List<ProcessData> ProcessFileList, List<ProcessData> ProcessIDList, int[] topLengths, int[] topIDLengths, string[] topFileNames, string[] topProcessID)
        {

            string operationValue = OperationComboBox.SelectedItem.ToString();
            var query = from ProcessData data in ProcessFileList
                        where data.Operation == operationValue
                        orderby data.ProcessLength descending
                        select data;
            var topTenIDList = (query.OrderByDescending(i => i.ProcessLength).Take(10));

            var query2 = from ProcessData data in ProcessIDList
                         where data.Operation == operationValue
                        orderby data.ProcessLength descending
                        select data;
            var topTenList = (query2.OrderByDescending(i => i.ProcessLength).Take(10));
            
            int topListCounter = 0;
            foreach (var item in topTenIDList)
            {
                        topLengths[topListCounter] = Convert.ToInt32(item.ProcessLength);
                        topFileNames[topListCounter] = item.ProcessFileName;

                topListCounter++;
            }

            int topProcessCounter = 0;
            foreach (var item in topTenList)
            {
                        //Convert byte to kilobyte
                        topIDLengths[topProcessCounter] = Convert.ToInt32(item.ProcessLength);
                        topProcessID[topProcessCounter] = item.ProcessName + " " + item.ProcessPID;

              
                topProcessCounter++;

            }
            
        }

        private void PopulateChart(int[] topLengths, int[] topIDLengths, string[] topProcessNames, string[] topProcessID)
        {
            //BarChart1.Series.Add(series);
            BarChart1.Series[0].ChartType = SeriesChartType.RangeColumn;
            BarChart1.Series[0].Points.DataBindXY(new[] {topProcessNames[0], topProcessNames[1],topProcessNames[2],topProcessNames[3],topProcessNames[4],
                                                         topProcessNames[5], topProcessNames[6],topProcessNames[7],topProcessNames[8],topProcessNames[9]},
                                                  new[] {topLengths[0], topLengths[1], topLengths[2], topLengths[3], topLengths[4],
                                                         topLengths[5], topLengths[6], topLengths[7], topLengths[8], topLengths[9], });
            BarChart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            BarChart1.ChartAreas[0].AxisX.Interval = 1;
            BarChart1.Legends[0].Enabled = true;
            BarChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            BarChart1.ChartAreas[0].AxisY.Title = "Bytes Used";
            //BarChart1.ChartAreas[0].AxisY.TitleFont="Arial", 16, FontStyle.Bold;
            BarChart1.Legends[0].Enabled = true;

            //3D Pie chart
            pieChart1.Series[0].ChartType = SeriesChartType.Pie;
            pieChart1.Series[0].Points.DataBindXY(topProcessID, topIDLengths);
            pieChart1.Legends[0].Enabled = true;
            pieChart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        private void ExitButton(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton(object sender, EventArgs e)
        {
            SaveToCSV(dataGridView1);
            SaveToCSV(dataGridViewProcesses);
        }

        private void OperationComboBox_SelectedIndexChanged(List<ProcessData> dataFileList, List<ProcessData> dataProcessesList)
        {
            string operationValue = OperationComboBox.SelectedItem.ToString();
                           var query = from ProcessData data in dataFileList
                            where data.Operation == operationValue
                            orderby data.ProcessLength descending
                            select data;
                
                //files
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = query.ToList();

                //processes 
                dataProcessesList = query.GroupBy(x => x.ProcessName).Select(x => x.First()).ToList();
                dataGridViewProcesses.DataSource = null;
                dataGridViewProcesses.DataSource = dataProcessesList;
            
        }

        
    }
}
