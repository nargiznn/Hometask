

using Lesson1;
using System.Collections.Generic;
using System.Data.SqlClient;

string opt;
do
{
    Console.WriteLine("1. Create group");
    Console.WriteLine("2. Get group by Id");
    Console.WriteLine("3. Get all groups");
    Console.WriteLine("4.Delete");
    Console.WriteLine("0. Exit");
    List<Group> groups = new List<Group>();
    Console.WriteLine("Select an operation: ");
    opt = Console.ReadLine();

    switch (opt)
    {
        case "1":
            Console.WriteLine("\nEnter new group\n============");
            Console.Write("no: ");
            string no = Console.ReadLine();
            Console.Write("limit: ");
            byte limit = Convert.ToByte (Console.ReadLine());

            InsertGroup(no, limit);
           
            break;
        case "2":
            Console.WriteLine("\nEnter selected group id\n============");
            Console.Write("id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var data = GetGroupById(id);

            if (data == null) Console.WriteLine("Group not found");
            else Console.WriteLine(data);
            break;
        case "3":
            Console.WriteLine("\nAll groups\n============");
            foreach (var item in GetAllGroups())
            {
                Console.WriteLine(item);
            }
            break;
            case "4":
            Console.WriteLine("Delete ");
            Console.WriteLine("\nEnter delete group\n=====");
            Console.Write("id: ");
            string deleteno = Console.ReadLine();

         

            break;
        case "0":
            Console.WriteLine("Finished");
            break;
        default:
            Console.WriteLine("Wrong operation!");
            break;
    }

} while (opt!="0");


void InsertGroup(string no,byte limit)
{
    string connectionStr = "ServerMOON10\\MAINDB;Database=coursedb;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        connection.Open();
        string query = "insert into Groups (No,Limit) values (@no,@limit)";
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@limit", limit);
            cmd.ExecuteNonQuery();
        }
    }
}
void DeleteGroup(string id)
{
    string connectionStr = "ServerMOON10\\MAINDB;Database=coursedb;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        connection.Open();
        string query = "delete into Groups (Id) values (@Id)";
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {

            cmd.ExecuteNonQuery();
        }
    }

}

List<Group> GetAllGroups()
{
    List<Group> groups = new List<Group>(); 
    string connectionStr = "ServerMOON10\\MAINDB;Database=coursedb;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        connection.Open();
        string query = "select Id, No, Limit from Groups";
        SqlCommand cmd = new SqlCommand(query,connection);

        using(SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Group grp = new Group
                {

                    Id = reader.GetInt32(0),
                    No = reader.GetString(1),
                    Limit = reader.GetByte(2)
                };
                groups.Add(grp);
            }
        }
    }
    return groups;
}

Group GetGroupById(int id)
{
    Group group = null;
    string connectionStr = "ServerMOON10\\MAINDB;Database=coursedb;Trusted_Connection=true";
    using(SqlConnection connection = new SqlConnection(connectionStr))
    {
        connection.Open();
        string query = "select TOP(1) * from Groups where Id=@id";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        using(var reader = cmd.ExecuteReader())
        {
            if (!reader.HasRows) return null;
            while (reader.Read())
            {
                group = new Group();
                group.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                group.No = reader.GetString(reader.GetOrdinal("No"));
                group.Limit = reader.GetByte(reader.GetOrdinal("Limit"));
            }
        }
    }
    return group;
}