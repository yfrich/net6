using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Pipelines.Sockets.Unofficial.Arenas;
using System.Data;

namespace Identity框架.SignalR案例导入英汉词典
{
    public class ImportExecutor
    {
        private readonly IHubContext<ImportDictHub> hubContext;

        public ImportExecutor(IHubContext<ImportDictHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task ExecuteAsync(string connectionId)
        {
            try
            {
                await DoExecuteAsync(connectionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导入异常：{ex}");
            }
        }

        public async Task DoExecuteAsync(string connectionId)
        {

            string[] lines = File.ReadAllLines(@"F:\于富\git管理代码\aspNetCore\Part5\stardict.csv");
            int totalCount = lines.Length - 1;//跳过表头 总行数
            string connStr = "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=aspnetcoreef; Integrated Security=SSPI;Encrypt=false;";
            SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr);
            bulkCopy.DestinationTableName = "T_WordItems";
            bulkCopy.ColumnMappings.Add("Word", "Word");
            bulkCopy.ColumnMappings.Add("Phonetic", "Phonetic");
            bulkCopy.ColumnMappings.Add("Definition", "Definition");
            bulkCopy.ColumnMappings.Add("Translation", "Translation");

            using DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Word");
            dataTable.Columns.Add("Phonetic");
            dataTable.Columns.Add("Definition");
            dataTable.Columns.Add("Translation");
            int counter = 0;
            foreach (string line in lines)
            {
                string[] strs = line.Split(',');
                string word = strs[0];
                string? phonetic = strs[1];
                string? definition = strs[2];
                string? translation = strs[3];
                DataRow row = dataTable.NewRow();
                row["Word"] = word;
                row["Phonetic"] = phonetic;
                row["Definition"] = definition;
                row["Translation"] = translation;
                dataTable.Rows.Add(row);
                counter++;
                if (dataTable.Rows.Count == 2000)
                {
                    await bulkCopy.WriteToServerAsync(dataTable);
                    dataTable.Clear();
                    await hubContext.Clients.Client(connectionId).SendAsync("ImportProgress", totalCount, counter);
                }
            }

            await bulkCopy.WriteToServerAsync(dataTable);
        }
    }
}
