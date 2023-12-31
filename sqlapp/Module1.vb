Imports System.Data.SqlClient

Using System.Data;
Using System.Data.SqlClient;
Using System.Reflection.Metadata;

String strFilePath = "C:\\tmp3\\Log.csv";
StreamReader? logReader = null;
int Id = 1;
SqlConnection appdbConnection =
    New SqlConnection("Server=tcp:appserver344343.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

SqlParameter paramId = New SqlParameter();
paramId.ParameterName = "@Id";

SqlParameter paramOperationname = New SqlParameter();
paramOperationname.ParameterName = "@Operationname";


SqlParameter paramStatus = New SqlParameter();
paramStatus.ParameterName = "@Status";


SqlParameter paramEventcategory = New SqlParameter();
paramEventcategory.ParameterName = "@Eventcategory";

SqlParameter paramResourcetype = New SqlParameter();
paramResourcetype.ParameterName = "@Resourcetype";

SqlParameter paramResource = New SqlParameter();
paramResource.ParameterName = "@Resource";

Try
{
    appdbConnection.Open();    
    
    If (File.Exists(strFilePath))
    {
        logReader = New StreamReader(strFilePath);
        
        SqlCommand logdataCmd = New SqlCommand();
        logdataCmd.CommandType = CommandType.Text;
        logdataCmd.Connection = appdbConnection;

        logdataCmd.Parameters.Add(paramId);
        logdataCmd.Parameters.Add(paramOperationname);
        logdataCmd.Parameters.Add(paramStatus);
        logdataCmd.Parameters.Add(paramEventcategory);
        logdataCmd.Parameters.Add(paramResourcetype);
        logdataCmd.Parameters.Add(paramResource);

        While (!logReader.EndOfStream)
        {
            var logDataValues = logReader.ReadLine().Split(',');

paramId.Value = Id + +;
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
Catch (SqlException e)
{
    Console.WriteLine(e.ToString());
    appdbConnection.Close();
}

