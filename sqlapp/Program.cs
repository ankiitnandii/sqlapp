using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

string strFilePath = "E:\\Webapp\\Log.csv";
StreamReader? logReader = null;
int Id = 1;
SqlConnection appdbConnection =
    new SqlConnection("Server=tcp:logdatawoorkspaace.database.windows.net,1433;Initial Catalog=logdatadb;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

SqlParameter paramId = new SqlParameter();
paramId.ParameterName = "@Id";

SqlParameter paramOperationname = new SqlParameter();
paramOperationname.ParameterName = "@Operationname";


SqlParameter paramStatus = new SqlParameter();
paramStatus.ParameterName = "@Status";


SqlParameter paramEventcategory = new SqlParameter();
paramEventcategory.ParameterName = "@Eventcategory";

SqlParameter paramResourcetype = new SqlParameter();
paramResourcetype.ParameterName = "@Resourcetype";

SqlParameter paramResource = new SqlParameter();
paramResource.ParameterName = "@Resource";

try
{
    appdbConnection.Open();

    if (File.Exists(strFilePath))
    {
        logReader = new StreamReader(strFilePath);

        SqlCommand logdataCmd = new SqlCommand();
        logdataCmd.CommandType = CommandType.Text;
        logdataCmd.Connection = appdbConnection;

        logdataCmd.Parameters.Add(paramId);
        logdataCmd.Parameters.Add(paramOperationname);
        logdataCmd.Parameters.Add(paramStatus);
        logdataCmd.Parameters.Add(paramEventcategory);
        logdataCmd.Parameters.Add(paramResourcetype);
        logdataCmd.Parameters.Add(paramResource);

        while (!logReader.EndOfStream)
        {
            var logDataValues = logReader.ReadLine().Split(',');

            paramId.Value = Id++;
            paramOperationname.Value = logDataValues[0];
            paramStatus.Value = logDataValues[1];
            paramEventcategory.Value = logDataValues[2];
            paramResourcetype.Value = logDataValues[3];
            paramResource.Value = logDataValues[4];

            logdataCmd.CommandText = "INSERT INTO [logdata] (Id,Operationname,Status,Eventcategory,Resourcetype,Resource) VALUES" +
                " (@Id,@Operationname,@Status,@Eventcategory,@Resourcetype,@Resource)";

            logdataCmd.ExecuteNonQuery();
            Console.WriteLine("Written Record {0}", (Id - 1));


        }
    }
}
catch (SqlException e)
{
    Console.WriteLine(e.ToString());
    appdbConnection.Close();
}
