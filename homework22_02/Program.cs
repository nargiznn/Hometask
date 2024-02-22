//C# projectinizdə brand classiniz olur və ona uyğun aşağıdaki menunu yazırsınız
//1.Brand create
//2. Brand delete
//3. Brand get by id
//4. Get all brands
//5. Update brand
//0. Exit
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using homework22_02;
string opt;
do
{
    ShowMenu();
    opt = Console.ReadLine();

    switch (opt)
    {
        case "1":
            //1.Brand create+
            switch1();
            break;
        case "2":
            //2. Brand delete+
            switch2();
            break;
        case "3":
            //3. Brand get by id+
            switch3();
            break;
        case "4":
            //4. Get all brands+
            switch4();
            break;
        case "5":
            //5. Update brand+
            ShowMenu2();
            break;
        case "0":
            //0. Exit+
            Console.WriteLine("\nOperation finished");
            break;
        default:
            Console.WriteLine("\nThe operation is incorrect");
            break;

    }
} while (opt != "0");

void ShowMenu()
{
    Console.WriteLine("1.Brand create");
    Console.WriteLine("2. Brand delete");
    Console.WriteLine("3. Brand get by id");
    Console.WriteLine("4. Get all brands");
    Console.WriteLine("5. Update brand");
    Console.WriteLine("0. Exit");
    Console.WriteLine("Select Operation");
}
void switch1()
{
    Console.WriteLine("\nEnter new Brand\n\t");
    Console.Write("Brand Name: ");
    string name = Console.ReadLine();
    Console.Write("Datetime (exp: 2020/12/12) ");
    DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
    InsertBrand(name, dateTime);
}
void switch2()
{
    Console.WriteLine("\nEnter deleted brands id");
    Console.Write("id: ");
    int deleteId = Convert.ToInt32(Console.ReadLine());
    DeleteBrand(deleteId);

}
void switch3()
{
    Console.WriteLine("\nEnter selected brand id\t");
    Console.Write("id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    var data = GetBrandById(id);
    if (data == null) Console.WriteLine("Brand not found");
    else Console.WriteLine(data);
}
void switch4 ()
{
    Console.WriteLine("\nAll Brands\n\t");
    foreach (var item in GetAllBrands())
    {
        Console.WriteLine(item);
    }
}
void InsertBrand(string name, DateTime dateTime)
{
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "insert into Brands(Name,Year) values(@name,@dateTime)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@dateTime", dateTime);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
void DeleteBrand(int id)
{
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "delete from Brands where id=@id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Brand deleted successfully");
                }
                else
                {
                    Console.WriteLine("No brand found");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
List<Brand> GetAllBrands()
{
    List<Brand> brands = new List<Brand>();
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "select Id, Name, Year from Brands";
            SqlCommand cmd = new SqlCommand(query, connection);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Brand brd = new Brand
                    {

                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Year = reader.GetDateTime(2)
                    };
                    brands.Add(brd);
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
        return brands;

}
Brand GetBrandById(int id)
{
    Brand brand = null;
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "select TOP(1) * from Brands where Id=@id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows) return null;
                while (reader.Read())
                {
                    brand = new Brand();
                    brand.Id = reader.GetInt32(0);
                    brand.Name = reader.GetString(1);
                    brand.Year = reader.GetDateTime(2);
                }

            }
        }

        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }
    return brand;

}
void UpdateBrandbyName(int id,string newname)
{
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "UPDATE Brands SET Name = @newname WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@newname", newname);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Brand update successfully");
                }
                else
                {
                    Console.WriteLine("No brand found");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
void UpdateBrandbyYear(int id, DateTime newdatetime)
{
    string connectionStr = "Server=MOON10\\MAINDB;Database=Store;Trusted_Connection=true";
    using (SqlConnection connection = new SqlConnection(connectionStr))
    {
        try
        {
            connection.Open();
            string query = "UPDATE Brands SET Year = @newdatetime WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@newdatetime", newdatetime);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Brand update successfully");
                }
                else
                {
                    Console.WriteLine("No brand found");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
void ShowMenu2()
{
    string opt2;
        Console.WriteLine("Update brand");
        Console.WriteLine("1.Name");
        Console.WriteLine("2.Year");
        Console.WriteLine("0.Exit");
        opt2 = Console.ReadLine();
        switch (opt2)
        {
            case "1":
                Console.WriteLine("\n Enter brand id");
                Console.Write("id: ");
                int updateId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\nEnter new Name ");
                string newname = Console.ReadLine();
                UpdateBrandbyName(updateId,newname);
                break;
            case "2":
                Console.WriteLine("\n Enter brand id");
                Console.Write("id: ");
                updateId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\nEnter new Year ");
                DateTime newdateTime = Convert.ToDateTime(Console.ReadLine());
                UpdateBrandbyYear(updateId, newdateTime);
                break;
            case "0":
                Console.WriteLine("Opt finished");
                break;
            default:
                Console.WriteLine("Opt is incorrect");
                break;
        }

}
